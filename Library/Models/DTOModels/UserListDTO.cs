using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DTOModels
{
    /// <summary>
    /// The user list DTO model holds user full name and id and is for
    /// displaying less information about an user, for example when displaying
    /// a list of all users.
    /// </summary>
    public class UserListDTO
    {
        public int UserId { get; set; }
        public string FullName { get; set; }

    }
}
