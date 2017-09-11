using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace JB2.Common
{
    public static class ValidationExtensions
    {
        public static ServiceResult IsEmail(this string email)
        {
            if (email != null) return Regex.IsMatch(email, JB2.Common.Pattern.EmailPattern);
            else return false;
        }

        public static string GetFirstMessage(this IServiceResult sr)
        {
            if (sr.Count > 0)
                return sr.Validation.FirstOrDefault().Message;
            else
                return string.Empty;
        }

        public static IEnumerable<string> GetMessages(this IServiceResult sr)
        {
            List<string> result = new List<string>(sr.Count);

            foreach(var v in sr.Validation)
            {
                if(!string.IsNullOrEmpty(v.Message))
                {
                    result.Add(v.Message);
                }
            }

            return result.ToArray();
        }
    }
}
