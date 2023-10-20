using DataAccess.DataSource.SqlServer;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using DataAccess.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Implementations
{
    public class SqlServerUserRepository : IUserRepository
    {
        private readonly TestDbContext _dbContext;

        public SqlServerUserRepository(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<User>> AddUserAsync(User user)
        {
            try
            {
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
                return new OperationResult<User> { Succeeded = true , Data = user};
            }
            catch (Exception ex)
            {
                return new OperationResult<User> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<OperationResult<Guid>> DeleteUserAsync(Guid id)
        {
            var result = await GetUserByIdAsync(id);
            if (!result.Succeeded) return new OperationResult<Guid> { Succeeded = false, ErrorMessage = result.ErrorMessage };

            try
            {
                _dbContext.Users.Remove(result.Data);
                await _dbContext.SaveChangesAsync();
                return new OperationResult<Guid> { Succeeded = true, Data = id };
            }
            catch (Exception ex)
            {
                return new OperationResult<Guid> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<OperationResult<IEnumerable<User>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _dbContext.Users.ToListAsync();
                return new OperationResult<IEnumerable<User>> { Succeeded = true, Data = users };
            }
            catch (Exception ex)
            {
                return new OperationResult<IEnumerable<User>> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<OperationResult<User>> GetUserByIdAsync(Guid id)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (user != null)
                {
                    return new OperationResult<User> { Succeeded = true, Data = user };
                }
                else
                {
                    return new OperationResult<User> { Succeeded = false, ErrorMessage = "NotFound" };
                }
            }
            catch (Exception ex)
            {
                return new OperationResult<User> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<OperationResult<User>> UpdateUserAsync(User user)
        {
            try
            {
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
                return new OperationResult<User> { Succeeded = true , Data = user };
            }
            catch (Exception ex)
            {
                return new OperationResult<User> { Succeeded = false, ErrorMessage = ex.Message };
            }
        }
    }
}
