using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{

    public delegate void ListBoxHandler(Room room);

    public class Core
    {

        public event ListBoxHandler ListBoxAdd;

        public event ListBoxHandler ListBoxRemove;

        private FileReadedHandler callback;

        private FileSystemWatcher _dirWatcher;

        private Room _active { get; set; }

        public List<Room> rooms { get; set; }

        private DirectoryInfo _dir { get; set; }

        public Core(FileReadedHandler r)
        {
            callback = r;
        }

        public void Start()
        {
            //string path = @"C:\Users\lukyh\Desktop\Chat Room";
            string path = Directory.GetCurrentDirectory();
            _dir = new DirectoryInfo(path);

            _dirWatcher = new FileSystemWatcher(path, "room_*.txt");
            _dirWatcher.EnableRaisingEvents = true;
            _dirWatcher.Created += _dirWatcher_Created;
            _dirWatcher.Deleted += _dirWatcher_Deleted;


            rooms = new List<Room>();

            Dir(_dir.GetFiles("room_*.txt"));
        }

        private void _dirWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            Remove(rooms.Where<Room>((x) => x.Name == e.Name));
        }

        private void Remove(IEnumerable<Room> col)
        {
            List<Room> removal = new List<Room>();
            foreach (Room room in col)
            {
                ListBoxRemove(room);
                removal.Add(room);
            }
            foreach (Room item in removal)
            {
                rooms.Remove(item);
            }
        }

        private void _dirWatcher_Created(object sender, FileSystemEventArgs e)
        {
            Dir(_dir.GetFiles("room*.txt").Where<FileInfo>((x) => x.Name == e.Name));
        }

        private void Dir(IEnumerable<object> col)
        {
            foreach (FileInfo item in col)
            {
                Room room = new Room(item, item.Name);
                room.FileReaded += callback;
                rooms.Add(room);
                ListBoxAdd(room);
            }
        }

        public void SelectActive(int index)
        {
            if (this._active != null)
            {
                this._active.SetWatcherState(false);
            }
            this._active = rooms[index];
            this._active.SetWatcherState(true);
            this._active.Read();
        }

        public void Send(string text)
        {
            if (this._active != null)
                this._active.Write(text);
        }

        public bool HasActive()
        {
            return this._active != null;
        }

        public string GetActiveName()
        {
            return this._active.Name;
        }
    }
}
