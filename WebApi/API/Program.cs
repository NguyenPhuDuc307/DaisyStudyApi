using Application.Categories;
using Application.Comments;
using Application.Contacts;
using Application.Courses;
using Application.FileStorages;
using Application.Lessons;
using Application.News;
using Application.Ratings;
using Application.Users;
using DaisyStudy.Utilities.Constants;
using Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DaisyStudyDbContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString(SystemConstants.MainConnectionString) 
            ?? throw new InvalidOperationException("Connection string 'DaisyStudyDbContext' not found."),
        providerOptions => providerOptions.EnableRetryOnFailure()));

// Declare DI
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IContactService, ContactService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<ILessonService, LessonService>();
builder.Services.AddTransient<IRatingService, RatingService>();
builder.Services.AddTransient<INewService, NewService>();
builder.Services.AddTransient<IFileStorageService, FileStorageService>();

builder.Services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger DaisyStudy", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization(); // Add it here

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger DaisyStudy V1");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

