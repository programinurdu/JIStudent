﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIStudent.Models.Account
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}