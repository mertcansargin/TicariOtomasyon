﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicariOtomasyon.Models.Siniflar
{
    public class Mesajlar
    {

        [Key]
        public int MesajID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Gönderici { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Alıcı { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Konu { get; set; } 

        [Column(TypeName = "Varchar")]
        [StringLength(2000)]
        public string Icerik { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Tarih { get; set; }
    }
}