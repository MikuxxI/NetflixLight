using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace user_service.Model
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("use_id")]
        public int Id { get; set; }

        [Column("use_firstname")]
        public string? Firstname { get; set; } = "";

        [Column("use_lastname")]
        public string? Lastname { get; set; } = "";

        [Column("use_username")]
        public string? Username { get; set; } = "";

        [Column("use_password")]
        public string? Password { get; set; } = "";

        [Column("use_sold")]
        public double Sold { get; set; }

        [Column("use_adminrole")]
        public bool? AdminRole { get; set; }
    }
}
