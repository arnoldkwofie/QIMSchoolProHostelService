using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qface.Domain.Shared.Attributes
{
    public enum DatabaseSchemeName
    {
        Default,
        UserManagement,
        Registration,
        Lookup,
        Person,
        Employee
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class T4DomainModelAttribute : Attribute
    {
        public T4DomainModelAttribute(DatabaseSchemeName schemaName = DatabaseSchemeName.Default)
        {
            SchemaName = schemaName;
        }

        public DatabaseSchemeName SchemaName { get; }
    }


    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreMemberAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class MappableAttribute : Attribute
    {
        public MappableAttribute(string suffix = "Dto")
        {
            Suffix = suffix;
        }

        public string Suffix { get; }
    }
}
