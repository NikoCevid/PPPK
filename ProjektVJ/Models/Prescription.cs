namespace Medical.Models
{
    public class Prescription
    {
        public int Id { get; set; }

        public string Condition { get; set; } = default!; 
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }          

        public int PatientId { get; set; }
        public Patient Patient { get; set; } = default!;

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = default!;

        public int DrugId { get; set; }
        public Drug Drug { get; set; } = default!;
    }

}
