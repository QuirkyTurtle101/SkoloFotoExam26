using SkoloFotoExam26.Interfaces;
using SkoloFotoExam26.Models;
using SkoloFotoExam26.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddTransient<IPhotographingEventRepoAsync, PhotographingEventRepoAsync>();
builder.Services.AddTransient<ISchoolRepoAsync, SchoolRepoAsync>();

builder.Services.AddTransient<IRepoAsync<Administrator, int>, AdminRepoAsync>();
builder.Services.AddTransient<IRepoAsync<Parent, int>, ParentRepoAsync>();
builder.Services.AddTransient<IRepoAsync<SchoolSecretary, int>, SchoolSecretaryRepoAsync>();
builder.Services.AddTransient<IRepoAsync<Photographer, int>, PhotographerRepoAsync>();
builder.Services.AddTransient<IRepoAsync<Teacher, int>, TeacherRepoAsync>();

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
