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

namespace GroupProjectW
{
    /// <summary>
    /// Interaction logic for ViewRoomsPage.xaml
    /// </summary>
    public partial class ViewRoomsPage : Page
    {
        Frame mMain = null;
        public ViewRoomsPage(Frame main)
        {
            InitializeComponent();
            mMain = main;
        }
        private void MyBookings_Button_Clicked(object sender, RoutedEventArgs e)
        {
            mMain.Content = new CurrentBookingsPage(mMain);
        }

        private void MakeBooking_Button_Clicked(object sender, RoutedEventArgs e)
        {
            mMain.Content = new MakeBookingPage(mMain);
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

        private void UserIcon_Button_Clicked(object sender, RoutedEventArgs e)
        {
            if (Logout__Button.Visibility == Visibility.Visible)
            { Logout__Button.Visibility = Visibility.Hidden; }
            else
            { Logout__Button.Visibility = Visibility.Visible; }
        }
    }
}
