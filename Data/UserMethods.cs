using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Overshare.Data
{
    public class UserMethods
    {

        public static User Authenticate(string email, string password)
        {
            email = CleanInput(email);
            password = CleanInput(password);

            if (IsAuthenticated(email, password))
            {
                // Query the database to retrieve user details and return a User object
                return UserController.GetUserByEmail(email);
            }

            return null;
        }

        private static string CleanInput(string input)
        {
            return input.Trim();
        }

        private static string GetStoredHashedPassword(string email)
        {
            // Implement database query to fetch the hashed password for the provided email
            string connectionString = WebConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Password FROM UserInformation a JOIN Users b on a.UserID = b.UserId WHERE a.Email = @Email";
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    return command.ExecuteScalar() as string;
                }
            }
        }

        public static bool IsAuthenticated(string email, string password)
        {
            // Fetch the hashed password associated with the provided email from the database
            string storedHashedPassword = GetStoredHashedPassword(email);

            if (string.IsNullOrWhiteSpace(storedHashedPassword))
            {
                // User with the provided email does not exist
                return false;
            }

            // Verify the provided password by comparing it to the stored hashed password
            return BCrypt.Net.BCrypt.Verify(password, storedHashedPassword);
        }

        public static bool RegisterUser(string email, string password, string firstName, string lastName)
        {
            // Hash the user's password
            string hashedPassword = Hasher.HashPassword(password);

            // Generate a new unique user ID
            Guid newUserID = User.GenerateUniqueUserId();

            // Perform the database insertion using a transaction
            string connectionString = WebConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Step 1: Insert user data into the Users table
                    using (SqlCommand userInsertCommand = new SqlCommand("INSERT INTO Users (UserID, Password) VALUES (@UserID, @Username, @Password)", connection, transaction))
                    {
                        userInsertCommand.Parameters.AddWithValue("@UserID", newUserID);
                        userInsertCommand.Parameters.AddWithValue("@Password", hashedPassword);
                        userInsertCommand.ExecuteNonQuery();
                    }

                    // Step 2: Insert user account data into the UserAccount table
                    using (SqlCommand accountInsertCommand = new SqlCommand("INSERT INTO UserAccount (UserID, PlanID, AccountStatus, SubscriptionStartDate, SubscriptionEndDate, PreferredLanguage, FilesUploaded) VALUES (@UserID, @PlanID, @AccountStatus, @SubscriptionStartDate, @SubscriptionEndDate, @PreferredLanguage, @FilesUploaded)", connection, transaction))
                    {
                        accountInsertCommand.Parameters.AddWithValue("@UserID", newUserID);
                        accountInsertCommand.Parameters.AddWithValue("@PlanID", 1); 
                        accountInsertCommand.Parameters.AddWithValue("@AccountStatus", "Active"); 
                        accountInsertCommand.Parameters.AddWithValue("@SubscriptionStartDate", DateTime.Now);
                        accountInsertCommand.Parameters.AddWithValue("@SubscriptionEndDate", DateTime.Now.AddMonths(1)); // 1-month subscription
                        accountInsertCommand.Parameters.AddWithValue("@PreferredLanguage", "English");
                        accountInsertCommand.Parameters.AddWithValue("@FilesUploaded", 0);
                        accountInsertCommand.ExecuteNonQuery();
                    }

                    // Step 3: Insert user information into the UserInformation table
                    using (SqlCommand infoInsertCommand = new SqlCommand("INSERT INTO UserInformation (UserID, FirstName, LastName, Email, RegistrationDate) VALUES (@UserID, @FirstName, @LastName, @Email, @RegistrationDate)", connection, transaction))
                    {
                        infoInsertCommand.Parameters.AddWithValue("@UserID", newUserID);
                        infoInsertCommand.Parameters.AddWithValue("@FirstName", firstName);
                        infoInsertCommand.Parameters.AddWithValue("@LastName", lastName);
                        infoInsertCommand.Parameters.AddWithValue("@Email", email);
                        infoInsertCommand.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);
                        infoInsertCommand.ExecuteNonQuery();
                    }

                    // Commit the transaction
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    // An error occurred, rollback the transaction
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}