using Hotel.DB;
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

namespace Hotel
{
    /// <summary>
    /// Логика взаимодействия для ChangePass.xaml
    /// </summary>
    public partial class ChangePass : Window, INotifyPropertyChanged
    {
        public User User { get; set; }


        public ChangePass(User user )
        {
            InitializeComponent();
            
            User = user;
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        private void SavePass(object sender, RoutedEventArgs e)
        {
            if(passwordBox.Password == User.Password && 
                passwordBox.Password != newpassword.Password &&
                newpassword.Password == confpassword.Password)
            {
                User.Password = newpassword.Password;
                
                DataBase.GetInstance().Users.Update(User);
                MessageBox.Show("Вы успешно изменили пароль");

                DataBase.GetInstance().SaveChanges();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();

            }           
            else if (passwordBox.Password != User.Password)
            {
                MessageBox.Show("Неверно введеный пароль! Попробуйтке ещё раз.");
            }
            else if(passwordBox.Password == newpassword.Password) 
            {
                MessageBox.Show("Новый пароль должен отличаться от текущего!");
            }
            else if (newpassword.Password != confpassword.Password)
            {
                MessageBox.Show("Пароли не совпадают! Попробуйте ещё раз.");
            }

            
        }
    }
}
