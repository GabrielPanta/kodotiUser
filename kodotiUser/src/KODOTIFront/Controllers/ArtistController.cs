using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DtoLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;

namespace KODOTIFront.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistService _artistService;

        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
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
    }
}