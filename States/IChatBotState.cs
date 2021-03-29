using System.Collections.Generic;

namespace TelegramBot.Readers
{
    public interface IChatBotState
    {
        ChatBotStateData Data { get; }

        void LoadState();

        void SaveState();
        void SaveIDInformation(int id);
    }
}
