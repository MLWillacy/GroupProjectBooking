﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GroupProjectW
{
    class User
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
    }
}
