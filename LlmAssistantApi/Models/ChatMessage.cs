namespace LlmAssistantApi.Models;
//OpenRouter API'sine gönderilecek veya ondan gelecek olan tek bir mesajı temsil eder
public class ChatMessage
{
    public string Role { get; set; } = "";
    public string Content { get; set; } = "";
}
