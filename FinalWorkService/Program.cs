
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace FinalWorkService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("ocelot.json").Build();

            builder.Services.AddOcelot(configuration);
            builder.Services.AddSwaggerForOcelot(configuration);


            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerForOcelotUI(opt =>
            {
                opt.PathToSwaggerGenerator = "/swagger/docs";
            }).UseOcelot().Wait();

            app.UseHttpsRedirection();



            app.Run();
        }
    }
}
