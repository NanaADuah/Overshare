using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;

namespace Overshare.Data
{
    public class UserAccount
    {
        public int PlanID { get; set; } // Adjust the data type as needed
        public string AccountStatus { get; set; }
        public string PreferredLanguage { get; set; }
        public int FilesUploaded { get; set; }
        public decimal TotalStorageSpaceMB { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime SubscriptionEndDate { get; set; }

        public UserAccount()
        {
  
        }

        public UserAccount(Guid userID)
        {
            LoadInformationFromDatabase(userID);
        }

        private void LoadInformationFromDatabase(Guid userGuid)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT PlanID, AccountStatus, SubscriptionStartDate, SubscriptionEndDate, FilesUploaded, TotalStorageSpaceMB FROM UserAccount WHERE UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userGuid);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            PlanID = (int)reader["PlanID"];
                            AccountStatus = reader["AccountStatus"].ToString();
                            SubscriptionStartDate = (DateTime)reader["SubscriptionStartDate"];
                            SubscriptionEndDate = (DateTime)reader["SubscriptionEndDate"];
                            TotalStorageSpaceMB = (decimal)reader["TotalStorageSpaceMB"];
                            FilesUploaded = (int)reader["FilesUploaded"];
                        }
                    }
                }
            }
        }

        public long GetAccountPlanSize()
        {
            return 5 * (1024*1024);
        }

        public static void EnsureUserFolderAndFilesExist(Guid userID)
        {
            string userFolder = Path.Combine(HttpContext.Current.Server.MapPath("~/Users/"), userID.ToString());

            if (!Directory.Exists(userFolder))
                CreateDirectoryAndFiles(userFolder, userID);
            else
                EnsureUserFilesExist(userID);
        }

        private static void CreateDirectoryAndFiles(string userFolder, Guid userID)
        {
            Directory.CreateDirectory(userFolder);
            Directory.CreateDirectory($"{userFolder}/Files");

            CreateJsonFile(userFolder, "uploadedFiles.json", new List<FileDetails>(){ });
            CreateJsonFile(userFolder, "userActions.json", new List<UserActions>(){ });
            CreateJsonFile(userFolder, "shareList.json", new List<User>(){ });
            SaveDefaultProfileImage(userID);
        }

        private static void EnsureUserFilesExist(Guid userID)
        {
            string userFolder = Path.Combine(HttpContext.Current.Server.MapPath("~/Users/"), userID.ToString());
            string uploadedFilesPath = Path.Combine(userFolder, "uploadedFiles.json");
            string userActionsPath = Path.Combine(userFolder, "userActions.json");
            string shareListPath = Path.Combine(userFolder, "shareList.json");
            string profileImagePath = Path.Combine(userFolder, "profile.svg");

            if (!File.Exists(uploadedFilesPath))
            {
                // Create JSON file for uploaded files if it doesn't exist
                File.WriteAllText(uploadedFilesPath, "[]");
            }

            if (!File.Exists(userActionsPath))
            {
                // Create JSON file for user actions if it doesn't exist
                File.WriteAllText(userActionsPath, "[]");
            }

            if (!File.Exists(shareListPath))
            {
                // Create JSON file for user actions if it doesn't exist
                File.WriteAllText(shareListPath, "[]");
            }

            var defaultProfileSvgCode = "<svg width=\"30\" height=\"30\" viewBox=\"0 0 30 30\" fill=\"none\" xmlns=\"http://www.w3.org/2000/svg\">\n" +
                                "  <path d=\"M25 26.25V23.75C25 22.4239 24.4732 21.1521 23.5355 20.2145C22.5979 19.2768 21.3261 18.75 20 18.75H10C8.67392 18.75 7.40215 19.2768 6.46447 20.2145C5.52678 21.1521 5 22.4239 5 23.75V26.25\" stroke=\"black\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\"/>\n" +
                                "  <path d=\"M15 13.75C17.7614 13.75 20 11.5114 20 8.75C20 5.98858 17.7614 3.75 15 3.75C12.2386 3.75 10 5.98858 10 8.75C10 11.5114 12.2386 13.75 15 13.75Z\" stroke=\"black\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\"/>\n" +
                                "</svg>";

            if (!File.Exists(profileImagePath))
            {
                // Create image if it doesn't exist
                File.WriteAllText(profileImagePath, defaultProfileSvgCode);
            }
        }

        private static void CreateJsonFile<T>(string folderPath, string fileName, T data)
        {
            string filePath = Path.Combine(folderPath, fileName);
            string jsonData = new JavaScriptSerializer().Serialize(data);

            File.WriteAllText(filePath, jsonData);
        }

        private static void SaveDefaultProfileImage(Guid userId)
        {
            var userFolder = Path.Combine(HttpContext.Current.Server.MapPath("~/Users/"), userId.ToString());
            var defaultProfileSvgCode = "<svg width=\"30\" height=\"30\" viewBox=\"0 0 30 30\" fill=\"none\" xmlns=\"http://www.w3.org/2000/svg\">\n" +
                                        "  <path d=\"M25 26.25V23.75C25 22.4239 24.4732 21.1521 23.5355 20.2145C22.5979 19.2768 21.3261 18.75 20 18.75H10C8.67392 18.75 7.40215 19.2768 6.46447 20.2145C5.52678 21.1521 5 22.4239 5 23.75V26.25\" stroke=\"black\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\"/>\n" +
                                        "  <path d=\"M15 13.75C17.7614 13.75 20 11.5114 20 8.75C20 5.98858 17.7614 3.75 15 3.75C12.2386 3.75 10 5.98858 10 8.75C10 11.5114 12.2386 13.75 15 13.75Z\" stroke=\"black\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\"/>\n" +
                                        "</svg>";

            // Save the default profile SVG code to the user's folder
            var userDefaultProfileImagePath = Path.Combine(userFolder, "profile.svg");
            File.WriteAllText(userDefaultProfileImagePath, defaultProfileSvgCode);
        }
    }
}