using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Adbeniz.Weather.Restful.Application.Configurations;
using Adbeniz.Weather.Restful.Application.Models.Authenticate;
using Adbeniz.Weather.Restful.Domain;
using Adbeniz.Weather.Restful.Domain.Entities;
using Adbeniz.Weather.Restful.Infrastructure.Data.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Adbeniz.Weather.Restful.Application.Services
{
	 public interface IUserService
    {
        Task<AuthenticateResult> Authenticate(AuthenticateRequest model);
        //IEnumerable<User> GetAll();
        //User GetById(int id);
    }
    public class UserService : IUserService
    {
		private readonly IRepositoryQuery<ClimasDbContext, User> repositoryQuery;
        private readonly TokenConfiguration tokenConfiguration;

		public UserService(IRepositoryQuery<ClimasDbContext, User> repositoryQuery, IOptions<TokenConfiguration> options)
		{
			this.repositoryQuery = repositoryQuery;
			tokenConfiguration = options.Value;
		}

        public async Task<AuthenticateResult> Authenticate(AuthenticateRequest model)
        {
            var user = await repositoryQuery.GetFirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResult(user, token);
        }

        // public IEnumerable<User> GetAll()
        // {
        //     return _users;
        // }

        // public User GetById(int id)
        // {
        //     return _users.FirstOrDefault(x => x.Id == id);
        // }

        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenConfiguration.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.ID.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
