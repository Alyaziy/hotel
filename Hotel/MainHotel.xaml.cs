﻿using System;
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
using Hotel.DB;
using Microsoft.EntityFrameworkCore;

namespace Hotel
{
    /// <summary>
    /// Логика взаимодействия для MainHotel.xaml
    /// </summary>
    public partial class MainHotel : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public List<Book> Books { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Guest> Guests { get; set; }
        
        public User SelectedUser { get; set; }

        public MainHotel()
        {
            InitializeComponent();
            
            LoadRoom();
            DataContext = this;
           

        }

        public void LoadRoom()
        {
            var result = DB.DataBase.GetInstance().Books.Include(s => s.Room).
                ThenInclude(s => s.Status).
                Include(s => s.Guest).ToList(); 
            Books = result;
        }

        private void Button_NUser(object sender, RoutedEventArgs e)
        {
            new AddUser(new User()).ShowDialog();
            LoadRoom();

        }

        private void Button_EUser(object sender, RoutedEventArgs e)
        {
            if (SelectedUser != null)
            {
                new AddUser(SelectedUser).ShowDialog();
                LoadRoom();
            }
        }

        private void Button_Schedule(object sender, RoutedEventArgs e)
        {
            Schedule schedule = new Schedule();
            schedule.ShowDialog();
        }
    }
}
