using Hotel.DB;
using Hotel.Storage;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string errorMessage;

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public string Login {  get; set; }
        public string ErrorMessage 
        { 
            get => errorMessage; 
            set 
            {
                errorMessage = value;
                Signal();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Login(object sender, RoutedEventArgs e)
        {
            if (!CheckAuth(Login, passwordBox.Password))
            {
                ErrorMessage = "Неверный логин или пароль";

            }
            
        }

        private bool CheckAuth(string login,  string password)
        {
            var user = DataBase.GetInstance().Users.Include(s => s.Role).FirstOrDefault(s => s.Login == login && s.Password == password);

            if (user == null)
            {
                ErrorMessage = "Ошибка авторизации или пользователь не найден";
                return false;
            }
            else
            {
                UserStorage.User = user;

                MessageBox.Show("Вы успешно авторизовались");

                MainHotel mainHotel = new MainHotel();
                mainHotel.Show();
                Close();
                return true;
            }
        }
    }
}