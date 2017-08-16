using ProjektNET.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektNET.Models
{
    public class Trup
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Pole imię jest wymagane")]
        [DisplayName("Imię")]
        [Napis(ErrorMessage = "Imię jest niepoprawne")]
        public string Imie { get; set; }
        [Required(ErrorMessage = "Pole nazwisko jest wymagane")]
        [Napis(ErrorMessage = "Nazwisko jest niepoprawne")]
        public string Nazwisko { get; set; }
        [Required(ErrorMessage = "Pole pesel jest wymagane")]
        [Pesel(ErrorMessage = "Pesel jest niepoprawny")]
        public string Pesel { get; set; }
        [Required(ErrorMessage = "Pole data śmierci jest wymagane")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Data śmierci")]
        public DateTime DataSmierci { get; set; }

        public virtual ICollection<Pogrzeb> Pogrzeby { get; set; }
    }
}