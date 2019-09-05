using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DtoLayer;
using KODOTIFront.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;

namespace KODOTIFront.Controllers
{ 
    [Authorize]
    public class ArtistController : Controller
    {
        private readonly IArtistService _artistService;
        private readonly IAlbumService _albumService;

        public ArtistController(
            IArtistService artistService,
            IAlbumService albumService
            )
        {
            _artistService = artistService;
            _albumService = albumService;
        }

        public async Task<IActionResult> Index(int p=1)
        {
            ViewData["page"] = p;
            return View(
                await _artistService.GetPaged(p)
                );
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Create(ArtistCreateDto model)
        {
            if (ModelState.IsValid)
            {
               var result= await _artistService.Create(model);
                if (result.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
                else
                {

                    throw new Exception("No se pudo crear el artista");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(
                await _artistService.Get(id)
            );
        }

        [HttpPost]
        public async Task<IActionResult> Update(ArtistUpdateDto model, IFormFile file = null)
        {
            if (ModelState.IsValid)
            {
                var result = await _artistService.Update(model, file);

                if (result.IsSuccess)
                {
                    return RedirectToAction("Index");
                }

                throw new Exception("No se pudo actualizar el artista");
            }

            return View(model);
        }

        [HttpGet("/artist/{artistId}/album")]
        public async Task<IActionResult> Album(int artistId)
        {
            var artist = await _artistService.Get(artistId);
            var albums = await _albumService.GetAllByArtist(artistId);

            return View(new AlbumViewModel
            {
                ArtistId = artist.ArtistId,
                ArtistName = artist.Name,
                Albums=albums
            });
        }


        [HttpPost]
        public async Task<IActionResult> AddAlbum(AlbumCreateDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _albumService.Create(model);

                if (result.IsSuccess)
                {
                    return RedirectToAction("Album", new { artistId = model.ArtistId });
                }

                throw new Exception("No se pudo registrar el álbum");
            }

            // Volvemos a pasar el modelo en caso falle para que cargue la data de nuevo
            var artist = await _artistService.Get(model.ArtistId);

            var resultViewModel = new AlbumViewModel
            {
                ArtistId = artist.ArtistId,
                ArtistName = artist.Name,
                AlbumCreate = model
            };

            // Especificamos la ruta de la vista manualmente
            return View("Album", resultViewModel);
        }
    }
}