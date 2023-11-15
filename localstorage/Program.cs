using System.Net.Http.Headers;
using System.Text.Json;
using localstorage.ApiModels;

HttpClient client = new HttpClient();

await ProcessRepositories(client);

static async Task ProcessRepositories(HttpClient client)
{
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    client.DefaultRequestHeaders.Add("User-Agent", ".NET Console");

    var response = await client.GetAsync("https://localhost:7211/api/Genre");
    
    if (response.Headers.Contains("Cache-Control"))
    {
        var cachedtime = response.Headers.CacheControl.MaxAge;
        
        var genreData = response.Content.ReadAsStringAsync().Result;
        
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var genres = JsonSerializer.Deserialize<List<GenreApiModel>>(genreData, options);

        foreach (var genre in genres!)
        {
            // If in a browser client app save data to localstorage
            Console.WriteLine(genre.Name);
        }
    }
    else
    {
        //the header is missing
        Console.WriteLine("the header is missing");
    }
}