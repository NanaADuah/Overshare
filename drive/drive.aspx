<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="drive.aspx.cs" Inherits="Overshare.drive.drive" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.min.js" integrity="sha384-cuYeSxntonz0PPNlHhBs68uyIAVpIIOZZ5JqeqvYYIcEL727kskC66kF92t6Xl2V" crossorigin="anonymous"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.2/css/all.min.css" />
    <link rel="stylesheet" href="../css/drive.css" />
    <link rel="stylesheet" href="../css/all.css" />

    <title>My Drive | OverShare </title>
    <style>
        /* Styles for the percentage bar container */

        .video-bar-fill {
            height: 10px; /* Adjust the height as needed */
            background-color: #3498db; /* Blue color for the video portion */
            transition: width 0.5s; /* Add a smooth transition effect */
        }

        .image-bar-fill {
            height: 10px; /* Adjust the height as needed */
            background-color: #2ecc71; /* Green color for the image portion */
            transition: width 0.5s; /* Add a smooth transition effect */
        }

        .percentage-text {
            text-align: center;
            font-size: 16px;
            color: #333;
        }
    </style>
    <script type="text/javascript">
        function callUploadMethod() {
            alert("Uploading File");
            PageMethods.UploadFile();
        }

        function onSuccess(result) {
            alert(result); // Handle the result from C# function
        }

        function onError(error) {
            alert(error.get_message()); // Handle the error
        }

    </script>
    <script>
        var fileTypePercentages = {
            "image": 20,
            "video": 30, // Example: 30%
            "document": 10, // Example: 10%
            "audio": 15, // Example: 15%
            "other": 25 // Example: 25%
        };

        var documents = document.getElementById('documentFill');
        var images = document.getElementById('imageFill');
        var videos = document.getElementById('videoFill');
        var audio = document.getElementById('audioFill');
        var other = document.getElementById('documentFill');
        var pSizes = percentageSizes;
        //images.style.width = pSizes <>;



    </script>
    <script>
        const dropArea = document.getElementById('dropArea');

        // Prevent default behaviors for drag-and-drop events
        ['dragover', 'dragenter', 'dragleave', 'drop'].forEach(eventName => {
            dropArea.addEventListener(eventName, preventDefaults, false);
            document.body.addEventListener(eventName, preventDefaults, false);
        });

        function preventDefaults(e) {
            e.preventDefault();
            e.stopPropagation();
        }

        // Highlight drop area when a file is dragged over
        ['dragenter', 'dragover'].forEach(eventName => {
            dropArea.addEventListener(eventName, highlight, false);
        });

        // Unhighlight drop area when a file is dragged out
        ['dragleave', 'drop'].forEach(eventName => {
            dropArea.addEventListener(eventName, unhighlight, false);
        });

        // Handle dropped files
        dropArea.addEventListener('drop', handleDrop, false);

        function highlight() {
            dropArea.classList.add('highlight');
        }

        function unhighlight() {
            dropArea.classList.remove('highlight');
        }

        function handleDrop(e) {
            const dt = e.dataTransfer;
            const files = dt.files;

            handleFiles(files);
        }

        function handleFiles(files) {
            // Process the dropped files
            // You can upload them to the server using an AJAX request
            // or perform other actions as needed
            console.log(files);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" class="h-100">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true"></asp:ScriptManager>

        <%if (user != null)
            { %>
        <div class="row h-100 d-flex flex-shirnk-0" id="wrapper">
            <div id="options" class="column p-1">
                <div id="logo" class="mb-3 w-75 mx-auto my-2">
                    <img src="../src/Logo.svg" class="img-fluid p-2" />
                </div>
                <div class="d-none">
                    <asp:Button ID="btnViewHome" OnClick="btnViewHome_Click"  runat="server"/>
                    <asp:Button ID="btnViewShared" OnClick="btnViewShared_Click"  runat="server"/>
                    <asp:Button ID="btnViewFavourites" OnClick="btnViewFavourites_Click"   runat="server"/>
                    <asp:Button ID="btnViewTrash" OnClick="btnViewTrash_Click"  runat="server"/>
                    <asp:Button ID="btnViewSettings" OnClick="btnViewSettings_Click"  runat="server"/>
                    <asp:Button ID="btnLogout" OnClick="btnLogout_Click"  runat="server"/>
                </div>
                <div id="options-wrapper" class="w-75 align-content-center">
                    <ul class="list-group-horizontal mx-auto" id="options-list">
                        <li class="list-group-item options-list rounded-5" onclick="btnViewHome.click()" style="<%=(CurrentSelectedPage == "Home") ? "background-color: #6DB5CA": "background-color: inherit"%>">
                            <img class="img-fluid options-icon mx-2 py-2 my-1" src="../src/home.svg" />
                            Home</li>
                        <li class="list-group-item options-list rounded-5" onclick="btnViewShared.click()" style="<%=(CurrentSelectedPage == "Shared") ? "background-color: #6DB5CA": "background-color: inherit"%>">
                            <img class="img-fluid options-icon mx-2 py-2 my-1" src="../src/users.svg" />
                            Shared</li>
                        <li class="list-group-item options-list rounded-5" onclick="btnViewFavourites.click()" style="<%=(CurrentSelectedPage == "Favourites") ? "background-color: #6DB5CA": "background-color: inherit"%>">
                            <img class="img-fluid options-icon mx-2 py-2 my-1" src="../src/favourites.svg" />
                            Favourites</li>
                        <li class="list-group-item options-list rounded-5" onclick="btnViewTrash.click()" style="<%=(CurrentSelectedPage == "Trash") ? "background-color: #6DB5CA": "background-color: inherit"%>">
                            <img class="img-fluid options-icon mx-2 py-2 my-1" src="../src/trash-2.svg" />
                            Trash</li>
                    </ul>
                </div>
                <div id="options-bottom-wrapper" class="bottom-0 position-absolute mx-auto">
                    <ul class="list-group-horizontal align-bottom" id="bottom-items">
                        <li class="list-group-item options-list rounded-5" onclick="btnViewSettings.click()" style="<%=(CurrentSelectedPage == "Settings") ? "background-color: #6DB5CA": "inherit"%>">
                            <img class="rounded img-fluid options-icon mx-2 py-2 my-1" src="../src/settings.svg" />
                            Settings</li>
                        <li class="list-group-item options-list rounded-5" onclick="btnLogout_Click.click()">
                            <img class="img-fluid options-icon mx-2 py-2 my-1" src="../src/log-out.svg" />
                            Logout</li>
                    </ul>
                </div>
            </div>
            <div id="middle" class="column p-2 flex-fill px-4">
                <div runat="server" id="home">
                    <div id="overview" class="column flex-row d-flex align-items-center justify-content-center" style="gap: 15px;">
                        <div class="row d-flex flex-row m-0 px-1" style="flex-wrap: nowrap; gap: 15px;">
                            <div class="column p-0" style="width: fit-content">
                                <img class="" id="user-image" src="<%=$"{user.GetUserPath()}\\profile.svg"%>" />
                            </div>
                            <div class="column p-0 px-5 fw-bold" style="padding-left: 0px !important;">
                                <span class="text-body"><%=TimeGreeting%>,</span><br />
                                <span class="text-body-secondary"><%=user.FirstName%></span>

                            </div>
                        </div>
                        <div class="column flex-fill mx-auto">
                            <div class="rounded-5 border-0 p-2 mx-auto w-100" style="background-color: #B8D8E0;">
                                <img src="../src/search.svg" style="margin-right: 10px" />
                                <input class="border-0 input " placeholder="Search in OverShare" title="Search" id="search-input" />
                            </div>
                        </div>
                        <div class="m-2 rounded-5 px-4" style="background-color: #B8D8E0;">
                            <div class="h-100 p-2">
                                <img src="../src/bell_empty.svg" />
                                New
                            </div>
                        </div>
                        <div class="text-center p-2" style="background-color: #6DB5CA">Upgrade plan</div>

                    </div>
                    <div id="storage" class="py-4">
                        <h5>Storage (<%=String.Format($"{TotalStorage}% Used",TotalStorage)%>)
                        </h5>
                        <div class="justify-content-center mx-4 py-2 percentage-bar-container">
                            <div style="height: 10px; background-color: #b1b1b1" class="rounded-5 w-100 text-center mx-auto">
                                <div class="document-bar-fill" id="documentFill" style="width: 100%;"></div>
                                <div class="video-bar-fill" id="videoFill" style="width: 0%; background-color: #3498db;"></div>
                                <div class="audio-bar-fill" id="audioFill" style="width: 0%; background-color: #e74c3c;"></div>
                                <div class="image-bar-fill" id="imageFill" style="width: 0%; background-color: #f39c12;"></div>
                                <div class="other-bar-fill" id="otherFill" style="width: 0%; background-color: #95a5a6;"></div>
                            </div>
                        </div>
                    </div>
                    <div id="drive-items" class="row d-flex flex-container p-2 m-2 ">
                        <div class="d-none">
                            <asp:Button id="btnViewDocuments" runat="server" OnClick="btnViewDocuments_Click"/>
                            <asp:Button id="btnViewImages" runat="server" OnClick="btnViewImages_Click"/>
                            <asp:Button id="btnViewVideos" runat="server" OnClick="btnViewVideos_Click"/>
                            <asp:Button id="btnViewAudio" runat="server" OnClick="btnViewAudio_Click"/>
                            <asp:Button id="btnViewOthers" runat="server" OnClick="btnViewOthers_Click"/>
                        </div>
                        <div class="user-item column rounded-5 align-content-center p-2 py-4 flex-fill" onclick="btnViewDocuments.click()" style="<%=(DisplayFilter == "Documents") ? "background-color: #8db4be": "background-color: #B8D8E0"%>">
                            <img src="../src/file.svg" /><br />
                            Documents
                        </div>
                        <div class="user-item column rounded-5 align-content-center p-2 py-4 flex-fill" onclick="btnViewImages.click()" style="<%=(DisplayFilter == "Images") ? "background-color: #8db4be": "background-color: #B8D8E0"%>">
                            <img src="../src/image.svg" /><br />
                            Images
                        </div>
                        <div class="user-item column rounded-5 align-content-center p-2 py-4 flex-fill" onclick="btnViewVideos.click()" style="<%=(DisplayFilter == "Videos") ? "background-color: #8db4be": "background-color: #B8D8E0"%>">
                            <img src="../src/file.svg" /><br />
                            Videos
                        </div>
                        <div class="user-item column rounded-5 align-content-center p-2 py-4 flex-fill" onclick="btnViewAudio.click()" style="<%=(DisplayFilter == "Audio") ? "background-color: #8db4be": "background-color: #B8D8E0"%>">
                            <img src="../src/image.svg" /><br />
                            Audio
                        </div>
                        <div class="user-item column rounded-5 align-content-center p-2 py-4 flex-fill" onclick="btnViewOthers.click()" style="<%=(DisplayFilter == "Others") ? "background-color: #8db4be": "background-color: #B8D8E0"%>">
                            <img src="../src/image.svg" /><br />
                            Other
                        </div>
                    </div>
                    <div id="files" class="px-4 justify-content-center">
                        <%if (fileCount == 0)
                            { %>
                        <table class="table table-borderless table-hover table-">
                            <thead>
                                <tr>
                                    <th>Information</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <th><%=(DisplayFilter == "All")?"You have no files":"No files match selected criteria" %></th>
                                </tr>
                            </tbody>
                        </table>
                        <%}
                            else
                            {%>
                        <table class="table table-borderless table-hover">
                            <thead class="fw-bold fs-5">
                                <tr>
                                    <th scope="col">Files <i class="fa-solid fa-chevron-down"></i></th>
                                    <th scope="col">Share date <i class="fa-solid fa-chevron-down"></i></th>
                                    <th scope="col">Share by <i class="fa-solid fa-chevron-down"></i></th>
                                    <th scope="col" class="text-end">Size <i class="fa-solid fa-chevron-down"></i></th>
                                </tr>
                            </thead>
                            <tbody>
                                <%foreach (var item in files)
                                    { %>
                                <tr style="vertical-align: middle;">
                                    <td>
                                        <img style="height: 30px; padding-right: 10px" src='<%=$"../src/{GetFileType(item.FileExtension)}.svg"%>' />
                                        <a id="itemName" class="text-decoration-none text-black" href="/drive/file.aspx?id=<%=item.FileID%>"><%=item.FileName%></a> </td>
                                    <td><%=item.UploadDate.ToString("MMM dd, yyyy") %></td>
                                    <td id="shareUser"><%=String.Format("{0} {1}",item.Owner.FirstName, item.Owner.LastName)%></td>
                                    <td class="text-end"><%=Overshare.Data.UserActions.FormatFileSize(item.FileSize) %></td>
                                </tr>
                                <%}%>
                            </tbody>
                        </table>
                        <%}
%>
                    </div>
                </div>
                <div id="shared" runat="server" style="display:none">Shared</div>
                <div id="favourites" runat="server" style="display:none"> Favourites</div>
                <div id="trash" runat="server" style="display:none">Trash</div>
                <div id="settings" runat="server" style="display:none">Settings</div>
            </div>

            <div id="upload" class="row d-flex p-2">
                <div class="column p-0 w-75 mx-auto" id="dropArea">
                    <p class="fw-bold py-3 ">
                        Upload
                    </p>
                    <div class="bg-white text-center rounded-4 py-4">
                        <img src="../src/upload-cloud.svg" /><br />
                        Drag Files
                    </div>
                    <div class="p-4 text-center">
                        <p class="text-center text-decoration-underline" onclick="FileUploads.click()" style="cursor: pointer">Choose from device</p>
                        <asp:FileUpload ID="FileUploads" runat="server" class="d-none" />
                        <asp:Button class="btn rounded-5" runat="server" ID="btnUpload" Style="background-color: #6DB5CA !important; cursor: pointer" OnClick="btnUpload_Click" Text="Upload" />
                    </div>
                </div>
                <div class="column" style="padding: 0px">
                    <div class="align-content-center ">
                        <div class="fw-normal px-5 fs-5">
                            <p class="text-center">Share With</p>
                        </div>
                        <%if (ShareList.Count == 0 || ShareList == null)
                            {%>
                        <div class="text-center p-5">
                            No users to share with.
                        </div>
                        <%}
                            else
                            {%>
                        <ul class="list-group-horizontal users-list justify-content-center align-items-center">
                            <% foreach (var item in ShareList)
                                { %>

                            <li class="list-group-item share-user rounded-5 my-2 px-2 w-75">
                                <img class="share-profile-img" src="../src/user.svg" />
                                <%=item.GetFullName()%></li>
                            <%}%>
                            </ul>
                            <%} %>
                    </div>
                    <%if (ShareList.Count != 0 && ShareList != null)
                        {%>
                    <div>
                        <p class="text-center" href="#">View All</p>
                    </div>
                    <%} %>
                </div>
            </div>
        </div>
        <%}
            else
            {  %>
        <div>
            You are not authorized to view this page
        </div>
        <%} %>
    </form>
</body>
</html>
