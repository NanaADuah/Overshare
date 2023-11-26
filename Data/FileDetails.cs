using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Web;

namespace Overshare.Data
{
    public class FileDetails
    {
        public Guid FileID { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public long FileSize { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }
        public User Owner { get; set; }

        public FileDetails()
        {
            
        }

        public static FileDetails GetSharedFileDetails(Guid fileID, Guid userID)
        {
            return null;
        }

        public static FileDetails GetUsersFileDetails(Guid FileID, User user)
        {
            string filePath = Path.Combine(user.GetUserPath() + "/uploadedFiles.json");

            // Read the JSON content from the file
            string jsonContent = File.ReadAllText(filePath);

            // Deserialize the JSON into a list of UploadedFile objects
            List<FileDetails> uploadedFiles = JsonSerializer.Deserialize<List<FileDetails>>(jsonContent);

            // Use LINQ to find the file details based on the fileId
            FileDetails foundFile = uploadedFiles.FirstOrDefault(file => file.FileID == FileID);

            // Create and return a FileDetails object if the file is found
            if (foundFile != null)
            {
                return new FileDetails
                {
                    FileID = foundFile.FileID,
                    FileName = foundFile.FileName,
                    FileSize = foundFile.FileSize,
                    UploadDate = foundFile.UploadDate,
                    FilePath = foundFile.FilePath,
                    Owner = foundFile.Owner,

                    // Assign other properties if needed...
                };
            }
            else
            {
                // Handle the case where the file with the given fileId is not found
                // You might want to throw an exception or handle it accordingly
                return null;
            }
        }

        public static Guid GenerateFileId()
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
    }
}