using Qface.Domain.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Domain.ValueObjects
{
    public class Guardian : ValueObjectType
    {
        public int? RelationshipToStudentId { get; set; }
        public int? OccupationId { get; set; }
        public bool? IsUniversityStaff { get; set; }
        public string? StaffPosition { get; set; }
        public string? Name { get; set; }

        public Guardian WithName(string name)
        {
            Name = name;
            return this;
        }
        public Guardian WithStaffPosition(string staffPosition)
        {
            StaffPosition = staffPosition;
            return this;
        }
        public Guardian WithRelationShipId(int relationshipId)
        {
            RelationshipToStudentId = relationshipId;
            return this;
        }
        public Guardian WithOccupationId(int occupationId)
        {
            OccupationId = occupationId;
            return this;
        }
        public Guardian WithIsUniversityStaff(bool? isUniversityStaff)
        {
            IsUniversityStaff = isUniversityStaff;
            return this;
        }



    }

}
