﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Processors.Exceptions
{

    public class CustomValidationException : Exception
    {
        public CustomValidationException(string message) : base(message) { }
    }

    public class CustomNullException : Exception
    {  public CustomNullException(string message) : base(message) { }
    }
}
