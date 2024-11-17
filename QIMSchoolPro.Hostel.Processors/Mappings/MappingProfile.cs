using AutoMapper;
using QIMSchoolPro.Hostel.Domain.Entities;
using QIMSchoolPro.Hostel.Domain.ValueObjects;
using QIMSchoolPro.Hostel.Processors.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Processors.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Building, BuildingDto>().ReverseMap();
            CreateMap<Floor, FloorDto>().ReverseMap();
            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<Bed, BedDto>().ReverseMap();
            CreateMap<Party, PartyDto>().ReverseMap();
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Name, NameDto>().ReverseMap();
            CreateMap<Programme, ProgrammeDto>().ReverseMap();
                 CreateMap<Payment, PaymentDto>().ReverseMap();
                 CreateMap<PaymentVendor, PaymentVendorDto>().ReverseMap();
                 CreateMap<PaymentItemLine, PaymentItemLineDto>().ReverseMap();



        }
    }
}
