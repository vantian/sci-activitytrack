using System.ComponentModel.DataAnnotations;

namespace Bkpm.ActivityTracker.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}