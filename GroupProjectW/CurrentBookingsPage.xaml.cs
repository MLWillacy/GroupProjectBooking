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
    /// Interaction logic for CurrentBookingsPage.xaml
    /// </summary>
    public partial class CurrentBookingsPage : Page
    {

        Frame mMain = null;
        User mUser;

        public CurrentBookingsPage(Frame main, User user)
        {
            InitializeComponent();
            mMain = main;
            mUser = user;
            for (int i = 0; i < mUser.societies.Length; i++)
            {
                if (mUser.societies[i] == "UnionStaff")
                {
                    RemoveBooking__Button.Visibility = Visibility.Visible;
                    AddUser__Button.Visibility = Visibility.Visible;
                }
            }
            loadBookingData();
        }

        public void loadBookingData()
        {
            Bookings_Text.Content = "Bookings:\n";
            for(int i = 0; i < mUser.bookings.Count; i++)
            {
                Bookings_Text.Content = Bookings_Text.Content + mUser.bookings[i] + "\n";
            }
        }



        #region Page Template

        /*********************************************************************************
         * Page Template Functions
        *********************************************************************************/

        private void MyBookings_Button_Clicked(object sender, RoutedEventArgs e)
        {
        }

        private void MakeBooking_Button_Clicked(object sender, RoutedEventArgs e)
        {
            mMain.Content = new MakeBookingPage(mMain,mUser);
        }

        private void ViewRooms_Button_Clicked(object sender, RoutedEventArgs e)
        {
            mMain.Content = new ViewRoomsPage(mMain,mUser);
        }

        private void RemoveBooking_Button_Clicked(object sender, RoutedEventArgs e)
        {
            mMain.Content = new DeleteBookingsPage(mMain, mUser);
        }
        private void AddUser_Button_Clicked(object sender, RoutedEventArgs e)
        {
            mMain.Content = new AddUserPage(mMain, mUser);
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

        #endregion

        
    }
}
