using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace e_rehistro
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Make sure to fetch data only on initial load
            {
                FetchAndBindData();
            }
        }

        private bool RequireLogin()
        {
            if (!SessionManager.IsLoggedIn)
            {
                ((MasterPage)this.Master).ShowPage("AuthenticationPage");
                return false;
            }
            return true;
        }

        private bool RequireAdmin()
        {
            if (!RequireLogin()) return false;
            if (!SessionManager.IsAdmin)
            {
                ((MasterPage)this.Master).ShowPage("HomePage");
                return false;
            }
            return true;
        }

        private void FetchAndBindData()
        {
            string connString = ConfigurationManager.ConnectionStrings["ERehistroDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                string query = "SELECT (UserData.userFirst+' '+UserData.userLast) as Name, userInfoPic.fileBytes FROM UserData join userInfoPic on UserData.userId=userInfoPic.userId";  // Replace with your actual query
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Create divs and populate them
                foreach (DataRow row in dataTable.Rows)
                {
                    HtmlGenericControl itemDiv = new HtmlGenericControl("div");
                    itemDiv.Attributes.Add("document-table", "data-item");
                    itemDiv.InnerHtml = "<b>Full Name:</b> " + row["Name"] + "<br/>" +
                                        "<b>Document:</b> " + row["fileBytes"] + "<br/>";
                    // Create buttons
                    Button btnApprove = new Button();
                    btnApprove.Text = "Approve";
                    btnApprove.CssClass = "button approve-button"; // Optionally add CSS classes 
                    btnApprove.Click += Approve_Click; // Add event handler (see below)

                    Button btnDeny = new Button();
                    btnDeny.Text = "Reject";
                    btnDeny.CssClass = "button reject-button";
                    btnDeny.Click += Reject_Click;  // Add event handler (see below)

                    // Add buttons to the div
                    itemDiv.Controls.Add(btnApprove);
                    itemDiv.Controls.Add(btnDeny);
                    dataContainer.Controls.Add(itemDiv);
                }

                void Approve_Click(object sender, EventArgs e)
                {
                    // Handle approval logic here
                }

                void Reject_Click(object sender, EventArgs e)
                {
                    // Handle denial logic here
                }
            }
        }

        protected void Signup_Click(object sender, EventArgs e)
        {
            string email = txtSignupEmail.Text;
            string password = txtSignupPassword.Text;
            string hashedPassword = PasswordHelper.HashPassword(password);
            string connectionString = ConfigurationManager.ConnectionStrings["ERehistroDB"].ConnectionString;
            string query = "INSERT INTO Register (email, password, role) VALUES (@email, @password, @role)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@password", hashedPassword);
                        command.Parameters.AddWithValue("@role", "user");
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Response.Write("<script>alert('Registered successfully')</script>");
                            Home_Click(this, EventArgs.Empty);
                        }
                        else
                        {
                            Response.Write("<script>alert('Registration failed. Try again')</script>");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Connection failed')</script>");
                }
            }
        }

        protected void LogIn_Click(object sender, EventArgs e)
        {
            string email = txtSigninEmail.Text;
            string password = txtSigninPassword.Text;
            string connectionString = ConfigurationManager.ConnectionStrings["ERehistroDB"].ConnectionString;
            string query = "SELECT userId, email, password, role FROM Register WHERE email=@email";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            string dbPassword = reader["password"].ToString();
                            if (PasswordHelper.VerifyPassword(password, dbPassword))
                            {
                                string role = reader["role"].ToString();

                                // Store user identity in session
                                SessionManager.UserId = Convert.ToInt32(reader["userId"]);
                                SessionManager.Email = email;
                                SessionManager.Role = role;

                                if (role == "admin")
                                {
                                    Response.Write("<script>alert('Logged in')</script>");
                                    AdminHome_Click(sender, EventArgs.Empty);
                                    FetchAndBindData();
                                }
                                else
                                {
                                    Response.Write("<script>alert('Logged in')</script>");
                                    Home_Click(sender, EventArgs.Empty);
                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('Invalid email or password. Try again')</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('Invalid email or password. Try again')</script>");
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Connection failed')</script>");
                    }
                }
            }
        }

        protected void AdminHome_Click(object sender, EventArgs e)
        {
            if (!RequireAdmin()) return;
            ((MasterPage)this.Master).ShowPage("AdminPage");
        }

        protected void Home_Click(object sender, EventArgs e)
        {
            if (!RequireLogin()) return;
            ((MasterPage)this.Master).ShowPage("HomePage");
        }

        protected void Registration_Click(object sender, EventArgs e)
        {
            if (!RequireLogin()) return;
            ((MasterPage)this.Master).ShowPage("RegistrationPage");
        }

        protected void FirstRegistrationForm_Click(object sender, EventArgs e)
        {
            if (!RequireLogin()) return;
            ((MasterPage)this.Master).ShowPage("FirstRegistrationForm");
        }

        protected void NextPage_Click(object sender, EventArgs e)
        {
            if (!RequireLogin()) return;
            ((MasterPage)this.Master).ShowPage("SecondRegistrationForm");
        }

        protected void UploadDocumentPage_Click(object sender, EventArgs e)
        {
            if (!RequireLogin()) return;
            ((MasterPage)this.Master).ShowPage("UploadDocumentPage");
        }

        protected void FormSubmit_Click(object sender, EventArgs e)
        {
            if (!RequireLogin()) return;
            ((MasterPage)this.Master).ShowPage("RegistrationPage");

            string userFirst = firstName.Text;
            string userLast = lastName.Text;
            string userMiddle = middleName.Text;
            string userSuffix = suffix.Text;
            string userHouseNum = houseNum.Text;
            string userBarangay = barangay.Text;
            string userCity = municipality.Text;
            string userProv = prov.Text;

            string selectedVal = citizenship.SelectedValue;
            string citizen = selectedVal;

            string dateOfnat = dateOfNat.Text;
            string certNum = certNo.Text;

            string Gender = gender.SelectedValue;

            string birthdate = birthDate.Text;
            string birthMuni = birthCity.Text;
            string birthProv = birthProvince.Text;
            string fatName = fatherName.Text;
            string motName = motherName.Text;

            string oath = oathVal.SelectedValue;
            string registered = isRegistered.SelectedValue;

            string connectionString = ConfigurationManager.ConnectionStrings["ERehistroDB"].ConnectionString;
            string query = "INSERT INTO UserData (userLast, userFirst, userSuffix, userMiddle, userGender, userBirthday, userBirthCity, userBirthProvince, userProvince, userCity, userBarangay, userBlknlot, userCitizenship, userDateofNat, userCertNo, fatherName, motherName, oath, registered) VALUES (@userLast, @userFirst, @userSuffix, @userMiddle, @gender, @birthDate, @birthMuni, @birthProv, @userProv, @userCity, @userBarangay, @userHouseNum, @citizenship, @dateOfnat, @certNum, @fatName, @motName, @oath, @registered)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userFirst", userFirst);
                        command.Parameters.AddWithValue("@userLast", userLast);
                        command.Parameters.AddWithValue("@userSuffix", userSuffix);
                        command.Parameters.AddWithValue("@userMiddle", userMiddle);
                        command.Parameters.AddWithValue("@userHouseNum", userHouseNum);
                        command.Parameters.AddWithValue("@userBarangay", userBarangay);
                        command.Parameters.AddWithValue("@userCity", userCity);
                        command.Parameters.AddWithValue("@userProv", userProv);
                        command.Parameters.AddWithValue("@citizenship", citizen);
                        command.Parameters.AddWithValue("@dateOfnat", dateOfnat);
                        command.Parameters.AddWithValue("@certNum", certNum);
                        command.Parameters.AddWithValue("@gender", Gender);
                        command.Parameters.AddWithValue("@birthDate", birthdate);
                        command.Parameters.AddWithValue("@birthMuni", birthMuni);
                        command.Parameters.AddWithValue("@birthProv", birthProv);
                        command.Parameters.AddWithValue("@fatName", fatName);
                        command.Parameters.AddWithValue("@motName", motName);
                        command.Parameters.AddWithValue("@oath", oath);
                        command.Parameters.AddWithValue("@registered", registered);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Response.Write("<script>alert('Saved')</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Save failed. Enter again')</script>");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        protected void Pending_Click(object sender, EventArgs e)
        {
            if (!RequireLogin()) return;
            ((MasterPage)this.Master).ShowPage("PendingStatusPage");
        }

        protected void ViewVoterID_Click(object sender, EventArgs e)
        {
            if (!RequireLogin()) return;
            ((MasterPage)this.Master).ShowPage("VoterIDInfo");
        }

        protected void DocumentSubmit_Click(object sender, EventArgs e)
        {
            if (!RequireLogin()) return;
            ((MasterPage)this.Master).ShowPage("RegistrationPage");

            if (fileUploadControl.HasFile)
            {
                try
                {
                    byte[] fileBytes = fileUploadControl.FileBytes;

                    string connectionString = ConfigurationManager.ConnectionStrings["ERehistroDB"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO userInfoPic (fileBytes, fileName) VALUES (@PictureData, @FileName)";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@PictureData", fileBytes);
                            command.Parameters.AddWithValue("@FileName", fileUploadControl.FileName);
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }

                    Response.Write("<script>alert('Picture uploaded successfully!')</script>");
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                }
            }
        }
    }
}