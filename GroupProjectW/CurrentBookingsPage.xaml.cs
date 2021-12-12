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
        //Timetable Variables
        int mWeek = 1;
        int[,] bookings = new int[14, 7]; //states represented by int (0 = available, 1 = currently booking, 2 = unavailable)
        bool startTimeSelected = false;
        bool endTimeSelected = false;
        int[] startTimeCoord = new int[2];
        int[] endTimeCoord = new int[2];

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
            updateTimetableUI();
            
        }

        #region Update Timetable Functions
        /*********************************************************************************
         * Update Timetable Functions
        *********************************************************************************/
        private void updateTimetableUI()
        {
            initialiseTimetable();
            readBookingsFromDatabase();
            setTimetableColours();
        }

        private void initialiseTimetable()
        {
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    bookings[i, j] = 0;
                }
            }
        }
        private void readBookingsFromDatabase()
        {
            //if room booked at time set corresponding bookings int to 2
        } //******************************************************************************************************************************* needs adding
        private void setTimetableColours()
        {
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    System.Windows.Controls.Button thisButton = BookingsCoord2Button(i, j);

                    if (bookings[i, j] == 0)
                    { thisButton.Background = Brushes.LawnGreen; }
                    else if (bookings[i, j] == 1)
                    { thisButton.Background = Brushes.Yellow; }
                    else if (bookings[i, j] == 2)
                    { thisButton.Background = Brushes.Red; }
                }
            }


        }
        private void changeStartTime(System.Windows.Controls.Button thisButton)
        {
            updateTimetableUI();
            thisButton.Background = Brushes.Yellow;
            startTimeSelected = true;
            startTimeCoord = button2BookingsCoord(thisButton);
            endTimeSelected = false;
            updateStartTimeText(thisButton);
        }

        private void timetableButtonPressed(System.Windows.Controls.Button thisButton)
        {
            if (thisButton.Background == Brushes.Red)
            {
                endTimeCoord = button2BookingsCoord(thisButton);
                if (endTimeSelected == false && startTimeSelected == true)
                {
                    if (startTimeCoord[0] <= endTimeCoord[0] && startTimeCoord[1] == endTimeCoord[1])
                    {
                        if (checkForClashes())
                        { changeStartTime(thisButton); }
                        else
                        {
                            thisButton.Background = Brushes.Yellow;
                            endTimeSelected = true;
                            fillBookingGap();
                            updateEndTimeText(thisButton);
                        }
                    }
                    else
                    { changeStartTime(thisButton);}
                }
                else
                {changeStartTime(thisButton); }
            }
        }
        private void fillBookingGap()
        {
            int day = startTimeCoord[1];
            for (int i = 0; i < endTimeCoord[0] - startTimeCoord[0]; i++)
            {
                BookingsCoord2Button(startTimeCoord[0] + i, day).Background = Brushes.Yellow;
            }
        }
        private bool checkForClashes()
        {
            bool clashes = false;
            int day = startTimeCoord[1];
            for (int i = 0; i < endTimeCoord[0] - startTimeCoord[0]; i++)
            {
                if (BookingsCoord2Button(startTimeCoord[0] + i, day).Background == Brushes.LawnGreen)
                {
                    clashes = true;
                    break;
                }
            }
            return clashes;
        }

        /*********************************************************************************
         * Timetable Forward/Backwards Functions
        *********************************************************************************/
        private void timetableForward_Button_Clicked(object sender, RoutedEventArgs e)
        {
            if (mWeek > 0 && mWeek < 3)
            {
                Monday_Text.Content = updateWeek(Monday_Text.Content.ToString(), true);
                Tuesday_Text.Content = updateWeek(Tuesday_Text.Content.ToString(), true);
                Wednesday_Text.Content = updateWeek(Wednesday_Text.Content.ToString(), true);
                Thursday_Text.Content = updateWeek(Thursday_Text.Content.ToString(), true);
                Friday_Text.Content = updateWeek(Friday_Text.Content.ToString(), true);
                Saturday_Text.Content = updateWeek(Saturday_Text.Content.ToString(), true);
                Sunday_Text.Content = updateWeek(Sunday_Text.Content.ToString(), true);

                updateTimetableUI();
                mWeek = mWeek + 1;
            }
        }
        private void timetableBack_Button_Clicked(object sender, RoutedEventArgs e)
        {
            if (mWeek > 1 && mWeek < 4)
            {
                Monday_Text.Content = updateWeek(Monday_Text.Content.ToString(), false);
                Tuesday_Text.Content = updateWeek(Tuesday_Text.Content.ToString(), false);
                Wednesday_Text.Content = updateWeek(Wednesday_Text.Content.ToString(), false);
                Thursday_Text.Content = updateWeek(Thursday_Text.Content.ToString(), false);
                Friday_Text.Content = updateWeek(Friday_Text.Content.ToString(), false);
                Saturday_Text.Content = updateWeek(Saturday_Text.Content.ToString(), false);
                Sunday_Text.Content = updateWeek(Sunday_Text.Content.ToString(), false);

                updateTimetableUI();
                mWeek--;
            }
        }

        private string updateWeek(string day, bool forward)
        {
            string[] daySplit = day.Split();
            string[] date = daySplit[1].Split('/');
            int number = int.Parse(date[0]);
            int numberUpdated = 0;
            if (forward)
            { numberUpdated = number + 7; }
            else
            { numberUpdated = number - 7; }
            date[0] = numberUpdated.ToString();
            string updatedDay = daySplit[0] + " " + date[0] + "/" + date[1] + "/" + date[2];

            startTimeSelected = false;

            return updatedDay;
        }

        #endregion
        #region Continue Button Functions

        /*********************************************************************************
         * Continue Button Functions
        *********************************************************************************/

        private void continue_Button_Clicked(object sender, RoutedEventArgs e)
        {
            if (endTimeSelected == true)
            {
                saveTempBooking();
                updateTimetableUI();
            }
        }
        private void updateStartTimeText(System.Windows.Controls.Button thisButton)
        {
            if (!startTimeSelected)
            {
                continue__Button.Content = "Cancel: startTime - endTime";
            }
            else
            {
                string date = button2Date(thisButton);
                string startTime = Button2Time(thisButton);
                continue__Button.Content = "Cancel: " + date + " " + startTime + " - endTime";
            }
        }
        private void updateEndTimeText(System.Windows.Controls.Button thisButton)
        {
            if (!endTimeSelected)
            {
                updateStartTimeText(BookingsCoord2Button(startTimeCoord[0], startTimeCoord[1]));
            }
            else
            {
                string date = button2Date(thisButton);
                System.Windows.Controls.Button startButton = BookingsCoord2Button(startTimeCoord[0], startTimeCoord[1]);
                string startTime = Button2Time(startButton);

                string endTime = Button2Time(thisButton);
                string x = endTime.Substring(0, 1);
                int timeToUpdate = int.Parse(endTime.Substring(0, 2));
                int timeUpdated = timeToUpdate + 1;
                endTime = timeUpdated.ToString() + endTime.Substring(2);


                continue__Button.Content = "Cancel: " + date + " " + startTime + " - " + endTime;
            }

        }
        private void saveTempBooking()
        {
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (BookingsCoord2Button(i, j).Background == Brushes.Yellow)
                    {
                        bookings[i, j] = 0;
                    }
                }
            }
        }
        #endregion




        #region Timetable Buttons
        /*********************************************************************************
         * Timetable Buttons
        *********************************************************************************/

        private void moNine_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(moNine__Button);
        }
        private void tuNine_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(tuNine__Button);
        }
        private void weNine_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(weNine__Button);
        }
        private void thNine_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(thNine__Button);
        }
        private void frNine_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(frNine__Button);
        }
        private void saNine_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(saNine__Button);
        }
        private void suNine_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(suNine__Button);
        }
        //------------------------------------------------
        private void moTen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(moTen__Button);
        }
        private void tuTen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(tuTen__Button);
        }
        private void weTen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(weTen__Button);
        }
        private void thTen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(thTen__Button);
        }
        private void frTen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(frTen__Button);
        }
        private void saTen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(saTen__Button);
        }
        private void suTen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(suTen__Button);
        }
        //--------------------------------------------------------------
        private void moEleven_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(moEleven__Button);
        }
        private void tuEleven_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(tuEleven__Button);
        }
        private void weEleven_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(weEleven__Button);
        }
        private void thEleven_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(thEleven__Button);
        }
        private void frEleven_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(frEleven__Button);
        }
        private void saEleven_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(saEleven__Button);
        }
        private void suEleven_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(suEleven__Button);
        }
        //------------------------------------------------
        private void moTwelve_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(moTwelve__Button);
        }
        private void tuTwelve_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(tuTwelve__Button);
        }
        private void weTwelve_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(weTwelve__Button);
        }
        private void thTwelve_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(thTwelve__Button);
        }
        private void frTwelve_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(frTwelve__Button);
        }
        private void saTwelve_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(saTwelve__Button);
        }
        private void suTwelve_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(suTwelve__Button);
        }
        //------------------------------------------------
        private void moThirteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(moThirteen__Button);
        }
        private void tuThirteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(tuThirteen__Button);
        }
        private void weThirteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(weThirteen__Button);
        }
        private void thThirteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(thThirteen__Button);
        }
        private void frThirteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(frThirteen__Button);
        }
        private void saThirteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(saThirteen__Button);
        }
        private void suThirteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(suThirteen__Button);
        }
        //------------------------------------------------
        private void moFourteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(moFourteen__Button);
        }
        private void tuFourteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(tuFourteen__Button);
        }
        private void weFourteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(weFourteen__Button);
        }
        private void thFourteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(thFourteen__Button);
        }
        private void frFourteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(frFourteen__Button);
        }
        private void saFourteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(saFourteen__Button);
        }
        private void suFourteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(suFourteen__Button);
        }
        //------------------------------------------------
        private void moFithteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(moFithteen__Button);
        }
        private void tuFithteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(tuFithteen__Button);
        }
        private void weFithteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(weFithteen__Button);
        }
        private void thFithteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(thFithteen__Button);
        }
        private void frFithteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(frFithteen__Button);
        }
        private void saFithteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(saFithteen__Button);
        }
        private void suFithteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(suFithteen__Button);
        }
        //------------------------------------------------
        private void moSixteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(moSixteen__Button);
        }
        private void tuSixteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(tuSixteen__Button);
        }
        private void weSixteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(weSixteen__Button);
        }
        private void thSixteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(thSixteen__Button);
        }
        private void frSixteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(frSixteen__Button);
        }
        private void saSixteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(saSixteen__Button);
        }
        private void suSixteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(suSixteen__Button);
        }
        //------------------------------------------------
        private void moSeventeen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(moSeventeen__Button);
        }
        private void tuSeventeen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(tuSeventeen__Button);
        }
        private void weSeventeen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(weSeventeen__Button);
        }
        private void thSeventeen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(thSeventeen__Button);
        }
        private void frSeventeen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(frSeventeen__Button);
        }
        private void saSeventeen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(saSeventeen__Button);
        }
        private void suSeventeen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(suSeventeen__Button);
        }
        //------------------------------------------------
        private void moEightteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(moEightteen__Button);
        }
        private void tuEightteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(tuEightteen__Button);
        }
        private void weEightteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(weEightteen__Button);
        }
        private void thEightteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(thEightteen__Button);
        }
        private void frEightteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(frEightteen__Button);
        }
        private void saEightteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(saEightteen__Button);
        }
        private void suEightteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(suEightteen__Button);
        }
        //------------------------------------------------
        private void moNineteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(moNineteen__Button);
        }
        private void tuNineteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(tuNineteen__Button);
        }
        private void weNineteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(weNineteen__Button);
        }
        private void thNineteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(thNineteen__Button);
        }
        private void frNineteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(frNineteen__Button);
        }
        private void saNineteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(saNineteen__Button);
        }
        private void suNineteen_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(suNineteen__Button);
        }
        //------------------------------------------------
        private void moTwenty_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(moTwenty__Button);
        }
        private void tuTwenty_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(tuTwenty__Button);
        }
        private void weTwenty_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(weTwenty__Button);
        }
        private void thTwenty_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(thTwenty__Button);
        }
        private void frTwenty_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(frTwenty__Button);
        }
        private void saTwenty_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(saTwenty__Button);
        }
        private void suTwenty_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(suTwenty__Button);
        }
        //------------------------------------------------
        private void moTwentyOne_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(moTwentyOne__Button);
        }
        private void tuTwentyOne_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(tuTwentyOne__Button);
        }
        private void weTwentyOne_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(weTwentyOne__Button);
        }
        private void thTwentyOne_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(thTwentyOne__Button);
        }
        private void frTwentyOne_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(frTwentyOne__Button);
        }
        private void saTwentyOne_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(saTwentyOne__Button);
        }
        private void suTwentyOne_Button_Clicked(object sender, RoutedEventArgs e)
        {
            timetableButtonPressed(suTwentyOne__Button);
        }
        #endregion
        #region Conversion Functions
        /*********************************************************************************
        * Conversion Functions
       *********************************************************************************/
        private void time2BookingCoord()
        { }
        private void BookingCoord2Time()
        { }

        private string button2Date(System.Windows.Controls.Button thisButton)
        {
            int dateInt = button2BookingsCoord(thisButton)[1];
            string date = null;

            if (dateInt == 0)
            { date = Monday_Text.Content.ToString(); }
            else if (dateInt == 1)
            { date = Tuesday_Text.Content.ToString(); }
            else if (dateInt == 2)
            { date = Wednesday_Text.Content.ToString(); }
            else if (dateInt == 3)
            { date = Thursday_Text.Content.ToString(); }
            else if (dateInt == 4)
            { date = Friday_Text.Content.ToString(); }
            else if (dateInt == 5)
            { date = Saturday_Text.Content.ToString(); }
            else if (dateInt == 6)
            { date = Sunday_Text.Content.ToString(); }

            return date;
        }
        private string Button2Time(System.Windows.Controls.Button thisButton)
        {
            int timeInt = button2BookingsCoord(thisButton)[0];
            string time = null;

            if (timeInt == 0)
            { time = Nine_Text.Content.ToString(); }
            else if (timeInt == 1)
            { time = Ten_Text.Content.ToString(); }
            else if (timeInt == 2)
            { time = Eleven_Text.Content.ToString(); }
            else if (timeInt == 3)
            { time = Twelve_Text.Content.ToString(); }
            else if (timeInt == 4)
            { time = Thirteen_Text.Content.ToString(); }
            else if (timeInt == 5)
            { time = Fourteen_Text.Content.ToString(); }
            else if (timeInt == 6)
            { time = Fithteen_Text.Content.ToString(); }
            else if (timeInt == 7)
            { time = Sixteen_Text.Content.ToString(); }
            else if (timeInt == 8)
            { time = Seventeen_Text.Content.ToString(); }
            else if (timeInt == 9)
            { time = Eightteen_Text.Content.ToString(); }
            else if (timeInt == 10)
            { time = Nineteen_Text.Content.ToString(); }
            else if (timeInt == 11)
            { time = Twenty_Text.Content.ToString(); }
            else if (timeInt == 12)
            { time = Twenty_Text.Content.ToString(); }

            return time;

        }

        private Button BookingsCoord2Button(int coord1, int coord2)
        {
            Button thisButton = null;

            if (coord1 == 0 && coord2 == 0)
            { thisButton = moNine__Button; }
            else if (coord1 == 0 && coord2 == 1)
            { thisButton = tuNine__Button; }
            else if (coord1 == 0 && coord2 == 2)
            { thisButton = weNine__Button; }
            else if (coord1 == 0 && coord2 == 3)
            { thisButton = thNine__Button; }
            else if (coord1 == 0 && coord2 == 4)
            { thisButton = frNine__Button; }
            else if (coord1 == 0 && coord2 == 5)
            { thisButton = saNine__Button; }
            else if (coord1 == 0 && coord2 == 6)
            { thisButton = suNine__Button; }

            else if (coord1 == 1 && coord2 == 0)
            { thisButton = moTen__Button; }
            else if (coord1 == 1 && coord2 == 1)
            { thisButton = tuTen__Button; }
            else if (coord1 == 1 && coord2 == 2)
            { thisButton = weTen__Button; }
            else if (coord1 == 1 && coord2 == 3)
            { thisButton = thTen__Button; }
            else if (coord1 == 1 && coord2 == 4)
            { thisButton = frTen__Button; }
            else if (coord1 == 1 && coord2 == 5)
            { thisButton = saTen__Button; }
            else if (coord1 == 1 && coord2 == 6)
            { thisButton = suTen__Button; }

            else if (coord1 == 2 && coord2 == 0)
            { thisButton = moEleven__Button; }
            else if (coord1 == 2 && coord2 == 1)
            { thisButton = tuEleven__Button; }
            else if (coord1 == 2 && coord2 == 2)
            { thisButton = weEleven__Button; }
            else if (coord1 == 2 && coord2 == 3)
            { thisButton = thEleven__Button; }
            else if (coord1 == 2 && coord2 == 4)
            { thisButton = frEleven__Button; }
            else if (coord1 == 2 && coord2 == 5)
            { thisButton = saEleven__Button; }
            else if (coord1 == 2 && coord2 == 6)
            { thisButton = suEleven__Button; }

            else if (coord1 == 3 && coord2 == 0)
            { thisButton = moTwelve__Button; }
            else if (coord1 == 3 && coord2 == 1)
            { thisButton = tuTwelve__Button; }
            else if (coord1 == 3 && coord2 == 2)
            { thisButton = weTwelve__Button; }
            else if (coord1 == 3 && coord2 == 3)
            { thisButton = thTwelve__Button; }
            else if (coord1 == 3 && coord2 == 4)
            { thisButton = frTwelve__Button; }
            else if (coord1 == 3 && coord2 == 5)
            { thisButton = saTwelve__Button; }
            else if (coord1 == 3 && coord2 == 6)
            { thisButton = suTwelve__Button; }

            else if (coord1 == 4 && coord2 == 0)
            { thisButton = moThirteen__Button; }
            else if (coord1 == 4 && coord2 == 1)
            { thisButton = tuThirteen__Button; }
            else if (coord1 == 4 && coord2 == 2)
            { thisButton = weThirteen__Button; }
            else if (coord1 == 4 && coord2 == 3)
            { thisButton = thThirteen__Button; }
            else if (coord1 == 4 && coord2 == 4)
            { thisButton = frThirteen__Button; }
            else if (coord1 == 4 && coord2 == 5)
            { thisButton = saThirteen__Button; }
            else if (coord1 == 4 && coord2 == 6)
            { thisButton = suThirteen__Button; }

            else if (coord1 == 5 && coord2 == 0)
            { thisButton = moFourteen__Button; }
            else if (coord1 == 5 && coord2 == 1)
            { thisButton = tuFourteen__Button; }
            else if (coord1 == 5 && coord2 == 2)
            { thisButton = weFourteen__Button; }
            else if (coord1 == 5 && coord2 == 3)
            { thisButton = thFourteen__Button; }
            else if (coord1 == 5 && coord2 == 4)
            { thisButton = frFourteen__Button; }
            else if (coord1 == 5 && coord2 == 5)
            { thisButton = saFourteen__Button; }
            else if (coord1 == 5 && coord2 == 6)
            { thisButton = suFourteen__Button; }

            else if (coord1 == 6 && coord2 == 0)
            { thisButton = moFithteen__Button; }
            else if (coord1 == 6 && coord2 == 1)
            { thisButton = tuFithteen__Button; }
            else if (coord1 == 6 && coord2 == 2)
            { thisButton = weFithteen__Button; }
            else if (coord1 == 6 && coord2 == 3)
            { thisButton = thFithteen__Button; }
            else if (coord1 == 6 && coord2 == 4)
            { thisButton = frFithteen__Button; }
            else if (coord1 == 6 && coord2 == 5)
            { thisButton = saFithteen__Button; }
            else if (coord1 == 6 && coord2 == 6)
            { thisButton = suFithteen__Button; }

            else if (coord1 == 7 && coord2 == 0)
            { thisButton = moSixteen__Button; }
            else if (coord1 == 7 && coord2 == 1)
            { thisButton = tuSixteen__Button; }
            else if (coord1 == 7 && coord2 == 2)
            { thisButton = weSixteen__Button; }
            else if (coord1 == 7 && coord2 == 3)
            { thisButton = thSixteen__Button; }
            else if (coord1 == 7 && coord2 == 4)
            { thisButton = frSixteen__Button; }
            else if (coord1 == 7 && coord2 == 5)
            { thisButton = saSixteen__Button; }
            else if (coord1 == 7 && coord2 == 6)
            { thisButton = suSixteen__Button; }

            else if (coord1 == 8 && coord2 == 0)
            { thisButton = moSeventeen__Button; }
            else if (coord1 == 8 && coord2 == 1)
            { thisButton = tuSeventeen__Button; }
            else if (coord1 == 8 && coord2 == 2)
            { thisButton = weSeventeen__Button; }
            else if (coord1 == 8 && coord2 == 3)
            { thisButton = thSeventeen__Button; }
            else if (coord1 == 8 && coord2 == 4)
            { thisButton = frSeventeen__Button; }
            else if (coord1 == 8 && coord2 == 5)
            { thisButton = saSeventeen__Button; }
            else if (coord1 == 8 && coord2 == 6)
            { thisButton = suSeventeen__Button; }

            else if (coord1 == 9 && coord2 == 0)
            { thisButton = moEightteen__Button; }
            else if (coord1 == 9 && coord2 == 1)
            { thisButton = tuEightteen__Button; }
            else if (coord1 == 9 && coord2 == 2)
            { thisButton = weEightteen__Button; }
            else if (coord1 == 9 && coord2 == 3)
            { thisButton = thEightteen__Button; }
            else if (coord1 == 9 && coord2 == 4)
            { thisButton = frEightteen__Button; }
            else if (coord1 == 9 && coord2 == 5)
            { thisButton = saEightteen__Button; }
            else if (coord1 == 9 && coord2 == 6)
            { thisButton = suEightteen__Button; }

            else if (coord1 == 10 && coord2 == 0)
            { thisButton = moNineteen__Button; }
            else if (coord1 == 10 && coord2 == 1)
            { thisButton = tuNineteen__Button; }
            else if (coord1 == 10 && coord2 == 2)
            { thisButton = weNineteen__Button; }
            else if (coord1 == 10 && coord2 == 3)
            { thisButton = thNineteen__Button; }
            else if (coord1 == 10 && coord2 == 4)
            { thisButton = frNineteen__Button; }
            else if (coord1 == 10 && coord2 == 5)
            { thisButton = saNineteen__Button; }
            else if (coord1 == 10 && coord2 == 6)
            { thisButton = suNineteen__Button; }

            else if (coord1 == 11 && coord2 == 0)
            { thisButton = moTwenty__Button; }
            else if (coord1 == 11 && coord2 == 1)
            { thisButton = tuTwenty__Button; }
            else if (coord1 == 11 && coord2 == 2)
            { thisButton = weTwenty__Button; }
            else if (coord1 == 11 && coord2 == 3)
            { thisButton = thTwenty__Button; }
            else if (coord1 == 11 && coord2 == 4)
            { thisButton = frTwenty__Button; }
            else if (coord1 == 11 && coord2 == 5)
            { thisButton = saTwenty__Button; }
            else if (coord1 == 11 && coord2 == 6)
            { thisButton = suTwenty__Button; }

            else if (coord1 == 12 && coord2 == 0)
            { thisButton = moTwentyOne__Button; }
            else if (coord1 == 12 && coord2 == 1)
            { thisButton = tuTwentyOne__Button; }
            else if (coord1 == 12 && coord2 == 2)
            { thisButton = weTwentyOne__Button; }
            else if (coord1 == 12 && coord2 == 3)
            { thisButton = thTwentyOne__Button; }
            else if (coord1 == 12 && coord2 == 4)
            { thisButton = frTwentyOne__Button; }
            else if (coord1 == 12 && coord2 == 5)
            { thisButton = saTwentyOne__Button; }
            else if (coord1 == 12 && coord2 == 6)
            { thisButton = suTwentyOne__Button; }

            return thisButton;
        }
        private int[] button2BookingsCoord(System.Windows.Controls.Button thisButton)
        {
            int[] coords = new int[2];

            if (thisButton == moNine__Button)
            {
                coords[0] = 0;
                coords[1] = 0;
            }
            else if (thisButton == tuNine__Button)
            {
                coords[0] = 0;
                coords[1] = 1;
            }
            else if (thisButton == weNine__Button)
            {
                coords[0] = 0;
                coords[1] = 2;
            }
            else if (thisButton == thNine__Button)
            {
                coords[0] = 0;
                coords[1] = 3;
            }
            else if (thisButton == frNine__Button)
            {
                coords[0] = 0;
                coords[1] = 4;
            }
            else if (thisButton == saNine__Button)
            {
                coords[0] = 0;
                coords[1] = 5;
            }
            else if (thisButton == suNine__Button)
            {
                coords[0] = 0;
                coords[1] = 6;
            }



            else if (thisButton == moTen__Button)
            {
                coords[0] = 1;
                coords[1] = 0;
            }
            else if (thisButton == tuTen__Button)
            {
                coords[0] = 1;
                coords[1] = 1;
            }
            else if (thisButton == weTen__Button)
            {
                coords[0] = 1;
                coords[1] = 2;
            }
            else if (thisButton == thTen__Button)
            {
                coords[0] = 1;
                coords[1] = 3;
            }
            else if (thisButton == frTen__Button)
            {
                coords[0] = 1;
                coords[1] = 4;
            }
            else if (thisButton == saTen__Button)
            {
                coords[0] = 1;
                coords[1] = 5;
            }
            else if (thisButton == suTen__Button)
            {
                coords[0] = 1;
                coords[1] = 6;
            }



            else if (thisButton == moEleven__Button)
            {
                coords[0] = 2;
                coords[1] = 0;
            }
            else if (thisButton == tuEleven__Button)
            {
                coords[0] = 2;
                coords[1] = 1;
            }
            else if (thisButton == weEleven__Button)
            {
                coords[0] = 2;
                coords[1] = 2;
            }
            else if (thisButton == thEleven__Button)
            {
                coords[0] = 2;
                coords[1] = 3;
            }
            else if (thisButton == frEleven__Button)
            {
                coords[0] = 2;
                coords[1] = 4;
            }
            else if (thisButton == saEleven__Button)
            {
                coords[0] = 2;
                coords[1] = 5;
            }
            else if (thisButton == suEleven__Button)
            {
                coords[0] = 2;
                coords[1] = 6;
            }



            else if (thisButton == moTwelve__Button)
            {
                coords[0] = 3;
                coords[1] = 0;
            }
            else if (thisButton == tuTwelve__Button)
            {
                coords[0] = 3;
                coords[1] = 1;
            }
            else if (thisButton == weTwelve__Button)
            {
                coords[0] = 3;
                coords[1] = 2;
            }
            else if (thisButton == thTwelve__Button)
            {
                coords[0] = 3;
                coords[1] = 3;
            }
            else if (thisButton == frTwelve__Button)
            {
                coords[0] = 3;
                coords[1] = 4;
            }
            else if (thisButton == saTwelve__Button)
            {
                coords[0] = 3;
                coords[1] = 5;
            }
            else if (thisButton == suTwelve__Button)
            {
                coords[0] = 3;
                coords[1] = 6;
            }



            else if (thisButton == moThirteen__Button)
            {
                coords[0] = 4;
                coords[1] = 0;
            }
            else if (thisButton == tuThirteen__Button)
            {
                coords[0] = 4;
                coords[1] = 1;
            }
            else if (thisButton == weThirteen__Button)
            {
                coords[0] = 4;
                coords[1] = 2;
            }
            else if (thisButton == thThirteen__Button)
            {
                coords[0] = 4;
                coords[1] = 3;
            }
            else if (thisButton == frThirteen__Button)
            {
                coords[0] = 4;
                coords[1] = 4;
            }
            else if (thisButton == saThirteen__Button)
            {
                coords[0] = 4;
                coords[1] = 5;
            }
            else if (thisButton == suThirteen__Button)
            {
                coords[0] = 4;
                coords[1] = 6;
            }



            else if (thisButton == moFourteen__Button)
            {
                coords[0] = 5;
                coords[1] = 0;
            }
            else if (thisButton == tuFourteen__Button)
            {
                coords[0] = 5;
                coords[1] = 1;
            }
            else if (thisButton == weFourteen__Button)
            {
                coords[0] = 5;
                coords[1] = 2;
            }
            else if (thisButton == thFourteen__Button)
            {
                coords[0] = 5;
                coords[1] = 3;
            }
            else if (thisButton == frFourteen__Button)
            {
                coords[0] = 5;
                coords[1] = 4;
            }
            else if (thisButton == saFourteen__Button)
            {
                coords[0] = 5;
                coords[1] = 5;
            }
            else if (thisButton == suFourteen__Button)
            {
                coords[0] = 5;
                coords[1] = 6;
            }



            else if (thisButton == moFithteen__Button)
            {
                coords[0] = 6;
                coords[1] = 0;
            }
            else if (thisButton == tuFithteen__Button)
            {
                coords[0] = 6;
                coords[1] = 1;
            }
            else if (thisButton == weFithteen__Button)
            {
                coords[0] = 6;
                coords[1] = 2;
            }
            else if (thisButton == thFithteen__Button)
            {
                coords[0] = 6;
                coords[1] = 3;
            }
            else if (thisButton == frFithteen__Button)
            {
                coords[0] = 6;
                coords[1] = 4;
            }
            else if (thisButton == saFithteen__Button)
            {
                coords[0] = 6;
                coords[1] = 5;
            }
            else if (thisButton == suFithteen__Button)
            {
                coords[0] = 6;
                coords[1] = 6;
            }



            else if (thisButton == moSixteen__Button)
            {
                coords[0] = 7;
                coords[1] = 0;
            }
            else if (thisButton == tuSixteen__Button)
            {
                coords[0] = 7;
                coords[1] = 1;
            }
            else if (thisButton == weSixteen__Button)
            {
                coords[0] = 7;
                coords[1] = 2;
            }
            else if (thisButton == thSixteen__Button)
            {
                coords[0] = 7;
                coords[1] = 3;
            }
            else if (thisButton == frSixteen__Button)
            {
                coords[0] = 7;
                coords[1] = 4;
            }
            else if (thisButton == saSixteen__Button)
            {
                coords[0] = 7;
                coords[1] = 5;
            }
            else if (thisButton == suSixteen__Button)
            {
                coords[0] = 7;
                coords[1] = 6;
            }



            else if (thisButton == moSeventeen__Button)
            {
                coords[0] = 8;
                coords[1] = 0;
            }
            else if (thisButton == tuSeventeen__Button)
            {
                coords[0] = 8;
                coords[1] = 1;
            }
            else if (thisButton == weSeventeen__Button)
            {
                coords[0] = 8;
                coords[1] = 2;
            }
            else if (thisButton == thSeventeen__Button)
            {
                coords[0] = 8;
                coords[1] = 3;
            }
            else if (thisButton == frSeventeen__Button)
            {
                coords[0] = 8;
                coords[1] = 4;
            }
            else if (thisButton == saSeventeen__Button)
            {
                coords[0] = 8;
                coords[1] = 5;
            }
            else if (thisButton == suSeventeen__Button)
            {
                coords[0] = 8;
                coords[1] = 6;
            }



            else if (thisButton == moEightteen__Button)
            {
                coords[0] = 9;
                coords[1] = 0;
            }
            else if (thisButton == tuEightteen__Button)
            {
                coords[0] = 9;
                coords[1] = 1;
            }
            else if (thisButton == weEightteen__Button)
            {
                coords[0] = 9;
                coords[1] = 2;
            }
            else if (thisButton == thEightteen__Button)
            {
                coords[0] = 9;
                coords[1] = 3;
            }
            else if (thisButton == frEightteen__Button)
            {
                coords[0] = 9;
                coords[1] = 4;
            }
            else if (thisButton == saEightteen__Button)
            {
                coords[0] = 9;
                coords[1] = 5;
            }
            else if (thisButton == suEightteen__Button)
            {
                coords[0] = 9;
                coords[1] = 6;
            }



            else if (thisButton == moNineteen__Button)
            {
                coords[0] = 10;
                coords[1] = 0;
            }
            else if (thisButton == tuNineteen__Button)
            {
                coords[0] = 10;
                coords[1] = 1;
            }
            else if (thisButton == weNineteen__Button)
            {
                coords[0] = 10;
                coords[1] = 2;
            }
            else if (thisButton == thNineteen__Button)
            {
                coords[0] = 10;
                coords[1] = 3;
            }
            else if (thisButton == frNineteen__Button)
            {
                coords[0] = 10;
                coords[1] = 4;
            }
            else if (thisButton == saNineteen__Button)
            {
                coords[0] = 10;
                coords[1] = 5;
            }
            else if (thisButton == suNineteen__Button)
            {
                coords[0] = 10;
                coords[1] = 6;
            }



            else if (thisButton == moTwenty__Button)
            {
                coords[0] = 11;
                coords[1] = 0;
            }
            else if (thisButton == tuTwenty__Button)
            {
                coords[0] = 11;
                coords[1] = 1;
            }
            else if (thisButton == weTwenty__Button)
            {
                coords[0] = 11;
                coords[1] = 2;
            }
            else if (thisButton == thTwenty__Button)
            {
                coords[0] = 11;
                coords[1] = 3;
            }
            else if (thisButton == frTwenty__Button)
            {
                coords[0] = 11;
                coords[1] = 4;
            }
            else if (thisButton == saTwenty__Button)
            {
                coords[0] = 11;
                coords[1] = 5;
            }
            else if (thisButton == suTwenty__Button)
            {
                coords[0] = 11;
                coords[1] = 6;
            }



            else if (thisButton == moTwentyOne__Button)
            {
                coords[0] = 12;
                coords[1] = 0;
            }
            else if (thisButton == tuTwentyOne__Button)
            {
                coords[0] = 12;
                coords[1] = 1;
            }
            else if (thisButton == weTwentyOne__Button)
            {
                coords[0] = 12;
                coords[1] = 2;
            }
            else if (thisButton == thTwentyOne__Button)
            {
                coords[0] = 12;
                coords[1] = 3;
            }
            else if (thisButton == frTwentyOne__Button)
            {
                coords[0] = 12;
                coords[1] = 4;
            }
            else if (thisButton == saTwentyOne__Button)
            {
                coords[0] = 12;
                coords[1] = 5;
            }
            else if (thisButton == suTwentyOne__Button)
            {
                coords[0] = 12;
                coords[1] = 6;
            }

            return coords;
        }
        #endregion
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
