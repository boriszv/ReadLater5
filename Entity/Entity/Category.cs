﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Category
    {
        [Key]
        public int ID { get; set; }

        [StringLength(maximumLength: 50)]
        public string Name { get; set; }

        public string UserID { get; set; }
        public IdentityUser User { get; set; }
    }
}
