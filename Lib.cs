using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeasedCarService
{
    public class Lib
    {
        public string Left(string param, int length)
        {
            string result = param.Substring(0, length);
            return result;
        }
        public string Right(string param, int length)
        {
            string result = param.Substring(param.Length - length, length);
            return result;
        }
    }
}
