


namespace Citizens.Core.Service.Sync
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    public static class StringExtension
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(StringExtension));
        public static string Clearup(this string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            text = text.TrimStart('\"').TrimEnd('\"');
            text = text.TrimEnter();
            text = text.Trim();
            return text;
        }
        public static string TrimEnter(this string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            return text.Replace("/r/n", string.Empty);
        }
        public static DateTime? TrytoDateTime(this string text)
        {
            if (DateTime.TryParse(text, out DateTime dateTime))
            {
                return dateTime;
            }
            return null;
        }
        public static string TrimHttplink(this string text, string root)
        {
            if (text.StartsWith("http:", StringComparison.OrdinalIgnoreCase) || text.StartsWith("https:", StringComparison.OrdinalIgnoreCase))
            {
                return text;
            }
            return string.Concat(root, text);
        }
        public static string GetQueryParameters(this string queryString, out string key, params string[] names)
        {
            key = string.Empty;
            var uri = new Uri(queryString);
            var dictionary = new Dictionary<string, string>();
            var parameters = uri.Query.ToLower().TrimStart('?').Split("&");

            foreach (var parameter in parameters)
            {
                var keyval = parameter.Split('=');
                if (keyval.Length.Equals(2))
                    dictionary.Add(keyval[0], keyval[1]);
                else
                {
                    dictionary.Add(keyval[0], string.Empty);
                }
            }
            if (dictionary.Keys == null)
            {
                Logger.Error($"Cant find key in one of [{string.Join(',', names)}] from query string {queryString}");
                return string.Empty;
            }
            key = dictionary.Keys.Where(o => names.Any(n => n.Equals(o, StringComparison.OrdinalIgnoreCase))).FirstOrDefault();
            return dictionary[key] ?? string.Empty;


        }
        public static string GetIdfromurl(this string url)
        {
            var array = url.Split('/');
            return array[array.Length - 1].Split('.')[0];
        }
    }
}
