using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;
using UserAppService.Models;
using UserAppService.Utility.Extensions;

namespace UserAppService.InputModel
{
    public abstract class InputBaseModel
    {
        private string _language;


        /// <summary>
        /// Required API Key
        /// </summary>
        //[Required]
        //public string apiKey { get; set; }


        /// <summary>
        /// Optional, if nothing is passed 'English' will be assumed. Valid language values are 'En' for English and 'Es' for Spanish
        /// </summary>
        public string language
        {
            get
            {
                if (_language.HasNoValue())
                {
                    return "En";
                }
                return _language;
            }
            set { _language = value; }
        }


        [IgnoreDataMember]
        [JsonIgnore]
        public HttpRequestMessage Request { get; set; }
        public bool ShouldSerializeRequest()
        {
            return false;
        }


        [IgnoreDataMember]
        [JsonIgnore]
        internal string trackingId { get; set; }
        public bool ShouldSerializeTrackingId()
        {
            return false;
        }

        /// <summary>
        /// Not currently used, but if we intend to add server side logging of all API calls, this will be used in that process.
        /// </summary>
        [JsonIgnore]
        public bool trackMe { get; set; }
        public bool ShouldSerializeTrackMe()
        {
            return false;
        }

        [IgnoreDataMember]
        [JsonIgnore]
        internal string jsonString { get; set; }
        public bool ShouldSerializejsonString()
        {
            return false;
        }

        [IgnoreDataMember]
        [JsonIgnore]
        public bool IsInTestMode { get; set; }
        public bool ShouldSerializeIsInTestMode()
        {
            return false;
        }


        [IgnoreDataMember]
        [JsonIgnore]
        public User CurrentUser { get; set; }
        public bool ShouldSerializeCurrentUser()
        {
            return false;
        }

        [IgnoreDataMember]
        [JsonIgnore]
        public string MethodName { get; set; }
        public bool ShouldSerializeMethodName()
        {
            return false;
        }

        //public void CheckCommonRequirements(string methodName)
        //{
        //    //require API Key
        //    ValidateAPIKey();

        //    //Handle Tracking
        //    //TrackAPICall(methodName);
        //}

        //private void ValidateAPIKey()
        //{
        //    var im = this;
        //    if (im.apiKey.HasValue() == false)
        //    {
        //        ThrowException(ResourceFiles.LocalizedText.RequireAPIKey);
        //    }

        //    if (APIKey.APIKEY_IOS.ToString().Equals(im.apiKey, StringComparison.InvariantCultureIgnoreCase)) return;
        //    if (APIKey.APIKEY_ANDROID.ToString().Equals(im.apiKey, StringComparison.InvariantCultureIgnoreCase)) return;

        //    ThrowException(ResourceFiles.LocalizedText.InvalidAPIKey);
        //}

        public virtual void Validate(BaseAPIController controller, string methodName)
        {
            this.CurrentUser = controller.CurrentUser;
            this.MethodName = methodName;
            //this.Request = controller.Request;
            this.IsInTestMode = controller.IsInTestMode;

            //CheckCommonRequirements(methodName);
        }

        #region Throw Exception Helpers

        protected void ThrowException(string message, HttpStatusCode statusCode = HttpStatusCode.ExpectationFailed)
        {
            if (IsInTestMode)
            {
                throw new Exception(message);
            }
            throw new HttpResponseException(Request.CreateErrorResponse(statusCode: statusCode, message: message));
        }

        protected void ThrowException(HttpError customError, HttpStatusCode statusCode = HttpStatusCode.ExpectationFailed)
        {
            if (IsInTestMode)
            {
                var x = new Exception(customError.Message);
                throw x;
            }

            throw new HttpResponseException(Request.CreateErrorResponse(statusCode: statusCode, error: customError));
        }

