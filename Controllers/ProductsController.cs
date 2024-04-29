using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using FurnitureStore3.Domain.Services;
using FurnitureStore3.Infrastructure;
using FurnitureStore3.Domain.Entities;
using FurnitureStore3.ViewModels;

namespace FurnitureStore3.Controllers
{
    public class ProductsController: Controller
    {
        private readonly IProductsReader reader;
        private readonly IProductsService productsService;
        private readonly IWebHostEnvironment appEnvironment;

        
        public ProductsController(IProductsReader reader,
            IProductsService productsService,
            IWebHostEnvironment appEnvironment/*!!!!!!!!!!!!!!!!!!*/)
        {
            this.reader = reader;
            this.productsService = productsService;
            this.appEnvironment = appEnvironment;            
        }



        [Authorize]
        public async Task<IActionResult> Index(string searchString = "", int categoryId = 0)
        {
            var viewModel = new ProductsCatalogViewModel
            {
                Products = await reader.FindProductsAsync(searchString, categoryId),
                Categories = await reader.GetCategoriesAsync()
            };

            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddProduct()
        {
            var viewModel = new ProductViewModel();

            // загружаем список категорий (List<Category>)
            var categories = await reader.GetCategoriesAsync();

            // получаем элементы для <select> с помощью нашего листа категорий
            // (List<SelectListItem>)
            var items = categories.Select(c =>
                new SelectListItem { Text = c.Name, Value = c.Id.ToString() });

            // добавляем список в модель представления
            viewModel.Categories.AddRange(items);
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddProduct(ProductViewModel productVm)
        {
            // загружаем список категорий (List<Category>)
            var categories = await reader.GetCategoriesAsync();

            // получаем элементы для <select> с помощью нашего листа категорий
            // (List<SelectListItem>)
            var items = categories.Select(c =>
                new SelectListItem { Text = c.Name, Value = c.Id.ToString() });

            // добавляем список в модель представления
            productVm.Categories.AddRange(items);

            if (!ModelState.IsValid)
            {
                return View(productVm);
            }

            try
            {
                var product = new Product
                {
                    Name = productVm.Name,
                    Weight =Convert.ToDouble(productVm.Weight),
                    Price = productVm.Price,
                    CategoryId = productVm.CategoryId,                    
                    Description = productVm.Description
                };
                string wwwroot = appEnvironment.WebRootPath; // получаем путь до wwwroot

                product.ImageUrl =
                    await productsService.LoadPhoto(productVm.Photo.OpenReadStream(), Path.Combine(wwwroot, "images", "products"));
                await productsService.AddProduct(product);

            }
            catch (IOException)
            {
                ModelState.AddModelError("ioerror", "Не удалось сохранить файл.");
                return View(productVm);

            }
            catch
            {
                ModelState.AddModelError("database", "Ошибка при сохранении в базу данных.");
                return View(productVm);
            }

            return RedirectToAction("Index", "Products");
        }

        [HttpGet]
        [Authorize(Roles = "admin")] 
        public async Task<IActionResult> UpdateProduct(int ProductId)
        {
            var product = await reader.FindProductAsync(ProductId);
            if (product is null)
            {
                return NotFound();
            }
            var productVm = new UpdateProductViewModel
            {
                Id = product.Id,                
                ProductName = product.Name,
                Weight = Convert.ToDouble(product.Weight),
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId,                
                PhotoString = product.ImageUrl
            };

            var categories = await reader.GetCategoriesAsync();
            var items = categories.Select(c =>
                new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
            productVm.Categories.AddRange(items);

            return View(productVm);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateProduct(UpdateProductViewModel productVm)
        {
            // загружаем список категорий (List<Category>)
            var categories = await reader.GetCategoriesAsync();

            // получаем элементы для <select> с помощью нашего листа категорий
            // (List<SelectListItem>)
            var items = categories.Select(c =>
                new SelectListItem { Text = c.Name, Value = c.Id.ToString() });

            // добавляем список в модель представления
            productVm.Categories.AddRange(items);
            // если модель не валидна, то возвращаем пользователя на форму
            if (!ModelState.IsValid)
            {
                return View(productVm);
            }
            // находим книгу по Id
            var product = await reader.FindProductAsync(productVm.Id);
            // если книга почему-то не найдена, то выведем сообщение
            if (product is null)
            {
                ModelState.AddModelError("not_found", "Товар не найден!");
                return View(productVm);
            }
            

            try
            {
                // заполняем поля книги                
                product.CategoryId = productVm.CategoryId;
                product.Name = productVm.ProductName;
                product.Weight = productVm.Weight;
                product.Price = productVm.Price;
                product.Description = productVm.Description;                
                // получаем путь до wwwroot
                string wwwroot = appEnvironment.WebRootPath;
                
                  
                // если формой было передано изображение, то меняем его
                if (productVm.Photo is not null)
                {
                    product.ImageUrl = await productsService.LoadPhoto(
                            productVm.Photo.OpenReadStream(),
                            Path.Combine(wwwroot, "images", "products")
                    );
                }
                // обновляем файл
                await productsService.UpdateProduct(product);
            }
            catch (IOException)
            {
                ModelState.AddModelError("ioerror", "Не удалось сохранить файл.");
                return View(productVm);
            }
            catch
            {
                ModelState.AddModelError("database", "Ошибка при сохранении в базу данных.");
                return View(productVm);
            }
            return RedirectToAction("Index", "products");
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var product = await reader.FindProductAsync(productId);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost/*("DeleteProduct")*/]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProductPost(int id)
        {
            var product = await reader.FindProductAsync(id);
            try
            {
                await productsService.DeleteProduct(product);
            }
            catch (Exception)
            {
                ModelState.AddModelError("database", "Ошибка при удалении");
                return View(product);

            }
            return RedirectToAction("Index", "Products");
        }
    }
}
