using Microsoft.AspNet.Identity;

namespace DataLayer.Helper
{
    public class IdentitiyMessageModified : IdentityMessage
    {
        public virtual int ID { get; set; }
        public virtual string Token { get; set; }    
        public IdentitiyMessageModified()
            :base()
        {

        }
    }
}
