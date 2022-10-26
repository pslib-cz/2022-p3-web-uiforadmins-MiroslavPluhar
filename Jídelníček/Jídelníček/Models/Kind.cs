using System.ComponentModel.DataAnnotations;

namespace Jídelníček.Models
{
    public enum Kind
    {
        [Display(Name = "Snídaně")]
        Snídaně,
        [Display(Name = "Oběd")]
        Oběd,
        [Display(Name = "Večeře")]
        Večeře,
    }
}
