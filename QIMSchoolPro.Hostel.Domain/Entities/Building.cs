using Qface.Domain.Shared.Common;
using QIMSchoolPro.Hostel.Domain.Enums;
using QIMSchoolPro.Hostel.Domain.ValueObjects;


namespace QIMSchoolPro.Hostel.Domain.Entities
{
    public class Building:  AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public OwnershipType OwnerShipType { get; set; }
        public GenderOption GenderOption { get; set; }
        public Geolocation Geolocation { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string PicturePath { get; set; }
        public BuildingType BuildingType { get; set; }

        public List<Floor> Floors { get; set; }

        public Building()
        {
                
        }

        public Building(string name, OwnershipType ownershipType, GenderOption genderOption, Geolocation geolocation, string address, string description, string picturePath, BuildingType buildingType)
        {
            Name= name;
            OwnerShipType= ownershipType;
            GenderOption= genderOption;
            Geolocation= geolocation;
            Address= address;
            Description= description;
            PicturePath= picturePath;
            BuildingType = buildingType;

        }

        public static Building Create (string name, OwnershipType ownershipType, GenderOption genderOption, Geolocation geolocation, string address, string description, string picturePath, BuildingType buildingType)
        {
            return new Building(name, ownershipType, genderOption, geolocation, address, description, picturePath, buildingType);   
        }
    }
}
