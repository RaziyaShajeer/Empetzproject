﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    [Serializable]
    public class PetNotFoundException: Exception
    {
        public PetNotFoundException()
        {
            

        }
        public PetNotFoundException(string message)
        : base(message)
        {

        }
    }
}
