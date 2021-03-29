using TelegramBot.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBot.Extensions
{
    public static class TelegramBotSettingsExtensions
    {
        public static string GetBotApiEndpoint(this TelegramBotSettings botSettings)
        {
            return string.Format(botSettings.BotApiUrl, botSettings.Token);
        }
    }
}
