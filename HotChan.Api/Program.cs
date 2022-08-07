using HotChan.DataAccess.Repositories;
using HotChan.DataBase;
using HotChan.DataBase.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//Microsoft.EntityFrameworkCore.Design import is required for efcore-tools. this is probably due to the "implicitusing usings" flag.
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationManager configuration = builder.Configuration;

// For Entity Framework
//var csBuilder = new SqlConnectionStringBuilder(
//                configuration.GetConnectionString("hotchandatabase-postgres-dev"));
//csBuilder.Password = configuration["db:password"];
//string _connection = csBuilder.ConnectionString;

builder.Services.AddPooledDbContextFactory<HotChanContext>(options => options.UseNpgsql("Host=localhost;Port=5432;User Id=postgres;Password=p1ckler1ck;Database=HotChanData-dev;Pooling=true;"));
builder.Services.AddScoped<HotChanContext>();
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly()); builder.Services.AddScoped<PostRepostiry>();

// wait for .NET 7.0
//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseCors();

app.MapGet("/post/{postId}", async (Guid postId, [FromServices]PostRepostiry postRepo) =>
{
    if(postId == Guid.Empty)
        return Results.BadRequest();

    var post = await postRepo.GetPostById(postId);
    return post != null ? Results.Ok(post) : Results.NotFound();
});

app.MapGet("/post/catalog", async ([FromServices] PostRepostiry postRepo) =>
{
    var post = await postRepo.GetPostsforCatalog();
    return Results.Ok(post);
});

app.MapPost("/post/new", async ([FromServices]PostRepostiry postRepo, HttpRequest request) =>
{
    var newPost = await request.ReadFromJsonAsync<PostDialogueDto>();
    if (newPost == null)
        return Results.BadRequest();

    var result = await postRepo.CreatePost(newPost);

    return Results.Ok(result);
});

app.Run();