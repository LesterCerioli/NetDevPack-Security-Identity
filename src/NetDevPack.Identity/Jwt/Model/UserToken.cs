﻿using System.Collections.Generic;

namespace NetDevPack.Identity.Jwt.Model
{
    public class UserToken
    {
        public dynamic Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }
    }
}