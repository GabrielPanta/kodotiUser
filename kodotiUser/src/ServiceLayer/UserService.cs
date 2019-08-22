using AutoMapper;
using CommonLayer;
using DtoLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersistenceLayer;
using System;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IUserService
    {
        Task<UserGetDto> Get(string id);
        Task<ResponseHelper> Update(UserUpdateDto model);
    }

    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;

        public UserService(
            DataContext context,
            ILogger<UserService> logger
        )
        {
            _context = context;
            _logger = logger;
        }

        public async Task<UserGetDto> Get(string id)
        {
            var result = new UserGetDto();

            try
            {
                result = Mapper.Map<UserGetDto>(
                    await _context.Users.SingleAsync(x => x.Id == id)
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return result;
        }

        public async Task<ResponseHelper> Update(UserUpdateDto model)
        {
            var result = new ResponseHelper();

            try
            {
                _logger.LogInformation("Obteniendo usuario: {0}", model.Id);
                var originalEntry = await _context.Users.SingleAsync(x => x.Id == model.Id);

                _logger.LogInformation("Seteando propiedades");
                originalEntry.FirstName = model.FirstName;
                originalEntry.LastName = model.LastName;

                _logger.LogInformation("Actualizando usuario");
                _context.Update(originalEntry);

                _logger.LogInformation("Guardando los cambios");
                await _context.SaveChangesAsync();

                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return result;
        }
    }
}

