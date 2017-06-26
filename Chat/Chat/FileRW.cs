using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Chat
{
    public class FileRW
    {
        public string ReadAll(string path)
        {
            bool written = false;
            string result = "";
            while (!written)
            {
                try
                {
                    List<string> lines = new List<string>();
                    StreamReader reader = new StreamReader(path, Encoding.Unicode);
                    result = reader.ReadToEnd();
                    reader.Close();
                    written = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}");
                    Console.WriteLine($"Reader is occupied!");
                }
            }
            return result;
        }

        public void WriteAll(string path, string message)
        {
            bool written = false;
            while (!written)
            {
                try
                {
                    StreamWriter writer = new StreamWriter(path, true, Encoding.Unicode);
                    writer.WriteLine($"[{DateTime.Now.ToString("dd.MM HH:mm:ss")}]<{UserInfo.User.Name}>: {message}");
                    writer.Close();
                    written = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}");
                    Console.WriteLine($"Writer is occupied!");
                }
            }
        }
    }
}
