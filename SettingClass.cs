using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeasedCarService
{
    public class SettingClass
    {
        private readonly IConfiguration configruration;

        public SettingClass(IConfiguration configruration)
        {
            this.configruration = configruration;
        }

        public string TestReadAppSetting(string strAppSet)
        {
            //return configruration.GetValue<string>(strAppSet);
            return configruration[strAppSet];
        }
    }
}
