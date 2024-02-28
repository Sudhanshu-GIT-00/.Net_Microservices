namespace AdminPanel.Utility
{
    public class SD
    {
        public static string CouponAPIBase {  get; set; }
        public static string ProductAPIBase { get; set; }
        public static string AuthAPIBase { get; set; }
        public static string ShoppingCartAPIBase { get; set; }
        public static string OrderAPIBase { get; set; }

        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";
        public const string TokenCookie = "JWTToken";

        public enum ApiType
        {
            GET, 
            POST, 
            PUT, 
            DELETE 
        } 

        public const string status_pending = "pending";
        public const string Status_Approved= "Approved";
        public const string Status_ResdyForPickup= "ResdyForPickup";
        public const string Status_Completed= "Completed";
        public const string Status_Refunded= "Refunded";
        public const string Status_Cancelled= "Cancelled";
        public const string Status_OrderTime= "OrderTime";

        public enum ContentType
        {
            Json,
            MultipartFormData,
        }
    }
}
