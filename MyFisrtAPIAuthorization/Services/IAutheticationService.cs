using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFisrtAPIAuthorization.Services
{
    public interface IAutheticationService
    {
        Task<string> AuthenticateAsync(string name, string id);
    }
}
