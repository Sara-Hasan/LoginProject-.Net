using System;

namespace UserApi.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string UserActive { get; set; }
        public string MobileNumber { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
