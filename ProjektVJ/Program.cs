using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Medical.DBContext;
using Medical.Models;

static MedicalContext CreateDbContext()
{
    var options = new DbContextOptionsBuilder<MedicalContext>()
        .UseNpgsql("Host=localhost;Port=5432;Database=medical;Username=postgres;Password=password;")
        .Options;

    return new MedicalContext(options);
}

while (true)
{
    Console.Clear();
    Console.WriteLine("=== MEDICAL SYSTEM ===");
    Console.WriteLine("1. Show all patients");
    Console.WriteLine("2. Add patient");
    Console.WriteLine("3. Edit patient");
    Console.WriteLine("4. Delete patient");
    Console.WriteLine("5. Add diagnosis");
    Console.WriteLine("6. List diagnoses");
    Console.WriteLine("7. Add drug");
    Console.WriteLine("8. List drugs");
    Console.WriteLine("9. Assign drug (prescription)");
    Console.WriteLine("10. List prescriptions");
    Console.WriteLine("11. Schedule examination");
    Console.WriteLine("12. List examinations");
    Console.WriteLine("13. Show patient history");
    Console.WriteLine("0. Exit");
    Console.Write("Choose option: ");

    var choice = Console.ReadLine();

    try
    {
        switch (choice)
        {
            case "1": ListPatients(); break;
            case "2": CreatePatient(); break;
            case "3": UpdatePatient(); break;
            case "4": DeletePatient(); break;
            case "5": AddDiagnosis(); break;
            case "6": ListDiagnoses(); break;
            case "7": CreateDrug(); break;
            case "8": ListDrugs(); break;
            case "9": CreatePrescription(); break;
            case "10": ListPrescriptions(); break;
            case "11": ScheduleExamination(); break;
            case "12": ListExaminations(); break;
            case "13": ShowPatientDetails(); break;
            case "0": return;
            default: Console.WriteLine("Invalid option."); break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    Console.WriteLine("\nPress ENTER to continue...");
    Console.ReadLine();
}

static void ListPatients()
{
    using var db = CreateDbContext();
    var patients = db.Patients.OrderBy(p => p.LastName).ToList();

    foreach (var p in patients)
        Console.WriteLine($"{p.Id} | {p.FirstName} {p.LastName} | OIB: {p.OIB}");
}

static void CreatePatient()
{
    using var db = CreateDbContext();

    Console.Write("First name: ");
    var fn = Console.ReadLine()!;
    Console.Write("Last name: ");
    var ln = Console.ReadLine()!;
    Console.Write("OIB: ");
    var oib = Console.ReadLine()!;
    Console.Write("Birth date (dd.MM.yyyy): ");
    var bd = DateTime.ParseExact(Console.ReadLine()!, "dd.MM.yyyy", CultureInfo.InvariantCulture);
    Console.Write("Gender: ");
    var g = Console.ReadLine()!;

    db.Patients.Add(new Patient
    {
        FirstName = fn,
        LastName = ln,
        OIB = oib,
        BirthDate = bd,
        Gender = g
    });

    db.SaveChanges();
}

static void UpdatePatient()
{
    using var db = CreateDbContext();
    Console.Write("Patient ID: ");
    int id = int.Parse(Console.ReadLine()!);

    var p = db.Patients.Find(id);
    if (p == null) return;

    Console.Write("New first name: ");
    p.FirstName = Console.ReadLine()!;
    Console.Write("New last name: ");
    p.LastName = Console.ReadLine()!;

    db.SaveChanges();
}

static void DeletePatient()
{
    using var db = CreateDbContext();
    Console.Write("Patient ID: ");
    int id = int.Parse(Console.ReadLine()!);

    var p = db.Patients.Find(id);
    if (p == null) return;

    db.Patients.Remove(p);
    db.SaveChanges();
}

static void AddDiagnosis()
{
    using var db = CreateDbContext();

    Console.Write("Patient ID: ");
    int pid = int.Parse(Console.ReadLine()!);
    Console.Write("Description: ");
    var desc = Console.ReadLine()!;
    Console.Write("From date (dd.MM.yyyy): ");
    var from = DateTime.ParseExact(Console.ReadLine()!, "dd.MM.yyyy", CultureInfo.InvariantCulture);

    db.Diagnoses.Add(new Diagnosis
    {
        PatientId = pid,
        Description = desc,
        FromDate = from
    });

    db.SaveChanges();
}

static void ListDiagnoses()
{
    using var db = CreateDbContext();

    var list = db.Diagnoses
        .Include(d => d.Patient)
        .ToList();

    foreach (var d in list)
        Console.WriteLine($"{d.Id} | {d.Description} | {d.Patient.LastName}");
}

static void CreateDrug()
{
    using var db = CreateDbContext();
    Console.Write("Name: ");
    var name = Console.ReadLine()!;

    db.Drugs.Add(new Drug { Name = name });
    db.SaveChanges();
}

static void ListDrugs()
{
    using var db = CreateDbContext();
    foreach (var d in db.Drugs)
        Console.WriteLine($"{d.Id} | {d.Name}");
}

static void CreatePrescription()
{
    using var db = CreateDbContext();

    Console.Write("Patient ID: ");
    int pid = int.Parse(Console.ReadLine()!);
    Console.Write("Drug ID: ");
    int did = int.Parse(Console.ReadLine()!);
    Console.Write("Doctor ID: ");
    int doc = int.Parse(Console.ReadLine()!);

    db.Prescriptions.Add(new Prescription
    {
        PatientId = pid,
        DrugId = did,
        DoctorId = doc,
        StartDate = DateTime.UtcNow
    });

    db.SaveChanges();
}

static void ListPrescriptions()
{
    using var db = CreateDbContext();

    var list = db.Prescriptions
        .Include(p => p.Patient)
        .Include(p => p.Drug)
        .ToList();

    foreach (var p in list)
        Console.WriteLine($"{p.Id} | {p.Patient.LastName} | {p.Drug.Name}");
}

static void ScheduleExamination()
{
    using var db = CreateDbContext();

    Console.Write("Patient ID: ");
    int pid = int.Parse(Console.ReadLine()!);
    Console.Write("Doctor ID: ");
    int did = int.Parse(Console.ReadLine()!);
    Console.Write("Type: ");
    var type = Console.ReadLine()!;
    Console.Write("Date (dd.MM.yyyy HH:mm): ");
    var dt = DateTime.ParseExact(Console.ReadLine()!, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);

    db.Examinations.Add(new Examination
    {
        PatientId = pid,
        SpecialistDoctorId = did,
        Type = type,
        ScheduledAt = dt
    });

    db.SaveChanges();
}

static void ListExaminations()
{
    using var db = CreateDbContext();

    var list = db.Examinations
        .Include(e => e.Patient)
        .Include(e => e.SpecialistDoctor)
        .ToList();

    foreach (var e in list)
        Console.WriteLine($"{e.Id} | {e.Type} | {e.Patient.LastName}");
}

static void ShowPatientDetails()
{
    using var db = CreateDbContext();

    Console.Write("Patient ID: ");
    int id = int.Parse(Console.ReadLine()!);

    var p = db.Patients
        .Include(p => p.Diagnoses)
        .Include(p => p.Prescriptions).ThenInclude(pr => pr.Drug)
        .Include(p => p.Examinations).ThenInclude(e => e.SpecialistDoctor)
        .FirstOrDefault(p => p.Id == id);

    if (p == null) return;

    Console.WriteLine($"\n{p.FirstName} {p.LastName}");

    foreach (var d in p.Diagnoses)
        Console.WriteLine($"Diagnosis: {d.Description}");

    foreach (var pr in p.Prescriptions)
        Console.WriteLine($"Drug: {pr.Drug.Name}");

    foreach (var e in p.Examinations)
        Console.WriteLine($"Exam: {e.Type} at {e.ScheduledAt}");
}
