using AutoMapper;
using CommonLayer;
using DomainLayer;
using DtoLayer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersistenceLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IArtistService
    {
        Task<ResponseHelper> Create(ArtistCreateDto model);
        Task<DataCollection<ArtistDto>> GetPaged(int page = 1);
        Task<ResponseHelper> Update(ArtistUpdateDto model, IFormFile file = null);
        Task<ArtistDto> Get(int id);
        Task<ResponseHelper> Delete(int artistId);
    }

    public class ArtistService : IArtistService
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _hostingEnvironment;


        public ArtistService(
            DataContext context,
            ILogger<UserService> logger,
            IHostingEnvironment hostingEnvironment
        )
        {
            _context = context;
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
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

        public async Task<ResponseHelper> Update(ArtistUpdateDto model, IFormFile file = null)
        {
            var result = new ResponseHelper();

            try
            {
                var originalEntry = await _context.Artists.SingleAsync(x => x.ArtistId == model.ArtistId);
                Mapper.Map(model, originalEntry);

                if (file != null && file.Length > 0)
                {
                    var filePath = _hostingEnvironment.WebRootPath + @"\uploads\" + file.FileName;

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        originalEntry.LogoUrl = file.FileName.ToLower().Trim();
                    }
                }

                _context.Update(originalEntry);
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

        public async Task<ArtistDto> Get(int id)
        {
            var result = new ArtistDto();

            try
            {
                result = Mapper.Map<ArtistDto>(
                    await _context.Artists.SingleAsync(x => x.ArtistId == id)
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return result;
        }

        public async Task<ResponseHelper> Delete(int artistId)
        {
            var result = new ResponseHelper();

            try
            {
                _context.Remove(new Artist
                {
                    ArtistId = artistId
                });

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

