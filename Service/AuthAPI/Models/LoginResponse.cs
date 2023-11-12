﻿using EntityLayer.Concrete;

namespace AuthAPI.Models
{
    public class LoginResponse
    {
        public User User { get; set; }
        public string? Token { get; set; }
    }
}
