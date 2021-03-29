namespace TelegramBot.Readers
{
    public interface IChatUpdatesReader<T> where T: class
    {
        T GetUpdates(int offset);
    }
}
