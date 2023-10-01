using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Baskets.Dto
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
        virtual public DateTime CreatedDate { get; set; }
        virtual public DateTime? UpdatedDate { get; set; }
        virtual public bool IsActive { get; set; }
    }
}
