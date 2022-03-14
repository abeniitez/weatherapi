using System;

namespace Adbeniz.Weather.Restful.Application.Models.Authenticate
{
    public class AuthenticateRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
