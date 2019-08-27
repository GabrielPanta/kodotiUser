using AutoMapper;
using CommonLayer;
using DomainLayer;
using DtoLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersistenceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IArtistService
    {
        Task<ResponseHelper> Create(ArtistCreateDto model);
        Task<DataCollection<ArtistDto>> GetPaged(int page = 1);
    }

    public class ArtistService : IArtistService
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;

        public ArtistService(
            DataContext context,
            ILogger<UserService> logger
        )
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ResponseHelper> Create(ArtistCreateDto model)
        {
            var result = new ResponseHelper();

            try
            {
               var entry= Mapper.Map<Artist>(model);
                entry.LogoUrl ="logo.png";
                await _context.AddAsync(entry);
                await _context.SaveChangesAsync();

                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return result;
        }
        public async Task<DataCollection<ArtistDto>> GetPaged(int page=1)
        {
            var result = new DataCollection<ArtistDto>();

            try
            {
                var take = 2;

                page--;

                if (page > 0)
                {
                    page = page * take;
                }
                var records = (await _context.Artists.OrderByDescending(x => x.ArtistId)
                    .Skip(page)
                    .Take(take)
                    .ToListAsync());

                result.Items = 
                    Mapper.Map<List<ArtistDto>>(records);

                result.Total = await _context.Artists.CountAsync();
                result.Pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(result.Total) / take));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return result;
        }
    }
}

