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
            if (!IsPostBack && SessionManager.IsAdmin)
                FetchAndBindData();
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
            if (!RequireAdmin()) return;

            dataContainer.Controls.Clear();
            var submissions = UserDataHelper.GetAllSubmissions();

            foreach (var submission in submissions)
            {
                var itemDiv = new HtmlGenericControl("div");
                itemDiv.Attributes["class"] = "submission-row";

                itemDiv.InnerHtml =
                    $"<b>Full Name:</b> {HttpUtility.HtmlEncode(submission.FullName)}<br/>" +
                    $"<b>Document:</b> {HttpUtility.HtmlEncode(submission.FileName)}<br/>" +
                    $"<b>Status:</b> {HttpUtility.HtmlEncode(submission.Status)}<br/>";

                if (submission.Status == "pending")
                {
                    var btnApprove = new Button();
                    btnApprove.Text = "Approve";
                    btnApprove.CssClass = "button approve-button";
                    btnApprove.CommandArgument = submission.UserId.ToString();
                    btnApprove.Click += Approve_Click;

                    var btnReject = new Button();
                    btnReject.Text = "Reject";
                    btnReject.CssClass = "button reject-button";
                    btnReject.CommandArgument = submission.UserId.ToString();
                    btnReject.Click += Reject_Click;

                    itemDiv.Controls.Add(btnApprove);
                    itemDiv.Controls.Add(btnReject);
                }

                dataContainer.Controls.Add(itemDiv);
            }
        }

        protected void Approve_Click(object sender, EventArgs e)
        {
            if (!RequireAdmin()) return;
            var btn = (Button)sender;
            int targetUserId = Convert.ToInt32(btn.CommandArgument);
            UserDataHelper.UpdateStatus(targetUserId, "approved");
            FetchAndBindData();
        }

        protected void Reject_Click(object sender, EventArgs e)
        {
            if (!RequireAdmin()) return;
            var btn = (Button)sender;
            int targetUserId = Convert.ToInt32(btn.CommandArgument);
            UserDataHelper.UpdateStatus(targetUserId, "declined");
            FetchAndBindData();
        }

        protected void Signup_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
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
                        command.Parameters.Add("@email",    SqlDbType.VarChar).Value = email;
                        command.Parameters.Add("@password", SqlDbType.VarChar).Value = hashedPassword;
                        command.Parameters.Add("@role",     SqlDbType.VarChar, 20).Value = "user";
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
                catch (Exception)
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
                    command.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string dbPassword = reader["password"].ToString();
                                if (PasswordHelper.VerifyPassword(password, dbPassword))
                                {
                                    string role = reader["role"].ToString();

                                    SessionManager.UserId = Convert.ToInt32(reader["userId"]);
                                    SessionManager.Email = email;
                                    SessionManager.Role = role;

                                    if (role == "admin")
                                    {
                                        AdminHome_Click(sender, EventArgs.Empty);
                                    }
                                    else
                                    {
                                        string status = UserDataHelper.GetUserStatus(SessionManager.UserId);
                                        switch (status)
                                        {
                                            case "approved":
                                                ((MasterPage)this.Master).ShowPage("VerifiedStatusPage");
                                                break;
                                            case "declined":
                                                ((MasterPage)this.Master).ShowPage("DeclinedStatusPage");
                                                break;
                                            case "pending":
                                                ((MasterPage)this.Master).ShowPage("PendingStatusPage");
                                                break;
                                            default:
                                                Home_Click(sender, EventArgs.Empty);
                                                break;
                                        }
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
            if (!Page.IsValid) return;
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
            if (!Page.IsValid) return;

            try
            {
                UserDataHelper.InsertUserData(
                    userId:       SessionManager.UserId,
                    userLast:     lastName.Text,
                    userFirst:    firstName.Text,
                    userSuffix:   suffix.Text,
                    userMiddle:   middleName.Text,
                    userHouseNum: houseNum.Text,
                    userBarangay: barangay.Text,
                    userCity:     municipality.Text,
                    userProv:     prov.Text,
                    citizenship:  citizenship.SelectedValue,
                    dateOfnat:    dateOfNat.Text,
                    certNum:      certNo.Text,
                    gender:       gender.SelectedValue,
                    birthdate:    birthDate.Text,
                    birthMuni:    birthCity.Text,
                    birthProv:    birthProvince.Text,
                    fatName:      fatherName.Text,
                    motName:      motherName.Text,
                    oath:         oathVal.SelectedValue,
                    registered:   isRegistered.SelectedValue
                );
                ((MasterPage)this.Master).ShowPage("PendingStatusPage");
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "formErr",
                    "alert('Could not save your details. Please try again.');", true);
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

            var row = UserDataHelper.GetVoterData(SessionManager.UserId);
            if (row == null) return;

            // Part I: Province / City / Barangay (location)
            txtVoterPartI.Text = $"{row["userProvince"]}, {row["userCity"]}, {row["userBarangay"]}";

            // Part II: Voter ID number — userId zero-padded + birth date
            string birthdate = Convert.ToDateTime(row["userBirthday"]).ToString("yyyyMMdd");
            txtVoterPartII.Text = $"{Convert.ToInt32(row["userId"]):D8}-{birthdate}";

            // Part III: Full name (Last, First Middle Suffix)
            string middle = row["userMiddle"] != DBNull.Value ? " " + row["userMiddle"].ToString() : string.Empty;
            string sfx    = row["userSuffix"] != DBNull.Value ? " " + row["userSuffix"].ToString()  : string.Empty;
            txtVoterPartIII.Text = $"{row["userLast"]}, {row["userFirst"]}{middle}{sfx}".Trim();

            ((MasterPage)this.Master).ShowPage("VoterIDInfo");
        }

        protected void DocumentSubmit_Click(object sender, EventArgs e)
        {
            if (!RequireLogin()) return;
            if (!fileUploadControl.HasFile) return;
            if (!Page.IsValid) return;

            try
            {
                UserDataHelper.InsertDocument(
                    userId:    SessionManager.UserId,
                    fileBytes: fileUploadControl.FileBytes,
                    fileName:  fileUploadControl.FileName
                );
                ((MasterPage)this.Master).ShowPage("RegistrationPage");
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "docErr",
                    "alert('Could not upload the document. Please try again.');", true);
            }
        }

        protected void ContactSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            string connString = ConfigurationManager.ConnectionStrings["ERehistroDB"].ConnectionString;
            const string query =
                @"INSERT INTO ContactMessages (senderName, senderEmail, subject, body)
                  VALUES (@name, @email, @subject, @body)";

            try
            {
                using (var conn = new SqlConnection(connString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@name",    SqlDbType.VarChar, -1).Value = txtName.Text;
                    cmd.Parameters.Add("@email",   SqlDbType.VarChar, -1).Value = txtEmail.Text;
                    cmd.Parameters.Add("@subject", SqlDbType.VarChar, -1).Value = txtSubject.Text;
                    cmd.Parameters.Add("@body",    SqlDbType.VarChar, -1).Value = txtMessage.Text;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                txtName.Text    = string.Empty;
                txtEmail.Text   = string.Empty;
                txtSubject.Text = string.Empty;
                txtMessage.Text = string.Empty;

                ScriptManager.RegisterStartupScript(this, GetType(), "contactSent",
                    "alert('Your message has been sent. Thank you!');", true);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "contactErr",
                    "alert('Could not send your message. Please try again.');", true);
            }
        }
    }
}