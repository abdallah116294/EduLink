﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Utilities.DTO.User
{
    public class GoogleSignInVM
    {
        [Required]
        public string IdToken { get; set; }
    }
}
