using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARRMM.Utilities.Constants
{
    public static class TwoFactorTypes
    {
        public const string None = "None";
        public const string Email = "Email";
        public const string Phone = "Phone";
        public const string Authenticator = "Authenticator";
    }

    public static class CountryCode
    {
        public const string Pakistan = "0092";
        public const string Canada = "001";
    }

    public static class ApplicationStausCode
    {
        public const string None = "100";
        public const string Grant = "101";
        public const string Refuse = "102";
        public const string Lapse = "103";
    }

    public static class OperationStatusCode
    {
        public const string None = "200";
        public const string Surrender = "201";
        public const string Rewoke = "202";
        public const string Cancel = "203";
    }

    public static class MineralCode
    {
        public const string Gold = "GL";
        public const string Platinum = "PLT";
    }
}
