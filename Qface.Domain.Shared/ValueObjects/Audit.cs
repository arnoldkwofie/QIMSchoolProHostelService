using Humanizer;
using Qface.Domain.Shared.Common;
using Qface.Domain.Shared.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qface.Domain.Shared.ValueObjects
{
	public class Audit : ValueObjectType
	{
		public string CreatedBy { get; set; }
		public DateTime Created { get; set; }
		public string LastModifiedBy { get; set; }
		public DateTime? LastModified { get; set; }
		public EntityStatus? EntityStatus { get; set; }
		public DateTime? EntityStatusCreated { get; set; }
		public string EntityStatusCreateBy { get; set; }
		public DateTime? EntityStatusLastModified { get; set; }
		public string EntityStatusLastModifiedBy { get; set; }
        
		public Audit Modified(string name)
		{
			LastModifiedBy = name;
			LastModified = DateTime.UtcNow;
			return this;
		}


		public string CreatedDate => Created.Humanize();
		public string LastModifiedDate => LastModified == null ? "" : LastModified.Humanize();
		public string EntityStatusCreatedDate => EntityStatusCreated == null ? "" : LastModified.Humanize();
		public string EntityStatusLastModifiedDate => EntityStatusLastModified == null ? "" : LastModified.Humanize();

		public void SoftRemove(string userId)
		{
			EntityStatus = Enums.EntityStatus.Deleted;
			LastModified = DateTime.UtcNow;
			LastModifiedBy = userId;
		}
	}


}
