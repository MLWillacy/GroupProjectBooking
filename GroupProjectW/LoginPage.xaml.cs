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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        Frame mMain = null;
        public LoginPage(Frame main)
        {
            InitializeComponent();
            Incorrect_Login_Text.Visibility = Visibility.Hidden;
            mMain = main;
        }
        private void Email_Text_Changed(object sender, TextChangedEventArgs e)
        {

        }

        private void Password_Text_Changed(object sender, TextChangedEventArgs e)
        {

        }

        private void Login_Button_Clicked(object sender, RoutedEventArgs e)
        {
            bool correctDetails = true;

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
