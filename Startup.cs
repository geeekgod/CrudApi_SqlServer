using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using SqlServerRestApi.Jobs;
public class Startup
{
    public IConfiguration configRoot
    {
        get;
    }
    public Startup(IConfiguration configuration)
    {
        configRoot = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddHangfire(x => x.UseSqlServerStorage("Data Source=laptop-o1h43sif;Initial Catalog=SqlServerRestApi;Integrated Security=True"));
        services.AddHangfireServer();
    }
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHangfireDashboard();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapRazorPages();
        app.MapControllers();

        CronJob cronJob = new CronJob();
        Console.WriteLine("1");
        cronJob.TestJob();
        Console.WriteLine("2");

        app.Run();
    }
}