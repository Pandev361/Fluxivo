using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluxivo.Infrastructure.Models
{
    public class UserRole
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }

}
