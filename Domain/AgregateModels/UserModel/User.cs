using Microsoft.AspNetCore.Identity;


namespace Domain.AgregateModels.UserModel
{
    public class User : IdentityUser
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        
    }
}
