namespace TelegramBot.Requestors
{
    public interface IWebRequestor
    {
        string GetJson(string url);
    }
}
