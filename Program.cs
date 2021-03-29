using TelegramBot.Configuration;
using TelegramBot.Providers;
using TelegramBot.Publishers;
using TelegramBot.Readers;
using TelegramBot.Requestors;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;


namespace TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true);
                

            var configuration = builder.Build();

            var webClient = new WebClient();
            
            var fileProvider = new FileSystemProvider();
            var telegramBotSettings = configuration.GetSection("TelegramBotSettings").Get<TelegramBotSettings>();

            var webRequestor = new WebRequestor(webClient);
            var chatUpdateReader = new TelegramUpdatesReader(telegramBotSettings, webRequestor);
            var publishBotReaction = new TelegramChatMessagePublisher(telegramBotSettings, webRequestor);
            var chatBotState = new ChatBotState(fileProvider);
            chatBotState.LoadState();

            var pollingWatcher = new TelegramChatUpdatesPollingWatcher(chatUpdateReader, chatBotState);

            var cancellationTokenSource = new CancellationTokenSource();
            
           
            var hellow = new HellowAnswer(publishBotReaction);
                    var Say = new SayUserInformation(publishBotReaction);
            var weather = new WeatherAnswer(publishBotReaction);
            var vedomosti = new Vedomosti(publishBotReaction);
            var dw = new EconomicsDw(publishBotReaction);
            var cbr = new Cbr(publishBotReaction);
            var news = new NewsRu(publishBotReaction);
            var prime = new PrimeRu(publishBotReaction);
            var sport = new Sport(publishBotReaction);
            var utkin = new Utkin(publishBotReaction);
            var nike = new Nike(publishBotReaction);
            var legislat = new Legislation(publishBotReaction);
            var reebok = new Reebok(publishBotReaction);
            var newbalance = new Newbalance(publishBotReaction);
            var answers = new List<IBotAnswer>()
                    { hellow, Say, weather, vedomosti, dw, cbr, news, prime, sport, utkin, nike, legislat, reebok, newbalance };
            var asker = new Asker(answers);
            


            pollingWatcher.OnMessageArrived += (msg) => 
            {
                Console.WriteLine($"{msg.text} from {msg.from.username} with id { msg.chat.id} {msg.from.id} "  );
               
                    if (msg.text== "/start") 
                    publishBotReaction.SendMessage("Приветствую вас! Я  готов к работе. Команды бота:\n1) Скажи погоду: [место].\n2)Скажи информацию про состояние атмосферы в городе: [название города].\n3)Сколько времени в городе: [название города].\nСобытия в мире экономики по версии:\n4)Газета ведомости -\n/EconomicsVedomosti.\n5)Cайт dw.com\n/EconomicsDw\n6)Сайт ЦБ РФ-\n/EconomicsCbr.\n7)Сайт news.ru-\n/EconomicsNewsRu.\n8)Сайт 1prime.ru-\n/EconomicsPrimeRu.\nНОВОСТИ СПОРТА:\n9)Cайт sports.ru-\n/SportsNews\n10)Блог Уткина-\n/UtkinSportsNews\nНОВЫЕ ЗАКОНЫ:\n11)Сайт с обновлениями законодательства РФ-\n/LegislationChanges.\nНОВОСТИ БРЕНДОВ ОДЕЖДЫ:\n12)Nike-\n/NikenewsFeed\n13)Reebok-\n/ReebokNewsGlobal\n14)NewBalance-\n/NewBalanceNewsMarket.", msg.chat.id);
                    

                    
                    
                    
                   
                    
                

                   TimeAnswer response = new TimeAnswer(publishBotReaction, msg);
                    response.CanWork(msg.text);
                    if (response.CanWork(msg.text) == true)
                        response.Answer();
                    asker.Ask(msg.text, msg);
            };

            pollingWatcher.StartWatch(cancellationTokenSource.Token);

            var stop =Console.ReadLine();
            if (stop== "stop bot")
            {
                Console.WriteLine("Работа Бота остановлена. Сохранение.");
                cancellationTokenSource.Cancel();

                chatBotState.SaveState();

            }

            


        }

        

        
    }
}
