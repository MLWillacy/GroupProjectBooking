﻿using System;
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

namespace GroupProjectW
{
    /// <summary>
    /// Interaction logic for ViewRoomsPage.xaml
    /// </summary>
    public partial class ViewRoomsPage : Page
    {
        Frame mMain = null;
        User mUser;
        public ViewRoomsPage(Frame main, User user)
        {
            mUser = user;
            InitializeComponent();
            for (int i = 0; i < mUser.societies.Length; i++)
            {
                if (mUser.societies[i] == "UnionStaff")
                {
                    RemoveBooking__Button.Visibility = Visibility.Visible;
                    AddUser__Button.Visibility = Visibility.Visible;
                }
            }
            mMain = main;
            
        }
        private void MyBookings_Button_Clicked(object sender, RoutedEventArgs e)
        {
            mMain.Content = new CurrentBookingsPage(mMain,mUser);
        }

        private void MakeBooking_Button_Clicked(object sender, RoutedEventArgs e)
        {
            mMain.Content = new MakeBookingPage(mMain,mUser);
        }

        private void ViewRooms_Button_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void CloseTabs_Button_Clicked(object sender, RoutedEventArgs e)
        {
            Tabs.Visibility = Visibility.Hidden;
        }

        private void OpenTabs_Button_Clicked(object sender, RoutedEventArgs e)
        {
            Tabs.Visibility = Visibility.Visible;
        }

        private void Logout_Button_Clicked(object sender, RoutedEventArgs e)
        {
            mMain.Content = new LoginPage(mMain);
        }
        private void RemoveBooking_Button_Clicked(object sender, RoutedEventArgs e)
        {
            mMain.Content = new DeleteBookingsPage(mMain, mUser);
        }

        private void UserIcon_Button_Clicked(object sender, RoutedEventArgs e)
        {
            if (Logout__Button.Visibility == Visibility.Visible)
            { Logout__Button.Visibility = Visibility.Hidden; }
            else
            { Logout__Button.Visibility = Visibility.Visible; }
        }

        private void AddUser_Button_Clicked(object sender, RoutedEventArgs e)
        {
            mMain.Content = new AddUserPage(mMain,mUser);
        }
    }
}
