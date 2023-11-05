using Overshare.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Overshare
{
    public partial class signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string email = emailInput.Value.Trim();
            string password = passwordInput.Value;
            string firstName = firstNameInput.Value.Trim();
            string lastName = lastNameInput.Value.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                // Display an error message or handle validation errors as appropriate for your application.
                lblError.Text = "Please fill in all required fields.";
                return;
            }
            else
            {
                // Registration failed, handle the error (e.g., show an error message).
                lblError.Text = "Registration failed. Please try again.";
            }

            // Clean and hash the password (You can use the HashPassword method from the previous code)
            string hashedPassword = Hasher.HashPassword(password);

            // Call the RegisterUser method to create the user
            if (UserMethods.RegisterUser(email, hashedPassword, firstName, lastName))
            {
                // Registration successful, you can redirect to the home page or show a success message.
                Response.Redirect("Home.aspx");
            }
        }
    }
}