using System;
using TelegramBot.Publishers;
using TelegramBot.Dto;
using TelegramBot.Dto.TelegramApi;
using System.Net;
using System.IO;

namespace TelegramBot
{
    class TimeAnswer: ITimeAnswer
    {
        private TelegramChatMessagePublisher publish;
        private Message message;
        private string substr2;

        string rgx2 = @"Скажи информацию про состояние атмосферы в городе:(\w*)";

        public bool CanWork(string time)
        {
            if(time=="Сколько времени?")
            {
                return true;
            }
            return false;

        }
        public void Answer()
        {
            
                DateTime time = DateTime.UtcNow;
                string asString = time.ToString();


                publish.SendMessage(asString, this.message.chat.id);
            
        }
        public TimeAnswer(TelegramChatMessagePublisher publish, Message msg)
        {
            this.publish = publish;
            this.message = msg;

        }
    }
}
