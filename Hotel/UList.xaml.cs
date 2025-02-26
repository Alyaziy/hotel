using Hotel.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using static System.Reflection.Metadata.BlobBuilder;

namespace Hotel
{
    /// <summary>
    /// Логика взаимодействия для UList.xaml
    /// </summary>
    public partial class UList : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public List<User> Users { get; set; }
        public User SelectedUser { get; set; }

        public UList()
        {
            InitializeComponent();
            LoadUser();
            DataContext = this;
        }

        public void LoadUser()
        {
            var result = DB.DataBase.GetInstance().Users.Include(u => u.Role).ToList();
            Users = result;
            Signal(nameof(Users));
        }

        private void Button_BookList(object sender, RoutedEventArgs e)
        {
            MainHotel mainHotel = new MainHotel();
            mainHotel.Show();
            Close();
        }

        private void Button_NUser(object sender, RoutedEventArgs e)
        {
            new AddUser(new User()).ShowDialog();
            LoadUser();
        }

        private void Button_EUser(object sender, RoutedEventArgs e)
        {
            if (SelectedUser != null)
            {
                new AddUser(SelectedUser).ShowDialog();                
                LoadUser();
            }
        }

        private void Button_DEUser(object sender, RoutedEventArgs e)
        {
            if (SelectedUser != null)
            {
                if (MessageBox.Show("Удалить выбранного юзера?", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    DataBase.GetInstance().Users.Remove(SelectedUser);
                    DataBase.GetInstance().SaveChanges();
                    LoadUser();
                }
            }
        }


        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
