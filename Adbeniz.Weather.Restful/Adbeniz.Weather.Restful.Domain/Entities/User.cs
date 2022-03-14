using System;
using Adbeniz.Weather.Restful.Infrastructure.Data;

namespace Adbeniz.Weather.Restful.Domain.Entities
{
    public class User: EntityBase
    {
		public string Email { get; set; }
		public string Password {get; set; }
		public string FullName { get; set; }
		public DateTime DateOfBirth { get; set; }
    }
}
