using AutoMapper;
using Newtonsoft.Json;
using QIMSchoolPro.Hostel.Domain.Entities;
using QIMSchoolPro.Hostel.Domain.Enums;
using QIMSchoolPro.Hostel.Persistence;
using QIMSchoolPro.Hostel.Persistence.Interfaces;
using QIMSchoolPro.Hostel.Processors.Dtos;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QIMSchoolPro.Hostel.Processors.Processors
{
    [ProcessorBase]
    public class StudentProcessor
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentProcessor(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;

        }

        public async Task<StudentDto> GetStudentByStudentNumber(string id)
        {
            
                var studentFromDB = await _studentRepository.GetAsync(id);
                var student = _mapper.Map<StudentDto>(studentFromDB);

                return student;

        }


    }



}
