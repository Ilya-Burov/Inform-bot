using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TelegramBot.Providers
{
    public class FileSystemProvider : IFileSystemProvider
    {
        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public void WriteAllText(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}
