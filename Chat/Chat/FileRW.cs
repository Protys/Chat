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
            string result = "";
            try
            {
                List<string> lines = new List<string>();
                StreamReader reader = new StreamReader(path, Encoding.Unicode);
                while (!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine());
                }
                reader.Close();

                for (int i = lines.Count - 1; i >= 0; i--)
                {
                    result += $"{lines[i]}\n\n";
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
                Console.WriteLine($"Reader is occupied!");
            }
            return result;
        }

        public void WriteAll(string path, string message)
        {
            try
            {
                StreamWriter writer = new StreamWriter(path, true, Encoding.Unicode);
                writer.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}]<{UserInfo.User.Name}> {message}");
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
                Console.WriteLine($"Writer is occupied!");
            }
        }
    }
}
