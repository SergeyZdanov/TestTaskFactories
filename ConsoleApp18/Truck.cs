namespace ConsoleApp18
{
    internal class Truck
    {
        public int Capacity { get; }
        public Truck(int capacity)
        {
            Capacity = capacity;
        }
        public void Load(Warehouse warehouse)
        {
            var products = warehouse.RemoveProducts(Capacity);
            Console.WriteLine($"Грузовик загрузил {products.Sum(p => p.Value)} продукции.");
        }
    }

    class FactoryCreator
    {
        public static Factory CreateFactory(string factoryName, string productName, double productionRateMultiplier)
        {
            Product product = new Product(productName, 1.0, "Стандартная упаковка");
            double baseProductionRate = 50;
            return new Factory(factoryName, product, baseProductionRate * productionRateMultiplier);
        }
    }
}
