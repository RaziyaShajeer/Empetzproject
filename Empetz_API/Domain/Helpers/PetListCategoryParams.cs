﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public class PetListCategoryParams : PaginationParams
    {
        public Guid categoryid { get; set; }
    }
}
