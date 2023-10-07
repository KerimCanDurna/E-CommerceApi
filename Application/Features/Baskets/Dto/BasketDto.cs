using Application.Features.Products.Commands.Create;
using Domain.AgregateModels.CartModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Baskets.Dto
{
    public class BasketDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? GuestId { get; set; }
        virtual public bool IsActive { get; set; }
        virtual public DateTime CreatedDate { get; set; }
        

    }
}
