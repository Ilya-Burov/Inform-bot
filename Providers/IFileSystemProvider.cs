namespace TelegramBot.Providers
{
    public interface IFileSystemProvider
    {
        string ReadAllText(string path);
        void WriteAllText(string path, string content);
    }
}
