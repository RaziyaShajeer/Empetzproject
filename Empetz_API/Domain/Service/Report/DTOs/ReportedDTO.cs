using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Report.DTOs
{
    public class ReportedDTO
    {

        public Guid Id { get; set; }

        public Guid Pet { get; set; }

        public Guid User { get; set; }

        public Guid Reason { get; set; }
        public string Reasons { get; set; }
    }
}
