using System;
using Adbeniz.Weather.Restful.Domain.Entities;

namespace Adbeniz.Weather.Restful.Application.Models.Authenticate
{
    public class AuthenticateResult
    {
		public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }

		public AuthenticateResult()	{}

		public AuthenticateResult(User user, string token)
        {
            Id = user.ID;
            Email = user.Email;
            FullName = user.FullName;
            Token = token;
        }
    }
}
