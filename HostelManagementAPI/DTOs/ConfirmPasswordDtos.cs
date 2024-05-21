﻿using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class ConfirmPasswordDtos
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string SystemPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
