using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace category_service.Models
{
    [Table("categorie")]
    public class Categorie
    {
        [Key]
        [Column("cat_id")]
        public int Id { get; set; }

        [Column("cat_name")]
        public string Name { get; set; } = "";
    }
}
