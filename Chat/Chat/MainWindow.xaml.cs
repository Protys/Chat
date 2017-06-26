using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace Chat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private Core _chat { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this._chat = new Core(GetText);
            this._chat.ListBoxAdd += _chat_ListBoxAdd;
            this._chat.ListBoxRemove += _chat_ListBoxRemove;
            UserInfo.Configure(this.txb_username.Text);
        }

        private void _chat_ListBoxRemove(Room room)
        {
            Dispatcher.Invoke(delegate () {
                this.lxb_rooms.Items.Remove(room);
            });
        }

        private void _chat_ListBoxAdd(Room room)
        {
            Dispatcher.Invoke(delegate () {
                this.lxb_rooms.Items.Add(room);
            });
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this._chat.Start();
            this.txb_readMessages.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
        }

        private void GetText(string result, string name)
        {
            if (name == this._chat.GetActiveName())
            {
                Dispatcher.Invoke(delegate ()
                {
                    this.txb_readMessages.Text = result;
                    this.txb_readMessages.ScrollToEnd();
                });
            }
        }

        private void lxb_rooms_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lxb_rooms.SelectedIndex >= 0)
            {
                this._chat.SelectActive(lxb_rooms.SelectedIndex);
                this.lbl_selected.Content = this.lxb_rooms.SelectedItem.ToString();
            }
        }

        private void btn_send_Click(object sender, RoutedEventArgs e)
        {
            if (this.txb_send.Text.Length > 0 && this._chat.HasActive())
            {
                this._chat.Send(this.txb_send.Text);
                this.txb_send.Text = "";
            }
        }

        private void txb_username_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserInfo.Configure(txb_username.Text.Length > 0 ? txb_username.Text : "Jsem retard bez jména");
        }

        private void txb_send_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.txb_send.Text.Length > 0 && e.Key == Key.Enter && this._chat.HasActive())
            {
                this._chat.Send(this.txb_send.Text);
                this.txb_send.Text = "";
            }
        }
    }
}
