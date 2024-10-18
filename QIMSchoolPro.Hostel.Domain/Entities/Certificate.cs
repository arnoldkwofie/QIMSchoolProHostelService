using Qface.Domain.Shared.Common;
using QIMSchoolPro.Hostel.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Domain.Entities
{
    public class Certificate : LookupAuditableEntity<int>
    {
        public static Certificate ValidAddTestObject => Create("BSc", "New Model", "", "Bscc");
        public static Certificate InvalidTestObject => Create(null, null, "", "Bscc");


        public string Code { get; private set; }
        public string TranscriptCode { get; private set; }

        private Certificate() { }//EF Core

        private Certificate(
            string code,
            string name,
            string description,
            string transcriptCode
            )
        {
            Name = name;
            Description = description;
            Code = code;
            TranscriptCode = transcriptCode;
        }


        public static Certificate Create(
                        string code,
            string name,
            string description,
            string transcriptCode
            )
        {
            return new Certificate(
                code,
                name,
                description,
                transcriptCode);
        }
        public void Update(
                        string code,
            string name,
            string description,
            string transcriptCode
            )
        {
            Code = code;
            Name = name;
            Description = description;
            TranscriptCode = transcriptCode;
        }


        public Certificate WithId(int id)
        {
            Id = id;
            return this;
        }



    }
}
