using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBot
{
    interface ITimeAnswer
    {
        bool CanWork(string time);
        void Answer();
    }
}
