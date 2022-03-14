using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adbeniz.Weather.Restful.Infrastructure.Data
{
    public interface IDataModelBase { }

	public abstract class EntityBase : IDataModelBase
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
	}

	public abstract class AuditEntityBase : EntityBase
	{
		public DateTime? CreatedAt { get; set; }
		public string CreatedByName { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public string UpdatedByName { get; set; }
	}
}
