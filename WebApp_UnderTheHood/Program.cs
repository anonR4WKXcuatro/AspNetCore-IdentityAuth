using Microsoft.AspNetCore.Authorization;
using WebApp_UnderTheHood.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {

        options.Cookie.Name = "CookieAuth";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(480); // Cookies TimeSpan
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/AccessDenied";
       
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("HRDepartmentOnly", policy => policy.RequireClaim("role", "HR"));
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
    options.AddPolicy("HRManagerOnly", policy => policy
    .RequireClaim("role", "HR")
    .RequireClaim("Manager")
    .Requirements.Add(new HRManagerProbationRequirement(3)));

});
builder.Services.AddSingleton<IAuthorizationHandler, HRManagerProbationRequirementHandler>();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
