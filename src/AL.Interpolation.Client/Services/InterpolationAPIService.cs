using AL.Interpolation.Client.DTO;
using AL.Interpolation.Client.Settings;
using Newtonsoft.Json;
using System.Text;

namespace AL.Interpolation.Client.Services
{
    public class InterpolationAPIService
    {
        private readonly string guid = Guid.NewGuid().ToString();
        private readonly HttpClient _httpClient;
        private readonly ServerSettings _serverSettings;
        private readonly ILogger<InterpolationAPIService> _logger;

        public InterpolationAPIService(
            HttpClient httpClient,
            ServerSettings serverSettings,
            ILogger<InterpolationAPIService> logger)
        {
            _httpClient = httpClient;
            _serverSettings = serverSettings;
            _logger = logger;
        }

        public async Task<string> PostBSpline(BSplineParametersRequest request)
        {

            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"{_serverSettings.Url}/interpolation?authToken=1234567890";
            var result = string.Empty;

            HttpResponseMessage? response = null;
            try
            {
                response = await _httpClient.PostAsync(url, data);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation($"Задача успешно зарегистрирована : {result}");
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    _logger.LogError($"Ошибка при отправке задачи : {response?.StatusCode}");
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка при отправке задачи : {ex.Message}");
                return string.Empty;
            }
        }

        public Task<string> GetRequestsStatuses()
        {
            return null;
        }
    }
}