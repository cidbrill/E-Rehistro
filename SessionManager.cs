using System;
using System.Web;

namespace e_rehistro
{
    public static class SessionManager
    {
        public static int UserId
        {
            get
            {
                var val = HttpContext.Current.Session["UserId"];
                return val != null ? Convert.ToInt32(val) : 0;
            }
            set => HttpContext.Current.Session["UserId"] = value;
        }

        public static string Email
        {
            get => HttpContext.Current.Session["Email"]?.ToString() ?? string.Empty;
            set => HttpContext.Current.Session["Email"] = value;
        }

        public static string Role
        {
            get => HttpContext.Current.Session["Role"]?.ToString() ?? string.Empty;
            set => HttpContext.Current.Session["Role"] = value;
        }

        public static bool IsLoggedIn => UserId > 0;

        public static bool IsAdmin => Role == "admin";

        public static void Clear() => HttpContext.Current.Session.Clear();
    }
}
