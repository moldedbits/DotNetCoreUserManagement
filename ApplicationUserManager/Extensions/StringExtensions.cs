using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Newtonsoft.Json;

namespace UserAppService.Utility.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Determines if the string is not null and is not empty
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool HasValue(this string s)
        {
            return (!string.IsNullOrWhiteSpace(s));
        }

        public static string SplitCamelCase(this string str)
        {
            return Regex.Replace(
                Regex.Replace(
                    str,
                    @"(\P{Ll})(\P{Ll}\p{Ll})",
                    "$1 $2"
                ),
                @"(\p{Ll})(\P{Ll})",
                "$1 $2"
            );
        }

        /// <summary>
        ///     Indicates whether a specified string is null, empty, or consists only of white-space
        ///     characters.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool HasNoValue(this string s)
        {
            return (String.IsNullOrWhiteSpace(s));
        }

        // white space, em-dash, en-dash, underscore
        static readonly Regex WordDelimiters = new Regex(@"[\s—–_]", RegexOptions.Compiled);

        // characters that are not valid
        static readonly Regex InvalidChars = new Regex(@"[^a-z0-9\-]", RegexOptions.Compiled);

        // multiple hyphens
        static readonly Regex MultipleHyphens = new Regex(@"-{2,}", RegexOptions.Compiled);

        /// <summary>
        /// Use the current thread's culture info for conversion
        /// </summary>
        public static string ToTitleCase(this string str)
        {
            var cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            return cultureInfo.TextInfo.ToTitleCase(str.ToLower());
        }

        /// <summary>
        /// Overload which uses the culture info with the specified name
        /// </summary>
        public static string ToTitleCase(this string str, string cultureInfoName)
        {
            var cultureInfo = new CultureInfo(cultureInfoName);
            return cultureInfo.TextInfo.ToTitleCase(str.ToLower());
        }

        /// <summary>
        /// Overload which uses the specified culture info
        /// </summary>
        public static string ToTitleCase(this string str, CultureInfo cultureInfo)
        {
            return cultureInfo.TextInfo.ToTitleCase(str.ToLower());
        }

        public static long CountOfString(this string s, string delimiter = "\n")
        {
            long count = 0;
            int position = 0;
            while ((position = s.IndexOf(delimiter, position)) != -1)
            {
                count++;
                position++;
            }

            return count;
        }

        public static string GetLastNumberOfChars(this string source, int numOfChars)
        {
            if (numOfChars >= source.Length)
                return source;

            return source.Substring(source.Length - numOfChars);
        }

        public static string GetFirstNumberOfChars(this string source, int numOfChars)
        {
            if (numOfChars >= source.Length)
                return source;

            return source.Substring(numOfChars);
        }

        public const string NorthAmericanPhonePattern =
                @"^(\+?(?<NatCode>1)\s*[-\/\.]?)?(\((?<AreaCode>\d{3})\)|(?<AreaCode>\d{3}))\s*[-\/\.]?\s*(?<Number1>\d{3})\s*[-\/\.]?\s*(?<Number2>\d{4})\s*(([xX]|[eE][xX][tT])\.?\s*(?<Ext>\d+))*$"
            ;

        private static string PhoneNumberMatchEvaluator(Match match)
        {
            // Format to north american style phone numbers "0 (000) 000-0000"
            //                                          OR  "(000) 000-0000"

            if (match.Groups["NatCode"].Success)
            {
                if (match.Groups["NatCode"].Value == "1")
                {
                    return match.Result("(${AreaCode}) ${Number1}-${Number2}");
                }

                return match.Result("${NatCode} (${AreaCode}) ${Number1}-${Number2}");
            }
            else
            {
                return match.Result("(${AreaCode}) ${Number1}-${Number2}");
            }
        }

        public static string ToFormattedPhoneNumber(this string phoneNumber)
        {
            if (phoneNumber.HasNoValue()) return phoneNumber;

            var regex = new Regex(NorthAmericanPhonePattern, RegexOptions.IgnoreCase);
            return regex.Replace(phoneNumber, new MatchEvaluator(PhoneNumberMatchEvaluator));
        }

        public static string ToTwilioPhoneNumber(this string phoneNumber)
        {
            if (phoneNumber.HasNoValue()) return phoneNumber;

            string sPhone = phoneNumber.ToFormattedPhoneNumber()
                .Replace(" ", string.Empty)
                .Replace(".", string.Empty)
                .Replace("(", string.Empty)
                .Replace(")", string.Empty)
                .Replace("-", string.Empty)
                .Replace("∙", string.Empty)
                .Replace("·", string.Empty)
                .Replace(" ", string.Empty);

            if (sPhone.Length == 10)
            {
                sPhone = "+1" + sPhone;
            }
            return sPhone;
        }

        public static string GetFileNameExtension(this string value)
        {
            var ext = "";
            var fileExtPos = value.LastIndexOf(".");
            if (fileExtPos >= 0)
                ext = value.Substring(fileExtPos, value.Length - fileExtPos);

            return ext;
        }

        public static string ToEmberComponentName(this string value)
        {
            List<char> chars = new List<char>();

            foreach (char c in value)
            {
                if (Char.IsUpper(c))
                {
                    chars.Add(' ');
                    chars.Add(Char.ToLower(c));
                }
                else
                    chars.Add(c);
            }

            var sReturn = new string(chars.ToArray());
            sReturn = sReturn.Slugify2();
            sReturn = sReturn.Replace("components-", "components/");
            return sReturn;
        }

        public static string Slugify2(this string value)
        {
            // convert to lower case
            value = value.ToLowerInvariant();

            // remove diacritics (accents)
            value = RemoveDiacritics(value);

            // ensure all word delimiters are hyphens
            value = WordDelimiters.Replace(value, "-");

            // strip out invalid characters
            value = InvalidChars.Replace(value, "");

            // replace multiple hyphens (-) with a single hyphen
            value = MultipleHyphens.Replace(value, "-");

            // trim hyphens (-) from ends
            return value.Trim('-');
        }

        /// See: http://blogs.msdn.com/b/michkap/archive/2007/05/14/2629747.aspx
        private static string RemoveDiacritics(string stIn)
        {
            string stFormD = stIn.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }

            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        public static string RemoveAccent(this string txt)
        {
            byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return Encoding.ASCII.GetString(bytes);
        }

        public static string Slugify(this string phrase)
        {
            string str = phrase.RemoveAccent().ToLower();
            str = str.Replace('{', '-');
            str = str.Replace('}', '-');
            str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); // Remove all non valid chars          
            str = Regex.Replace(str, @"\s+", " ").Trim(); // convert multiple spaces into one space  
            str = Regex.Replace(str, @"\s", "-"); // //Replace spaces by dashes
            str = str.Replace("--", "-");
            return str;
        }

        // Convert the string to camel case.
        public static string ToCamelCase(this string the_string)
        {
            // If there are 0 or 1 characters, just return the string.
            if (the_string == null) return the_string;
            if (the_string.Length < 2) return the_string.ToLower();

            // Split the string into words.
            string[] words = the_string.Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            // Combine the words.
            string result = "";
            foreach (string word in words)
            {
                result +=
                    word.Substring(0, 1).ToLower() +
                    word.Substring(1);
            }

            return result;
        }

        public static string StripNonNumerals(this string s)
        {
            if (s.HasValue())
            {
                return Regex.Replace(s, "[^0-9]", "");
            }

            return String.Empty;
        }

        public static string ToJSON(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static string ToJSONPretty(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        public static DateTime ParseAndSpecifyUTC(this string dateString)
        {
            var dtStart = DateTime.Parse(dateString);
            dtStart = DateTime.SpecifyKind(dtStart, DateTimeKind.Utc);
            return dtStart;
        }

        public static string CleanFileName(this string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName,
                (current, c) => current.Replace(c.ToString(), String.Empty));
        }

        //https://msdn.microsoft.com/en-us/library/01escwtf(v=vs.110).aspx
        public static bool IsValidEmail(this string strIn)
        {
            var invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            MatchEvaluator domainMapper = match =>
            {
                // IdnMapping class with default property values.
                IdnMapping idn = new IdnMapping();

                string domainName = match.Groups[2].Value;
                try
                {
                    domainName = idn.GetAscii(domainName);
                }
                catch (ArgumentException)
                {
                    invalid = true;
                }
                return match.Groups[1].Value + domainName;
            };

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", domainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            try
            {
                return Regex.IsMatch(strIn,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static MemoryStream ToMemoryStream(this string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static bool IsDate(this String str)
        {
            if (str.Trim().HasValue())
            {
                DateTime chkDate;

                if (!DateTime.TryParse(str, out chkDate))
                    return false;
            }
            else
                return false;

            return true;
        }

        public static string Reverse(this string input)
        {
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new String(chars);
        }

        public static string RemoveSpaces(this string s)
        {
            return s.Replace(" ", "");
        }

        public static string RemoveDashes(this string s)
        {
            return s.Replace("-", "");
        }

        public static string SetValueIfNullOrEmpty(this string s, string testValue)
        {
            if (s.HasNoValue()) s = testValue;
            return s;
        }

        /// <summary>
        /// true, if the string can be parse as Double respective Int32
        /// Spaces are not considred.
        /// </summary>
        /// <param name="s">input string</param>
        public static bool IsInteger(this string s)
        {
            int result;

            if (Int32.TryParse(s.StripNonNumerals(), out result))
            {
                return true;
            }

            return false;
        }

        /// <param name="floatpoint">true, if Double is considered,
        /// otherwhise Int32 is considered.</param>
        /// <returns>true, if the string contains only digits or float-point</returns>
        public static bool IsNumber(this string s, bool floatpoint)
        {
            int i;
            double d;
            string withoutWhiteSpace = s.RemoveSpaces();
            if (floatpoint)
                return Double.TryParse(withoutWhiteSpace, NumberStyles.Any,
                    Thread.CurrentThread.CurrentUICulture, out d);
            else
                return Int32.TryParse(withoutWhiteSpace, out i);
        }

        /// <summary>
        /// true, if the string contains only digits or float-point.
        /// Spaces are not considred.
        /// </summary>
        /// <param name="s">input string</param>
        /// <param name="floatpoint">true, if float-point is considered</param>
        /// <returns>true, if the string contains only digits or float-point</returns>
        public static bool IsNumberOnly(this string s, bool floatpoint)
        {
            s = s.Trim();
            if (s.Length == 0)
                return false;

            foreach (char c in s)
            {
                if (!Char.IsDigit(c))
                {
                    if (floatpoint && (c == '.' || c == ','))
                        continue;

                    return false;
                }
            }

            return true;
        }

        private static readonly Regex domainRegex =
            new Regex(
                @"(((?<scheme>http(s)?):\/\/)?([\w-]+?\.\w+)+([a-zA-Z0-9\~\!\@\#\$\%\^\&amp;\*\(\)_\-\=\+\\\/\?\.\:\;\,]*)?)",
                RegexOptions.Compiled | RegexOptions.Multiline);

        public static string Linkify(this string text, string target = "")
        {
            return domainRegex.Replace(
                text,
                match =>
                {
                    var link = match.ToString();
                    var scheme = match.Groups["scheme"].Value == "https" ? Uri.UriSchemeHttps : Uri.UriSchemeHttp;

                    try
                    {
                        var url = new UriBuilder(link) { Scheme = scheme }.Uri.ToString();

                        if (target.HasValue())
                        {
                            return String.Format(@"<a href=""{0}"" target=""{1}"">{2}</a>", url, target, link);
                        }
                        else
                        {
                            return String.Format(@"<a href=""{0}"">{1}</a>", url, link);
                        }
                    }
                    catch (UriFormatException ex)
                    {
                        //if we don't know whwat to do with it, leave it alone
                        Trace.WriteLine(ex.Message);
                        return link;
                    }
                }
            );
        }

        /// <summary>
        /// Replace \r\n or \n by <br />
        /// from http://weblogs.asp.net/gunnarpeipman/archive/2007/11/18/c-extension-methods.aspx
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToNl2Br(this string s)
        {
            return s.Replace("\r\n", "<br />").Replace("\n", "<br />");
        }

        /// <summary>
        /// Remove HTML from string with Regex.
        /// </summary>
        public static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        /// <summary>
        /// Compiled regular expression for performance.
        /// </summary>
        static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        public static string StripTagsRegexCompiled(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return string.Empty;

            return _htmlRegex.Replace(source, string.Empty);
        }

        /// <param name="s"></param>
        /// <returns></returns>
        public static string StripHTML(this string s)
        {
            return StripTagsRegexCompiled(s);
        }

        public static string CleanWordHtml(this string html)
        {
            if (html.HasValue() == false)
                html = String.Empty;

            var sc = new List<string>();
            // get rid of unnecessary tag spans (comments and title)
            sc.Add(@"<!--(\w|\W)+?-->");
            sc.Add(@"<title>(\w|\W)+?</title>");
            // Get rid of classes and styles
            sc.Add(@"\s?class=\w+");
            sc.Add(@"\s+style='[^']+'");
            // Get rid of unnecessary tags
            sc.Add(@"<(meta|link|/?o:|/?style|/?div|/?st\d|/?head|/?html|body|/?body|/?span|!\[)[^>]*?>");
            // Get rid of empty paragraph tags
            sc.Add(@"(<[^>]+>)+&nbsp;(</\w+>)+");
            // remove bizarre v: element attached to <img> tag
            sc.Add(@"\s+v:\w+=""[^""]+""");
            // remove extra lines
            sc.Add(@"(\n\r){2,}");
            foreach (string s in sc)
            {
                html = Regex.Replace(html, s, "", RegexOptions.IgnoreCase);
            }

            return html;
        }

        /// <summary>
        /// from http://weblogs.asp.net/gunnarpeipman/archive/2007/11/18/c-extension-methods.aspx
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToMD5(this string s)
        {
            var s_md5 = new MD5CryptoServiceProvider();
            Byte[] newdata = Encoding.Default.GetBytes(s);
            Byte[] encrypted = s_md5.ComputeHash(newdata);
            return BitConverter.ToString(encrypted).Replace("-", "").ToLower();
        }

        //http://stackoverflow.com/questions/8386735/convert-string-to-html-hyperlink
        public static string ConvertUrlsToLinks(this string msg)
        {
            string regex =
                @"((www\.|(http|https|ftp|news|file)+\:\/\/)[&#95;.a-z0-9-]+\.[a-z0-9\/&#95;:@=.+?,##%&~-]*[^.|\'|\# |!|\(|?|,| |>|<|;|\)])";
            Regex r = new Regex(regex, RegexOptions.IgnoreCase);
            return r.Replace(msg,
                    "<a href=\"$1\" title=\"Click to open in a new window or tab\" target=\"&#95;blank\">$1</a>")
                .Replace("href=\"www", "href=\"http://www");
        }

        public static string SyntaxHighlightJson(this string original)
        {
            return Regex.Replace(
                original,
                @"(¤(\\u[a-zA-Z0-9]{4}|\\[^u]|[^\\¤])*¤(\s*:)?|\b(true|false|null)\b|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?)"
                    .Replace('¤', '"'),
                match =>
                {
                    var cls = "number";
                    if (Regex.IsMatch(match.Value, @"^¤".Replace('¤', '"')))
                    {
                        if (Regex.IsMatch(match.Value, ":$"))
                        {
                            cls = "key";
                        }
                        else
                        {
                            cls = "string";
                        }
                    }
                    else if (Regex.IsMatch(match.Value, "true|false"))
                    {
                        cls = "boolean";
                    }
                    else if (Regex.IsMatch(match.Value, "null"))
                    {
                        cls = "null";
                    }
                    return "<span class=\"" + cls + "\">" + match + "</span>";
                });
        }

        public static string ToJustDigits(this string toJustDigits)
        {
            if (toJustDigits == null)
            {
                return String.Empty;
            }

            var justDigits = new string(toJustDigits.Where(Char.IsDigit).ToArray());
            return justDigits;
        }

        public static string[] Tokenize1(this string s)
        {
            return Regex.Split(input: s, pattern: @"\W+");
        }

        public static string[] Tokenize2(this string s)
        {
            return Regex.Split(s, " ");
        }

        public static string[] Tokenize3(this string s)
        {
            return Regex.Split(s, @"<.*?>");
        }

        private static readonly Regex regex =
            new Regex("((http://|www\\.)([A-Z0-9.-:]{1,})\\.[0-9A-Z?;~&#=\\-_\\./]{2,})",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly string link = "<a href=\"{0}{1}\">{2}</a>";

        public static string ResolveLinks(this string body)
        {
            if (string.IsNullOrEmpty(body))
                return body;

            foreach (Match match in regex.Matches(body))
            {
                Debug.WriteLine("match: " + match.Value);

                if (match.Value.StartsWith("<a "))
                {
                    continue;
                }

                if (match.Value.Contains("://"))
                {
                    body = body.Replace(match.Value,
                        string.Format(link, string.Empty, match.Value, ShortenUrl(match.Value, 50)));
                }
                else
                {
                    body = body.Replace(match.Value,
                        string.Format(link, "http://", match.Value, ShortenUrl(match.Value, 50)));
                }
            }

            return body;
        }

        private static string ShortenUrl(string url, int max)
        {
            if (url.Length <= max)
                return url;

            // Remove the protocal
            int startIndex = url.IndexOf("://");
            if (startIndex > -1)
                url = url.Substring(startIndex + 3);

            if (url.Length <= max)
                return url;

            // Remove the folder structure
            int firstIndex = url.IndexOf("/") + 1;
            int lastIndex = url.LastIndexOf("/");
            if (firstIndex < lastIndex)
                url = url.Replace(url.Substring(firstIndex, lastIndex - firstIndex), "...");

            if (url.Length <= max)
                return url;

            // Remove URL parameters
            int queryIndex = url.IndexOf("?");
            if (queryIndex > -1)
                url = url.Substring(0, queryIndex);

            if (url.Length <= max)
                return url;

            // Remove URL fragment
            int fragmentIndex = url.IndexOf("#");
            if (fragmentIndex > -1)
                url = url.Substring(0, fragmentIndex);

            if (url.Length <= max)
                return url;

            // Shorten page
            firstIndex = url.LastIndexOf("/") + 1;
            lastIndex = url.LastIndexOf(".");
            if (lastIndex - firstIndex > 10)
            {
                string page = url.Substring(firstIndex, lastIndex - firstIndex);
                int length = url.Length - max + 3;
                url = url.Replace(page, "..." + page.Substring(length));
            }

            return url;
        }

        public static string LinkifyWade(this string s, bool debug = false)
        {
            //what has already been tried and didn't work
            //var myregex1 = @"(^|[^'"">])((ftp|http|https):\/\/(\S+))(\b|$)";
            ////s = Regex.Replace(s, myregex, @"$1<a href='$2'>$2</a>");
            //var myregex2 = @"<a(.+?)/>";
            //var myregex3 = @"\W+";
            //var myregex4 = @"/^<(\w+)((?:\s+\w+(?:\s*=\s*(?:(?:""[^""]*"")|(?:'[^']*')|[^>\s]+))?)*)\s*(\/?)>/";
            //var myregex5 = @"\w*";
            //var myregex6 = @"(\B|$)";
            //var myregex7 ="<.*?>";
            //var myregex9 = @"(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?";
            //var myregex8 = myregexAnchor + myQualifiedLink;
            //var myregex10 = myregex8 + "|" + myregex9;
            //var myregex11 = myregex8 + "|" + @"([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?";
            //var myregex12 = @"[a-zA-Z.]{2,5}([^\s]+)|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum)([^\s]+)|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum)";
            //var word_dot_word = @"(\w+\.\w+\.\w+)";
            //var myregex14 = @"^(?#Protocol)(?:(?:ht|f)tp(?:s?)\:\/\/|~\/|\/)?(?#Username:Password)(?:\w+:\w+@)?(?#Subdomains)(?:(?:[-\w]+\.)+(?#TopLevel Domains)(?:com|org|net|gov|mil|biz|info|mobi|name|aero|jobs|museum|travel|[a-z]{2}))(?#Port)(?::[\d]{1,5})?(?#Directories)(?:(?:(?:\/(?:[-\w~!$+|.,=]|%[a-f\d]{2})+)+|\/)+|\?|#)?(?#Query)(?:(?:\?(?:[-\w~!$+|.,*:]|%[a-f\d{2}])+=?(?:[-\w~!$+|.,*:=]|%[a-f\d]{2})*)(?:&(?:[-\w~!$+|.,*:]|%[a-f\d{2}])+=?(?:[-\w~!$+|.,*:=]|%[a-f\d]{2})*)*)*(?#Anchor)(?:#(?:[-\w~!$+|.,*:=]|%[a-f\d]{2})*)?$";
            //var myPath = @"!(?:[;/][^#?<>\\s]*)?";

            const string myregexAnchor = @"<a.*>";
            const string myQualifiedLink = @"((ftp|http|https):\/\/(\S+))(\b|$)|\b(\w+)";
            const string word_dot_word2 = @"(\b\S+\b)";
            const string path = @"(?:[;/][\w+]*)?";
            var myregex = myregexAnchor + "|" + word_dot_word2 + path + "|" + myQualifiedLink;

            foreach (Match match in Regex.Matches(s, myregex, RegexOptions.IgnoreCase))
            {
                var mv = match.Value;

                if (match.Value.ToLower().StartsWith("<a") == false)
                {
                    mv = mv.Linkify();
                    if (mv != match.Value)
                    {
                        s = s.Replace(match.Value, mv);
                        if (debug)
                        {
                            Debug.WriteLine("\tmatch    : " + match.Value + "\n    linkified: " + mv);
                        }
                    }
                }
                if (debug)
                {
                    Debug.WriteLine("\tmatch    : " + match.Value);
                }
            }

            return s;
        }

        public static string ToDashedNumber(this string s)
        {
            if (s.StripNonNumerals().Length == 10)
            {
                return $"{s.Substring(0, 3)}-{s.Substring(3, 3)}-{s.Substring(6, 4)}";
            }

            return s;
        }

        //SSN numeric length is 9.
        public static string ToDashedSSN(this string s)
        {
            if (s.StripNonNumerals().Length == 9)
            {
                return $"{s.Substring(0, 3)}-{s.Substring(3, 2)}-{s.Substring(5, 4)}";
            }

            return s;
        }

        public static string NameWithSpaces(this string s)
        {
            return Regex.Replace(s, "(\\B[A-Z])", " $1");
        }

        public static bool CompareStringsWithIgnoreCasing(string input, string comparingTo)
        {
            if (input == null) return false;

            return string.Equals(input.Trim(),
                comparingTo.Trim(), StringComparison.OrdinalIgnoreCase);
        }
    }
}
