using System;
using System.Threading;

namespace TelegramBot.Readers
{
    public interface IChatUpdatesWatcher<T> where T: class
    {
        event Action<T> OnMessageArrived;

        void StartWatch(CancellationToken cancellationToken);
    }
}
