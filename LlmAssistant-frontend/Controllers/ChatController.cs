using Microsoft.AspNetCore.Mvc;
using LlmAssistantApi.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace LlmAssistantApi.Controllers;

[ApiController]
[Route("[controller]")]// Router /chat olur 
public class ChatController : ControllerBase
{
    private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;// APIkey gibi verileri okur

    public ChatController(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;// OpenRouter istek atar
        _config = config;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ChatRequest request)// URL mesaj buradan gönderilir
    {
        var apiKey = _config["OpenRouter:ApiKey"];

        var body = new
        {
            model = "deepseek/deepseek-r1-0528:free",
            messages = request.Messages
        };

        var json = JsonSerializer.Serialize(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        //OpenRouter APIye POST isteği atar
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var response = await _httpClient.PostAsync("https://openrouter.ai/api/v1/chat/completions", content);

        if (!response.IsSuccessStatusCode)
            return StatusCode((int)response.StatusCode, "OpenRouter API isteği başarısız");
//yanıt okuma 
        var responseText = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(responseText);
        var reply = doc.RootElement
                       .GetProperty("choices")[0]
                       .GetProperty("message")
                       .GetProperty("content")
                       .GetString();

        return Ok(new { content = reply });
    }
}
