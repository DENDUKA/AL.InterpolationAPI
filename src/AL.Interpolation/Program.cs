using AL.Interpolation.API.DAL.Repositories.Interfaces;
using AL.Interpolation.API.DAL.Repositories.Redis;
using AL.Interpolation.API.DTO.Validators;
using AL.Interpolation.API.Repositories.Redis;
using AL.Interpolation.API.Servises;
using AL.Interpolation.Domain.Interfaces;
using AL.Interpolation.Domain.Services;
using FluentValidation;

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

            builder.Services.AddValidatorsFromAssemblyContaining<BSplineParametersResponceValidator>();

            builder.Services.AddScoped<IInterpolationTaskRegistrationService, InterpolationTaskRegistrationService>();

            builder.Services.AddRedis(builder.Configuration);

            builder.Services.AddSingleton<IInterpolationRequestsRepository, RedisIntRequestsRepository>();

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