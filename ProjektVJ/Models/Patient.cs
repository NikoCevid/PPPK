namespace Medical.Models
{
    public class Patient
    {
        public int Id { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string OIB { get; set; } = default!;  
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = default!;
        public string ResidenceAddress { get; set; } = default!;
        public string PermanentAddress { get; set; } = default!;

        public ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
        public ICollection<Examination> Examinations { get; set; } = new List<Examination>();

    }

}
