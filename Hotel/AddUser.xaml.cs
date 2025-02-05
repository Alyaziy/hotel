using Hotel.DB;
using Microsoft.EntityFrameworkCore.Metadata;
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
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Window, INotifyPropertyChanged
    {
        public bool Editable { get; set; }
        public List<Role> Roles { get; set; }
        public User User { get; set; } = new User();
        public ComboBoxItem SelectedBun { get; set; }

        public AddUser(User selectedUser)
        {
            InitializeComponent();
            Roles = DataBase.GetInstance().Roles.ToList();
            User = selectedUser;
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        private void SaveClose(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SelectedBun.Tag.ToString()))
                return;

            User.IsLocked = SelectedBun.Tag.ToString() == "true" ? true : false;

            if (User.Id == 0)
                DataBase.GetInstance().Users.Add(User);
            else
                DataBase.GetInstance().Users.Update(User);

            DataBase.GetInstance().SaveChanges();
            Close();

        }
    }
}

public static class UserExtension
{
    public static User Clone(this User user)
    {
        var values = DataBase.GetInstance().Users.Entry(user).CurrentValues.Clone();
        var clone = (User)values.ToObject();
        clone.Role = user.Role;
        return clone;
    }
}

