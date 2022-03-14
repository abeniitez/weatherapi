using System.Data.SqlClient;

namespace Adbeniz.Weather.Restful.Infrastructure.Data.Contracts
{
	public interface IQueryManager
	{
		string Query { get; }
		SqlParameter[] Parameters { get; }

		//void GenerateQuery(SqlCommandTypeInvocation typeInvocation, string name, IDictionary<string, object> parameters);
	}
}
