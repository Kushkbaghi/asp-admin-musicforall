using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicForAllAdmin.Data;
using MusicForAllAdmin.Models;
using LazZiya.ImageResize;
using System.Drawing;

namespace MusicForAllAdmin.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MusicForAllAdminContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;       // Provide info about the web hosting

        public ProductsController(MusicForAllAdminContext context, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
        }

        // GET: Products
        [Route("[controller]/index")]
        public async Task<IActionResult> Index()
        {
            Product product = new Product();        // Create object of Product class

            // Fetch data from API
            HttpResponseMessage? responseTask = GlobalVariables.client.GetAsync("products").Result;

            if (responseTask == null)           // check response
            {
                return NotFound();
            }

            // Store result value of responsTask
            var result = responseTask.Content.ReadFromJsonAsync<List<Product>>().Result;

            return View(result);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Fetch data from API
            HttpResponseMessage? responseTask = GlobalVariables.client.GetAsync("products/" + id).Result;

            if (responseTask == null)           // check response
            {
                return NotFound();
            }

            var result = responseTask.Content.ReadFromJsonAsync<Product>().Result;

            return View(result);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create To protect from overposting attacks, enable the specific properties
        // you want to bind to. For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Name,Price,Quantity,ImageFile,ImageAlt")]
        Product products)
        {
            // Just show admin's namn
            products.CreatedBy = User.Identity.Name.Split("@")[0];

            // Check if input value is valid
            if (ModelState.IsValid)
            {
                // If image exists
                if (products.Price != null)

                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;                                      // Get absolute avalue of root path
                    string fileName = Path.GetFileNameWithoutExtension(products.ImageFile.FileName);         //Get file just filename
                    string extension = Path.GetExtension(products.ImageFile.FileName);                       // Get the fime extension
                    fileName = fileName + DateTime.Now.ToString("yyyyMMddssff") + extension;

                    // Output path
                    string path = Path.Combine(wwwRootPath + "/images/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))          //Create an instant of file
                    {
                        await products.ImageFile.CopyToAsync(fileStream);                    // Copy content of uploaded file to fileStream
                    }

                    // Set file name
                    products.Image = fileName;

                    //// Mask image
                    //CreateNewImg(fileName);
                    // Resize image
                    CreateImageFile(fileName);
                }

                var responseTask = GlobalVariables.client.PostAsJsonAsync("products", products).Result;
                return RedirectToAction(nameof(Index));
            }
            return Redirect("Home/Privacy" + ModelState.IsValid);
        }

        // Resize image
        private void CreateImageFile(string fileName)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;

            using var img = Image.FromFile(Path.Combine(wwwRootPath + "/images/" + fileName));
            img.Scale(960, 960).SaveAs(Path.Combine(wwwRootPath + "/images/thumb_" + fileName));
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5 To protect from overposting attacks, enable the specific properties
        // you want to bind to. For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Quantity,Banner,Cta,Image,ImageAlt,Rate,CreatedBy,CreateAt")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'MusicForAllAdminContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (_context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}