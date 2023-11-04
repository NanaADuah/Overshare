<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="drive.aspx.cs" Inherits="Overshare.drive.drive" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.min.js" integrity="sha384-cuYeSxntonz0PPNlHhBs68uyIAVpIIOZZ5JqeqvYYIcEL727kskC66kF92t6Xl2V" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="../css/drive.css" />
    <link rel="stylesheet" href="../css/all.css" />
    <title>My Drive | OverShare </title>
</head>
<body>
    <form id="form1" runat="server" class="h-100">
        <div class="row h-100 d-flex flex-shirnk-0" id="wrapper">
            <div id="options" class="column p-1">
                <div id="logo" class="mb-3 w-75 mx-auto my-2">
                    <img src="../src/Logo.svg" class="img-fluid p-2" />
                </div>
                <div id="options-wrapper" class="w-75 align-content-center">
                    <ul class="list-group-horizontal mx-auto" id="options-list">
                        <li class="list-group-item options-list rounded-5" style="background-color: #6DB5CA">
                            <img class="img-fluid options-icon mx-2 py-2 my-1" src="../src/home.svg" />
                            Home</li>
                        <li class="list-group-item options-list rounded-5">
                            <img class="img-fluid options-icon mx-2 py-2 my-1" src="../src/cloud.svg" />
                            Cloud</li>
                        <li class="list-group-item options-list rounded-5">
                            <img class="img-fluid options-icon mx-2 py-2 my-1" src="../src/users.svg" />
                            Shared</li>
                        <li class="list-group-item options-list rounded-5">
                            <img class="img-fluid options-icon mx-2 py-2 my-1" src="../src/favourites.svg" />
                            Favourites</li>
                        <li class="list-group-item options-list rounded-5">
                            <img class="img-fluid options-icon mx-2 py-2 my-1" src="../src/trash-2.svg" />
                            Trash</li>
                    </ul>
                </div>
                <div id="options-bottom-wrapper" class="bottom-0 position-absolute mx-auto">
                    <ul class="list-group-horizontal align-bottom" id="bottom-items">
                        <li class="list-group-item options-list rounded-5">
                            <img class="rounded img-fluid options-icon mx-2 py-2 my-1" src="../src/settings.svg" />
                            Settings</li>
                        <li class="list-group-item options-list rounded-5">
                            <img class="img-fluid options-icon mx-2 py-2 my-1" src="../src/log-out.svg" />
                            Logout</li>
                    </ul>
                </div>
            </div>
            <div id="home" class="column p-2 flex-fill px-4">
                <div id="overview" class="column flex-row d-flex align-items-center justify-content-center" style="gap: 15px;">
                    <div class="row d-flex flex-row m-0 px-1" style="flex-wrap: nowrap; gap: 15px;">
                        <div class="column p-0" style="width: fit-content">
                            <img class="" id="user-image" src="../src/Nonye.jpg" />
                        </div>
                        <div class="column p-0 px-5 fw-bold" style="padding-left: 0px !important;">
                            <span class="text-body">Good Morning</span><br />
                            <span class="text-body-secondary">Nonye</span>

                        </div>
                    </div>
                    <div class="column flex-fill mx-auto">
                        <div class="rounded-5 border-0 p-2 mx-auto w-100" style="background-color: #B8D8E0;">
                            <img src="../src/search.svg" style="margin-right: 10px"/>
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
                    <h5>Storage (32% Used)
                    </h5>
                    <div class="justify-content-center mx-4 py-2">
                        <div style="height: 10px;" class="rounded-5 w-100 text-center bg-danger mx-auto"></div>
                    </div>
                </div>
                <div id="drive-items" class="row d-flex flex-container p-2 m-2 ">
                    <div class="user-item column rounded-5 align-content-center p-2 py-4 flex-fill" style="background-color: #FFA883">
                        <img src="../src/file.svg" /><br />
                        Documents
                    </div>
                    <div class="user-item column rounded-5 align-content-center p-2 py-4 flex-fill" style="background-color: #BBD38D">
                        <img src="../src/image.svg" /><br />
                        Images
                    </div>
                    <div class="user-item column rounded-5 align-content-center p-2 py-4 flex-fill" style="background-color: #FC7B7A">
                        <img src="../src/file.svg" /><br />
                        Videos
                    </div>
                    <div class="user-item column rounded-5 align-content-center p-2 py-4 flex-fill" style="background-color: #F29FB8">
                        <img src="../src/image.svg" /><br />
                        Audio
                    </div>
                    <div class="user-item column rounded-5 align-content-center p-2 py-4 flex-fill" style="background-color: #C3C8D3">
                        <img src="../src/image.svg" /><br />
                        Other
                    </div>
                </div>
                <div id="files" class="px-4 justify-content-center">
                    <table class="table table-borderless table-hover">
                        <thead>
                            <tr>
                                <th scope="col">Recent</th>
                                <th scope="col">Modified</th>
                                <th scope="col">Owner</th>
                                <th scope="col">Size</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Last Day of University</td>
                                <td>2023, 10 Nov</td>
                                <td>me</td>
                                <td>13 MB</td>
                            </tr>
                            <tr>
                                <td>Final Year-Party_321.mov</td>
                                <td>2023, 12 Sep</td>
                                <td>me</td>
                                <td>29 MB</td>
                            </tr>
                            <tr>
                                <td>Egg Head 001.jpg</td>
                                <td>2023, 9 Sep</td>
                                <td>me</td>
                                <td>39 KB</td>
                            </tr>
                            <tr>
                                <td>Retirement Fund Folder</td>
                                <td>2023, 2 Oct</td>
                                <td>me</td>
                                <td>4 MB</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div id="upload" class="row d-flex p-2">
                <div class="column p-0 w-75 mx-auto">
                    <p class="fw-bold py-3 ">
                        Upload
                    </p>
                    <div class="bg-white text-center rounded-4 py-4">
                        <img src="../src/upload-cloud.svg" /><br />
                        Drag Files
                    </div>
                    <div class="p-4">
                        <p class="text-center text-decoration-underline" href="#">Choose from device</p>
                    </div>
                </div>
                <div class="column" style="padding: 0px">
                    <div class="align-content-center ">
                    <div class="fw-normal px-5 fs-5">Share With</div>
                        <ul class="list-group-horizontal users-list justify-content-center align-items-center">
                            <li class="list-group-item share-user rounded-5 my-2 px-2 w-75"><img class="share-profile-img" src="../src/Nonye.jpg" /> Rumi Leigh</li>
                            <li class="list-group-item share-user rounded-5 my-2 px-2 w-75"><img class="share-profile-img" src="../src/Nonye.jpg" /> Oma King-John</li>
                            <li class="list-group-item share-user rounded-5 my-2 px-2 w-75"><img class="share-profile-img" src="../src/Nonye.jpg" /> Bean Suli</li>
                            <li class="list-group-item share-user rounded-5 my-2 px-2 w-75"><img class="share-profile-img" src="../src/Nonye.jpg" /> Kim Saen</li>
                            <li class="list-group-item share-user rounded-5 my-2 px-2 w-75"><img class="share-profile-img" src="../src/Nonye.jpg" /> Mimi Nose</li>
                        </ul>
                    </div>
                    <div>
                        <p class="text-center" href="#">View All</p>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
