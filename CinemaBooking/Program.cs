using CinemaBooking.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;
using CEN4020_Website.Pages.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options => {
    options.Cookie.Name = "MyCookieAuth";
    options.LoginPath = "/LoginRegister/Login";
    options.AccessDeniedPath = "/LoginRegister/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
});

//Creating the User Profiles for our specific users to determine who has access to what functionality
builder.Services.AddAuthorization(options => {
    options.AddPolicy("ManagerCredentialsRequired",
        policy => policy.RequireClaim("manager", "Manager"));
});

builder.Services.AddAuthorization(options => {
    options.AddPolicy("CrewmemberCredentials",
        policy => policy.RequireClaim("UserCrewmember", "Crewmember"));
});

builder.Services.AddAuthorization(options => {
    options.AddPolicy("CustomerCredentials",
        policy => policy.RequireClaim("UserCustomer", "Customer"));
});

//Pulls the SendGrid data needed to send the email such as APIKey, SenderEmail, and SenderName
builder.Services.AddTransient<IEmailSender, SendGridEmailSender>();
builder.Services.Configure<ResetMessageSenderOptions>(options =>
{
    options.SendGridKey = builder.Configuration["ExternalProviders:SendGrid:SENDGRID_API_KEY"];
    options.SenderEmail = builder.Configuration["ExternalProviders:SendGrid:SenderEmail"];
    options.SenderName = builder.Configuration["ExternalProviders:SendGrid:SenderName"];
});


// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
