using UkraineWarChronicles_Bot.Model;
using Newtonsoft.Json;
using System.Net.Http;

namespace UkraineWarChronicles_Bot.Clients
{
    public class NewsClient
    {
        public async Task<NewsOfDay> GetNewsByDayAsync(string Day)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://extract-news.p.rapidapi.com/v0/article?url=https%3A%2F%2Fnovynarnia.com%2Fhronika-oborony-ukrayiny-den-{Day}"),
                Headers =
                {
                 { "X-RapidAPI-Key", "cb4ca4fab2msha46f4c173b3d8edp16c072jsn288ade238ef8" },
                 { "X-RapidAPI-Host", "extract-news.p.rapidapi.com" },
                 },
            };
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();            

            NewsOfDay News = JsonConvert.DeserializeObject<NewsOfDay>(body);            

            return News;
        }
    }
}
