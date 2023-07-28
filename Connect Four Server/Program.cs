using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Connect_Four_Server.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<Connect_Four_ServerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connect_Four_ServerContext") ?? throw new InvalidOperationException("Connection string 'Connect_Four_ServerContext' not found.")));

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
app.MapControllers();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
