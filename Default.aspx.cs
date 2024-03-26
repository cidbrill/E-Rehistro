using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace e_rehistro
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Next_Clicked(object sender, EventArgs e)
        {
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

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dayao\Documents\GitHub\e-rehistro\App_Data\Registration.mdf;Integrated Security=True";
            string query = "INSERT INTO UserData (userLast, userFirst, userSuffix, userMiddle, userGender, userBirthday, userBirthCity, userBirthProvince, userProvince, userCity, userBarangay, userBlknlot, userCitizenship, userDateofNat, userCertNo, fatherName, motherName) VALUES (@userFirst, @userLast, @userSuffix, @userMiddle, @gender, @birthDate, @birthMuni, @birthProv, @userProv, @userCity, @userBarangay, @userHouseNum, @citizenship, @dateOfnat, @certNum, @fatName, @motName)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand(query, connection))
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
                }catch(Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}