using Present.Core.Domain.Generic;

namespace Present.Domain.Users
{
    public class User : EntityGeneric<int>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
