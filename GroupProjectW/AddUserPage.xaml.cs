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
using System.IO;

namespace GroupProjectW
{
    /// <summary>
    /// Interaction logic for ViewRoomsPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        Frame mMain = null;
        User mUser;

        string mEmail;
        string mPassword;
        string[] mSocieties;

        public AddUserPage(Frame main, User user)
        {
            InitializeComponent();
            mMain = main;
            mUser = user;
        }

        #region Tab Buttons
        private void MyBookings_Button_Clicked(object sender, RoutedEventArgs e)
        {
            mMain.Content = new CurrentBookingsPage(mMain,mUser);
        }

        private void MakeBooking_Button_Clicked(object sender, RoutedEventArgs e)
        {
            mMain.Content = new MakeBookingPage(mMain,mUser);
        }
        private void RemoveBooking_Button_Clicked(object sender, RoutedEventArgs e)
        {
            mMain.Content = new DeleteBookingsPage(mMain, mUser);
        }

        private void ViewRooms_Button_Clicked(object sender, RoutedEventArgs e)
        {
            mMain.Content = new ViewRoomsPage(mMain, mUser);
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

        private void Email_Text_Changed(object sender, TextChangedEventArgs e)
        {
            mEmail = Email_Textbox.Text;
            created_text.Visibility = Visibility.Hidden;
        }

        private void Password_Text_Changed(object sender, TextChangedEventArgs e)
        {
            mPassword = Password_Textbox.Text;
            created_text.Visibility = Visibility.Hidden;
        }

        private void Society_Text_Changed(object sender, TextChangedEventArgs e)
        {
            string societies = Society_Textbox.Text;
            mSocieties = societies.Split(',');
            created_text.Visibility = Visibility.Hidden;
        }

        private void Login_Button_Clicked(object sender, RoutedEventArgs e)
        {
            string[] emailSplit = mEmail.Split('@');
            string fileName = "Users/" + emailSplit[0] + ".txt";
            if (!File.Exists(fileName))
            {
                var myFile = File.Create(fileName);
                myFile.Close();
                saveFile(fileName);
                Email_Textbox.Text = "";
                Password_Textbox.Text = "";
                Society_Textbox.Text = "";
                created_text.Visibility = Visibility.Visible;

            }
        }

        private void saveFile(string fileName)
        {
            StreamWriter savefile = new StreamWriter(fileName);
            savefile.WriteLine("Email:" + mEmail);
            savefile.WriteLine("Password:" + mPassword);
            string societySav = "Societies:";
            for (int i = 0; i < mSocieties.Length; i++)
            {
                societySav = societySav + " " + mSocieties[i];
            }
            savefile.WriteLine(societySav);
            savefile.WriteLine("NumBookings:0");
            savefile.WriteLine("Bookings:");

            savefile.Close();
            
        }
    }
}
