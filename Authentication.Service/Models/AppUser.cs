using Microsoft.AspNetCore.Identity;

namespace Authentication.Service.Models;

public class AppUser : IdentityUser { 
    public List<PortFolio> PortFolios { get; set; } = [];
}
