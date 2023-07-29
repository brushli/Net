using System.ComponentModel.DataAnnotations;

namespace Lee.EMS.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}