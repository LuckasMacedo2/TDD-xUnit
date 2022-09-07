using CursoOnline.Dominio._Base;
using CursoOnline.Ioc;
using CursoOnline.Web.Filter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

StartupIoc.ConfigureServices(builder.Services, builder.Configuration);

builder.Services.AddMvc(config => config.Filters.Add(typeof(CustomExceptionFilter)));

var app = builder.Build();


// Middleware para commitar ao final de toda requisição
app.Use(async (context, next) =>
{
    await next.Invoke();
    var unitOfWork = (IUnitOfWork)context.RequestServices.GetService(typeof(IUnitOfWork));
    await unitOfWork.Commit();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Curso}/{action=Index}/{id?}");

app.Run();
