using System;
using System.Collections.Generic;
using System.Text;

namespace Mono.Core.Application.Common
{
    public class JwtConfig
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double DurationInMinutes { get; set; }
        public double TokenLifetimeInMin { get; set; }
    }
}
