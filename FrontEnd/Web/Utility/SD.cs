namespace Web.Utility
{
    public class SD
    {
        public static string AuthAPIBase { get; set; }
        public static string ProductAPIBase { get; set; }
        public static string CategoryAPIBase { get; set; }

        public const string TokenCookie = "JWTToken";
        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
