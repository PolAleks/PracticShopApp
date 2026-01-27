namespace OnlineShopApp.Models
{
    public class Product(int id, string name, decimal cost, string description)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public decimal Cost { get; set; } = cost;
        public string? Description { get; set; } = description;

        public override string ToString()
        {
            return $"Код товара: {Id}{Environment.NewLine}Наименование: {Name}{Environment.NewLine}Цена: {Cost:c}{Environment.NewLine}";
        }
    }
}
