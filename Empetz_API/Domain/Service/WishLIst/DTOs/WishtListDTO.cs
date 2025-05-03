using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.WishLIst.DTOs
{
    public class WishtListDTO
    {
        public Guid Id { get; set; }

        public Guid User { get; set; }

        public Guid Pet { get; set; }
    }
}
