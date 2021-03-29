using System.Text;
using TelegramBot.Dto.TelegramApi;
using TelegramBot.Publishers;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Xml;
using System.Collections.Generic;

namespace TelegramBot
{
    
    class Cbr : IBotAnswer
    {
        private TelegramChatMessagePublisher publish;
        private string substr1;
        XmlDocument xDoc = new XmlDocument();

        string rgx1 = "/EconomicsCbr";

        public bool CanWork(string message)
        {
            if (message == rgx1)
            {
                return true;
            }
            return false;

        }
        public void Answer(Message msg)
        {

            try
            {
                List<Items> items = new List<Items>();
                xDoc.Load("http://www.cbr.ru/rss/RssNews");
                XmlElement xRoot = xDoc.DocumentElement;

                foreach (XmlNode xnode in xRoot)
                {
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        Items item = new Items();
                        if (childnode.Name == "title")
                            item.Title = childnode.InnerText;

                        if (childnode.Name == "description")
                            item.Description = childnode.InnerText;

                        if (childnode.Name == "item")
                        {
                            foreach (XmlNode childnode2 in childnode)
                            {
                                if (childnode2.Name == "title")
                                    item.Title = childnode2.InnerText;

                                if (childnode2.Name == "description")
                                    item.Description = childnode2.InnerText;

                                if (childnode2.Name == "link")
                                    item.Link = childnode2.InnerText;
                            }
                            items.Add(item);
                        }
                    }
                }
                for (int i = 0; i < 8; ++i)
                    publish.SendMessage($"\nОписание:  {items[i].Title}\nСсылка на статью: {items[i].Link}", msg.chat.id);

            }
            catch
            {
                publish.SendMessage("Ошибка во вводе", msg.chat.id);
            }

        }
        public Cbr(TelegramChatMessagePublisher publish)
        {
            this.publish = publish;


        }
    }
}
