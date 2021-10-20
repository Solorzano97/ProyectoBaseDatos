using Microsoft.AspNetCore.Identity;

namespace Dominio
{
    public class usuario : IdentityUser
    {
        public string nombreCompleto {get;set;}
    }
}