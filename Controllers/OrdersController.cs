using FurnitureStore3.Domain.Entities;
using FurnitureStore3.Domain.Services;
using FurnitureStore3.Infrastructure;
using FurnitureStore3.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestSharp;
using System.Numerics;
using System;
using System.Web;
using System.Xml.Linq;

namespace FurnitureStore3.Controllers
{
    public class OrdersController: Controller
    {
        private readonly IOrdersReader reader;
        private readonly IOrdersService ordersService1;
        private readonly IWebHostEnvironment appEnvironment;


        public OrdersController(IOrdersReader reader,
           IOrdersService ordersService1,
           IWebHostEnvironment appEnvironment)
        {
            this.reader = reader;
            this.ordersService1 = ordersService1;
            this.appEnvironment = appEnvironment;            
        }

        [Authorize]
        public async Task<IActionResult> MyOrders(string searchString = "", int categoryId = 0)
        {
            var viewModel = new ProductsCatalogViewModel
            {                
                Orders = await reader.GetAllOrdersAsync(),
                Categories = await reader.GetCategoriesAsync(),
                Products = await reader.FindProductsAsync(searchString, categoryId),
            };            

            User.Identity.ToString();
            return View(viewModel);

        }

        [HttpGet]        
        public async Task<IActionResult> AddOrder(int ProductId)
        {
            var viewModel = new OrderViewModel();  
            // загружаем список категорий (List<Category>)
            var categories = await reader.GetCategoriesAsync();
            var products = await reader.GetAllProductsAsync();




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

            var items = categories.Select(c =>
                new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
            viewModel.Categories.AddRange(items);




            return View(viewModel);
        }

        [HttpPost]        
        public async Task<IActionResult> AddOrder(OrderViewModel orderVm)
        {
            
            // загружаем список категорий (List<Category>)
            var categories = await reader.GetCategoriesAsync();
            var products = await reader.GetAllProductsAsync();
            // получаем элементы для <select> с помощью нашего листа категорий
            // (List<SelectListItem>)
            var items = categories.Select(c =>
                new SelectListItem { Text = c.Name, Value = c.Id.ToString() });

            var items1 = products.Select(p => new SelectListItem { Value = p.Name, Text = p.Id.ToString() });
            // добавляем список в модель представления            
            orderVm.Categories.AddRange(items);            


            if (!ModelState.IsValid)
            {
                return View(orderVm);
            }

            // находим книгу по Id
            var product = await reader.FindProductAsync(orderVm.ProductId);
            // если книга почему-то не найдена, то выведем сообщение
            if (product is null)
            {
                ModelState.AddModelError("not_found", "Товар не найден!");
                return View(orderVm);
            }


            try
            {
                var order = new Order
                {
                    ClientUserId = orderVm.ClientUserId,
                    ProductId = orderVm.ProductId,
                    Price = orderVm.Price,
                    OrderStart = orderVm.OrderStart,
                    OrderFinish = orderVm.OrderFinish,
                    OrderAddress = orderVm.OrderAddress
                };
                string wwwroot = appEnvironment.WebRootPath; // получаем путь до wwwroot               
                await ordersService1.AddOrder(order);
            }
            catch (IOException)
            {
                ModelState.AddModelError("ioerror", "Не удалось сохранить файл.");
                return View(orderVm);

            }
            catch
            {
                ModelState.AddModelError("database", "Ошибка при сохранении в базу данных.");
                return View(orderVm);
            }

            return RedirectToAction("Index", "products");
        }
    }
}
