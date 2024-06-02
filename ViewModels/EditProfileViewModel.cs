using System.ComponentModel.DataAnnotations;

namespace TowFinder.ViewModels
{
    public class EditProfileViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string District { get; set; }

        [Required]
        public string ApprovalStatus { get; set; }
    }
}
