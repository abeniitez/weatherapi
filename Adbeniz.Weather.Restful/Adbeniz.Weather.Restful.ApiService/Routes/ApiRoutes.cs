using System;

namespace Adbeniz.Weather.Restful.ApiService.Routes
{
    public static class ApiRoutes
    {
		public const string VersionOne = "1";
		public const string Base = "api/v{version:apiVersion}/[controller]";
		public const string Auth = "IsUserApp";

		public static class Users
		{
			public const string GetAll = "/posts";

			public const string Update = "/posts/{postId}";

			public const string Delete = "/posts/{postId}";

			public const string Get = "/posts/{postId}";

			public const string Create = "/posts";
		}
    }
}
