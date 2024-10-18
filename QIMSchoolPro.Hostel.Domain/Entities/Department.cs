using Qface.Domain.Shared.Common;
using QIMSchoolPro.Hostel.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Domain.Entities
{
    public class Department : LookupAuditableEntity<int>
    {

        public static Department ValidAddTestObject => Create("New Model", "", 1, 1);
        public static Department InvalidTestObject => Create(null, "", 1, 1);

        public int Index { get; private set; }
        public Faculty Faculty { get; set; }
        public int FacultyId { get; private set; }
        public string FullName => $"{Name}/{Faculty.SchoolCentre.Name})/{Faculty.SchoolCentre.Campus.Name}";

        private Department() { }//EF Core


        private Department(
           string name,
           string description,
           int index,
           int facultyId
           )
        {
            Name = name;
            FacultyId = facultyId;
            Description = description;
            Index = index;

        }

        public static Department Create(
                string name,
           string description,
           int index,
           int facultyId)
        {
            return new Department(
                name,
                description,
                index,
                facultyId);

        }

        public void Update(
                string name,
           string description,
           int index,
           Faculty faculty)
        {

            Name = name;
            Faculty = faculty;
            Description = description;
            Index = index;




        }

        public Department WithId(int id)
        {
            Id = id;
            return this;
        }
    }
}
