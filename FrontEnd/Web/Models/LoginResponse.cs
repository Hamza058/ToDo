using EntityLayer.Concrete;

namespace Web.Models
{
    public class LoginResponse
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
}
