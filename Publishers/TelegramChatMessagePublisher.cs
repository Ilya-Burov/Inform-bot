using TelegramBot.Configuration;
using TelegramBot.Extensions;
using TelegramBot.Requestors;

namespace TelegramBot.Publishers
{
    public class TelegramChatMessagePublisher : IChatMessagePublisher
    {
        private readonly TelegramBotSettings botSettings;

        private readonly IWebRequestor webRequestor;

        public TelegramChatMessagePublisher(
            TelegramBotSettings botSettings, 
            IWebRequestor webRequestor)
        {
            this.botSettings = botSettings;
            this.webRequestor = webRequestor;
        }

        public void SendMessage(string messageText, int chatId)
        {
            webRequestor.GetJson($"{this.botSettings.GetBotApiEndpoint()}/sendMessage?chat_id={chatId}&text={messageText}");
        }
    }
}
