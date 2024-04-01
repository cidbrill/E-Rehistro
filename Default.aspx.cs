using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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

        private void FetchAndBindData()
        {
            string connString = "Data Source=tcp:e-rehistrodbserver.database.windows.net,1433;Initial Catalog=e-rehistro_db;User Id=**@e-rehistrodbserver;Password=**";

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
            string connectionString = @"Data Source=tcp:e-rehistrodbserver.database.windows.net,1433;Initial Catalog=e-rehistro_db;User Id=**@e-rehistrodbserver;Password=**";
            string query = "INSERT INTO Register (email, password) VALUES (@email, @password)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@password", password);
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
            {
                string email = txtSigninEmail.Text;
                string password = txtSigninPassword.Text;
                string connectionString = @"Data Source=tcp:e-rehistrodbserver.database.windows.net,1433;Initial Catalog=e-rehistro_db;User Id=**@e-rehistrodbserver;Password=**";
                string query = "SELECT email,password, substring(email,1,5) as adminVal FROM Register WHERE email=@email ";

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
                                string adminVal = reader["adminVal"].ToString();
                                string dbPassword = reader["Password"].ToString();
                                if (dbPassword == password)
                                {
                                    if (adminVal == "admin")
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
                        }
                        catch (Exception ex)
                        {
                            Response.Write("<script>alert('Connection failed')</script>");
                        }
                    }
                }
            }
        }

        protected void AdminHome_Click(object sender, EventArgs e)
        {
            ContentPlaceHolder authenticationPage = (ContentPlaceHolder)Master.FindControl("AuthenticationPage");
            ContentPlaceHolder homePage = (ContentPlaceHolder)Master.FindControl("HomePage");
            ContentPlaceHolder registrationPage = (ContentPlaceHolder)Master.FindControl("RegistrationPage");
            ContentPlaceHolder firstRegistrationForm = (ContentPlaceHolder)Master.FindControl("FirstRegistrationForm");
            ContentPlaceHolder secondRegistrationForm = (ContentPlaceHolder)Master.FindControl("SecondRegistrationForm");
            ContentPlaceHolder uploadDocumentPage = (ContentPlaceHolder)Master.FindControl("UploadDocumentPage");
            ContentPlaceHolder voterIDInfo = (ContentPlaceHolder)Master.FindControl("VoterIDInfo");
            ContentPlaceHolder newsAndEventsPage = (ContentPlaceHolder)Master.FindControl("NewsAndEventsPage");
            ContentPlaceHolder aboutPage = (ContentPlaceHolder)Master.FindControl("AboutPage");
            ContentPlaceHolder contactsPage = (ContentPlaceHolder)Master.FindControl("ContactsPage");
            ContentPlaceHolder adminPage = (ContentPlaceHolder)Master.FindControl("AdminPage");

            authenticationPage.Visible = false;
            homePage.Visible = false;
            registrationPage.Visible = false;
            firstRegistrationForm.Visible = false;
            secondRegistrationForm.Visible = false;
            uploadDocumentPage.Visible = false;
            voterIDInfo.Visible = false;
            newsAndEventsPage.Visible = false;
            aboutPage.Visible = false;
            contactsPage.Visible = false;
            adminPage.Visible = true;
        }
        protected void Home_Click(object sender, EventArgs e)
        {
            ((MasterPage)this.Master).Home_Click(this, EventArgs.Empty);
        }

        protected void Registration_Click(object sender, EventArgs e)
        {
            ((MasterPage)this.Master).Registration_Click(this, EventArgs.Empty);
        }

        protected void FirstRegistrationForm_Click(object sender, EventArgs e)
        {
            ContentPlaceHolder authenticationPage = (ContentPlaceHolder)Master.FindControl("AuthenticationPage");
            ContentPlaceHolder homePage = (ContentPlaceHolder)Master.FindControl("HomePage");
            ContentPlaceHolder registrationPage = (ContentPlaceHolder)Master.FindControl("RegistrationPage");
            ContentPlaceHolder firstRegistrationForm = (ContentPlaceHolder)Master.FindControl("FirstRegistrationForm");
            ContentPlaceHolder secondRegistrationForm = (ContentPlaceHolder)Master.FindControl("SecondRegistrationForm");
            ContentPlaceHolder uploadDocumentPage = (ContentPlaceHolder)Master.FindControl("UploadDocumentPage");
            ContentPlaceHolder voterIDInfo = (ContentPlaceHolder)Master.FindControl("VoterIDInfo");
            ContentPlaceHolder newsAndEventsPage = (ContentPlaceHolder)Master.FindControl("NewsAndEventsPage");
            ContentPlaceHolder aboutPage = (ContentPlaceHolder)Master.FindControl("AboutPage");
            ContentPlaceHolder contactsPage = (ContentPlaceHolder)Master.FindControl("ContactsPage");

            authenticationPage.Visible = false;
            homePage.Visible = false;
            registrationPage.Visible = false;
            firstRegistrationForm.Visible = true;
            secondRegistrationForm.Visible = false;
            uploadDocumentPage.Visible = false;
            voterIDInfo.Visible = false;
            newsAndEventsPage.Visible = false;
            aboutPage.Visible = false;
            contactsPage.Visible = false;
        }

        protected void NextPage_Click(object sender, EventArgs e)
        {
            ContentPlaceHolder authenticationPage = (ContentPlaceHolder)Master.FindControl("AuthenticationPage");
            ContentPlaceHolder homePage = (ContentPlaceHolder)Master.FindControl("HomePage");
            ContentPlaceHolder registrationPage = (ContentPlaceHolder)Master.FindControl("RegistrationPage");
            ContentPlaceHolder firstRegistrationForm = (ContentPlaceHolder)Master.FindControl("FirstRegistrationForm");
            ContentPlaceHolder secondRegistrationForm = (ContentPlaceHolder)Master.FindControl("SecondRegistrationForm");
            ContentPlaceHolder uploadDocumentPage = (ContentPlaceHolder)Master.FindControl("UploadDocumentPage");
            ContentPlaceHolder voterIDInfo = (ContentPlaceHolder)Master.FindControl("VoterIDInfo");
            ContentPlaceHolder newsAndEventsPage = (ContentPlaceHolder)Master.FindControl("NewsAndEventsPage");
            ContentPlaceHolder aboutPage = (ContentPlaceHolder)Master.FindControl("AboutPage");
            ContentPlaceHolder contactsPage = (ContentPlaceHolder)Master.FindControl("ContactsPage");

            authenticationPage.Visible = false;
            homePage.Visible = false;
            registrationPage.Visible = false;
            firstRegistrationForm.Visible = false;
            secondRegistrationForm.Visible = true;
            uploadDocumentPage.Visible = false;
            voterIDInfo.Visible = false;
            newsAndEventsPage.Visible = false;
            aboutPage.Visible = false;
            contactsPage.Visible = false;
        }

        protected void UploadDocumentPage_Click(object sender, EventArgs e)
        {
            ContentPlaceHolder authenticationPage = (ContentPlaceHolder)Master.FindControl("AuthenticationPage");
            ContentPlaceHolder homePage = (ContentPlaceHolder)Master.FindControl("HomePage");
            ContentPlaceHolder registrationPage = (ContentPlaceHolder)Master.FindControl("RegistrationPage");
            ContentPlaceHolder firstRegistrationForm = (ContentPlaceHolder)Master.FindControl("FirstRegistrationForm");
            ContentPlaceHolder secondRegistrationForm = (ContentPlaceHolder)Master.FindControl("SecondRegistrationForm");
            ContentPlaceHolder uploadDocumentPage = (ContentPlaceHolder)Master.FindControl("UploadDocumentPage");
            ContentPlaceHolder voterIDInfo = (ContentPlaceHolder)Master.FindControl("VoterIDInfo");
            ContentPlaceHolder newsAndEventsPage = (ContentPlaceHolder)Master.FindControl("NewsAndEventsPage");
            ContentPlaceHolder aboutPage = (ContentPlaceHolder)Master.FindControl("AboutPage");
            ContentPlaceHolder contactsPage = (ContentPlaceHolder)Master.FindControl("ContactsPage");

            authenticationPage.Visible = false;
            homePage.Visible = false;
            registrationPage.Visible = false;
            firstRegistrationForm.Visible = false;
            secondRegistrationForm.Visible = false;
            uploadDocumentPage.Visible = true;
            voterIDInfo.Visible = false;
            newsAndEventsPage.Visible = false;
            aboutPage.Visible = false;
            contactsPage.Visible = false;
        }

        protected void FormSubmit_Click(object sender, EventArgs e)
        {
            ContentPlaceHolder authenticationPage = (ContentPlaceHolder)Master.FindControl("AuthenticationPage");
            ContentPlaceHolder homePage = (ContentPlaceHolder)Master.FindControl("HomePage");
            ContentPlaceHolder registrationPage = (ContentPlaceHolder)Master.FindControl("RegistrationPage");
            ContentPlaceHolder firstRegistrationForm = (ContentPlaceHolder)Master.FindControl("FirstRegistrationForm");
            ContentPlaceHolder secondRegistrationForm = (ContentPlaceHolder)Master.FindControl("SecondRegistrationForm");
            ContentPlaceHolder uploadDocumentPage = (ContentPlaceHolder)Master.FindControl("UploadDocumentPage");
            ContentPlaceHolder voterIDInfo = (ContentPlaceHolder)Master.FindControl("VoterIDInfo");
            ContentPlaceHolder newsAndEventsPage = (ContentPlaceHolder)Master.FindControl("NewsAndEventsPage");
            ContentPlaceHolder aboutPage = (ContentPlaceHolder)Master.FindControl("AboutPage");
            ContentPlaceHolder contactsPage = (ContentPlaceHolder)Master.FindControl("ContactsPage");

            authenticationPage.Visible = false;
            homePage.Visible = false;
            registrationPage.Visible = true;
            firstRegistrationForm.Visible = false;
            secondRegistrationForm.Visible = false;
            uploadDocumentPage.Visible = false;
            voterIDInfo.Visible = false;
            newsAndEventsPage.Visible = false;
            aboutPage.Visible = false;
            contactsPage.Visible = false;

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

            string connectionString = @"Data Source=tcp:e-rehistrodbserver.database.windows.net,1433;Initial Catalog=e-rehistro_db;User Id=**@e-rehistrodbserver;Password=**";
            string query = "INSERT INTO UserData (userLast, userFirst, userSuffix, userMiddle, userGender, userBirthday, userBirthCity, userBirthProvince, userProvince, userCity, userBarangay, userBlknlot, userCitizenship, userDateofNat, userCertNo, fatherName, motherName, oath, registered) VALUES (@userFirst, @userLast, @userSuffix, @userMiddle, @gender, @birthDate, @birthMuni, @birthProv, @userProv, @userCity, @userBarangay, @userHouseNum, @citizenship, @dateOfnat, @certNum, @fatName, @motName, @oath, @registered)";

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
            ContentPlaceHolder authenticationPage = (ContentPlaceHolder)Master.FindControl("AuthenticationPage");
            ContentPlaceHolder homePage = (ContentPlaceHolder)Master.FindControl("HomePage");
            ContentPlaceHolder registrationPage = (ContentPlaceHolder)Master.FindControl("RegistrationPage");
            ContentPlaceHolder firstRegistrationForm = (ContentPlaceHolder)Master.FindControl("FirstRegistrationForm");
            ContentPlaceHolder secondRegistrationForm = (ContentPlaceHolder)Master.FindControl("SecondRegistrationForm");
            ContentPlaceHolder uploadDocumentPage = (ContentPlaceHolder)Master.FindControl("UploadDocumentPage");
            ContentPlaceHolder voterIDInfo = (ContentPlaceHolder)Master.FindControl("VoterIDInfo");
            ContentPlaceHolder newsAndEventsPage = (ContentPlaceHolder)Master.FindControl("NewsAndEventsPage");
            ContentPlaceHolder aboutPage = (ContentPlaceHolder)Master.FindControl("AboutPage");
            ContentPlaceHolder contactsPage = (ContentPlaceHolder)Master.FindControl("ContactsPage");
            ContentPlaceHolder pendingStatusPage = (ContentPlaceHolder)Master.FindControl("PendingStatusPage");


            authenticationPage.Visible = false;
            homePage.Visible = false;
            registrationPage.Visible = false;
            firstRegistrationForm.Visible = false;
            secondRegistrationForm.Visible = false;
            uploadDocumentPage.Visible = false;
            voterIDInfo.Visible = false;
            newsAndEventsPage.Visible = false;
            aboutPage.Visible = false;
            contactsPage.Visible = false;
            pendingStatusPage.Visible = true;


        }

        protected void ViewVoterID_Click(object sender, EventArgs e)
        {
            ContentPlaceHolder authenticationPage = (ContentPlaceHolder)Master.FindControl("AuthenticationPage");
            ContentPlaceHolder homePage = (ContentPlaceHolder)Master.FindControl("HomePage");
            ContentPlaceHolder registrationPage = (ContentPlaceHolder)Master.FindControl("RegistrationPage");
            ContentPlaceHolder firstRegistrationForm = (ContentPlaceHolder)Master.FindControl("FirstRegistrationForm");
            ContentPlaceHolder secondRegistrationForm = (ContentPlaceHolder)Master.FindControl("SecondRegistrationForm");
            ContentPlaceHolder uploadDocumentPage = (ContentPlaceHolder)Master.FindControl("UploadDocumentPage");
            ContentPlaceHolder voterIDInfo = (ContentPlaceHolder)Master.FindControl("VoterIDInfo");
            ContentPlaceHolder newsAndEventsPage = (ContentPlaceHolder)Master.FindControl("NewsAndEventsPage");
            ContentPlaceHolder aboutPage = (ContentPlaceHolder)Master.FindControl("AboutPage");
            ContentPlaceHolder contactsPage = (ContentPlaceHolder)Master.FindControl("ContactsPage");

            authenticationPage.Visible = false;
            homePage.Visible = false;
            registrationPage.Visible = false;
            firstRegistrationForm.Visible = false;
            secondRegistrationForm.Visible = false;
            uploadDocumentPage.Visible = false;
            voterIDInfo.Visible = true;
            newsAndEventsPage.Visible = false;
            aboutPage.Visible = false;
            contactsPage.Visible = false;

            
        }

        protected void DocumentSubmit_Click(object sender, EventArgs e)
        {
            ContentPlaceHolder authenticationPage = (ContentPlaceHolder)Master.FindControl("AuthenticationPage");
            ContentPlaceHolder homePage = (ContentPlaceHolder)Master.FindControl("HomePage");
            ContentPlaceHolder registrationPage = (ContentPlaceHolder)Master.FindControl("RegistrationPage");
            ContentPlaceHolder firstRegistrationForm = (ContentPlaceHolder)Master.FindControl("FirstRegistrationForm");
            ContentPlaceHolder secondRegistrationForm = (ContentPlaceHolder)Master.FindControl("SecondRegistrationForm");
            ContentPlaceHolder uploadDocumentPage = (ContentPlaceHolder)Master.FindControl("UploadDocumentPage");
            ContentPlaceHolder voterIDInfo = (ContentPlaceHolder)Master.FindControl("VoterIDInfo");
            ContentPlaceHolder newsAndEventsPage = (ContentPlaceHolder)Master.FindControl("NewsAndEventsPage");
            ContentPlaceHolder aboutPage = (ContentPlaceHolder)Master.FindControl("AboutPage");
            ContentPlaceHolder contactsPage = (ContentPlaceHolder)Master.FindControl("ContactsPage");

            authenticationPage.Visible = false;
            homePage.Visible = false;
            registrationPage.Visible = true;
            firstRegistrationForm.Visible = false;
            secondRegistrationForm.Visible = false;
            uploadDocumentPage.Visible = false;
            voterIDInfo.Visible = false;
            newsAndEventsPage.Visible = false;
            aboutPage.Visible = false;
            contactsPage.Visible = false;

            if (fileUploadControl.HasFile)
            {
                try
                {
                    // Read the file into a byte array
                    byte[] fileBytes = fileUploadControl.FileBytes;

                    // Establish connection to the database
                    string connectionString = @"Data Source=tcp:e-rehistrodbserver.database.windows.net,1433;Initial Catalog=e-rehistro_db;User Id=**@e-rehistrodbserver;Password=**";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // Insert the picture into the database
                        string query = "INSERT INTO userInfoPic (fileBytes, fileName) VALUES (@PictureData, @FileName)";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@PictureData", fileBytes);
                            command.Parameters.AddWithValue("@FileName", fileUploadControl.FileName);
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    } // Connection will be automatically closed here

                    // Display success message or redirect to another page
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