using Proyecto_1__CRUD.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMemoryCache();

// Register your services
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IMaquinariaService, MaquinariaService>();
builder.Services.AddScoped<IMantenimientoService, MantenimientoService>();

// Register HttpClient for IClienteService
builder.Services.AddHttpClient<IClienteService, ClienteService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5191/"); // Cambia esto por la URL de tu API
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
