using System;
using System.Web;

namespace e_rehistro
{
    public static class SessionManager
    {
        private const string AdminRole = "admin";

        public static int UserId
        {
            get
            {
                var session = HttpContext.Current?.Session;
                if (session == null) return 0;
                return int.TryParse(session["UserId"]?.ToString(), out var id) ? id : 0;
            }
            set
            {
                if (HttpContext.Current?.Session != null)
                    HttpContext.Current.Session["UserId"] = value;
            }
        }

        public static string Email
        {
            get => HttpContext.Current?.Session?["Email"]?.ToString() ?? string.Empty;
            set
            {
                if (HttpContext.Current?.Session != null)
                    HttpContext.Current.Session["Email"] = value;
            }
        }

        public static string Role
        {
            get => HttpContext.Current?.Session?["Role"]?.ToString() ?? string.Empty;
            set
            {
                if (HttpContext.Current?.Session != null)
                    HttpContext.Current.Session["Role"] = value;
            }
        }

        public static bool IsLoggedIn => UserId > 0;

        public static bool IsAdmin => Role.Equals(AdminRole, StringComparison.OrdinalIgnoreCase);

        public static void Clear() => HttpContext.Current?.Session?.Clear();
    }
}
