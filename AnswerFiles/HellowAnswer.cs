using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using TelegramBot.Dto.TelegramApi;
using TelegramBot.Publishers;

namespace TelegramBot
{
    class HellowAnswer : IBotAnswer
    {
        private TelegramChatMessagePublisher publish;
        private string substr3;
        string rgx3 = @"Сколько времени в городе:(\w*)";

        public bool CanWork(string message)
        {
            if (Regex.IsMatch(message, rgx3))
            {
                return true;
            }
            return false;

        }
        public void Answer(Message message)
        {
            string city3 = "Rome";
            try
            {
                substr3 = message.text.Substring(26);
                city3 = substr3;
                string url1 = ($"https://api.openweathermap.org/data/2.5/weather?q={city3}&units=metric&appid=01919e822c17cd48130238904353d786");
                HttpWebRequest httpWebRequest3 = (HttpWebRequest)WebRequest.Create(url1);

                HttpWebResponse httpWebResponse3 = (HttpWebResponse)httpWebRequest3.GetResponse();

                string response3;

                using (StreamReader streamReader3 = new StreamReader(httpWebResponse3.GetResponseStream()))
                {
                    response3 = streamReader3.ReadToEnd();
                }


                WeatherResponse weatherResponse3 = JsonConvert.DeserializeObject<WeatherResponse>(response3);
                float timezone = weatherResponse3.Timezone;
                float timezone1 = timezone / 3600;
                var timezone2 = (double)timezone1;
                string timezone1str = timezone1.ToString();
               

                DateTime time = DateTime.UtcNow;
                var time2 = time.AddHours(timezone2);
                string asString = time2.ToString();
                string asString2 = time2.ToString("dd.MM.yyyy, hh:mm:ss tt", CultureInfo.InvariantCulture);



                publish.SendMessage(asString, message.chat.id);
                publish.SendMessage(asString2, message.chat.id);
            }
            catch
            {
                publish.SendMessage("Неверный формат запроса о погоде в данном городе или ошибка в названии города", message.chat.id);

            }





        }
        public HellowAnswer(TelegramChatMessagePublisher publish)
        {
            this.publish = publish;
           

        }
    }
}
