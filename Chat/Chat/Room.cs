using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Chat
{

    public delegate void FileReadedHandler(string result, string name);

    public class Room
    {

        public event FileReadedHandler FileReaded;

        public string Name { get; set; }

        private FileInfo info { get; set; }

        private FileSystemWatcher watcher { get; set; }

        private FileRW _file { get; set; }

        public Room(FileInfo _info, string name)
        {
            this.Name = name;
            this.info = _info;
            _file = new FileRW();
            watcher = new FileSystemWatcher(info.DirectoryName, info.Name);
            watcher.EnableRaisingEvents = true;
            watcher.Changed += Watcher_Changed;
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs args)
        {
            this.Read();
        }

        public void Read()
        {
            FileReaded(_file.ReadAll(info.FullName), info.Name);
        }

        public void Write(string message)
        {
            _file.WriteAll(info.FullName, message);
        }

        public override string ToString()
        {
            return this.Name;
        }

        public void SetWatcherState(bool state)
        {
            watcher.EnableRaisingEvents = state;
        }

        public bool GetWatcherState()
        {
            return watcher.EnableRaisingEvents;
        }
    }
}
