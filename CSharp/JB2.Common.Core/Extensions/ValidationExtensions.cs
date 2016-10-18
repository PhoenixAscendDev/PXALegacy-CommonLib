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
    }
}
