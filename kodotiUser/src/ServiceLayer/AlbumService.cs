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
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{

    public interface IAlbumService
    {
        Task<ResponseHelper> Create(AlbumCreateDto model);
        Task<IEnumerable<AlbumDto>> GetAllByArtist(int artistId);
    }

    public class AlbumService : IAlbumService
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;

        public AlbumService(
            DataContext context,
            ILogger<AlbumService> logger
        )
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ResponseHelper> Create(AlbumCreateDto model)
        {
            var result = new ResponseHelper();

            try
            {
                var entry = Mapper.Map<Album>(model);

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

        public async Task<IEnumerable<AlbumDto>> GetAllByArtist(int artistId)
        {
            var result = new List<AlbumDto>();

            try
            {
                var records = await _context.Albums.Include(x => x.Songs)
                                                   .Where(x => x.ArtistId == artistId)
                                                   .ToListAsync();

                result = Mapper.Map<List<AlbumDto>>(records);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return result;
        }
    }
}
