using System.Collections.Generic;

namespace TelegramBot.Readers
{
    public class ChatBotStateData
    {
        public List<int> ChatIds { get; set; }

        public int UpdatesOffset { get; set; }

        public ChatBotStateData()
        {
            this.ChatIds = new List<int>();
        }
    }
}
