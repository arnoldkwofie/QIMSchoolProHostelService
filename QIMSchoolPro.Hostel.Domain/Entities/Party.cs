using Qface.Domain.Shared.Common;
using Qface.Domain.Shared.Enums;
using Qface.Domain.Shared.ValueObjects;
using QIMSchoolPro.Hostel.Domain.Enums;
using QIMSchoolPro.Hostel.Domain.ValueObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Domain.Entities
{
    public class Party : AuditableAutoEntity
    {
        public bool? IsDisability { get; set; }
        public string? DisabilityReason { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public string? PlaceOfBirth { get; set; }
        public PartyType PartyType { get; set; }
        public Name Name { get; set; }
        public EmailAddressValueType PrimaryEmailAddress { get; set; }
        public EmailAddressValueType OtherEmailAddress { get; set; }

        public PhoneNumberValueType PrimaryPhoneNumber { get; set; }
        public PhoneNumberValueType OtherPhoneNumber { get; set; }
        public string? FaxNumber { get; set; }

        public AddressLine AddressLine { get; set; }
     

        public Party()
        {

        }

        Party(Name name, DateTime dateOfBirth, MaritalStatus maritalStatus)
        {
            Name = name;
            MaritalStatus = maritalStatus;
            DateOfBirth = dateOfBirth;
        }



        Party(Name name, MaritalStatus maritalStatus)
        {
            Name = name;
            MaritalStatus = maritalStatus;
        }

        Party(Name name)
        {
            Name = name;
        }

        Party(Name name, DateTime dateOfBirth)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            //CheckRule(new ProvidedDateOfBirthMustBeRule(dateOfBirth));

        }

        public static Party Create(Sex sex)
        {
            return new Party();
        }
        public static Party Create(Name name)
        {
            return new Party(name);
        }
        public static Party Create(Name name, DateTime dateOfBirth)
        {
            return new Party(name, dateOfBirth);
        }
        public static Party Create(Name name, MaritalStatus maritalStatus)
        {
            return new Party(name, maritalStatus);

        }
        public static Party Create(Name name,
                   DateTime dateOfBirth,
                   MaritalStatus maritalStatus)
        {
            return new Party(name, dateOfBirth, maritalStatus);
        }


        public Party Of(PartyType type)
        {
            PartyType = type;
            return this;
        }



        public Party WithPrimaryEmail(string email)
        {
            PrimaryEmailAddress = EmailAddressValueType.Create(email, EmailType.Primary);
            return this;
        }
        public Party WithMaritalStatus(MaritalStatus maritalStatus)
        {
            MaritalStatus = maritalStatus;
            return this;
        }

        public Party WithSecondaryEmail(string email)
        {
            OtherEmailAddress = EmailAddressValueType.Create(email, EmailType.Other);
            return this;
        }
        public Party WithPrimaryPhone(string phone)
        {
            PrimaryPhoneNumber = PhoneNumberValueType.Create(phone, PhoneNumberType.Primary);
            return this;
        }
        public Party WithSecondaryPhone(string phone)
        {
            OtherPhoneNumber = PhoneNumberValueType.Create(phone, PhoneNumberType.Primary);
            return this;
        }
        public Party WithDateOfBirth(DateTime? dateOfBirth)
        {
            DateOfBirth = dateOfBirth;
            return this;
        }
        public Party WithPlaceOfBirth(string placeOfBirth)
        {
            PlaceOfBirth = placeOfBirth;
            return this;
        }
        public Party WithFaxNumber(string faxNumber)
        {
            FaxNumber = faxNumber;
            return this;
        }


    }

}
