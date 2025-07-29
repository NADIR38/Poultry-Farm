using Poultary.BL.Bl;
using Poultary.Interfaces;
using pro.BL.Bl;
using pro.DL;
using pro.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("MyConnection");

// Register DL layer dependencies
builder.Services.AddScoped<chickbatchDL>();
builder.Services.AddScoped<ChickenBatchInterface, ChickenbatchBL>();
builder.Services.AddScoped<SupplierDL>();
builder.Services.AddScoped<Isupplier, SupplierBL>();

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
    pattern: "{controller=Admin}/{action=Dashboard}/{id?}")
    .WithStaticAssets();

app.Run();
