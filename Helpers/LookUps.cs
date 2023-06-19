using System.ComponentModel;

namespace CIP.API.Helpers
{
    public static class LookUps
    {
        public enum Roles
        {
            None,

            [Description("User")]
            User,

            [Description("Admin")]
            Admin
        }
    }
}
