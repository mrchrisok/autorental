namespace AutoRental.Web.Mvc.Models
{
    public class AccountLoginModel
    {
        public string LoginEmail { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}