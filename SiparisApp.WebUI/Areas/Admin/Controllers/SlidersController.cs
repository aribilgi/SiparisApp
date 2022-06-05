using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Entities;
using BL;
using Microsoft.AspNetCore.Http;
using SiparisApp.WebUI.Utils;
using Microsoft.AspNetCore.Authorization;

namespace SiparisApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class SlidersController : Controller
    {
        private readonly IRepository<Slider> _repository;

        public SlidersController(IRepository<Slider> repository)
        {
            _repository = repository;
        }

        // GET: SlidersController
        public async Task<ActionResult> Index()
        {
            return View(await _repository.GetAllAsync());
        }

        // GET: SlidersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SlidersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SlidersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Slider slider, IFormFile Image)
        {
            try
            {
                slider.Image = FileHelper.FileLoader(Image);
                await _repository.AddAsync(slider);
                await _repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View(slider);
        }

        // GET: SlidersController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            return View(await _repository.FindAsync(id));
        }

        // POST: SlidersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Slider slider, IFormFile Image)
        {
            try
            {
                if (Image != null)
                {
                    slider.Image = FileHelper.FileLoader(Image);
                }
                _repository.Update(slider);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View(slider);
        }

        // GET: SlidersController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _repository.FindAsync(id));
        }

        // POST: SlidersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Slider slider)
        {
            try
            {
                _repository.Delete(slider);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
