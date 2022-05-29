using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using BL;

namespace SiparisApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AddressesController : Controller
    {
        private readonly IRepository<Address> _repository; // Dependency Injection ile veritabanı işlemleri yapabilmek için IRepository interface ini burada readonly olarak tanımlıyoruz. Startup.cs dosyasında tanımladığımız services sayesinde burada constructor da yapacağımız DI işlemiyle bizim için Repository sınıfından adres işlemleri için bir nesne oluşturulur.

        public AddressesController(IRepository<Address> repository)
        {
            _repository = repository;
        }

        // GET: AddressesController
        public async Task<ActionResult> IndexAsync()
        {
            return View(await _repository.GetAllAsync()); // GetAllAsync metodu asenkron metod olduğu için asenkron metotları çağırmak için başına await anahtar kelimesi yazmak zorundayız! await yazdığımızda ise ActionResult metodunu async olarak değiştirmemiz gerekir. Bu işlemi de ampule tıklayıp make method async diyerek visual studio ya yaptırabiliriz..
        }

        // GET: AddressesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AddressesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AddressesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Address address)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    address.CreateDate = DateTime.Now;
                    await _repository.AddAsync(address);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(address);
        }

        // GET: AddressesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AddressesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: AddressesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AddressesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}
