﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitportDownloader.RestApi
{
    public class BitportAppToken
    {
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
    }
}
