using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace Overshare.Data
{
    [Authorize]
    public class Drive
    {
        public static string DisplayGreetingMessage()
        {
            DateTime currentTime = DateTime.Now;
            int currentHour = currentTime.Hour;

            if (currentHour >= 5 && currentHour < 12)
            {
                return "Good Morning";
            }
            else if (currentHour >= 12 && currentHour < 18)
            {
                return "Good Afternoon";
            }
            else
            {
                return "Good Evening";
            }
        }

        public static void UploadFile(HttpPostedFile file)
        {
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    // Get file details
                    string fileName = Path.GetFileName(file.FileName);
                    string fileExtension = Path.GetExtension(fileName);
                    long fileSize = file.ContentLength;

                    // Save the file to a location on the server
                    string uploadFolder = HttpContext.Current.Server.MapPath("~/Uploads/");
                    string filePath = Path.Combine(uploadFolder, fileName);
                    file.SaveAs(filePath);

                    // Store file details in the database
                    StoreFileDetails(fileName, fileExtension, fileSize, filePath);

                    // You can return a success message or perform additional actions as needed
                    Console.WriteLine("File uploaded successfully!");
                }
                catch (Exception ex)
                {
                    // Handle exceptions (log, display an error message, etc.)
                    Console.WriteLine("Error uploading file: " + ex.Message);
                }
            }
        }

        [WebMethod]
        private static void StoreFileDetails(string fileName, string fileExtension, long fileSize, string filePath)
        {
            // Connection string from Web.config
            string connectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Store file details in the database
                string query = "INSERT INTO FileDetails (FileName, FileExtension, FileSize, FilePath, UploadDate) " +
                               "VALUES (@FileName, @FileExtension, @FileSize, @FilePath, @UploadDate)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FileName", fileName);
                    command.Parameters.AddWithValue("@FileExtension", fileExtension);
                    command.Parameters.AddWithValue("@FileSize", fileSize);
                    command.Parameters.AddWithValue("@FilePath", filePath);
                    command.Parameters.AddWithValue("@UploadDate", DateTime.Now);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}