using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GroupProjectW
{
    public class User
    {
        string mFilePpath;
        string mEmail;
        string mPassword;
        string[] mSocieties;
        int mNumBookings;
        List<string> mBookings = new List<string>();

        public User(string pFile)
        {
            mFilePpath = pFile;
            string[] lines = File.ReadAllLines(pFile);
            string[] emailSplit = lines[0].Split(':');
            string[] passwordSplit = lines[1].Split(':');

            mEmail = emailSplit[1];
            mPassword = passwordSplit[1];

            string[] societiesSplit = lines[2].Split(':');
            mSocieties = societiesSplit[1].Split();

            string[] numBookingsSplit = lines[3].Split(':');
            mNumBookings = int.Parse(numBookingsSplit[1]);

            int location = 5;

            for (int i = 0; i < mNumBookings; i++)
            {
                mBookings.Add(lines[i + location]);

            }

        }

        public List<string> bookings
        {
            get
            {
                return this.mBookings;
            }
            set
            {
                this.mBookings = value;
                mNumBookings++;
                saveToFile();
            }
        }

        public string[] societies
        {
            get
            {
                return this.mSocieties;
            }
        }
        public void saveToFile()
        {
            StreamWriter savefile = new StreamWriter(mFilePpath);
            savefile.WriteLine("Email:" + mEmail);
            savefile.WriteLine("Password:" + mPassword);
            string societySav = "Societies:";
            for (int i = 0; i < mSocieties.Length; i++)
            {
                societySav = societySav + " " + mSocieties[i];
            }
            savefile.WriteLine(societySav);
            savefile.WriteLine("NumBookings:" + mNumBookings);
            savefile.WriteLine("Bookings:");
            for (int j = 0; j < mNumBookings; j++)
            {
                savefile.WriteLine(mBookings[j]);
            }

            savefile.Close();

        }

    }
}
