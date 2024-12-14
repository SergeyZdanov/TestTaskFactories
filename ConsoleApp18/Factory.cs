namespace ConsoleApp18
{
    internal class Factory
    {
        public string Name { get; }
        public Product ProductType { get; }
        public double ProductionRate { get; }

        public Factory(string name, Product productType, double productionRate)
        {
            Name = name;
            ProductType = productType;
            ProductionRate = productionRate;
        }

        public async Task ProduceAsync(Warehouse warehouse, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(10);
                int unitsProduced = (int)ProductionRate;
                warehouse.AddProduct(ProductType, unitsProduced);
            }
        }
    }
}
