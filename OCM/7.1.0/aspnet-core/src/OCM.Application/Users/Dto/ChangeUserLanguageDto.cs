using System.ComponentModel.DataAnnotations;

namespace OCM.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}