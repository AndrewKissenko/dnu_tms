using tms.Models;
using System.ComponentModel.DataAnnotations;

namespace tms.Requests
{
    public class CreateUserRequest: User
    {

        [Required]
        public string RoleId { get; set; }
    }
}
