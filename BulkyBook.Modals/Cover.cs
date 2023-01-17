﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Modals
{
    public class Cover
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Cover Type")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
