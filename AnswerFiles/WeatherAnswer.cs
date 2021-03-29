using System;
using TelegramBot.Publishers;
using TelegramBot.Dto.TelegramApi;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace TelegramBot
{
    class WeatherAnswer : IBotAnswer
    {
        private TelegramChatMessagePublisher publish;
        private string substr;
        
        string rgx = @"Скажи погоду:(\w*)";

        public bool CanWork(string message)
        {

            


            if (Regex.IsMatch(message, rgx))
                {
                    return true;
                }
                return false;
            
            

        }
        public void Answer(Message message)
        {

            string city = "Rome";
            try
            {
                substr = message.text.Substring(14);
                city = substr;
                string url = ($"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid=01919e822c17cd48130238904353d786");
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                string response;

                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }


                WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);
                float curweatherweatherfloat = weatherResponse.Main.Temp;
                string curweatherstr = curweatherweatherfloat.ToString();
                float curweatherweather1float = weatherResponse.Main.Feels_like;
                string curweatherstr1 = curweatherweather1float.ToString();
                int humidity = weatherResponse.Main.Humidity;
                string humiditystr = humidity.ToString();
                float curwindfloat = weatherResponse.Wind.Speed;
                string curwindstr = curwindfloat.ToString();
                int pressure = weatherResponse.Main.Pressure;
                int pressure1 = (int)(pressure / 1.333);
                string pressurestr = pressure1.ToString();
                int clouds = weatherResponse.Clouds.All;
                string cloudsstr = clouds.ToString();
                int visibility = weatherResponse.Visibility;
                string visibilitystr = visibility.ToString();
















                publish.SendMessage($"Здравствуйте!\nТемпература в городе {curweatherstr} °C.\nТемпература ощущается как {curweatherstr1} °C.\nВлажность {humiditystr} %.\nДавление {pressurestr} мм рт. ст.\nСкорость ветра {curwindstr} м/c.\nОблачность {cloudsstr} %.\nВидимость {visibilitystr} метров.", message.chat.id);
            }
            catch
            {
                publish.SendMessage("Неверный формат запроса о погоде в данном городе или ошибка в названии города", message.chat.id);
            }

        }
        public WeatherAnswer(TelegramChatMessagePublisher publish)
        {
            this.publish = publish;


        }
    }
}

