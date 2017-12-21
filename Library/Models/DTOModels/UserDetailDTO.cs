using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DTOModels
{
    /// <summary>
    /// The user detail DTO model holds user id, name, address, email and a loan history.
    /// The model is for displaying full detailed information about an user, for example when requesting
    /// for an user by his/her id.
    /// </summary>
    public class UserDetailDTO
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public List<UserBookLoanDTO> LoanHistory { get; set; }
    }
}
