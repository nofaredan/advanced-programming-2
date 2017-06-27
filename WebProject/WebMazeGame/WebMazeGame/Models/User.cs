using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMazeGame.Models
{
    /// <summary>
    /// User class
    /// </summary>
    public class User
    {
        public int Id { set; get; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public DateTime DateRegister { get; set; }
    }
}