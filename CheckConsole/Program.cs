using API.Middlewares;
using API.Validators;
using Domain;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

#region cuvanjePacijenta
//Patient patient = new Patient();
//patient.FirstName = "Danica";
//patient.LastName = "Panov";
//Context context = new Context();
//Console.WriteLine($"Treba da sačuvamo pacijenta {patient.IdPatient} {patient.FirstName} {patient.LastName}");

//context.Patients.Add(patient);
//context.SaveChanges(); //neophodno da bi sačuvalo u bazi

//Console.WriteLine($"Pacijent {patient.IdPatient} {patient.FirstName} {patient.LastName} je sačuvana");
//context.Dispose();
#endregion

#region Add Medicine
//Medicine medicine = new Medicine();
//medicine.Name = "Antibiotik";
//medicine.Price = 250;
//Context context1 = new Context();
//context1.Add(medicine);
//context1.SaveChanges();
#endregion


#region Add PharmacyStore
//PharmacyStore pharmacyStore = new PharmacyStore();
//pharmacyStore.Name = "Vozdovac Pharma";
//Context context1 = new Context();
//context1.Add(pharmacyStore);
//context1.SaveChanges();
#endregion

#region Add Bill
//Bill bill = new Bill();
//bill.IdPatient = 2;
//bill.IdPharmacy = 2;
//bill.Date = DateTime.Now;
//bill.TotalPrice = 200;
//lista sa billitems-a, izmena racuna
//Context context1 = new Context();
//context1.Add(bill);
//context1.SaveChanges();
#endregion

#region ADD BILLITEM
//BillItem billItem = new BillItem();
//billItem.IdBill = 1;
//billItem.Price = 200;
//billItem.Description = "Na recept";
//billItem.IdMedicine = 2;
//Context context2 = new Context();
//context2.Add(billItem);
//context2.SaveChanges();
#endregion 

//using Context context = new Context();
//var Patients= context.Patients.ToList(); 1. način učitavanja svih pacijenata iz baze
#region UcitavanjaPacijenataBezToList
//foreach (var patient in Patients)
//{
//    Console.WriteLine($"Pacijent {patient.IdPatient} {patient.FirstName} {patient.LastName}");
//}
#endregion

#region VratiPacijentaId2
//int id = 2;
//Patient patientid2 = context.Patients.First(p => p.IdPatient == id
//&& p.FirstName.StartsWith("Tatjana"));
//Console.WriteLine(patientid2);
#endregion

//vrati sve pacijente koji počinju sa Ta
#region Pogledaj
//var patientsTa = context.Patients
//    .Where(p => p.FirstName.StartsWith("Ta"))
//    .ToList();
//foreach (var patient in patientsTa)
//{
//    Console.WriteLine(patient);
//}
#endregion

//vrati broj pacijenata čije ime počinje na Ta
#region BrojPacijenataTa
//int brojPacijenataTa = context.Patients.Count(p => p.FirstName.StartsWith("Ta"));
//Console.WriteLine($"Broj pacijenata čije ime počinje na Ta je: {brojPacijenataTa}");
#endregion

//preskoci prvog pacijenta i vrati druga dva pacijenta sa 2
#region Procitati
//var pacijenti2=context.Patients.Where(p => p.FirstName.StartsWith("Ta"))
//    .Skip(1)
//    .Take(2)
//    .ToList();
//foreach (var patient in pacijenti2)
//{
//    Console.WriteLine(patient);
//}
#endregion

//azuriranje
#region UpdatePatient
//Patient patients = context.Patients.Find(2);
//Console.WriteLine(patients);

//patients.FirstName = "Tamara";
//context.SaveChanges();
//Console.WriteLine(patients);
#endregion

#region UpdatePS
//PharmacyStore ps = context.PharmacyStores.Find(1);
//ps.Name = "Novi Beograd Pharma";
//context.SaveChanges();
#endregion

#region UpdateMedicine
//Medicine medicine= context.Medicines.Find(1);
//medicine.Price = 400;
//context.SaveChanges();
#endregion

#region UpdateBill 
//Bill bill = context.Bills.Find(1);
//bill.IdPharmacy = 2;
//context.SaveChanges();
#endregion

#region Azuriranje2

//Patient patient2 = new Patient { IdPatient = 2, FirstName="Tamara", LastName = "Milosavljevic" };
//context.Patients.Update(patient2);
//context.SaveChanges();

#endregion

//Brisanje
#region DeletePatient
//Patient pat2 = new Patient { IdPatient=6, FirstName = "Biljana", LastName = "Kuburovic" };
//context.Patients.Remove(pat2);
//context.SaveChanges();
#endregion

#region DeleteMedicine
//Medicine med = new Medicine();
//med.IdMedicine = 1;
//context.Medicines.Remove(med);
//context.SaveChanges();
#endregion

#region DeletePharmacyStore
//PharmacyStore ps = new PharmacyStore();
//ps.IdPharmacy = 1;
//context.PharmacyStores.Remove(ps);
//context.SaveChanges();
#endregion

#region DeleteBill
//Bill bill= new Bill();
//bill.IdBill = 1;
//context.Bills.Remove(bill);
//context.SaveChanges();
#endregion
#region DeleteBillItem
//BillItem bi = new BillItem();
//bi.Rb = 0;
//context.BillItems.Remove(bi);
//context.SaveChanges();
#endregion

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<Context>();
builder.Services.AddValidatorsFromAssemblyContaining<MedicineDtoValidator>();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseCors(config =>
{
    config.WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.UseMiddleware<ErrorHandlingMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
