using RestAPIAsg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPIAsg.Services
{
    public interface IAuthenticateService
    {
        User Authenticate(string userName, string password);
        string ValidateToken(string token);
    }
}
