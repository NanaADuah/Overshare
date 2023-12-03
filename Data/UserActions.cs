using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Overshare.Data
{
    public class UserActions
    {
        public DateTime ActionDate { get; set; }
        public string ActionDescription { get; set; }
        public string ActionName { get; set; }

        public static string FormatFileSize(long fileSizeInBytes)
        {
            const long kilobyteThreshold = 1024; // 1 KB
            const long megabyteThreshold = 1024 * 1024; // 1 MB
            const long gigabyteThreshold = 1024 * 1024 * 1024; // 1 GB

            if (fileSizeInBytes >= gigabyteThreshold)
            {
                double sizeInGB = (double)fileSizeInBytes / gigabyteThreshold;
                return $"{sizeInGB:F2} GB";
            }
            else if (fileSizeInBytes >= megabyteThreshold)
            {
                double sizeInMB = (double)fileSizeInBytes / megabyteThreshold;
                return $"{sizeInMB:F2} MB";
            }
            else if (fileSizeInBytes >= kilobyteThreshold)
            {
                double sizeInKB = (double)fileSizeInBytes / kilobyteThreshold;
                return $"{sizeInKB:F2} KB";
            }
            else
            {
                return $"{fileSizeInBytes} Bytes";
            }
        }
    }
}