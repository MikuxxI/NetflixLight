namespace category_service.Messaging
{
    public class CategorieCreatedCommand
    {
        public int CategorieId { get; set; }
        public string CategorieName { get; set; } = "";
    }
}
