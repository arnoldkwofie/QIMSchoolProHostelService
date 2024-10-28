using Qface.Domain.Shared.Common;
using Qface.Domain.Shared.Enums;
using Qface.Domain.Shared.ValueObjects;
using QIMSchoolPro.Hostel.Domain.Enums;
using QIMSchoolPro.Hostel.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Domain.Entities
{
    public class Booking : AuditableEntity
    {
        public int Id { get; set; }
        public string StudentNumber { get; set; }
        public int BedId { get; set; }
        public Bed Bed { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public AcademicPeriod? AcademicPeriod { get; set; }
        public bool IsVerified { get; set; }

        public Booking()
        {

        }


        public Booking(string studentNumber, int bedId, DateTime bookingDate, DateTime expiryDate, DateTime? confirmationDate,
            AcademicPeriod academicPeriod, bool isVerified)
        {
            StudentNumber = studentNumber;
            BedId = bedId;
            BookingDate = bookingDate;
            ExpiryDate = expiryDate;
            ConfirmationDate = confirmationDate;
            AcademicPeriod = academicPeriod;
            IsVerified = isVerified;


        }

        public static Booking Create(string studentNumber, int bedId, DateTime bookingDate, DateTime expiryDate, DateTime? confirmationDate,
            AcademicPeriod academicPeriod, bool isVerified)
        {
            return new(studentNumber, bedId, bookingDate, expiryDate, confirmationDate, academicPeriod, isVerified);
        }
    }



    public class AugmentedBooking 
    {
        public int Id { get; set; }
        public string StudentNumber { get; set; }
        public int BedId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public int AcademicPeriod_LowerYear { get; set; }
        public int AcademicPeriod_UpperYear { get; set; }
        public Semester AcademicPeriod_Semester { get; set; }
        public bool IsVerified { get; set; }
        public string? Audit_CreatedBy { get; set; }
        public DateTime Audit_Created { get; set; }
        public string? Audit_LastModifiedBy { get; set; }
        public DateTime Audit_LastModified { get; set; }
        public string Audit_EntityStatus { get; set; }
        public DateTime Audit_EntityStatusCreated { get; set; }
        public string? Audit_EntityStatusCreateBy { get; set; }
        public DateTime Audit_EntityStatusLastModified { get; set; }
        public string? Audit_EntityStatusLastModifiedBy { get; set; }
        public string? OtherProperty { get; set; }
        public string? OtherProperty1 { get; set; }

    }
}
