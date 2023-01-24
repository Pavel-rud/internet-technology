using System;
using System.Text;
using Microsoft.IdentityModel.Tokens; 

namespace IT_Project.Auth
{
    public class AuthOptions
    {
        public const string ISSUER = "IT_Project";
        public const string AUDIENCE = "http://localhost:5000/";
        const string KEY = "mysupersecret_secretkey!123";
        public const int LIFETIME = 1; // 1 hour
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
