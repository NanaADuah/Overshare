<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Overshare.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.min.js" integrity="sha384-cuYeSxntonz0PPNlHhBs68uyIAVpIIOZZ5JqeqvYYIcEL727kskC66kF92t6Xl2V" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="../css/login.css" />
    <link rel="stylesheet" href="../css/all.css" />
    <link rel="apple-touch-icon" sizes="180x180" href="/apple-touch-icon.png" />
    <link rel="icon" type="image/png" sizes="32x32" href="/favicon-32x32.png" />
    <link rel="icon" type="image/png" sizes="16x16" href="/favicon-16x16.png" />
    <link rel="manifest" href="/site.webmanifest" />
    <title>Login | OverShare</title>
    <style>
        /* Your existing styles */

        /* New styles for the fade transition */
        .mySlides {
            display: none;
            animation: fade 0.5s cubic-bezier(0.68, -0.55, 0.27, 1.55)
        }

        @keyframes fade {
            from {
                opacity: 0.4;
            }

            to {
                opacity: 1;
            }
        }

        .active-dot {
            background-color: #6DB5CA;
        }
    </style>
    <script>
        // Wrap your existing code in a function
        function startSlideshow() {
            let slideIndex = 0;
            showSlides();

            function showSlides() {
                let i;
                const slides = document.getElementsByClassName("mySlides");
                const dots = document.getElementsByClassName("dot");

                var t1 = document.getElementById("text1");
                var t2 = document.getElementById("text2");

                for (i = 0; i < slides.length; i++) {
                    slides[i].style.display = "none";
                }

                slideIndex++;

                if (slideIndex > slides.length) {
                    slideIndex = 1;
                }

                var dot1 = document.getElementById("dot1");
                var dot2 = document.getElementById("dot2");
                var dot3 = document.getElementById("dot3");

                if (slideIndex == 1) {
                    dot1.style.backgroundColor = "#6DB5CA";
                    dot2.style.backgroundColor = "white";
                    dot3.style.backgroundColor = "white";
                    t1.innerHTML = "unlimited possibilities, one storage";
                    t2.innerHTML = "Store anything, anytime, anywhere.";
                } else
                if (slideIndex == 2) {
                    dot1.style.backgroundColor = "white";
                    dot2.style.backgroundColor = "#6DB5CA";
                    dot3.style.backgroundColor = "white";
                    t1.innerHTML = "Share with ease";
                    t2.innerHTML = "Store, access, and collaborate anywhere.";
                }
                else
                if (slideIndex == 3) {
                    dot1.style.backgroundColor = "white";
                    dot2.style.backgroundColor = "white";
                    dot3.style.backgroundColor = "#6DB5CA";
                    t1.innerHTML = "Shieleded data";
                    t2.innerHTML = "Your information, locked and secure.";
                } else {
                    dot1.style.backgroundColor = "white";
                    dot2.style.backgroundColor = "white";
                    dot3.style.backgroundColor = "white";

                }

                slides[slideIndex - 1].style.display = "block";

                setTimeout(showSlides, 3000); // Change slide every 2 seconds
            }
        }

        // Use window.onload to call the function when the page is fully loaded
        window.onload = startSlideshow;
    </script>

</head>
<body>
    <form id="form1" runat="server" style="height: 100%">
        <div class="d-flex w-100 h-100 flex-fill align-items-center justify-content-center" id="wrapper" style="flex-basis: 0 !important">

            <div class="left justify-content-center align-items-center text-center mt-4" style="min-width: 0; width: 50%">
                <div class="w-auto mx-auto">
                    <img class="w-25 mx-auto" src="src/Logo.svg" style="width: fit-content" /><br />
                    <div class="mySlides">
                        <img class="justify-content-center" src="src/login/1.png" />
                    </div>
                    <div class="mySlides">
                        <img class="justify-content-center" src="src/login/2.png" />
                    </div>
                    <div class="mySlides">
                        <img class="justify-content-center" src="src/login/3.png" />
                    </div>
                </div>
                <div class="p-2 text-center" style="text-transform: capitalize">
                    <div id="text1" class="fs-4 fw-bold">Share with ease.</div>
                    <div id="text2" class="fs-5">Store, access, and collaborate anywhere.</div>
                </div>
                <div class="w-75 mx-auto">
                    <div class="mt-5 gap-2 row w-25 mx-auto flex-fill text-center justify-content-between" style="flex-basis: 0 !important">
                        <div class="dots column" id="dot1"></div>
                        <div class="dots column" id="dot2"></div>
                        <div class="dots column" id="dot3"></div>
                    </div>
                </div>
            </div>
            <div class="right flex-grow-1 align-content-center justify-content-center" style="width: 50%">
                <div class="align-content-center justify-content-center h-100 d-flex">
                    <div id="inputView" class="w-50 text-center justify-content-center mx-auto">
                        <asp:Label runat="server" Text="" ID="lblError"></asp:Label>
                        <div class="fs-4" style="text-align: left">
                            <span class="fw-bold">Welcome Back!</span><br />
                            <span class="fw-normal">Login to your account below:</span>
                        </div>
                        <div class="rounded border-0 p-2 m-4 mx-auto w-100 " style="background-color: #6DB5CA;">
                            <img src="src/Email.svg" style="margin-right: 10px" />
                            <input runat="server" class="border-0 input w-75" placeholder="Email" title="Email" id="emailInput" />
                        </div>
                        <div class="rounded border-0 p-2 m-4 mx-auto w-100" style="background-color: #6DB5CA;">
                            <img src="src/Lock.svg" style="margin-right: 10px" />
                            <input runat="server" class="border-0 input w-75" placeholder="Password" title="Password" type="password" id="passwordInput" />
                        </div>
                        <div class="px-4 py-3 mb-3 rounded-5 bg-black w-50 mx-auto fw-normal text-center text-white" style="cursor: pointer" onclick="document.getElementById('btnLogIn').click()">
                            <asp:Button runat="server" ID="btnLogIn" class="d-none" Text="Log In" OnClick="btnLogIn_Click" />
                            Login
                        </div>
                        <div>
                            <input runat="server" class="form-check-input" type="checkbox" value="" id="chkLoggedIn" />
                            <label class="form-check-label" for="chkLoggedIn">
                                Remember Me
                            </label>
                        </div>
                        <div class="p-2">
                            <div class="fw-normal fs-5">
                                Don't have an account? <a href="register.aspx" class="text-decoration-none text-decoration-underline text-black" style="cursor: pointer">Sign up</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>

    </form>
</body>
</html>
