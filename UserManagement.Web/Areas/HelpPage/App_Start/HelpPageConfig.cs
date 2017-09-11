// Uncomment the following to provide samples for PageResult<T>. Must also add the Microsoft.AspNet.WebApi.OData
// package to your project.
////#define Handle_PageResultOfT

using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

#if Handle_PageResultOfT
using System.Web.Http.OData;
#endif

namespace Apache.Web.Areas.HelpPage
{
    /// <summary>
    /// Use this class to customize the Help Page.
    /// For example you can set a custom <see cref="System.Web.Http.Description.IDocumentationProvider"/> to supply the documentation
    /// or you can provide the samples for the requests/responses.
    /// </summary>
    public static class HelpPageConfig
    {   
        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters",
            MessageId = "Apache.Web.Areas.HelpPage.TextSample.#ctor(System.String)",
            Justification = "End users may choose to merge this string with existing localized resources.")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly",
            MessageId = "bsonspec",
            Justification = "Part of a URI.")]
        public static void Register(HttpConfiguration config)
        {
            //// Uncomment the following to use the documentation from XML documentation file.
            config.SetDocumentationProvider(new XmlDocumentationProvider(HttpContext.Current.Server.MapPath("~/App_Data/XmlDocument.xml")));

            //// Uncomment the following to use "sample string" as the sample for all actions that have string as the body parameter or return type.
            //// Also, the string arrays will be used for IEnumerable<string>. The sample objects will be serialized into different media type 
            //// formats by the available formatters.
            //config.SetSampleObjects(new Dictionary<Type, object>
            //{
            //    {typeof(string), "sample string"},
            //    {typeof(IEnumerable<string>), new string[]{"sample 1", "sample 2"}}
            //});

            // Extend the following to provide factories for types not handled automatically (those lacking parameterless
            // constructors) or for which you prefer to use non-default property values. Line below provides a fallback
            // since automatic handling will fail and GeneratePageResult handles only a single type.
#if Handle_PageResultOfT
            config.GetHelpPageSampleGenerator().SampleObjectFactories.Add(GeneratePageResult);
#endif

            // Extend the following to use a preset object directly as the sample for all actions that support a media
            // type, regardless of the body parameter or return type. The lines below avoid display of binary content.
            // The BsonMediaTypeFormatter (if available) is not used to serialize the TextSample object.
            config.SetSampleForMediaType(
                new TextSample("Binary JSON content. See http://bsonspec.org for details."),
                new MediaTypeHeaderValue("application/bson"));
            
            //Note from WW: This is how you customize same responses when you have a generic type like a list of DenormalizedUser<User>
            //config.SetSampleObjects(new Dictionary<Type, object>
            //{
            //    {
            //        typeof (List<DenormalizedUserReference<User>>), new List<DenormalizedUserReference<User>>
            //        {
            //            new DenormalizedUserReference<User>
            //            {
            //                Id = 1,
            //                Name = CommonStrings.FakeUsers.DarthVader.Name,
            //                UserName =  CommonStrings.FakeUsers.DarthVader.UserName
            //            },
            //            new DenormalizedUserReference<User>
            //            {
            //                Id = 1,
            //                Name = CommonStrings.FakeUsers.LukeSkywalker.Name,
            //                UserName =  CommonStrings.FakeUsers.LukeSkywalker.UserName
            //            }
            //        }
            //    }
            //});

            

        }

#if Handle_PageResultOfT
        private static object GeneratePageResult(HelpPageSampleGenerator sampleGenerator, Type type)
        {
            if (type.IsGenericType)
            {
                Type openGenericType = type.GetGenericTypeDefinition();
                if (openGenericType == typeof(PageResult<>))
                {
                    // Get the T in PageResult<T>
                    Type[] typeParameters = type.GetGenericArguments();
                    Debug.Assert(typeParameters.Length == 1);

                    // Create an enumeration to pass as the first parameter to the PageResult<T> constuctor
                    Type itemsType = typeof(List<>).MakeGenericType(typeParameters);
                    object items = sampleGenerator.GetSampleObject(itemsType);

                    // Fill in the other information needed to invoke the PageResult<T> constuctor
                    Type[] parameterTypes = new Type[] { itemsType, typeof(Uri), typeof(long?), };
                    object[] parameters = new object[] { items, null, (long)ObjectGenerator.DefaultCollectionSize, };

                    // Call PageResult(IEnumerable<T> items, Uri nextPageLink, long? count) constructor
                    ConstructorInfo constructor = type.GetConstructor(parameterTypes);
                    return constructor.Invoke(parameters);
                }
            }

            return null;
        }
#endif
    }
}