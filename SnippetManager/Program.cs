using System.Text;
using Azure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SnippetManager.Database;
using SnippetManager.Models;
using SnippetManager.Repository;


var builder = WebApplication.CreateBuilder(args);

var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

//builder.Configuration.AddAzureKeyVault(
//	new Uri($"https://{builder.Configuration["KeyVault:Name"]}.vault.azure.net/"),
//	new ClientSecretCredential(
//		builder.Configuration["KeyVault:TenantId"],
//		builder.Configuration["KeyVault:ClientId"],
//		builder.Configuration["KeyVault:ClientSecret"]
//	));

builder.Services.AddDbContext<SnippetManagerContext>(options =>
	options.UseSqlServer(builder.Configuration["ConnectionStrings:SqlServer"]));

builder.Services.AddIdentity<AppUser, IdentityRole>()
	.AddEntityFrameworkStores<SnippetManagerContext>()
	.AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
	options.User.RequireUniqueEmail = true;
	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
	options.Lockout.MaxFailedAccessAttempts = 5;
	options.Lockout.AllowedForNewUsers = true;
});

builder.Services.AddAuthentication(option => {
	option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(option => {
		option.SaveToken = true;
		option.RequireHttpsMetadata = false;
		option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters() {
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidAudience = builder.Configuration["JWT:ValidAudience"],
			ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
		};
	});

builder.Services.AddCors(option =>
{
	option.AddDefaultPolicy(builder =>
	{
		builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
	});
});

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<ISnippetRepository, SnippetRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();


var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
