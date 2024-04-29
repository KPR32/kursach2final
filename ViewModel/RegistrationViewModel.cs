using System.ComponentModel.DataAnnotations;

namespace FurnitureStore3.ViewModels
{
    public class RegistrationViewModel
    {
        [MinLength(2, ErrorMessage = "Имя не может быть короче двух символов")]
        [MaxLength(50, ErrorMessage = "Максимальная длина имени составляет 50 знаков")]
        [Required(ErrorMessage = "Введите ваше имя")]
        public string Fullname { get; set; }
        
        [MinLength(10, ErrorMessage = "Телефон состоит из 10 чисел")]
        [MaxLength(10, ErrorMessage = "Телефон состоит из 10 чисел")]
        [Required(ErrorMessage = "Введите ваш телефон")]
        public string Phone { get; set; }

        [MinLength(3, ErrorMessage = "Имя пользователя должно быть 3 или более символов")]
        [MaxLength(50, ErrorMessage = "Имя пользователя не должно превышать 50 символов")]
        [RegularExpression(@"[A-Za-z0-9_]*",
            ErrorMessage = "Имя пользователя должно содержать только латинские символы, цифры и символ подчеркивания")]
        [Required(ErrorMessage = "Введите имя пользователя")]
        public string Username { get; set; }

        [MinLength(8, ErrorMessage = "Пароль должен быть длиной 8 или более символов")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Подтвердите пароль")]
        public string RepeatPassword { get; set; }
    }
}
