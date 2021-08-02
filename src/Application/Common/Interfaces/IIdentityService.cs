using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(int userId);

        Task<bool> IsInRoleAsync(int userId, string role);

        Task<bool> AuthorizeAsync(int userId, string policyName);

        Task<(Result Result, int UserId)> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(int userId);

        Task<Result> CreateUserAsync(string username, string email, string password);
    }
}
