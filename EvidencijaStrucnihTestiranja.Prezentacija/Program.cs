using EvidencijaStrucnihTestiranja_Sloj_Podataka.Tehnoloske_Klase;
using EvidencijaStrucnihTestiranja_Sloj_Poslovne_Logike;
using EvidencijaStrucnihTestiranja_Sloj_Servisa.Servisne_Klase;
using EvidencijaStrucnihTestiranja_Sloj_Servisa.Interfejsi;
using SoapCore;
using EvidencijaStrucnihTestiranja_Sloj_Servisa.SOAP;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddSoapCore();


string connectionString =
    builder.Configuration.GetConnectionString("EvidencijaStrucnihTestiranja")!;

builder.Services.AddSingleton(new KonekcijaSaBazom(connectionString));

builder.Services.AddScoped<ZaposleniDB>();
builder.Services.AddScoped<VrstaTestaDB>();
builder.Services.AddScoped<TestiranjeDB>();
builder.Services.AddScoped<KorisnikDB>();
builder.Services.AddScoped<TestiranjeXML>();

builder.Services.AddScoped<ZaposleniLogika>();
int minimalanBrojPoena =
    builder.Configuration.GetValue<int>("PoslovnaPravila:MinimalanBrojPoena");

builder.Services.AddScoped<TestiranjeLogika>(
    sp => new TestiranjeLogika(minimalanBrojPoena));

builder.Services.AddScoped<VrstaTestaServis>();
builder.Services.AddScoped<ZaposleniServis>();
builder.Services.AddScoped<ITestiranjeServis, TestiranjeServis>();
builder.Services.AddScoped<TestiranjeSoapServis>();
builder.Services.AddScoped<KorisnikServis>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Pocetna/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Pocetna}/{action=Index}/{id?}");

    endpoints.UseSoapEndpoint<TestiranjeSoapServis>(options =>
    {
        options.Path = "/TestiranjeServis.asmx";
        options.SoapSerializer = SoapSerializer.DataContractSerializer;
    });
});

app.Run();