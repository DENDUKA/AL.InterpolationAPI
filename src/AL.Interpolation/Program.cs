using AL.Interpolation.Domain.Interfaces;
using AL.Interpolation.Domain.Services;

namespace AL.Interpolation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

            builder.Services.AddScoped<IInterpolationService, InterpolationService>();

            var app = builder.Build();

            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();            

            app.Run();
        }
    }
}