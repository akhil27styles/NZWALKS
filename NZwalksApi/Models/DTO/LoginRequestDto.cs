﻿using System.ComponentModel.DataAnnotations;

namespace NZwalksApi.Models.DTO
{
    public class LoginRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]

        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]

        public string Password { get; set; }


    }
}
