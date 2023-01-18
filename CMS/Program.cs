
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

string allowSpecificOrigins = "_allowSpecificOrigins";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
    {
        c.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
        c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidAudience = builder.Configuration["Auth0:Audience"],
            ValidIssuer = $"https://{builder.Configuration["Auth0:Domain"]}"
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Participant", policy => policy.RequireClaim("https://localhost:7192/claims/role", "Participant"));
});

builder.Services.AddCors(options =>
{
	options.AddPolicy(allowSpecificOrigins,

		builder =>
		{
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
		});
});

var app = builder.Build();


app.UseRouting();

app.UseCors(allowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
