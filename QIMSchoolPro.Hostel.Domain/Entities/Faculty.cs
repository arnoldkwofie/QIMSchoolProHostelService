using Qface.Domain.Shared.Common;
using QIMSchoolPro.Hostel.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Domain.Entities
{
    public class Faculty : LookupAuditableEntity<int>
    {
        public static Faculty ValidAddTestObject => Create("NF", "New Model", "", "test@email.com", 0, 1);
        public static Faculty InvalidTestObject => Create(null, null, "", "test@email.com", 0, 1);
        public string Code { get; private set; }
        public string Email { get; private set; }
        public int Index { get; private set; }
        public SchoolCentre SchoolCentre { get; set; }
        public int SchoolCentreId { get; private set; }

        private Faculty() { }//EF Core

        private Faculty(
            string code,
            string name,
            string description,
            string email,
            int index,
            int schoolCentreId)
        {
            Code = code;
            Name = name;
            Description = description;
            Email = email;
            Index = index;
            SchoolCentreId = schoolCentreId;
        }


        public static Faculty Create(
            string code,
            string name,
            string description,
            string email,
            int facultyIndex,
            int schoolCentreId
            )
        {
            return new Faculty(
                code,
                name,
                description,
                email,
                facultyIndex, schoolCentreId);

        }
        public void Update(string code,
            string name,
            string description,
            string email, int index,
            SchoolCentre schoolCentre

            )
        {
            Code = code;
            Name = name;
            Description = description;
            Email = email;
            Index = index;
            SchoolCentre = schoolCentre;
        }
    }
}
