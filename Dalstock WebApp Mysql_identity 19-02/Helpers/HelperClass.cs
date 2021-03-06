﻿using Dalstock_WebApp_Mysql_identity_19_02.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Dalstock_WebApp_Mysql_identity_19_02.Helpers
{
    public class HelperClass
    {
        public static string FirstCharToUpper(string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1).ToLower();
            }
        }
        public static EmailViewModel ReadMailSettings(string stream, string company)
        {
            List<EmailViewModel> evm = new List<EmailViewModel>();
            using (StreamReader sr = new StreamReader(stream))
            {
                evm = JsonConvert.DeserializeObject<List<EmailViewModel>>(sr.ReadToEnd());
                return evm.Find(x => x.Company.Equals(company));
            }

        }
    }
}