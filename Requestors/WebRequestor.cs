using System.Net;

namespace TelegramBot.Requestors
{
    public class WebRequestor : IWebRequestor
    {
        private readonly WebClient webClient;

        public WebRequestor(WebClient webClient)
        {
            this.webClient = webClient;
        }

        public string GetJson(string url)
        {
            return webClient.DownloadString(url);
        }
    }
}
