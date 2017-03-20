namespace AutoRental.Web.Mvc.Core
{
    public interface ISecurityAdapter_x
    {
        void Initialize();
        void Register(string loginEmail, string password, object propertyValues);
        bool Login(string loginEmail, string password, bool rememberMe);
        bool ChangePassword(string loginEmail, string oldPassword, string newPassword);
        bool UserExists(string loginEmail);
    }
}