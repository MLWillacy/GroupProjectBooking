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
    /// Interaction logic for MakeBookingPage.xaml
    /// </summary>
    public partial class MakeBookingPage : Page
    {
        Frame mMain = null;

        //Room Selection Variables
        int mRoomSelected = 1;
        bool isRoomSelected = false;
        int numRooms = 12;
        int mRoomPage = 1;

        //Timetable Variables
        int mWeek = 1;
        int[,] bookings = new int[14, 7]; //states represented by int (0 = available, 1 = currently booking, 2 = unavailable)
        bool startTimeSelected = false;
        bool endTimeSelected = false;
        int[] startTimeCoord = new int[2];
        int[] endTimeCoord = new int[2];

        //Confrirmation Sheet Variables
        string mSociety = "enter";
        int mNumPeople = 0;
        string mReason = "enter";
        bool confirmationValid = false;

        //Continue button
        int continueStep = 0;


        public MakeBookingPage(Frame main)
        {
            mMain = main;
            continueStep = 0;

            InitializeComponent();

            initialiseRoomButtons();
            mRoomSelected = 1;
            isRoomSelected = false;

            SelectDate_Panel.Visibility = Visibility.Hidden;
            updateTimetableUI();
            mWeek = 1;
            startTimeSelected = false;
            endTimeSelected = false;

            mSociety = "enter";
            mReason = "enter";
            mNumPeople = 0;

            finalConfirmation_panel.Visibility = Visibility.Hidden;
        }

        #region Room Select Functions
        /*********************************************************************************
         * Room Select Functions
        *********************************************************************************/
        private System.Windows.Controls.Label int2RoomLabel(int roomNum)
        {
            System.Windows.Controls.Label correspondingLabel = null;

            if (roomNum == 1)
            { correspondingLabel = Room1_Title; }
            else if (roomNum == 2)
            { correspondingLabel = Room2_Title; }
            else if (roomNum == 3)
            { correspondingLabel = Room3_Title; }
            else if (roomNum == 4)
            { correspondingLabel = Room4_Title; }
            else if (roomNum == 5)
            { correspondingLabel = Room5_Title; }
            else if (roomNum == 6)
            { correspondingLabel = Room6_Title; }
            else if (roomNum == 7)
            { correspondingLabel = Room7_Title; }
            else if (roomNum == 8)
            { correspondingLabel = Room8_Title; }
            else if (roomNum == 9)
            { correspondingLabel = Room9_Title; }
            else if (roomNum == 10)
            { correspondingLabel = Room10_Title; }
            else if (roomNum == 11)
            { correspondingLabel = Room11_Title; }
            else if (roomNum == 12)
            { correspondingLabel = Room12_Title; }

            return correspondingLabel;
        }
        private void initialiseRoomButtons()
        {
            for (int i = 1; i < numRooms + 1; i++)
            {
                System.Windows.Controls.Label thisRoom = int2RoomLabel(i);
                thisRoom.Background = Brushes.WhiteSmoke;
            }
            updateRoomPage();
        }

        private void roomSelected(int room)
        {
            mRoomSelected = room;
            initialiseRoomButtons();
            System.Windows.Controls.Label thisRoom = int2RoomLabel(room);
            thisRoom.Background = Brushes.LawnGreen;
            isRoomSelected = true;
            updateConfirmedRoom();
        }
        private void Room1_Button_Clicked(object sender, RoutedEventArgs e)
        {
            roomSelected(1);
        }
        private void Room2_Button_Clicked(object sender, RoutedEventArgs e)
        {
            roomSelected(2);
        }
        private void Room3_Button_Clicked(object sender, RoutedEventArgs e)
        {
            roomSelected(3);
        }
        private void Room4_Button_Clicked(object sender, RoutedEventArgs e)
        {
            roomSelected(4);
        }
        private void Room5_Button_Clicked(object sender, RoutedEventArgs e)
        {
            roomSelected(5);
        }
        private void Room6_Button_Clicked(object sender, RoutedEventArgs e)
        {
            roomSelected(6);
        }
        private void Room7_Button_Clicked(object sender, RoutedEventArgs e)
        {
            roomSelected(7);
        }
        private void Room8_Button_Clicked(object sender, RoutedEventArgs e)
        {
            roomSelected(8);
        }
        private void Room9_Button_Clicked(object sender, RoutedEventArgs e)
        {
            roomSelected(9);
        }
        private void Room10_Button_Clicked(object sender, RoutedEventArgs e)
        {
            roomSelected(10);
        }
        private void Room11_Button_Clicked(object sender, RoutedEventArgs e)
        {
            roomSelected(11);
        }
        private void Room12_Button_Clicked(object sender, RoutedEventArgs e)
        {
            roomSelected(12);
        }

        private void initialiseRoomPageIcons()
        {
            roomPage1_Image.Source = new BitmapImage(new Uri(@"/Images/RoomsOffPageIcon.png", UriKind.Relative));
            roomPage2_Image.Source = new BitmapImage(new Uri(@"/Images/RoomsOffPageIcon.png", UriKind.Relative));
            roomPage3_Image.Source = new BitmapImage(new Uri(@"/Images/RoomsOffPageIcon.png", UriKind.Relative));
            pickRoom_panel.Visibility = Visibility.Hidden;
            pickRoom_panel2.Visibility = Visibility.Hidden;
            pickRoom_panel3.Visibility = Visibility.Hidden;
        }
        private void updateRoomPage()
        {
            initialiseRoomPageIcons();
            if (mRoomPage == 1)
            {
                roomPage1_Image.Source = new BitmapImage(new Uri(@"/Images/RoomsOnPageIcon.png", UriKind.Relative));
                pickRoom_panel.Visibility = Visibility.Visible;
            }
            else if (mRoomPage == 2)
            {
                roomPage2_Image.Source = new BitmapImage(new Uri(@"/Images/RoomsOnPageIcon.png", UriKind.Relative));
                pickRoom_panel2.Visibility = Visibility.Visible;
            }
            else if (mRoomPage == 3)
            {
                roomPage3_Image.Source = new BitmapImage(new Uri(@"/Images/RoomsOnPageIcon.png", UriKind.Relative));
                pickRoom_panel3.Visibility = Visibility.Visible;
            }
            roomPageIcons_panel.Visibility = Visibility.Visible;
        }
        private void roomPage1_Button_Clicked(object sender, RoutedEventArgs e)
        {
            mRoomPage = 1;
            updateRoomPage();
        }
        private void roomPage2_Button_Clicked(object sender, RoutedEventArgs e)
        {
            mRoomPage = 2;
            updateRoomPage();
        }
        private void roomPage3_Button_Clicked(object sender, RoutedEventArgs e)
        {
            mRoomPage = 3;
            updateRoomPage();
        }
        #endregion

        #region Timetable Functions
        /*********************************************************************************
         * Update Timetable Functions
        *********************************************************************************/
        private void updateTimetableUI()
        {
            initialiseTimetable();
            bookings[4, 0] = 2;
            bookings[12, 6] = 2;
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


        private void timetableButtonPressed(System.Windows.Controls.Button thisButton)
        {
            if (thisButton.Background != Brushes.Red)
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
                    { changeStartTime(thisButton); }
                }
                else
                { changeStartTime(thisButton); }
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
                if (BookingsCoord2Button(startTimeCoord[0] + i, day).Background == Brushes.Red)
                {
                    clashes = true;
                    break;
                }
            }
            return clashes;
        }

        private void readBookingsFromDatabase()
        {
            string[] lines = File.ReadAllLines("Rooms/Room" + mRoomSelected + ".txt");
            int numWeeks = 3; //change after prototype phase

            for (int i = 1; i < numWeeks + 1; i++)
            {
                if (i == mWeek)
                {
                    int location = (i * 8) + 2;
                    for (int j = 0; j < 7; j++)
                    {
                        string thisLine = lines[location + j];
                        string[] thisLineSplit = thisLine.Split();
                        for (int k = 0; k < 12; k++)
                        { bookings[k,j] = int.Parse(thisLineSplit[k + 1]); }
                    }
                }
            }
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

        #region Booking Sheet Functions
        /*********************************************************************************
         * Booking Sheet Functions
        *********************************************************************************/
        private void initialiseBookingSheet()
        {
            confirmBooking_panel.Visibility = Visibility.Visible;
            confirmName_label.Content = "firstname surname";
            confirmDate_label.Content = button2Date(BookingsCoord2Button(startTimeCoord[0], startTimeCoord[1]));

            string endTime = Button2Time(BookingsCoord2Button(endTimeCoord[0], endTimeCoord[1]));
            int timeToUpdate = int.Parse(endTime.Substring(0, 2));
            int timeUpdated = timeToUpdate + 1;
            endTime = timeUpdated.ToString() + endTime.Substring(2);

            confirmTime_label.Content = Button2Time(BookingsCoord2Button(startTimeCoord[0], startTimeCoord[1])) + " - " + endTime;

            confirmSociety_text.Text = mSociety;
            confirmNumPeople_text.Text = mNumPeople.ToString();
            confirmReason_text.Text = mReason;
        }
        private void confirmReason_text_changed(object sender, TextChangedEventArgs e)
        {
            mReason = confirmReason_text.Text;
        }
        private void confirmSociety_text_changed(object sender, TextChangedEventArgs e)
        {
            mSociety = confirmSociety_text.Text;
        }
        private void confirmNumPeople_text_changed(object sender, TextChangedEventArgs e)
        {
            try { mNumPeople = int.Parse(confirmNumPeople_text.Text); }
            catch { confirmationValid = false; }
        }
        private void validateConfirmationSheet() //******************************************************************************************************************************* needs adding
        {
            confirmationValid = true;

            try { mNumPeople = int.Parse(confirmNumPeople_text.Text); }
            catch { confirmationValid = false; }
        }
        #endregion

        #region Final Confirmation Functions
        /*********************************************************************************
        * Final Confirmation Functions
        *********************************************************************************/
        private void sendEmail_Button_Clicked(object sender, RoutedEventArgs e)
        {
            emailSent_label.Visibility = Visibility.Visible;
        }
        private void saveToDataBase()
        {
            string[] lines = File.ReadAllLines("Rooms/Room" + mRoomSelected + ".txt");
            int numWeeks = 3; //change after prototype phase

            for (int i = 1; i < numWeeks + 1; i++)
            {
                if (i == mWeek)
                {
                    int location = (i * 8) + 2;
                    for (int j = 0; j < 7; j++)
                    {
                        string thisLine = "";
                        for (int k = 0; k < 12; k++)
                        {
                            if (bookings[k,j] == 1) { bookings[k, j] = 2; }
                            thisLine = thisLine + " " + bookings[k, j];
                            
                        }
                        lines[location + j] = thisLine;
                    }
                }
            }

            File.WriteAllLines("Rooms/Room" + mRoomSelected + ".txt", lines);

        }
        #endregion

        #region Continue Button Functions
        /*********************************************************************************
         * Continue Button Functions
        *********************************************************************************/

        private void continue_Button_Clicked(object sender, RoutedEventArgs e)
        {
            if (isRoomSelected == true)
            {
                if (continueStep == 0)
                {
                    pickRoom_panel.Visibility = Visibility.Hidden;
                    pickRoom_panel2.Visibility = Visibility.Hidden;
                    pickRoom_panel3.Visibility = Visibility.Hidden;
                    roomPageIcons_panel.Visibility = Visibility.Hidden;
                    SelectDate_Panel.Visibility = Visibility.Visible;
                    back__Button.Visibility = Visibility.Visible;
                    updateEndTimeText(BookingsCoord2Button(endTimeCoord[0], endTimeCoord[1]));
                }
                else if (endTimeSelected == true)
                {
                    if (continueStep == 1)
                    {
                        SelectDate_Panel.Visibility = Visibility.Hidden;
                        saveTempBooking();
                        continue__Button.Content = "Continue";
                        initialiseBookingSheet();
                    }
                    else if (continueStep == 2)
                    {
                        validateConfirmationSheet();
                        if (confirmationValid)
                        {
                            confirmBooking_panel.Visibility = Visibility.Hidden;
                            confirmBooking_panel.Visibility = Visibility.Hidden;
                            finalConfirmation_panel.Visibility = Visibility.Visible;
                            back__Button.Visibility = Visibility.Hidden;
                            saveToDataBase();
                            continue__Button.Content = "Create another booking";
                        }
                        else
                        { continueStep--; }
                    }
                    else if (continueStep == 3)
                    {
                        mMain.Content = new MakeBookingPage(mMain);
                    }
                    if (continueStep < 4)
                    { continueStep++; }
                }
                if (continueStep == 0)
                { continueStep++; }
            }
        }
        private void back_Button_Clicked(object sender, RoutedEventArgs e)
        {
            if (continueStep == 1)
            {
                SelectDate_Panel.Visibility = Visibility.Hidden;
                updateRoomPage();
                back__Button.Visibility = Visibility.Hidden;
                updateConfirmedRoom();
                roomPageIcons_panel.Visibility = Visibility.Visible;
            }
            else if (continueStep == 2)
            {
                SelectDate_Panel.Visibility = Visibility.Visible;
                confirmBooking_panel.Visibility = Visibility.Hidden;
            }
            else if (continueStep == 3)
            {
                initialiseBookingSheet();
            }
            if (continueStep != 0)
            { continueStep--; }
        }

        private void updateConfirmedRoom()
        {
            continue__Button.Content = "Confirm: Room" + mRoomSelected;
        }

        private void saveTempBooking()
        {
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (BookingsCoord2Button(i, j).Background == Brushes.Yellow)
                    {
                        bookings[i, j] = 1;
                    }
                }
            }
        }
        private void updateStartTimeText(System.Windows.Controls.Button thisButton)
        {
            if (!startTimeSelected)
            {
                continue__Button.Content = "Confirm: startTime - endTime";
            }
            else
            {
                string date = button2Date(thisButton);
                string startTime = Button2Time(thisButton);
                continue__Button.Content = "Confirm: " + date + " " + startTime + " - endTime";
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


                continue__Button.Content = "Confirm: " + date + " " + startTime + " - " + endTime;
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
            mMain.Content = new CurrentBookingsPage(mMain);
        }

        private void MakeBooking_Button_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void ViewRooms_Button_Clicked(object sender, RoutedEventArgs e)
        {
            mMain.Content = new ViewRoomsPage(mMain);
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









