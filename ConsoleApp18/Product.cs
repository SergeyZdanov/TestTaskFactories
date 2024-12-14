namespace ConsoleApp18
{
    internal class Product
    {
        public string Name { get; }
        public double Weight { get; }
        public string Packaging { get; }

        public Product(string name, double weight, string packaging)
        {
            Name = name;
            Weight = weight;
            Packaging = packaging;
        }

    }
}
