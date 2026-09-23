using Proyecto1_Carniceria_MichaelVS.Datos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
// Creamos una sola instancia de DatosEnMemoria para toda la app,así los datos no se pierden entre peticiones.
builder.Services.AddSingleton<DatosEnMemoria>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
