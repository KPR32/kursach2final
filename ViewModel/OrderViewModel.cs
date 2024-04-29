using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore3.ViewModels
{
    public class OrderViewModel
    {
        public int ClientUserId { get; set; }
        public int ProductId { get; set; }
        public int Price { get; set; }
        public DateTime OrderStart { get; set; } = DateTime.Now;
        [Display(Name = "Выберите дату доставки заказа")]
        [Range(typeof(DateTime), "21.12.2023", "28.12.2023", ErrorMessage = "введите корректные значения даты доставки (до 7 календарных дней, начиная с текущего)")]
        public DateTime OrderFinish { get; set; } = DateTime.Now;

        [MinLength(6, ErrorMessage = "Введите адрес корректно")]
        [MaxLength(1000, ErrorMessage = "Введите адрес корректно")]
        [Display(Name = "Введите адрес доставки товара (Укажите улицу, дом, кв.)")]
        [Required(ErrorMessage = "Введите адрес доставки")]
        public string OrderAddress { get; set; } = null!;

        public List<SelectListItem> Categories { get; set; } = new();
        public List<SelectListItem> Products { get; set; } = new();             
    }
}
