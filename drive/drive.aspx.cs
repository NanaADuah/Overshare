using Microsoft.AspNetCore.Authorization;
using Overshare.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Overshare.drive
{
    [Authorize]
    public partial class drive : System.Web.UI.Page
    {
        protected User user;
        protected UserAccount userAccount;
        protected string TimeGreeting = Drive.DisplayGreetingMessage();
        protected List<FileDetails> files;
        protected int fileCount = 0;
        protected double TotalStorage = 0;
        protected Dictionary<string, int> percentageSizes;
        protected List<User> ShareList;
        protected string CurrentSelectedPage = "Home";
        protected string DisplayFilter  = "All";
        protected Guid CurrentFile;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!IsPostBack)
                {
                    ViewState["sortOrder"] = -1;
                    ViewState["ascOrder"] = true;
                }

                string userEmail = User.Identity.Name;
                user = UserController.GetUserByEmail(userEmail);
                userAccount = new UserAccount(user.UserID);

                if (ViewState["sortOrder"] == null || String.IsNullOrEmpty(ViewState["sortOrder"].ToString()))
                    ViewState["sortOrder"] = 0;

                files = SortFiles(GetUserFiles(DisplayFilter), (int)ViewState["sortOrder"], (bool)ViewState["ascOrder"]);
                percentageSizes = GetFileTypeDistribution(files);
                ValidateUploadedFiles();
                TotalStorage = GetStorageUsedWidth();
                RemoveDuplicateFiles(files);
                fileCount = files.Count;
                ShareList = user.GetShareList();
                changeUser.Value = user.GetFullName();
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        public static List<FileDetails> SortFiles(List<FileDetails> fileList, int columnIndex, bool ascending)
        {
            switch (columnIndex)
            {
                case 0:
                    return ascending
                        ? fileList.OrderBy(f => f.FileName).ToList()
                        : fileList.OrderByDescending(f => f.FileName).ToList();
                case 1:
                    return ascending
                        ? fileList.OrderBy(f => f.UploadDate).ToList()
                        : fileList.OrderByDescending(f => f.UploadDate).ToList();
                case 2:
                    return ascending
                        ? fileList.OrderBy(f => f.Owner.GetFullName()).ToList()
                        : fileList.OrderByDescending(f => f.UploadDate).ToList();
                case 3:
                    return ascending
                        ? fileList.OrderBy(f => f.FileSize).ToList()
                        : fileList.OrderByDescending(f => f.FileSize).ToList();
                default:
                    return fileList;
            }
        }

        public void ChangeName()
        {
            string newName = changeUser.Value.Trim();
            string fullName = Regex.Replace(newName, @"[^a-zA-Z\s]+", "");

            if (!Regex.IsMatch(fullName, @"^[a-zA-Z\s]+$"))
            {
                throw new ArgumentException("Invalid characters in the full name.");
            }
        }

        public long GetUsedStorage()
        {
            long total = 0;
            foreach (var item in files)
            {
                total += item.FileSize;
            }

            return total;
        }

        public int GetStorageUsedWidth()
        {
            var used = (double)GetUsedStorage();
            var usedMB = (used / 1024f) / 1024f; //testing purpose
            var size = (double)userAccount.GetAccountPlanSize();
            var sizeUsed = (size / 1024f) / 1024f; //testing purpose
            var output = (int)Math.Ceiling(used / size);
            return output;
        }

        public void DisplayPage(string CurrentSelectedPage, bool NotificationPreference = false)
        {
            if(CurrentSelectedPage == "Settings")
            {
                favourites.Style["Display"] = "none";
                settings.Style["Display"] = "block";
                viewFile.Style["Display"] = "none";
                shared.Style["Display"] = "none";
                trash.Style["Display"] = "none";
                home.Style["Display"] = "none";

                if(NotificationPreference)
                    sideBarSettingsRadio.Style["Display"] = "block";
                else
                    sideBarSettingsRadio.Style["Display"] = "none";
                upload.Style["Display"] = "none";
            }
            else
            if(CurrentSelectedPage == "Shared"){
                favourites.Style["Display"] = "none";
                settings.Style["Display"] = "none";
                viewFile.Style["Display"] = "none";
                shared.Style["Display"] = "block";
                trash.Style["Display"] = "none";
                home.Style["Display"] = "none";
            }
            else
            if (CurrentSelectedPage == "Trash")
            {
                favourites.Style["Display"] = "none";
                settings.Style["Display"] = "none";
                viewFile.Style["Display"] = "none";
                shared.Style["Display"] = "none";
                trash.Style["Display"] = "block";
                home.Style["Display"] = "none";
            }
            else
            if (CurrentSelectedPage == "Favourites")
            {
                favourites.Style["Display"] = "block";
                settings.Style["Display"] = "none";
                viewFile.Style["Display"] = "none";
                shared.Style["Display"] = "none";
                trash.Style["Display"] = "none";
                home.Style["Display"] = "none";
            }
            else
            if(CurrentSelectedPage == "Files"){
                favourites.Style["Display"] = "none";
                settings.Style["Display"] = "none";
                viewFile.Style["Display"] = "block";
                shared.Style["Display"] = "none";
                trash.Style["Display"] = "none";
                home.Style["Display"] = "none";
            }
            else
            {
                favourites.Style["Display"] = "none";
                settings.Style["Display"] = "none";
                viewFile.Style["Display"] = "none";
                upload.Style["Display"] = "block";
                shared.Style["Display"] = "none";
                trash.Style["Display"] = "none";
                home.Style["Display"] = "block";
                sideBarSettingsRadio.Style["Display"] = "none";
            }
        }
        private void ValidateUploadedFiles()
        {
            var userFolder = Path.Combine(user.GetUserPath() + "/Files/");

            var uploadedFilesPath = Path.Combine(user.GetUserPath() + "/uploadedFiles.json");

            if (!Directory.Exists(userFolder))
            {
                // If the user directory doesn't exist, remove all entries from the JSON file
                if (File.Exists(uploadedFilesPath))
                {
                    File.Delete(uploadedFilesPath);
                }
                return;
            }

            List<FileDetails> uploadedFiles;

            if (File.Exists(uploadedFilesPath))
            {
                var json = File.ReadAllText(uploadedFilesPath);
                uploadedFiles = JsonSerializer.Deserialize<List<FileDetails>>(json);
            }
            else
            {
                return;
            }

            // Create a HashSet for efficient lookup of filenames in the user directory
            var userFileSet = new HashSet<string>(Directory.GetFiles(userFolder).Select(Path.GetFileName), StringComparer.OrdinalIgnoreCase);

            // Validate files in the JSON against the files in the user directory
            for (int i = uploadedFiles.Count - 1; i >= 0; i--)
            {
                var fileName = uploadedFiles[i].FileName;

                if (!userFileSet.Contains(fileName) || !File.Exists(Path.Combine(userFolder, fileName)))
                {
                    // Remove entry from the JSON file if the file doesn't exist in the user directory
                    uploadedFiles.RemoveAt(i);
                }
            }

            // Remove files from the user directory that don't have corresponding entries in the JSON file
            foreach (var userFile in userFileSet)
            {
                if (!uploadedFiles.Any(file => file.FileName.Equals(userFile, StringComparison.OrdinalIgnoreCase)))
                {
                    // File exists in the user directory but not in the JSON file, delete it
                    File.Delete(Path.Combine(userFolder, userFile));
                }
            }

            // Write the updated list back to the file
            var updatedJson = JsonSerializer.Serialize(uploadedFiles);
            File.WriteAllText(uploadedFilesPath, updatedJson);
        }


        public double GetTotalStorage()
        {
            long FileSize = 0;

            foreach (var item in files)
            {
                FileSize += item.FileSize;
            }

            return FileSize;
        }


        [WebMethod]
        public void UploadUserFile()
        {
            if (FileUploads.HasFiles && User.Identity.IsAuthenticated)
            {
                try
                {
                    string folderPath = $"{user.GetUserPath()}/Files/";

                    if (!Directory.Exists(folderPath))
                    {
                        //If folder does not exists. Create it.
                        Directory.CreateDirectory(folderPath);
                    }

                    //save file in the specified folder and path
                    string outputFile = folderPath + Path.GetFileName(FileUploads.FileName);
                    FileUploads.SaveAs(outputFile);
                    TrackUserAction("Upload File", $"Uploaded file: {FileUploads.FileName}");
                    UpdateUploadedFiles(outputFile);
                    Response.Redirect("/drive/drive.aspx?page=home");
                }
                catch (Exception ex)
                {
                    //Log error one day
                    //console.Log(ex.Message);
                }
            } else
            if (!User.Identity.IsAuthenticated) {
                Response.Redirect("~/Login.aspx");
            }
        }
        public Dictionary<string, int> GetFileTypeDistribution(List<FileDetails> files)
        {
            Dictionary<string, long> fileTypeSizes = new Dictionary<string, long>{
                { "Image", 0 },
                { "Video", 0 },
                { "Document", 0 },
                { "Audio", 0 },
                { "Other", 0 }
            };

            long totalSize = 0;

            if (files != null)
                // Calculate total size and individual file type sizes
                foreach (var file in files)
                {
                    totalSize += file.FileSize;

                    // Determine file type based on file extension (you may have a different logic)
                    string fileType = GetFileType(file.FileName);

                    if (fileTypeSizes.ContainsKey(fileType))
                    {
                        fileTypeSizes[fileType] += file.FileSize;
                    }
                    else
                    {
                        fileTypeSizes["other"] += file.FileSize;
                    }
                }

            // Calculate percentages
            Dictionary<string, int> fileTypePercentages = new Dictionary<string, int>();

            foreach (var fileTypeSize in fileTypeSizes)
            {
                int percentage = totalSize == 0 ? 0 : (int)((double)fileTypeSize.Value / totalSize * 100);
                fileTypePercentages.Add(fileTypeSize.Key, percentage);
            }

            return fileTypePercentages;
        }

        private void TrackUserAction(string action, string description)
        {
            var userActionsPath = Path.Combine(user.GetUserPath() + "/userActions.json");

            List<UserActions> userActions;

            if (File.Exists(userActionsPath))
            {
                var json = File.ReadAllText(userActionsPath);
                userActions = JsonSerializer.Deserialize<List<UserActions>>(json);
            }
            else
            {
                userActions = new List<UserActions>();
            }

            userActions.Add(new UserActions
            {
                ActionDate = DateTime.UtcNow,
                ActionDescription = description,
                ActionName = action,
            });

            var updatedJson = JsonSerializer.Serialize(userActions);
            File.WriteAllText(userActionsPath, updatedJson);
        }

        private List<FileDetails> GetUserFiles(string DisplayFilter = "All")
        {
            var uploadedFilesPath = Path.Combine(user.GetUserPath(), "uploadedFiles.json");

            if (!File.Exists(uploadedFilesPath))
            {
                return new List<FileDetails>();
            }

            var json = File.ReadAllText(uploadedFilesPath);
            var uploadedFiles = JsonSerializer.Deserialize<List<FileDetails>>(json);

            if (DisplayFilter == "All")
            {
                // Return all files if the display filter is set to "All"
                return uploadedFiles;
            }
            else
            {
                // Use LINQ to filter files based on the display filter (file extension)
                return uploadedFiles
                    .Where(file => GetFileType(file.FileName)?.ToLower() == DisplayFilter.ToLower())
                    .ToList();
            }
        }

        private void UpdateUploadedFiles(string outputFile)
        {
            var uploadedFilesPath = Path.Combine(user.GetUserPath() + "/uploadedFiles.json");

            var newFile = new FileDetails
            {
                FileID = FileDetails.GenerateFileId(),
                FileName = FileUploads.FileName,
                FileExtension = Path.GetExtension(FileUploads.FileName),
                FileSize = new FileInfo(outputFile).Length ,
                FilePath = $"{user.GetUserPath()}/Files/{FileUploads.FileName}",
                UploadDate = DateTime.UtcNow,
                Owner = user,
            };

            List<FileDetails> uploadedFiles;

            if (File.Exists(uploadedFilesPath))
            {
                var json = File.ReadAllText(uploadedFilesPath);
                uploadedFiles = JsonSerializer.Deserialize<List<FileDetails>>(json);
            }
            else
            {
                uploadedFiles = new List<FileDetails>();
            }

            uploadedFiles.Add(newFile);

            var updatedJson = JsonSerializer.Serialize(uploadedFiles);
            File.WriteAllText(uploadedFilesPath, updatedJson);
        }

        private void RemoveDuplicateFiles(List<FileDetails> uploadedFiles)
        {
            // Use a HashSet to efficiently check for duplicate filenames
            var uniqueFileNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // Iterate in reverse to remove duplicates while preserving order
            for (int i = uploadedFiles.Count - 1; i >= 0; i--)
            {
                if (!uniqueFileNames.Add(uploadedFiles[i].FileName))
                {
                    // Duplicate found, remove it
                    uploadedFiles.RemoveAt(i);
                }
            }
        }

        public void ViewFile(Guid FileID)
        {
            CurrentFile = FileID;
            favourites.Style["Display"] = "none";
            settings.Style["Display"] = "none";
            viewFile.Style["Display"] = "block";
            shared.Style["Display"] = "none";
            trash.Style["Display"] = "none";
            home.Style["Display"] = "none";
            
            FileDetails fileInfo = FileDetails.GetUsersFileDetails(FileID, user);
            Response.Redirect(Page.Request.Url.ToString(), true);
        }

        public static string SanitizePath(string path)
        {
            var lastBackslash = path.LastIndexOf('\\');

            var dir = path.Substring(0, lastBackslash);
            var file = path.Substring(lastBackslash, path.Length - lastBackslash);

            foreach (var invalid in Path.GetInvalidFileNameChars())
            {
                file = file.Replace(invalid, '_');
            }

            return dir + file;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            UploadUserFile();
        }

        public static string GetFileType(string file)
        {
            string fileExtension = Path.GetExtension(file);
            string[] imageExtensions = { ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".svg" };
            string[] videoExtensions = { ".mp4", ".avi", ".mkv", ".mov", ".wmv" };
            string[] documentExtensions = { ".pdf", ".doc", ".docx", ".ppt", ".pptx", ".xls", ".xlsx" };
            string[] audioExtensions = { ".mp3", ".wav", ".ogg", ".flac" };

            if (imageExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase))
            {
                return "Image";
            }
            else if (videoExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase))
            {
                return "Video";
            }
            else if (documentExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase))
            {
                return "Document";
            }
            else if (audioExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase))
            {
                return "Audio";
            }
            else
            {
                return "Other";
            }
        }

        protected void btnViewSettings_Click(object sender, EventArgs e)
        {
         //   SetView("settings");
            CurrentSelectedPage = "Settings";
            DisplayPage(CurrentSelectedPage);

        }

        protected void btnViewTrash_Click(object sender, EventArgs e)
        {
           // SetView("trash");
            CurrentSelectedPage = "Trash";
            DisplayPage(CurrentSelectedPage);

        }

        protected void btnViewFavourites_Click(object sender, EventArgs e)
        {
           // SetView("favourites");
            CurrentSelectedPage = "Favourites";
            DisplayPage(CurrentSelectedPage);

        }

        protected void btnViewShared_Click(object sender, EventArgs e)
        {
          //  SetView("shared");
            CurrentSelectedPage = "Shared";
            DisplayPage(CurrentSelectedPage);

        }

        protected void btnViewHome_Click(object sender, EventArgs e)
        {
            //SetView();
            CurrentSelectedPage = "Home";
            DisplayPage(CurrentSelectedPage);
        }

        public void SetView(string activeDiv = "Home")
        {
            // Define the div names and their corresponding boolean values
            Dictionary<string, bool> divStates = new Dictionary<string, bool>{
                { "home", false },
                { "shared", false },
                { "favourites", false },
                { "settings", false },
                { "trash", false }
            };

            // Enable the specified div and disable others
            if (divStates.ContainsKey(activeDiv))
            {
                divStates[activeDiv] = true;
            }

            // Now you can use 'divStates' to perform actions based on the enabled/disabled status of each div
            foreach (var divState in divStates)
            {
                string divName = divState.Key;
                bool isEnabled = divState.Value;

                var div = Page.FindControl(divName) as HtmlGenericControl;
                if (div != null)
                {
                    if (!isEnabled)
                    {
                        div.Style.Add(HtmlTextWriterStyle.Display, "none");
                        div.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
                    }
                    else
                        div.Style.Add(HtmlTextWriterStyle.Display, "block");
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            LogoutUser.Logout(user);
        }


        protected void btnViewDocuments_Click(object sender, EventArgs e)
        {
            if (DisplayFilter == "Documents")
                DisplayFilter = "All";
            else
            DisplayFilter = "Documents";
        }

        protected void btnViewImages_Click(object sender, EventArgs e)
        {
            if (DisplayFilter == "Images")
                DisplayFilter = "All";
            else
                DisplayFilter = "Images";
        }

        protected void btnViewVideos_Click(object sender, EventArgs e)
        {
            if (DisplayFilter == "Videos")
                DisplayFilter = "All";
            else
                DisplayFilter = "Videos";
        }

        protected void btnViewAudio_Click(object sender, EventArgs e)
        {
            if (DisplayFilter == "Audio")
            {
                DisplayFilter = "All";
                files = GetUserFiles();
            }
            else
                DisplayFilter = "Audio";
        }

        protected void btnViewOthers_Click(object sender, EventArgs e)
        {
            if (DisplayFilter == "Others")
                DisplayFilter = "All";
            else
                DisplayFilter = "Others";
        }

        public static string TruncateFileNameWithoutExtension(string fileName, int maxLength, string truncationIndicator = "...")
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return fileName;
            }

            string fileNameWithoutExtension = fileName;
            //

            //int extensionIndex = fileName.LastIndexOf('.');
            //string extension = extensionIndex != -1 ? fileName.Substring(extensionIndex) : string.Empty;

            //string fileNameWithoutExtension = extensionIndex != -1
            //    ? fileName.Substring(0, extensionIndex)
            //    : fileName;

            if (fileName.Length > maxLength)
            {
                fileNameWithoutExtension = fileNameWithoutExtension.Substring(0, maxLength - truncationIndicator.Length);
                fileNameWithoutExtension += truncationIndicator;
            }

            return fileNameWithoutExtension;
        }

        protected void sortFileName_Click(object sender, EventArgs e)
        {
            ViewState["sortOrder"] = 0;
            ViewState["ascOrder"] = !(bool)ViewState["ascOrder"];
        }

        protected void sortFileSize_Click(object sender, EventArgs e)
        {
            ViewState["sortOrder"] = 3;
            ViewState["ascOrder"] = !(bool)ViewState["ascOrder"];
        }

        protected void sortFileUser_Click(object sender, EventArgs e)
        {
            ViewState["sortOrder"] = 2;
            ViewState["ascOrder"] = !(bool)ViewState["ascOrder"];
        }

        protected void sortFileDate_Click(object sender, EventArgs e)
        {
            ViewState["sortOrder"] = 1;
            ViewState["ascOrder"] = !(bool)ViewState["ascOrder"]; 
        }

        protected void btnEditPreferences_Click(object sender, EventArgs e)
        {
            CurrentSelectedPage = "Settings";
            DisplayPage(CurrentSelectedPage, true);
        }
    }
}