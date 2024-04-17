using Inlämning2.Core.Interfaces;
using Inlämning2.Core.Services;
using Inlämning2.Data.Context;
using Inlämning2.Data.Interfaces;
using Inlämning2.Data.Repositories;
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


//JWT första delen
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
            ValidateLifetime = true, //Hur länge token ska gälla
            ValidateIssuerSigningKey = true, //man validerar den som utfärdar denna
            ValidIssuer = "http://localhost:5171/",//Vilken site som utfärdat detta
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

//För att kunna använda authentication och authorization.
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
