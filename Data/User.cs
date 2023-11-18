﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Web;
using System.Web.Configuration;

namespace Overshare.Data
{
    public class User
    {
        public Guid UserID { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public UserAccount Account { get; set; }

        public User(Guid userID, string username, string firstName, string lastName, string email, DateTime registrationDate)
        {
            UserID = userID;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            RegistrationDate = registrationDate;
            Account = new UserAccount(userID);
        }

        public string GetUserProfileUrl()
        {
            return $"{HttpContext.Current.Server.MapPath("~")}//src//Users//{UserID}//profile.svg";
        }

        public User()
        {

        }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public static Guid GenerateUniqueUserId()
        {
            byte[] guidBytes = new byte[16];

            byte[] timestampBytes = BitConverter.GetBytes(DateTime.UtcNow.Ticks);

            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(guidBytes, 12, 4); 
            }
            Array.Copy(timestampBytes, 0, guidBytes, 0, 8);

            guidBytes[7] = (byte)((guidBytes[7] & 0x0F) | 0x10); // Version 1
            guidBytes[8] = (byte)((guidBytes[8] & 0x3F) | 0x80); // Variant 2

            return new Guid(guidBytes);
        }

        public string GetUserPath()
        {
            return Path.Combine(HttpContext.Current.Server.MapPath("~/Users/"), UserID.ToString());
        }

        public List<User> GetShareList()
        {
            var shareListPath = Path.Combine(GetUserPath() + "/shareList.json");

            List<User> uploadedFiles;

            if (File.Exists(shareListPath))
            {
                var json = File.ReadAllText(shareListPath);
                uploadedFiles = JsonSerializer.Deserialize<List<User>>(json);
            }
            else
            {
                uploadedFiles = new List<User>();
            }

            return uploadedFiles;
        }
    }
}