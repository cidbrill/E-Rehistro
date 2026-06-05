using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace e_rehistro
{
    public class UserSubmission
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string FileName { get; set; }
        public string Status { get; set; }
    }

    public static class UserDataHelper
    {
        private static readonly string _connString =
            ConfigurationManager.ConnectionStrings["ERehistroDB"].ConnectionString;

        private const string QueryAllSubmissions =
            @"SELECT r.userId, (ud.userFirst + ' ' + ud.userLast) AS FullName,
                     pic.fileName, ud.status
              FROM Register r
              JOIN UserData ud ON r.userId = ud.userId
              JOIN userInfoPic pic ON r.userId = pic.userId";

        private const string QueryUserStatus =
            "SELECT status FROM UserData WHERE userId = @userId";

        private const string QueryInsertUserData =
            @"INSERT INTO UserData
              (userId, userLast, userFirst, userSuffix, userMiddle, userGender,
               userBirthday, userBirthCity, userBirthProvince, userProvince,
               userCity, userBarangay, userBlknlot, userCitizenship,
               userDateofNat, userCertNo, fatherName, motherName, oath, registered)
              VALUES
              (@userId, @userLast, @userFirst, @userSuffix, @userMiddle, @gender,
               @birthDate, @birthMuni, @birthProv, @userProv,
               @userCity, @userBarangay, @userHouseNum, @citizenship,
               @dateOfnat, @certNum, @fatName, @motName, @oath, @registered)";

        private const string QueryInsertDocument =
            "INSERT INTO userInfoPic (userId, fileBytes, fileName) VALUES (@userId, @fileBytes, @fileName)";

        private const string QueryUpdateStatus =
            "UPDATE UserData SET status = @status WHERE userId = @userId";

        private const string QueryGetVoterData =
            @"SELECT ud.userFirst, ud.userLast, ud.userMiddle, ud.userSuffix,
                     ud.userBirthday, ud.userGender, ud.userBarangay,
                     ud.userCity, ud.userProvince, r.userId
              FROM UserData ud
              JOIN Register r ON ud.userId = r.userId
              WHERE ud.userId = @userId";

        public static List<UserSubmission> GetAllSubmissions()
        {
            var results = new List<UserSubmission>();
            using (var conn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand(QueryAllSubmissions, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(new UserSubmission
                        {
                            UserId   = Convert.ToInt32(reader["userId"]),
                            FullName = reader["FullName"].ToString(),
                            FileName = reader["fileName"].ToString(),
                            Status   = reader["status"].ToString()
                        });
                    }
                }
            }
            return results;
        }

        public static string GetUserStatus(int userId)
        {
            using (var conn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand(QueryUserStatus, conn))
            {
                cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                conn.Open();
                var result = cmd.ExecuteScalar();
                return result?.ToString() ?? string.Empty;
            }
        }

        public static bool HasSubmittedForm(int userId)
        {
            return GetUserStatus(userId) != string.Empty;
        }

        public static void UpdateStatus(int userId, string status)
        {
            using (var conn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand(QueryUpdateStatus, conn))
            {
                cmd.Parameters.Add("@status", SqlDbType.VarChar, 20).Value = status;
                cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void InsertUserData(int userId, string userLast, string userFirst,
            string userSuffix, string userMiddle, string userHouseNum, string userBarangay,
            string userCity, string userProv, string citizenship, string dateOfnat,
            string certNum, string gender, string birthdate, string birthMuni,
            string birthProv, string fatName, string motName, string oath, string registered)
        {
            using (var conn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand(QueryInsertUserData, conn))
            {
                cmd.Parameters.Add("@userId",       SqlDbType.Int).Value           = userId;
                cmd.Parameters.Add("@userLast",     SqlDbType.VarChar).Value        = userLast;
                cmd.Parameters.Add("@userFirst",    SqlDbType.VarChar).Value        = userFirst;
                cmd.Parameters.Add("@userSuffix",   SqlDbType.VarChar, 8).Value     = string.IsNullOrWhiteSpace(userSuffix) ? (object)DBNull.Value : userSuffix;
                cmd.Parameters.Add("@userMiddle",   SqlDbType.VarChar).Value        = string.IsNullOrWhiteSpace(userMiddle) ? (object)DBNull.Value : userMiddle;
                cmd.Parameters.Add("@gender",       SqlDbType.VarChar, 8).Value     = gender;
                cmd.Parameters.Add("@birthDate",    SqlDbType.Date).Value           = DateTime.Parse(birthdate);
                cmd.Parameters.Add("@birthMuni",    SqlDbType.VarChar).Value        = birthMuni;
                cmd.Parameters.Add("@birthProv",    SqlDbType.VarChar).Value        = birthProv;
                cmd.Parameters.Add("@userProv",     SqlDbType.VarChar).Value        = userProv;
                cmd.Parameters.Add("@userCity",     SqlDbType.VarChar).Value        = userCity;
                cmd.Parameters.Add("@userBarangay", SqlDbType.VarChar).Value        = userBarangay;
                cmd.Parameters.Add("@userHouseNum", SqlDbType.VarChar).Value        = userHouseNum;
                cmd.Parameters.Add("@citizenship",  SqlDbType.VarChar, 20).Value    = citizenship;
                cmd.Parameters.Add("@dateOfnat",    SqlDbType.Date).Value           = string.IsNullOrWhiteSpace(dateOfnat) ? (object)DBNull.Value : DateTime.Parse(dateOfnat);
                cmd.Parameters.Add("@certNum",      SqlDbType.VarChar).Value        = string.IsNullOrWhiteSpace(certNum) ? (object)DBNull.Value : certNum;
                cmd.Parameters.Add("@fatName",      SqlDbType.VarChar).Value        = fatName;
                cmd.Parameters.Add("@motName",      SqlDbType.VarChar).Value        = motName;
                cmd.Parameters.Add("@oath",         SqlDbType.VarChar, 15).Value    = oath;
                cmd.Parameters.Add("@registered",   SqlDbType.VarChar, 100).Value   = registered;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void InsertDocument(int userId, byte[] fileBytes, string fileName)
        {
            using (var conn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand(QueryInsertDocument, conn))
            {
                cmd.Parameters.Add("@userId",    SqlDbType.Int).Value            = userId;
                cmd.Parameters.Add("@fileBytes", SqlDbType.VarBinary).Value      = fileBytes;
                cmd.Parameters.Add("@fileName",  SqlDbType.VarChar).Value        = fileName;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static DataRow GetVoterData(int userId)
        {
            using (var conn = new SqlConnection(_connString))
            using (var cmd = new SqlCommand(QueryGetVoterData, conn))
            {
                cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                conn.Open();
                var adapter = new SqlDataAdapter(cmd);
                var table = new DataTable();
                adapter.Fill(table);
                return table.Rows.Count > 0 ? table.Rows[0] : null;
            }
        }
    }
}
