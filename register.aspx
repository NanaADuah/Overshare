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
            <asp:Label runat="server" Text="" ID="lblError"></asp:Label>
            <div runat="server" class="align-content-center justify-content-center h-100" id="detailsPage" style="padding-top: 50px">
                <div class="fs-1 text-center mx-auto p-5">
                    Over<span class="fw-bold">Share</span>
                </div>
                <div id="inputView" class="w-25 text-center justify-content-center mx-auto">
                    <div class="fs-4" style="text-align: left">
                        <span class="fw-bold">Welcome Back!</span><br />
                        <span class="fw-normal">Register an account below:</span>
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
                    <div class="px-4 py-3 mb-3 rounded-5 bg-black w-50 mx-auto text-center text-white" onclick="selectPlan()" style="cursor: pointer">
                        <%--<asp:Button runat="server" ID="btnNext" class="btn text-white mx-auto p-0" Text="Next" OnClick="btnNext_Click" />--%>
                        Next
                    </div>
                    <div class="p-2">
                        <div class="fw-normal fs-5">
                            Already have an account? <a href="login.aspx" class="text-decoration-none text-decoration-underline text-black" style="cursor: pointer">Log In</a>
                        </div>
                    </div>
                </div>
            </div>
            <div runat="server" style="display:none" id="pricePlanPage">
                <div class="w-75 mx-auto">
                    <div class="align-content-center justify-content-center d-table mx-auto w-100 text-center p-2">
                        <div class="fw-bold d-inline-block fs-4" style="line-height: 40px">Select your preferred plan:</div>
                        <div class="fs-4" style="line-height: 40px">50% off Premium Plan in the first month</div>
                    </div>
                </div>
                <div class="h-auto mx-auto px-4" style="padding-left: 20px !important; padding-right: 20px !important">
                    <div class="d-flex row gap-3 flex-row p-4 w-75 flex-fill mx-auto " style="flex-wrap: nowrap; flex-basis: 0; padding-bottom: 0 !important">
                        <div class="card-top w-auto column p-0 m-0" style="visibility: hidden">
                            <div class="text-center fs-5 fw-bold p-2">
                                Free
                            </div>
                        </div>
                        <div class="card-top w-auto column p-0 m-0" style="visibility: hidden">
                            <div class="text-center fs-5 fw-bold p-2">
                                Basic
                            </div>
                        </div>
                        <div class="card-top w-auto column p-0 m-0">
                            <div id="popularTab" class="text-center fs-5 fw-bold p-2 w-50 mx-auto" style="background-color: #1E1E1E; color: white; border-radius: 10px 10px 0px 0px">
                                Popular
                            </div>
                        </div>
                        <div class="card-top w-auto column p-0 m-0">
                            <div class="text-center fs-5 fw-bold p-2" style="visibility: hidden">
                                Premium
                            </div>
                        </div>
                    </div>
                    <div class="d-flex row gap-3 flex-row p-4 w-75 flex-fill mx-auto" style="flex-wrap: nowrap; flex-basis: 0; padding-top: 0 !important; margin-top: 0 !important">
                        <div class="plan-card w-auto column cardTab" onclick="changeColor('freeCard')" id="freeCard">
                            <div class="fs-1 fw-bold">Free</div>
                            <br />
                            <div class="te" style="text-align: left">
                                <div>Free for eternity</div>
                                <br />
                                <br />
                                <div class="planDetails">
                                    <img src="src/check.svg" />
                                    Storage: 5GB
                                </div>
                                <div class="planDetails">
                                    <img src="src/check.svg" />
                                    File Size Limit: 100MB
                                </div>
                                <div class="planDetails">
                                    <img src="src/check.svg" />
                                    Access from one device
                                </div>
                                <div class="planDetails">
                                    <img src="src/check.svg" />
                                    Limited customer support
                                </div>
                                <div class="planDetails">
                                    <img src="src/check.svg" />
                                    Basic file sharing and collaboration
                                </div>
                            </div>
                            <div class="text-decoration-underline position-relative bottom-0">
                                See all features
                            </div>
                        </div>
                        <div class="plan-card w-auto column cardTab">
                            <div class="fs-1 fw-bold">Basic</div>
                            <br />
                            <div class="te" style="text-align: left">
                                <div>R5 every month with a 1-month money back guarantee.</div>
                                <br />
                                <div class="planDetails">
                                    <img src="src/check.svg" />
                                    Storage: 50GB
                                </div>
                                <div class="planDetails">
                                    <img src="src/check.svg" />
                                    File Size Limit: 500MB
                                </div>
                                <div class="planDetails">
                                    <img src="src/check.svg" />
                                    Multi-device access
                                </div>
                                <div class="planDetails">
                                    <img src="src/check.svg" />
                                    Priority customer support
                                </div>
                                <div class="planDetails">
                                    <img src="src/check.svg" />
                                    Enhanced file sharing  with password protection
                                </div>
                            </div>
                        </div>
                        <div class="plan-card w-auto column text-white cardTab" style="background-color: #1E1E1E" onmouseover="hoverItem()" onmouseout="unhoverItem()">
                            <div class="fs-1 fw-bold">Business</div>
                            <br />
                            <div class="te" style="text-align: left">
                                <div>R15 every month with a 6-month money back guarantee.</div>
                                <br />
                                <div class="planDetails">
                                    <img src="src/check-white.svg" />
                                    Storage: 5GB
                                </div>
                                <div class="planDetails">
                                    <img src="src/check-white.svg" />
                                    File Size Limit: 2GB
                                </div>
                                <div class="planDetails">
                                    <img src="src/check-white.svg" />
                                    Advanced file sharing with link expiration
                                </div>
                                <div class="planDetails">
                                    <img src="src/check-white.svg" />
                                    24/7 customer support
                                </div>
                                <div class="planDetails">
                                    <img src="src/check-white.svg" />
                                    Team collaboration with document versioning
                                </div>
                            </div>
                        </div>
                        <div class="plan-card w-auto column cardTab">
                            <div class="fs-1 fw-bold">Premium</div>
                            <br />
                            <div class="te" style="text-align: left">
                                <div>R30 every month with a 1-year money back guarantee.</div>
                                <br />
                                <div class="planDetails">
                                    <img src="src/check.svg" />
                                    Storage: Unlimited
                                </div>
                                <div class="planDetails">
                                    <img src="src/check.svg" />
                                    File Size Limit: None
                                </div>
                                <div class="planDetails">
                                    <img src="src/check.svg" />
                                    Advanced security features including end-to-end encryption
                                </div>
                                <div class="planDetails">
                                    <img src="src/check.svg" />
                                    Premium team collaboration with real-time document editing
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="px-4 py-3 m-3 rounded-5 bg-black w-25 mx-auto text-center text-white" style="cursor: pointer" onclick="document.getElementById('btnRegister').click()">
                        <asp:Button runat="server" ID="btnRegister" class="d-none" OnClick="btnRegister_Click" />
                        Sign Up
                    </div>
                    <div class="p-2 text-center">
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

<script type="text/javascript">
    function selectPlan() {
        var details = document.getElementById("detailsPage");
        var pricepage = document.getElementById("pricePlanPage");
        details.style.display = "none";
        pricepage.style.display = "block";

    }

    function changeColor(item) {

        var current = document.getElementById(item).style.backgroundColor = "white";
    }

    function hoverItem(){
        var current = document.getElementById("popularTab");
        
        current.style.backgroundColor = "white";
        current.style.color = "black";
    }

    function unhoverItem() {
        var current = document.getElementById("popularTab");
        
        current.style.backgroundColor = "#1E1E1E";
        current.style.color = "white";
    }
</script>
