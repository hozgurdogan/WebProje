using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RezervasyonUcak.Models
{

    public enum Role
    {
        User,
        Admin
    }


    public class User
    {
        [Key]
        private int id;
        private string name;    
        private string surname;
        private string email;
        private string password;
        private Role role;
        private bool deleted;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public Role Role { get => role; set => role = value; }
        public bool Deleted { get => deleted; set => deleted = value; }
    }
}
