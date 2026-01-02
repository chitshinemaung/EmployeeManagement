namespace EmployeeManagementSystem.Models
{
    public class LoginRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseModel
    {
        
        public string Username { get; set; } = string.Empty;
        public string fullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;       
        public string Email { get; set; }

        //public string Token { get; set; }
        //public DateTime Expiration { get; set; }
        //public string Password { get; set; }
        //public string Phone { get; set; }
        //public string Address { get; set; }
        //public string City { get; set; }
        //public string State { get; set; }
        //public string ZipCode { get; set; }
        //public string Country { get; set; }

    }
}
