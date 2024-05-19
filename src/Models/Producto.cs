namespace App.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public decimal Price { get; set; }
        public string? Description { get; set; } 
        public int Stock { get; set; }
        public int MaxStock { get; set; }
    }
}