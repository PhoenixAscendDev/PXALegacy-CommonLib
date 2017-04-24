using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;

using JB2.Common;

namespace JB2.Dictionary
{
    public static class ErrorCodes
    {
        #region Fields

        private static Dictionary<string, StatusCode> _errorCodes;

        #endregion Fields

        public static void Add(StatusCode c)
        {
            if (_errorCodes == null)
                init();
            _errorCodes.Add(c.Code, c);
        }
        public static void Remove(StatusCode c)
        {
            if (_errorCodes == null)
                init();
            _errorCodes.Remove(c.Code);
        }

        public static void AddRange(IEnumerable<StatusCode> list)
        {
            if (_errorCodes == null)
                init();
            foreach (var sc in list)
            {
                Add(sc);
            }
        }


        public static StatusCode Get(string statuscode)
        {
            if (_errorCodes == null)
                init();
            if (_errorCodes.ContainsKey(statuscode))
                return _errorCodes[statuscode];
            else
                return new StatusCode() { Code = statuscode, Description = string.Empty };
        }

        public static string GetDescription(string statuscode)
        {
            if (_errorCodes == null)
                init();

            if (_errorCodes.ContainsKey(statuscode))
                return _errorCodes[statuscode].Description;
            else
                return string.Empty;
        }

        public static IEnumerable<StatusCode> All()
        {
            if (_errorCodes == null)
                init();
            return _errorCodes.Values.ToList();
        }

        public static void Reset()
        {
            init();
        }

        private static void init()
        {
            _errorCodes = new Dictionary<string, StatusCode>();
            _errorCodes.Add("E-01-100011", new StatusCode("E-01-100011", "Setting Not Found"));
            _errorCodes.Add("E-01-100010", new StatusCode("E-01-100010", "Settings was not configured"));
            _errorCodes.Add("E-01-000200", new StatusCode("E-01-000200", "No Error"));

        }
    }
}
