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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        Frame mMain = null;

        string mEmail = "";
        string mPassword = "";
        User mUser;

        public LoginPage(Frame main)
        {
            InitializeComponent();
            Incorrect_Login_Text.Visibility = Visibility.Hidden;
            mMain = main;
        }
        private void Email_Text_Changed(object sender, TextChangedEventArgs e)
        {
            mEmail = Email_Textbox.Text;
        }

        private void Password_Text_Changed(object sender, TextChangedEventArgs e)
        {
            mPassword = Password_Textbox.Text;
        }

        private void Login_Button_Clicked(object sender, RoutedEventArgs e)
        {
            bool correctDetails = false;

            string[] users = Directory.GetFiles(Directory.GetCurrentDirectory() + "/Users", "*.txt");

            for (int i = 0; i < users.Length; i++)
            {
                string[] lines = File.ReadAllLines(users[i]);
                string[] emailSplit = lines[0].Split(':');
                string[] passwordSplit = lines[1].Split(':');

                if (emailSplit[1] == mEmail && passwordSplit[1] == mPassword)
                {
                    correctDetails = true;
                    User mUser = new User(users[i]);
                    break;
                }
            }

            if(correctDetails)
            {
                mMain.Content = new CurrentBookingsPage(mMain);
            }
            else
            {
                Incorrect_Login_Text.Visibility = Visibility.Visible;
            }
        }
    }
}
