﻿namespace Trainify.Models
{
    public class UpdatePasswordModel
    {
        public string Id { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
