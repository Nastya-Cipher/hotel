using System.Web.Security;
using BookingHotels.WebUI.Infrastructure.Abstract;

namespace BookingHotels.WebUI.Infrastructure.Concrete
{
    public class FormAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
                FormsAuthentication.SetAuthCookie(username, false);
            return result;
        }
    }
}