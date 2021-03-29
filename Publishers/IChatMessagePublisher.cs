namespace TelegramBot.Publishers
{
    public interface IChatMessagePublisher
    {
        void SendMessage(string messageText, int chatId);
    }
}
