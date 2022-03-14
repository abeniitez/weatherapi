using System.Collections.Generic;

namespace Adbeniz.Weather.Restful.Application.Mappers
{
	public interface IMapperEntityModel<TEntity, TModel>
    {
		TModel MapEntityToModel(TEntity entity);
		TEntity MapModelToEntity(TModel model);
		IEnumerable<TModel> MapEntityToModelCollection(IEnumerable<TEntity> entity);
		IEnumerable<TEntity> MapModelToEntityCollection(IEnumerable<TModel> model);
    }
}
