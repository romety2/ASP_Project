using ProjektNET.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektNET.Models
{
    public class Pogrzeb
    {
        public int PogrzebID { get; set; }
        [DisplayName("Trup")]
        public int TrupID { get; set; }
        [DisplayName("Grabarz")]
        public int GrabarzID { get; set; }
        [Required(ErrorMessage = "Pole data jest wymagane")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }
        [Min(0.0, ErrorMessage = "Cena nie może być mniejsza niż 0")]
        [Required(ErrorMessage = "Pole cena jest wymagane")]
        public double Cena { get; set; }
        public string Opis { get; set; }

        public virtual Trup Trup { get; set; }
        public virtual Grabarz Grabarz { get; set; }
    }
}