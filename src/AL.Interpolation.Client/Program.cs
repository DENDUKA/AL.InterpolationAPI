using AL.Interpolation.Client.Services;
using AL.Interpolation.Client.Settings;

namespace AL.Interpolation.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var serverConnectionSettings =
                builder.Configuration.GetSection("Docker:Endpoint")
                .Get<ServerSettings>();

            builder.Services.AddAuthorization();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHostedService<DataGeneratorHostedService>();
            builder.Services.AddHttpClient();

            builder.Services.AddSingleton<DataGeneratorService>();

            builder.Services.AddSingleton(sp => new InterpolationAPIService(
                sp.GetRequiredService<HttpClient>(),
                serverConnectionSettings,
                sp.GetRequiredService<ILogger<InterpolationAPIService>>()));

            builder.Services.AddSingleton<InterpolationCenterService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.Run();
        }
    }
}