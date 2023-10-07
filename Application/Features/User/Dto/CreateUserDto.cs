using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TokenIdentity.Dto
{
    public class CreateUserDto
    {
        public string? GuestId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Password { get; set; }
        public string PasswordAgain { get; set; }
    }
}