        protected void ThrowExceptionEx(string message, HttpStatusCode statusCode = HttpStatusCode.ExpectationFailed)
        {
            if (IsInTestMode)
            {
                throw new Exception(message);
            }

            throw new HttpResponseException(Request.CreateErrorResponse(statusCode, new HttpError() { { "statusCode", statusCode }, { "Message", message } }));
        }
        protected void ThrowExceptionEx(string message, HttpStatusCode statusCode, HttpError error)
        {
            if (IsInTestMode)
            {
                throw new Exception(message);
            }

            throw new HttpResponseException(Request.CreateErrorResponse(statusCode, error));
        }

        #endregion

        #region Input Validation Helpers

        public void RequireInput(string requiredText, string message)
        {
            if (requiredText.HasValue() == false)
            {
                ThrowException(message);
            }
        }

        public void RequireInputEitherOr(string requiredInputChoice1, string requiredInputChoice2, string message)
        {
            if (requiredInputChoice1.HasNoValue() && requiredInputChoice2.HasNoValue())
            {
                ThrowException(message);
            }

        }

        public void RequireInputEitherOrButNotBoth(string requiredInputChoice1, string requiredInputChoice2, string message)
        {
            if (requiredInputChoice1.HasNoValue() && requiredInputChoice2.HasNoValue())
            {
                ThrowException(message);
            }


            if (requiredInputChoice1.HasValue() && requiredInputChoice2.HasValue())
            {
                ThrowException(message);
            }

        }

        public void RequireInput(object o, string message)
        {
            if (o == null)
            {
                ThrowException(message);
            }
        }

        public void RequireInput(IEnumerable<string> requiredText, string message)
        {
            if (requiredText == null || requiredText.Any() == false)
            {
                ThrowException(message);
            }
        }

        public void RequireInput(IEnumerable requiredText, string message)
        {
            if (requiredText == null)
            {
                ThrowException(message);
            }
        }

        public void RequireInput(DateTime requiredText, string message)
        {
            if (requiredText == DateTime.MinValue)
            {
                ThrowException(message);
            }
        }

        public void RequireInput(Guid? requiredText, string message)
        {
            if (requiredText.Equals(null))
            {
                ThrowException(message);
            }

            if (requiredText.ToString().Equals(""))
            {
                ThrowException(message);
            }
        }

        public void RequireInput(int requiredNumberGreaterThanZero, string message)
        {
            if (requiredNumberGreaterThanZero < 1)
            {
                ThrowException(message);
            }

        }

        public void RequireInputToBeGreaterThanEqualToZero(int requiredNumberGreaterThanEqualToZero, string message)
        {
            if (requiredNumberGreaterThanEqualToZero < 0)
            {
                ThrowException(message);
            }

        }

        public void RequireInputToBeIntGreaterThanZero(int? requiredNumberGreaterThanZero, string message)
        {

            if (requiredNumberGreaterThanZero == null || requiredNumberGreaterThanZero <= 0)
            {
                ThrowException(message);
            }

        }

        public void RequireInputToBeIntOrNull(int? requiredNumberGreaterThanZero, string message)
        {
            if (requiredNumberGreaterThanZero == null)
            {
                return;
            }

            if (requiredNumberGreaterThanZero < 1)
            {
                ThrowException(message);
            }

        }

        public void RequireInputToBeIntOrNullOrZero(int? requiredNumberGreaterThanZero, string message)
        {
            if (requiredNumberGreaterThanZero == null)
            {
                return;
            }

            if (requiredNumberGreaterThanZero < 0)
            {
                ThrowException(message);
            }

        }

        public void RequireInputToBeDecimalOrNullOrZero(decimal? requiredNumberGreaterThanZero, string message)
        {
            if (requiredNumberGreaterThanZero == null)
            {
                return;
            }

            if (requiredNumberGreaterThanZero < 0)
            {
                ThrowException(message);
            }

        }


        public void RequireInput(decimal requiredNumberGreaterThanZero, string message)
        {
            if (requiredNumberGreaterThanZero < 1)
            {
                ThrowException(message);
            }

        }

        #endregion
    }
}
