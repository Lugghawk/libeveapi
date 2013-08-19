using System;
using System.Collections.Generic;
using System.Text;

using libeveapi;

namespace UnitTests
{
    public class Utility
    {
        public static void UseLocalUrls()
        {
            Constants.ApiPrefix = "http://localhost/eveapi";
        }
    }
}
