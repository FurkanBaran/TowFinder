
using System.ComponentModel.DataAnnotations;

namespace TowFinder.ViewModels
{
    public class TowOperatorRegisterViewModel
    {
        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Telefon alanı zorunludur.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Şehir alanı zorunludur.")]
        public string City { get; set; }

        [Required(ErrorMessage = "İlçe alanı zorunludur.")]
        public string District { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı alanı zorunludur.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}