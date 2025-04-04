using api.Admin;
using api.Data;
using api.Interface;
using api.Model;
using api.Repository;
using api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});



builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
}
);

builder.Services.AddDbContext<ApplicationDBContext>(option=>{
option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<User,IdentityRole>(options=>{
options.Password.RequireDigit=true;
options.Password.RequiredLength=10;
options.Password.RequireNonAlphanumeric=true;
options.Password.RequireUppercase=true;
options.Password.RequireLowercase=true;
}).AddEntityFrameworkStores<ApplicationDBContext>();

builder.Services.AddAuthentication(option=>{
option.DefaultAuthenticateScheme=
option.DefaultChallengeScheme=
option.DefaultForbidScheme=
option.DefaultScheme=
option.DefaultSignInScheme=
option.DefaultSignOutScheme=JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option=>{

#pragma warning disable CS8604 // Possible null reference argument.
option.TokenValidationParameters=new TokenValidationParameters{
ValidateIssuer=true,
ValidIssuer=builder.Configuration["JWT:Issuer"],
ValidateAudience=true,
ValidAudience=builder.Configuration["JWT:Audience"],
ValidateIssuerSigningKey=true,
IssuerSigningKey=new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]))

    };
#pragma warning restore CS8604 // Possible null reference argument.
});

builder.Services.AddScoped<ITokenService,TokenServices>();
builder.Services.AddScoped<ICartItem,CartItemRepos>();
builder.Services.AddScoped<ICart,CartRepo>();
builder.Services.AddScoped<IAdmin,AdminUser>();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
        });
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();


