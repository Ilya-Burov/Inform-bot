using TelegramBot.Dto.TelegramApi;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TelegramBot.Readers
{
    public class TelegramChatUpdatesPollingWatcher : IChatUpdatesWatcher<Message>
    {
        private readonly IChatUpdatesReader<TelegramApiResponse> chatUpdatesReader;
        private readonly IChatBotState chatBotState;

        public event Action<Message> OnMessageArrived;

        public TelegramChatUpdatesPollingWatcher(IChatUpdatesReader<TelegramApiResponse> chatUpdatesReader, IChatBotState chatBotState)
        {
            this.chatUpdatesReader = chatUpdatesReader;
            this.chatBotState = chatBotState;
        }

        public void StartWatch(CancellationToken cancellationToken)
        {
            Task.Run(async() => 
            {
                while (true)
                {
                    await Task.Delay(1500);

                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    var updates = this.chatUpdatesReader.GetUpdates(this.chatBotState.Data.UpdatesOffset);

                    if (updates.result == null || !updates.result.Any())
                    {
                        continue;
                    }

                    var newMessages = updates.result.Select(s => s.message);

                    this.chatBotState.Data.UpdatesOffset = updates.result.Max(x => x.update_id);

                    foreach (var message in newMessages)
                    {
                        bool adding = true;

                        if(message != null)
                        {
                            foreach (var id in chatBotState.Data.ChatIds)
                            {
                                if (id == message.chat.id)
                                    adding = false;

                            }

                            if (adding)
                            {
                                this.chatBotState.SaveIDInformation(message.chat.id);
                            }
                            this.OnMessageArrived?.Invoke(message);
                        }
                        else Console.WriteLine("Бот остановлен!");
                    }

                    this.chatBotState.Data.UpdatesOffset++;
                }
            }, cancellationToken);
          
        }
    }
}
