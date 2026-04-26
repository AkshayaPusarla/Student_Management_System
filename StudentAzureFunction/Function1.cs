using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

public static class GetStudentsTimer
{
    private static readonly HttpClient client = new HttpClient();

    [FunctionName("GetStudentsTimer")]
    public static async Task Run(
        [TimerTrigger("0 */1 * * * *")] TimerInfo myTimer,
        ILogger log)
    {
        log.LogInformation($"Function executed at: {DateTime.Now}");

        try
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "https://localhost:44308/api/student"
            );

            // 🔐 ADD YOUR TOKEN HERE
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", "PASTE_YOUR_TOKEN_HERE");

            var response = await client.SendAsync(request);

            var result = await response.Content.ReadAsStringAsync();

            log.LogInformation("Students Data:");
            log.LogInformation(result);
        }
        catch (Exception ex)
        {
            log.LogError($"Error: {ex.Message}");
        }
    }
}