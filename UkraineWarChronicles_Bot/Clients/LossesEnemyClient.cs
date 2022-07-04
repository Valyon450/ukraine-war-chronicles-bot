using UkraineWarChronicles_Bot.Model;
using Newtonsoft.Json;
using System.Net.Http;

namespace UkraineWarChronicles_Bot.Clients
{
    public class LossesEnemyClient
    {
        public async Task<VideoOfDay> GetLossesEnemyByDayAsync(string Day)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://ukraine-war-chronicles-appi.herokuapp.com/LossesEnemyOfDay?Day={Day}"),
                Headers =
                {
                    { "X-RapidAPI-Key", "cb4ca4fab2msha46f4c173b3d8edp16c072jsn288ade238ef8" },
                    { "X-RapidAPI-Host", "youtube138.p.rapidapi.com" },
                },
            };

            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();

            VideoOfDay Losses = JsonConvert.DeserializeObject<VideoOfDay>(body);

            return Losses;
        }
    }
}
