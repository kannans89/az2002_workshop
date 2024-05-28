namespace _04ProductRazorPageApp.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; }

    }
}
