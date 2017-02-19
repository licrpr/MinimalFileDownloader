using System.Collections.Generic;
using System.Linq;

namespace MinimalFileDownloader.App.WebApp.Utils
{
    internal static class TextUtils
    {
        private static readonly Dictionary<char, string> Map = new Dictionary<char, string>();

        static TextUtils()
        {
            Map[' '] = "%20";
            Map['!'] = "%21";
            Map['"'] = "%22";
            Map['#'] = "%23";
            Map['$'] = "%24";
            Map['%'] = "%25";
            Map['&'] = "%26";
            Map['\''] = "%27";
            Map['('] = "%28";
            Map[')'] = "%29";
            Map['*'] = "%2A";
            Map['+'] = "%2B";
            Map[','] = "%2C";
            Map['/'] = "%2F";
            Map[':'] = "%3A";
            Map[';'] = "%3B";
            Map['<'] = "%3C";
            Map['='] = "%3D";
            Map['>'] = "%3E";
            Map['?'] = "%3F";
            Map['@'] = "%40";
            Map['['] = "%5B";
            Map['\\'] = "%5C";
            Map[']'] = "%5D";
            Map['^'] = "%5E";
            Map['_'] = "%5F";
            Map['`'] = "%60";
            Map['{'] = "%7B";
            Map['|'] = "%7C";
            Map['}'] = "%7D";
            Map['~'] = "%7E";
            Map['`'] = "%E2%82%AC";
            Map[''] = "%81";
            Map['‚'] = "%E2%80%9A";
            Map['ƒ'] = "%C6%92";
            Map['„'] = "%E2%80%9E";
            Map['…'] = "%E2%80%A6";
            Map['†'] = "%E2%80%A0";
            Map['‡'] = "%E2%80%A1";
            Map['ˆ'] = "%CB%86";
            Map['‰'] = "%E2%80%B0";
            Map['Š'] = "%C5%A0";
            Map['‹'] = "%E2%80%B9";
            Map['Œ'] = "%C5%92";
            Map[''] = "%C5%8D";
            Map['Ž'] = "%C5%BD";
            Map[''] = "%8F";
            Map[''] = "%C2%90";
            Map['‘'] = "%E2%80%98";
            Map['’'] = "%E2%80%99";
            Map['“'] = "%E2%80%9C";
            Map['”'] = "%E2%80%9D";
            Map['•'] = "%E2%80%A2";
            Map['–'] = "%E2%80%93";
            Map['—'] = "%E2%80%94";
            Map['˜'] = "%CB%9C";
            Map['™'] = "%E2%84";
            Map['š'] = "%C5%A1";
            Map['›'] = "%E2%80";
            Map['œ'] = "%C5%93";
            Map[''] = "%9D";
            Map['ž'] = "%C5%BE";
            Map['Ÿ'] = "%C5%B8";
            Map['¡'] = "%C2%A1";
            Map['¢'] = "%C2%A2";
            Map['£'] = "%C2%A3";
            Map['¤'] = "%C2%A4";
            Map['¥'] = "%C2%A5";
            Map['¦'] = "%C2%A6";
            Map['§'] = "%C2%A7";
            Map['¨'] = "%C2%A8";
            Map['©'] = "%C2%A9";
            Map['ª'] = "%C2%AA";
            Map['«'] = "%C2%AB";
            Map['¬'] = "%C2%AC";
            Map['®'] = "%C2%AE";
            Map['¯'] = "%C2%AF";
            Map['°'] = "%C2%B0";
            Map['±'] = "%C2%B1";
            Map['²'] = "%C2%B2";
            Map['³'] = "%C2%B3";
            Map['´'] = "%C2%B4";
            Map['µ'] = "%C2%B5";
            Map['¶'] = "%C2%B6";
            Map['·'] = "%C2%B7";
            Map['¸'] = "%C2%B8";
            Map['¹'] = "%C2%B9";
            Map['º'] = "%C2%BA";
            Map['»'] = "%C2%BB";
            Map['¼'] = "%C2%BC";
            Map['½'] = "%C2%BD";
            Map['¾'] = "%C2%BE";
            Map['¿'] = "%C2%BF";
            Map['À'] = "%C3%80";
            Map['Á'] = "%C3%81";
            Map['Â'] = "%C3%82";
            Map['Ã'] = "%C3%83";
            Map['Ä'] = "%C3%84";
            Map['Å'] = "%C3%85";
            Map['Æ'] = "%C3%86";
            Map['Ç'] = "%C3%87";
            Map['È'] = "%C3%88";
            Map['É'] = "%C3%89";
            Map['Ê'] = "%C3%8A";
            Map['Ë'] = "%C3%8B";
            Map['Ì'] = "%C3%8C";
            Map['Í'] = "%C3%8D";
            Map['Î'] = "%C3%8E";
            Map['Ï'] = "%C3%8F";
            Map['Ð'] = "%C3%90";
            Map['Ñ'] = "%C3%91";
            Map['Ò'] = "%C3%92";
            Map['Ó'] = "%C3%93";
            Map['Ô'] = "%C3%94";
            Map['Õ'] = "%C3%95";
            Map['Ö'] = "%C3%96";
            Map['×'] = "%C3%97";
            Map['Ø'] = "%C3%98";
            Map['Ù'] = "%C3%99";
            Map['Ú'] = "%C3%9A";
            Map['Û'] = "%C3%9B";
            Map['Ü'] = "%C3%9C";
            Map['Ý'] = "%C3%9D";
            Map['Þ'] = "%C3%9E";
            Map['ß'] = "%C3%9F";
            Map['à'] = "%C3%A0";
            Map['á'] = "%C3%A1";
            Map['â'] = "%C3%A2";
            Map['ã'] = "%C3%A3";
            Map['ä'] = "%C3%A4";
            Map['å'] = "%C3%A5";
            Map['æ'] = "%C3%A6";
            Map['ç'] = "%C3%A7";
            Map['è'] = "%C3%A8";
            Map['é'] = "%C3%A9";
            Map['ê'] = "%C3%AA";
            Map['ë'] = "%C3%AB";
            Map['ì'] = "%C3%AC";
            Map['í'] = "%C3%AD";
            Map['î'] = "%C3%AE";
            Map['ï'] = "%C3%AF";
            Map['ð'] = "%C3%B0";
            Map['ñ'] = "%C3%B1";
            Map['ò'] = "%C3%B2";
            Map['ó'] = "%C3%B3";
            Map['ô'] = "%C3%B4";
            Map['õ'] = "%C3%B5";
            Map['ö'] = "%C3%B6";
            Map['÷'] = "%C3%B7";
            Map['ø'] = "%C3%B8";
            Map['ù'] = "%C3%B9";
            Map['ú'] = "%C3%BA";
            Map['û'] = "%C3%BB";
            Map['ü'] = "%C3%BC";
            Map['ý'] = "%C3%BD";
            Map['þ'] = "%C3%BE";
            Map['ÿ'] = "%C3%BF";
        }

        public static string FtpEscape(this string text)
        {
            return string.Join("", text.Select(o => Map.ContainsKey(o) ? Map[o] : o + ""));
        }

        public static string RemoveInitialSlash(this string path)
        {
            if (path.FirstOrDefault() == '/')
                return path.Substring(1);
            else
                return path;
        }

        public static string AddInitialSlash(this string path)
        {
            path = path.Trim();
            if (path.FirstOrDefault() != '/')
            {
                path = "/" + path;
            }
            return path;
        }
    }
}