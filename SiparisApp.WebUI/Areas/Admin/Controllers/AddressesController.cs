using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Entities;
using BL;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SiparisApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AddressesController : Controller
    {
        private readonly IRepository<Address> _repository; // Dependency Injection ile veritabanı işlemleri yapabilmek için IRepository interface ini burada readonly olarak tanımlıyoruz. Startup.cs dosyasında tanımladığımız services sayesinde burada constructor da yapacağımız DI işlemiyle bizim için Repository sınıfından adres işlemleri için bir nesne oluşturulur.
        private readonly IRepository<Customer> _customerRepository;
        private readonly CustomerManager _customerManager;

        public AddressesController(IRepository<Address> repository, CustomerManager customerManager, IRepository<Customer> customerRepository)
        {
            _repository = repository;
            _customerManager = customerManager;
            _customerRepository = customerRepository;
        }

        // GET: AddressesController
       /* public async Task<ActionResult> Index()
        {
            return View(await _repository.GetAllAsync()); // GetAllAsync metodu asenkron metod olduğu için asenkron metotları çağırmak için başına await anahtar kelimesi yazmak zorundayız! await yazdığımızda ise ActionResult metodunu async olarak değiştirmemiz gerekir. Bu işlemi de ampule tıklayıp make method async diyerek visual studio ya yaptırabiliriz..
        }*/
        public ActionResult Index()
        {
            return View(_repository.GetAllInclude("Customer"));
        }
        // GET: AddressesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AddressesController/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.CustomerId = new SelectList(await _customerManager.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: AddressesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Address address)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    address.CreateDate = DateTime.Now;
                    await _repository.AddAsync(address);
                    await _repository.SaveChangesAsync(); //
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
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.CustomerId = new SelectList(await _customerRepository.GetAllAsync(), "Id", "Name");
            return View(await _repository.FindAsync(id));
        }

        // POST: AddressesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Address address)
        {
            try
            {
                _repository.Update(address);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.CustomerId = new SelectList(await _customerRepository.GetAllAsync(), "Id", "Name");
                return View(address);
            }
        }

        // GET: AddressesController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            return View(await _repository.FindAsync(id));
        }

        // POST: AddressesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Address address)
        {
            try
            {
                _repository.Delete(address);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
