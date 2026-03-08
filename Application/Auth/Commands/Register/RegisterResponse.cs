using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Auth.Commands.Register
{
    public class RegisterResponse
    {
        public bool Succeeded { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
