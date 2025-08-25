using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Model
{
    public class RegisterResponse
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public int ItemCount { get; set; }
    }
}
