﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public class PetListUserParams : PaginationParams
    {
        public Guid Userid { get; set; }

    }
}
