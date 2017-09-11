using Newtonsoft.Json;
using System.Runtime.Serialization;
using UserAppService.Interfaces;
using UserAppService.Models;

namespace UserAppService.OutputModels
{
    public class GoogleExternalLoginResult : ICanGenerateHelpSampleData
    {
        /// <summary>
        /// Indicative of success or failure of the call.
        /// </summary>
        public bool WasSuccessful { get; set; }

        /// <summary>
        /// Any extra information needed regarding the call. Often this is used to return a reason for a failed call.
        /// </summary>
        public string Status { get; set; }
        //public string OptionalURL { get; set; }

        public GoogleTokenInfo AuthTokenInfo { get; set; }

        public void GenerateHelpSampleData()
        {
            WasSuccessful = true;
            Status = "";
        }

        [IgnoreDataMember]
        [JsonIgnore]
        public bool IsHelpSampleDataGenerated { get; set; }
        public bool ShouldSerializeIsHelpSampleDataGenerated()
        {
            return false;
        }

        public static BoolMethodResult Instance(bool wasSuccessfull, string status)
        {
            return new BoolMethodResult { WasSuccessful = wasSuccessfull, Status = status };
        }
    }
}