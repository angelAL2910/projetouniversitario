﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STL.POS.Frontend.Web.Helpers
{
    public static class ExtensionMethods
    {

        /// <summary>
        /// Author : Lic. Carlos Ml Lebron B.
        /// Capitalize
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Capitalize(this string value, char? separator = null)
        {
            string result = string.Empty;

            value = value.ToLower();

            if (string.IsNullOrWhiteSpace(value))
                return result;

            if (string.IsNullOrEmpty(separator.ToString()))
            {
                result = value.Substring(0, 1).ToUpper() +
                         value.Substring(1, value.Length - 1).ToLower();
            }
            else
            {
                var s = value.Split(separator.Value);

                for (int i = 0; i < s.Length; i++)
                {
                    result += (s[i].Substring(0, 1).ToUpper() +
                               s[i].Substring(1, s[i].Length - 1).ToLower()) + " ";
                }

                result = result.Remove(result.LastIndexOf(' '), 1);
            }

            return result;
        }

    }
}