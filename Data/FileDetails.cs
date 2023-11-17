using System;
using System.Collections.Generic;
using System.Linq;
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