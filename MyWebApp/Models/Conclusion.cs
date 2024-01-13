using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public enum Conclusion
    {
        [Display(Name ="Выздоровление")]
        Recovery,
        [Display(Name = "Болезнь")]
        Disease,
        [Display(Name = "Смерть")]
        Death
    }
}
