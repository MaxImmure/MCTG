﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTG.Models.Exceptions
{
    public class UserNotFoundException : NullReferenceException
    {
        public UserNotFoundException() { }
        public UserNotFoundException(string message) : base(message) { }
        public UserNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
