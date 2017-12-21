using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.EntityModels
{
    /// <summary>
    /// The user entity model maps the user database table. It holds the
    /// Id of the user, the name, address, email and the phone number.
    /// </summary>
    public class User
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
