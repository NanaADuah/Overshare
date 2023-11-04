<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Overshare.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.min.js" integrity="sha384-cuYeSxntonz0PPNlHhBs68uyIAVpIIOZZ5JqeqvYYIcEL727kskC66kF92t6Xl2V" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="../css/login.css" />
    <link rel="stylesheet" href="../css/all.css" />
    <title>Login | OverShare</title>

</head>
<body>
    <form id="form1" runat="server">
        <div class="d-flex w-100 h-100" id="wrapper">
            <div class="left justify-content-center align-items-center flex-fill">
                <img src="src/login_background.png" />
            </div>
            <div class="right flex-fill">
                <img class="w-50" src="src/Logo.svg" />
                <div>
                    <span class="fw-bold">Welcome Back!</span>
                    <span class="fw-normal text-black-50">Login to your account below:</span>
                    <div class="w-50 text-center justify-content-center">
                        <div class="rounded-5 border-0 p-2 mx-auto w-100" style="background-color: #6DB5CA;">
                            <img src="src/Email.svg" style="margin-right: 10px" />
                            <input class="border-0 input " placeholder="Search in OverShare" title="Search" id="email-input" />
                        </div>
                        <div class="rounded-5 border-0 p-2 mx-auto w-100" style="background-color: #6DB5CA;">
                            <img src="src/Lock.svg" style="margin-right: 10px" />
                            <input class="border-0 input " placeholder="Search in OverShare" title="Search" id="password-input" />
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
