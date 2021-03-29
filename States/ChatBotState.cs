using TelegramBot.Providers;
using System;
using System.Text.Json;

namespace TelegramBot.Readers
{
    public class ChatBotState : IChatBotState
    {
        private readonly IFileSystemProvider fileSystemProvider;
        private const string stateFileName = "chatBotState.json";

         private ChatBotStateData _informdata;
        public ChatBotStateData Data
        {
            get
            {
                return _informdata;
            }
        }

        public ChatBotState(IFileSystemProvider fileSystemProvider)
        {
            this.fileSystemProvider = fileSystemProvider;
        }
        
         
        public void LoadState()
        {
            try
            {
                this._informdata = JsonSerializer.Deserialize<ChatBotStateData>(fileSystemProvider.ReadAllText(stateFileName));
            }
            catch(Exception ex)
            {
                this._informdata = new ChatBotStateData();
                Console.WriteLine(ex);
            }
        }
       
        public void SaveState()
        {
            fileSystemProvider.WriteAllText(stateFileName, JsonSerializer.Serialize(this.Data));
        }
        public void SaveIDInformation(int id)
        {
            this.Data.ChatIds.Add(id); 
        } 
    }
}
