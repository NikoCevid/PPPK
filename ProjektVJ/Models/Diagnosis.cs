using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical.Models
{
    public class Diagnosis
    {
        public int Id { get; set; }

        public string Description { get; set; } = default!;  
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }            

        public int PatientId { get; set; }
        public Patient Patient { get; set; } = default!;
    }

}
