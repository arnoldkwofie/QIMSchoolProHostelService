using Qface.Domain.Shared.Common;
using Qface.Domain.Shared.Enums;

namespace Qface.Domain.Shared.ValueObjects
{
	public class AddressLine : ValueObjectType
	{
		public AddressType? AddressType { get; private set; }

		public string? Line1 { get; private set; }
		public string? Line2 { get; private set; }
		public string? Line3 { get; private set; }
		public string? Line4 { get; private set; }

		public int? RegionId { get; private set; }
		

		public string? CountryId { get; private set; }
		

		public int? DistrictId { get; private set; }
		public string? CityText { get; set; }

		public string? HomeTown { get; set; }

		public int? CityId { get; private set; }
		public string? GhanaPostCode { get; private set; }

		private AddressLine() { }

		private AddressLine(string countryId,
			int? regionId,
			int? districtId,
			int? cityId,
			string line1,
			string line2 = null,
			string line3 = null,
			string line4 = null)
		{
			CountryId = countryId;
			RegionId = regionId;
			DistrictId = districtId;
			CityId = cityId;
			Line1 = line1;
			Line2 = line2;
			Line3 = line3;
			Line4 = line4;

			CheckRule(new AddressLineMustBeValidRule(this));

		}

		public static AddressLine Create(string countryId,
			int? regionId,
			int? districtId,
			int? cityId,
			string line1,
			string line2 = null,
			string line3 = null,
			string line4 = null)
		{
			return new AddressLine(countryId, regionId,
				districtId, cityId, line1, line2, line3, line4);
		}

		public AddressLine(string line1, string line2, string line3, string line4)
		{
			Line1 = line1;
			Line2 = line2;
			Line3 = line3;
			Line4 = line4;

		}
		public static AddressLine Create(
		   string line1,
		   string line2,
		   string line3,
		   string line4)
		{
			return new AddressLine(line1, line2, line3, line4);
		}
		public override string ToString()
		{
			return $"{Line1}\n{Line2}\n{Line3}\n{Line4}";
		}

        public AddressLine WithCountryId(string countryId)
        {
            CountryId = countryId;
            return this;
        }
        public AddressLine WithRegionId(int regionId)
        {
            RegionId = regionId;
            return this;
        }

        public AddressLine WithHomeTown(string homeTown)
        {
            HomeTown = homeTown;
            return this;
        }
        public AddressLine WithCity(string city)
        {
            CityText = city;
            return this;
        }
        public AddressLine WithLine1(string line1)
        {
            Line1 = line1;
            return this;
        }
        public AddressLine WithLine2(string line2)
        {
            Line2 = line2;
            return this;
        }
        public AddressLine WithGPSCode(string gpsCode)
        {
            GhanaPostCode = gpsCode;
            return this;
        }
	}
}
