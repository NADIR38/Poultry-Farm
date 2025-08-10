using Poultary.BL.Bl;
using Poultary.Interfaces;
using pro.BL.Bl;
using pro.DL;
using pro.Interface;
using sample.Repository;
using sample.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("MyConnection");

// Register DL layer dependencies
builder.Services.AddScoped<chickbatchDL>();
builder.Services.AddScoped<ChickenBatchInterface, ChickenbatchBL>();
builder.Services.AddScoped<SupplierDL>();
builder.Services.AddScoped<Isupplier, SupplierBL>();
builder.Services.AddScoped<StaffDL>();
builder.Services.AddScoped<Istaff, StaffBL>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customer}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
