using AutoMapper;
using CommonLayer;
using DomainLayer;
using DtoLayer;
using Microsoft.Extensions.Logging;
using PersistenceLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface ISongService
    {
        Task<ResponseHelper> Create(SongCreateDto model);
    }
    public class SongService : ISongService
    {
        private readonly DataContext _dataContext;
        private readonly ILogger _logger;

        public SongService(
            DataContext dataContext,
            ILogger<AlbumService> logger
            )
        {
            _dataContext = dataContext;
            _logger = logger;
        }
        public async Task<ResponseHelper> Create(SongCreateDto model)
        {
            var result = new ResponseHelper();
            try
            {
                var entry = Mapper.Map<Song>(model);
                entry.DurationTime = TimeSpan.FromSeconds(model.DurationTime);

                await _dataContext.AddAsync(entry);
                await _dataContext.SaveChangesAsync();

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
