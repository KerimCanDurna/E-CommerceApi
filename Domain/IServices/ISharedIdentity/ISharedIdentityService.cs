using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IServices.ISharedIdentity
{
    public interface ISharedIdentityService
    {
        public string GetUserId { get; }
    }
}
