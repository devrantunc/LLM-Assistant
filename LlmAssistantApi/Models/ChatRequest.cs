using System.Collections.Generic;
//React forntend'den gelen mesajlar listesini tutar
namespace LlmAssistantApi.Models;

public class ChatRequest
{
    public List<ChatMessage> Messages { get; set; } = new();
}
