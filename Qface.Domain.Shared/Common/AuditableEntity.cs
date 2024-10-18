using Newtonsoft.Json;
using Qface.Domain.Shared.Exceptions;
using Qface.Domain.Shared.Interfaces;
using Qface.Domain.Shared.ValueObjects;
using System.Collections.Generic;
using System.IO;

namespace Qface.Domain.Shared.Common
{

	public abstract class AuditableEntityBase : AuditableDomainEvent
	{

		public Audit Audit { get; set; }
		protected void CheckRule(IBusinessRule rule)
		{
			if (rule.IsBroken())
			{
				throw new BusinessRuleValidationException(rule);
			}
		}
		public static List<T> GetTestData<T>(string rootPath)
		{
			var list = new List<T>();
			var jsonString = File.ReadAllText($"{rootPath}/{typeof(T).Name}.json");
			var models = JsonConvert.DeserializeObject<List<T>>(jsonString);
			list.AddRange(models);
			return list;
		}
		public static List<T> GetTestData<T>(string rootPath, string fileName)
		{
			var list = new List<T>();
			var jsonString = File.ReadAllText($"{rootPath}/{fileName}.json");
			var models = JsonConvert.DeserializeObject<List<T>>(jsonString);
			list.AddRange(models);
			return list;
		}
		public static List<T> GetTestData<T>(string rootPath, string fileName, JsonConverter[] convertors)
		{
			var list = new List<T>();
			var jsonString = File.ReadAllText($"{rootPath}/{fileName}.json");
			var models = JsonConvert.DeserializeObject<List<T>>(jsonString, convertors);
			list.AddRange(models);
			return list;
		}


	}

	public abstract class AuditableEntity : AuditableEntityBase
	{
		public string? OtherProperty { get;  set; }
		public string? OtherProperty1 { get;  set; }

	}
	public abstract class AuditableEntity<T> : AuditableEntityBase
	{
		public T Id { get; set; }
		public string OtherProperty { get; private set; }
		public string OtherProperty1 { get; private set; }

	}
	public abstract class AuditableAutoEntity : AuditableEntityBase
	{
		public int Id { get; set; }
		public string OtherProperty { get; private set; } = "Normal";
		public string OtherProperty1 { get; private set; } = "Normal";


        //public byte[] RowVersion { get; set; }

    }
	
}
