using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore3.ViewModels
{
    public class ProductViewModel
    {
        [MinLength(2, ErrorMessage = "Название не может быть короче двух символов")]
        [MaxLength(150, ErrorMessage = "Название не может быть таким длинным")]
        [Display(Name = "Название товара")]
        [Required(ErrorMessage = "Введите название товара")]
        public string Name { get; set; } = string.Empty;


        [MinLength(2, ErrorMessage = "Описание не может быть короче двух символов")]
        [MaxLength(1000, ErrorMessage = "Описание не может быть таким длинным")]
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Введите описание товара")]
        public string Description { get; set; } = string.Empty;

        
        [Display(Name = "Вес (в килограммах)")]
        [Range(0.1, 10000, ErrorMessage = "Введите корректный вес")]
        [Required(ErrorMessage = "Введите вес")]
        public double Weight { get; set; }

        
        [Display(Name = "Цена")]
        [Range(1, 10000, ErrorMessage = "Введите корректную цену")]
        [Required(ErrorMessage = "Введите цену")]
        public int Price { get; set; }

        [Display(Name = "Изображение")]
        [Required(ErrorMessage ="Добавьте изображение")]
        public IFormFile Photo { get; set; }




        [Required(ErrorMessage = "Укажите категорию")]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; } = new();
    }
}
