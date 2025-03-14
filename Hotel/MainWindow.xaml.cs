﻿using Hotel.DB;
using Hotel.Storage;
using Microsoft.EntityFrameworkCore;
using System;
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

        Dictionary<string, int> BlockList = new Dictionary<string, int>();

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

            var span = DateTime.Now.AddMonths(-1);

            var shit = DataBase.GetInstance().Users
                .Where(s => s.LastAuth != null && s.LastAuth <= span)
                .ExecuteUpdate(s => s.SetProperty(
                    p => p.IsLocked, true
                    ));
        }

        int count = 0;
        private void Button_Login(object sender, RoutedEventArgs e)
        {
            ErrorMessage = string.Empty;

            if (!CheckAuth(Login, passwordBox.Password))
            {
                MessageBox.Show( "Неверный логин или пароль");
               
            }            
        }

        private bool CheckAuth(string login,  string password)
        {
            var user = DataBase.GetInstance().Users.Include(s => s.Role).FirstOrDefault(s => s.Login == login && s.Password == password);

            if (user == null)
            {
                ErrorMessage = "Ошибка авторизации или юзера нету";
                if (BlockList.TryGetValue(Login!, out int count))
                {
                    if (count >= 3)
                    {
                        DataBase.GetInstance().Users
                                                .Where(s => s.Login == Login)
                                                .ExecuteUpdate(s => s.SetProperty(
                                                    p => p.IsLocked, true
                                                    ));
                        MessageBox.Show("Вы заблокированы. Обратитесь к администратору");
                    }

                    BlockList[Login!] = ++count;
                }
                else
                    BlockList.Add(Login!, 1);
                return false;
            }
            else if  (user.IsLocked)
            {
                ErrorMessage = "У вас спид.. ой, бан! Обратитесь к админу.";
                return true;
            }
            else
            {
                UserStorage.User = user;

                //DataBase.GetInstance().Users
                //.Where(s => s.Id == user.Id)
                //.ExecuteUpdate(s => s.SetProperty(
                //    p => p.LastAuth, DateTime.Now
                //    ));

                MessageBox.Show("Вы успешно авторизовались");
                if(user.LastAuth == null)
                {
                    ChangePass changePass = new ChangePass(user);
                    changePass.Show();
                    Close();
                    user.LastAuth = DateTime.Now;
                    DataBase.GetInstance().Users.Update(user);
                    DataBase.GetInstance().SaveChanges();
                    return true;
                }

                user.LastAuth = DateTime.Now;
                DataBase.GetInstance().Users.Update(user);
                DataBase.GetInstance().SaveChanges();

                if (user.RoleId == 3)
                {
                    GuestPage guestPage = new GuestPage();
                    guestPage.Show();
                    Close();
                }
                else
                {
                    MainHotel mainHotel = new MainHotel();
                    mainHotel.Show();
                    Close();
                }
                
                return true;
            }
        }
    }
}