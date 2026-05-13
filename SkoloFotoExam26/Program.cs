using Microsoft.Extensions.DependencyInjection.Extensions;
using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using SkoloFotoExam26.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDistributedMemoryCache(); // Påkrævet for session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Hvor længe man er logget ind
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddRazorPages();

builder.Services.AddTransient<IRepoAsync<PhotographingEvent, int>, PhotographingEventRepoAsync>();
builder.Services.AddTransient<IRepoAsync<School, int>, SchoolRepoAsync>();
builder.Services.AddTransient<IRepoAsync<Booking, int>, BookingRepoAsync>();
builder.Services.AddTransient<IRepoAsync<Administrator, int>, AdminRepoAsync>();
builder.Services.AddTransient<IRepoAsync<Parent, int>, ParentRepoAsync>();
builder.Services.AddTransient<IRepoAsync<SchoolClass, int>, SchoolClassRepoAsync>();
builder.Services.AddTransient<IRepoAsync<SchoolSecretary, int>, SchoolSecretaryRepoAsync>();
builder.Services.AddTransient<IRepoAsync<Photographer, int>, PhotographerRepoAsync>();
builder.Services.AddTransient<IRepoAsync<Teacher, int>, TeacherRepoAsync>();
builder.Services.AddTransient<IRepoAsync<LoginInfo, string>, LoginRepoAsync>();
builder.Services.AddTransient<IRepoAsync<Student, int>, StudentRepoAsync>();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();