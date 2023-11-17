using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState;

namespace Overshare.Data
{
    public class UserController
    {
        public static User GetUserByEmail(string email)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT i.UserID, Email, FirstName, LastName, RegistrationDate FROM Users e INNER JOIN UserInformation i ON e.UserID = i.UserID WHERE Email = @Email";
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Retrieve user data
                            Guid userID = (Guid)reader["UserID"];
                            string userEmail = (string)reader["Email"];
                            string firstName = (string)reader["FirstName"];
                            string lastName = (string)reader["LastName"];
                            DateTime registrationDate = (DateTime)reader["RegistrationDate"];

                            // Create a User object and return it
                            return new User
                            {
                                UserID = userID,
                                Email = userEmail,
                                FirstName = firstName,
                                LastName = lastName,
                                RegistrationDate = registrationDate,

                            };
                        }
                    }
                }
            }

            // User with the provided email not found
            return null;
        }

        public void StoreUserInSession(User user)
        {
            if (user != null)
            {
                HttpSessionState session = HttpContext.Current.Session;
                session["CurrentUser"] = user;
            }
        }
    }

}