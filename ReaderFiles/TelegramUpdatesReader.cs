using TelegramBot.Configuration;
using TelegramBot.Dto.TelegramApi;
using TelegramBot.Extensions;
using TelegramBot.Requestors;
using System.Collections;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TelegramBot.Readers
{ 
    public class TelegramUpdatesReader : IChatUpdatesReader<TelegramApiResponse>
    {
        private readonly TelegramBotSettings botSettings;
        private readonly IWebRequestor webRequestor;

        public TelegramUpdatesReader(
            TelegramBotSettings botSettings, 
            IWebRequestor webRequestor)
        {
            this.botSettings = botSettings;
            this.webRequestor = webRequestor;
        }

        public TelegramApiResponse GetUpdates(int offset)
        {           
            string url = $"{this.botSettings.GetBotApiEndpoint()}/getUpdates?offset={offset}";

            var dataAsString = webRequestor.GetJson(url);

            var data = JsonSerializer.Deserialize<TelegramApiResponse>(dataAsString);

            return data;
        }
    }
}
