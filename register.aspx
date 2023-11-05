<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="Overshare.signup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.min.js" integrity="sha384-cuYeSxntonz0PPNlHhBs68uyIAVpIIOZZ5JqeqvYYIcEL727kskC66kF92t6Xl2V" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="../css/register.css" />
    <link rel="stylesheet" href="../css/all.css" />
    <title>Register | OverShare</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="overview">
            <div class="align-content-center justify-content-center h-100 d-flex">
                <div id="inputView" class="w-25 text-center justify-content-center mx-auto">
                    <div class="fs-4" style="text-align: left">
                        <span class="fw-bold">Welcome Back!</span><br />
                        <span class="fw-normal">Register an account below:</span>
                        <asp:Label runat="server" Text="" ID="lblError"></asp:Label>
                    </div>
                    <div class="rounded border-0 p-2 m-4 mx-auto w-100" style="background-color: #6DB5CA;">
                        <img src="src/user.svg" style="margin-right: 10px" />
                        <input runat="server" class="border-0 input w-75" placeholder="Name" title="First Name" type="text" id="firstNameInput" />
                    </div>
                    <div class="rounded border-0 p-2 m-4 mx-auto w-100" style="background-color: #6DB5CA;">
                        <img src="src/user.svg" style="margin-right: 10px" />
                        <input runat="server" class="border-0 input w-75" placeholder="Surname" title="Last Name" type="text" id="lastNameInput" />
                    </div>
                    <div class="rounded border-0 p-2 m-4 mx-auto w-100 " style="background-color: #6DB5CA;">
                        <img src="src/Email.svg" style="margin-right: 10px" />
                        <input runat="server" class="border-0 input w-75" placeholder="Email" title="Email" type="text" id="emailInput" />
                    </div>
                    <div class="rounded border-0 p-2 m-4 mx-auto w-100" style="background-color: #6DB5CA;">
                        <img src="src/Lock.svg" style="margin-right: 10px" />
                        <input runat="server" class="border-0 input w-75" placeholder="Password" title="Password" type="password" id="passwordInput" />
                    </div>
                    <div class="rounded border-0 p-2 m-4 mx-auto w-100" style="background-color: #6DB5CA;">
                        <img src="src/Lock.svg" style="margin-right: 10px" />
                        <input runat="server" class="border-0 input w-75" placeholder="Confirm Password" title="Password" type="password" id="passwordConfirmInput" />
                    </div>

                    <div class="px-4 py-3 mb-3 rounded-5 bg-black w-50 mx-auto text-center">
                        <asp:Button runat="server" ID="btnRegister" class="btn text-white mx-auto p-0" Text="Register" OnClick="btnRegister_Click" />
                    </div>
                    <div class="p-2">
                        <div class="fw-normal fs-5">
                            Already have an account? <a href="login.aspx" class="text-decoration-none text-decoration-underline text-black" style="cursor: pointer">Log In</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
