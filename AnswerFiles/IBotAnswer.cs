using TelegramBot.Dto.TelegramApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBot
{
    public interface IBotAnswer
    {
        bool CanWork(string time);
        void Answer(Message msg);

    }
    
    

}
