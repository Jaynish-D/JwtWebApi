using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWebApi.JwtCode
{
    public static class SecurityKeys
    {
        private static string key = "This is Token Key";

        public static string Key { get => key; }
    }
}
