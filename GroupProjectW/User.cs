using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GroupProjectW
{
    class User
    {
        string mEmail;
        string mPassword;
        string[] mSocieties;
        string[] mBookings;

        public User(string pFile)
        {
            string[] lines = File.ReadAllLines(pFile);
            string[] emailSplit = lines[0].Split(':');
            string[] passwordSplit = lines[1].Split(':');

            mEmail = emailSplit[1];
            mPassword = passwordSplit[1];


        }
    }
}
