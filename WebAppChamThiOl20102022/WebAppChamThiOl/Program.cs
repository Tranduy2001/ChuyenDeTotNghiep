using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAppChamThiOl.Data;
using WebAppChamThiOl.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");;

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));;

builder.Services.AddScoped<AccountServices>();
//builder.Services.AddScoped<ShortStoryServices>();
builder.Services.AddScoped<SubjectServices>();
builder.Services.AddScoped<CategoryServices>();
builder.Services.AddScoped<QuizServices>();
builder.Services.AddScoped<ResultQuizServices>();
builder.Services.AddScoped<ReportServices >();
builder.Services.AddScoped<CategoryStudentServices>();
builder.Services.AddControllersWithViews();
builder.Services.AddMvc()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("/{0}.cshtml");
    });
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.AddMvc();

app.Run();
