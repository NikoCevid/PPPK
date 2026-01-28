namespace Medical.Models
{
    public class Examination
    {
        public int Id { get; set; }

        public string Type { get; set; } = string.Empty;       
        public DateTime ScheduledAt { get; set; }         

        public int PatientId { get; set; }
        public Patient Patient { get; set; } = default!;

        public int SpecialistDoctorId { get; set; }
        public Doctor SpecialistDoctor { get; set; } = default!;
    }

}
