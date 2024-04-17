using Inl�mning2.Core.Interfaces;
using Inl�mning2.Core.Services;
using Inl�mning2.Data.Context;
using Inl�mning2.Data.Interfaces;
using Inl�mning2.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();

builder.Services.AddSwaggerGen();


//JWT f�rsta delen
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    //JWT andra delen
    .AddJwtBearer(opt => {

        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true, //Hur l�nge token ska g�lla
            ValidateIssuerSigningKey = true, //man validerar den som utf�rdar denna
            ValidIssuer = "http://localhost:5171/",//Vilken site som utf�rdat detta
            ValidAudience = "http://localhost:5171/",//Vilken site vi loggar in mot
            IssuerSigningKey =
             new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysecretKey12345!#123456789101112"))
        };
    });




builder.Services.AddDbContext<WebShopContext>(

        options => options.UseSqlServer(@"Data Source=localhost;Initial Catalog=WebShopDB;Integrated Security=SSPI;TrustServerCertificate=True;")

 );


var app = builder.Build();

app.UseRouting();

//F�r att kunna anv�nda authentication och authorization.
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
