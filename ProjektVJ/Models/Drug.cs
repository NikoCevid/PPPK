namespace Medical.Models
{
    public class Drug
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;        
        public string Form { get; set; } = default!;        
        public string Strength { get; set; } = default!;  
        public string Frequency { get; set; } = default!;  

        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    }

}
