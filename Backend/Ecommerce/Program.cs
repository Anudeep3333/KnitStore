using Ecommerce.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUserService, UserService>(); 
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddCors();

//configure mysql database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EcommerceDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy.AllowAnyOrigin() 
            .AllowAnyHeader()
            .AllowAnyMethod());
});
//WithOrigins("http://localhost:4200") // The URL of your Angular app
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS middleware
app.UseCors("AllowAngularApp");
app.UseHttpsRedirection();
app.UseAuthorization();

// Configure the HTTP request pipeline.
app.MapControllers();

app.Run();
//localdb
//"DefaultConnection": "server=localhost;port=3306;database=ecom;user=root;password=Admin@123",
//"DefaultConnection": "server=knitstore.cdwkqiyk2z1l.eu-north-1.rds.amazonaws.com;port=3306;database=Knitstore;user=admin;password=Anudeep3333"
//"DefaultConnection": "server=localhost;port=3306;database=ecom;user=root;password=Admin@123",

