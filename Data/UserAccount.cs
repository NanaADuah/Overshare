using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

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
                            SubscriptionStartDate = (DateTime)reader["RegistrationDate"];
                            SubscriptionEndDate = (DateTime)reader["SubscriptionEndDate"];
                            TotalStorageSpaceMB = (decimal)reader["TotalStorageSpaceMB"];
                            FilesUploaded = (int)reader["FilesUploaded"];
                        }
                    }
                }
            }
        }
    }
}