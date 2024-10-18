using Qface.Application.Shared.Common.Mappings;
using Qface.Domain.Shared.Enums;
using Qface.Domain.Shared.ValueObjects;

namespace Qface.Application.Shared.Dtos
{
	public class AuditDto : IMapFrom<Audit>
	{
		public string CreatedBy { get; set; }
		public string LastModifiedBy { get; set; }
		public EntityStatus? EntityStatus { get; set; }
		public string EntityStatusCreateBy { get; set; }
		public string EntityStatusLastModifiedBy { get; set; }
		public string CreatedDate { get; set; }
		public string LastModifiedDate { get; set; }
		public string EntityStatusCreatedDate { get; set; }
		public string EntityStatusLastModifiedDate { get; set; }

	}
}
