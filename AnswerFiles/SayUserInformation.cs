using System;
using System.Collections.Generic;
using System.Text;
using TelegramBot.Dto.TelegramApi;
using TelegramBot.Publishers;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace TelegramBot
{
    class SayUserInformation : IBotAnswer
    {
        private TelegramChatMessagePublisher publish;
        private string substr1;

        string rgx1 = @"Скажи информацию про состояние атмосферы в городе:(\w*)";

        public bool CanWork(string message)
        {
            if (Regex.IsMatch(message, rgx1))
            {
                return true;
            }
            return false;

        }
        public void Answer(Message msg)
        {
            string city1 = "Rome";
            try
            {
                substr1 = msg.text.Substring(51);
                city1 = substr1;
                string url1 = ($"https://api.openweathermap.org/data/2.5/weather?q={city1}&units=metric&appid=01919e822c17cd48130238904353d786");
                HttpWebRequest httpWebRequest1 = (HttpWebRequest)WebRequest.Create(url1);

                HttpWebResponse httpWebResponse1 = (HttpWebResponse)httpWebRequest1.GetResponse();

                string response1;

                using (StreamReader streamReader1 = new StreamReader(httpWebResponse1.GetResponseStream()))
                {
                    response1 = streamReader1.ReadToEnd();
                }


                WeatherResponse weatherResponse1 = JsonConvert.DeserializeObject<WeatherResponse>(response1);
                float lon = weatherResponse1.Coord.Lon;
                float lat = weatherResponse1.Coord.Lat;
                string url2= ($"http://api.openweathermap.org/data/2.5/air_pollution?lat={lat}&lon={lon}&appid=01919e822c17cd48130238904353d786");
                HttpWebRequest httpWebRequest2 = (HttpWebRequest)WebRequest.Create(url2);

                HttpWebResponse httpWebResponse2 = (HttpWebResponse)httpWebRequest2.GetResponse();

                string response2;

                using (StreamReader streamReader2 = new StreamReader(httpWebResponse2.GetResponseStream()))
                {
                    response2 = streamReader2.ReadToEnd();
                }


                PollutionResponse1 weatherResponse2 = JsonConvert.DeserializeObject<PollutionResponse1>(response2);
                int quality = weatherResponse2.List[0].Main.Aqi;
                float cofloat = weatherResponse2.List[0].Components.Co;
                string costr = cofloat.ToString();
                float nofloat = weatherResponse2.List[0].Components.No;
                string nostr = nofloat.ToString();
                float no2float = weatherResponse2.List[0].Components.No2;
                string no2str = no2float.ToString();
                float o3float = weatherResponse2.List[0].Components.O3;
                string o3str = o3float.ToString();

                string qualitystr = quality.ToString();




                publish.SendMessage($"Здравствуйте!\nСостояние атмосферы {qualitystr} по пятибальной шкале\n(5-отлично, 4-хороший и чистый воздух, 3-средний, 2-плохой, 1-очень плохой воздух).\nСодержание CO {costr} мкг/м3.\nСодержание NO {nostr} мкг/м3.\nСодержание NO2 {no2str} мкг/м3.\nСодержание O3 {o3str} мкг/м3.", msg.chat.id);
            }
            catch
            {
                publish.SendMessage("Неверный формат запроса о погоде в данном городе или ошибка в названии города", msg.chat.id);
            }

        }
        public SayUserInformation(TelegramChatMessagePublisher publish)
        {
            this.publish = publish;
            

        }
    }
}
