namespace ConsoleApp18
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            int M = 100;
            int numberOfFactories = 3;
            var warehouse = new Warehouse(M * numberOfFactories * 50);
            var factories = new List<Factory>
        {
            FactoryCreator.CreateFactory("Завод A", "Продукция A", 1.0),
            FactoryCreator.CreateFactory("Завод B", "Продукция B", 1.1),
            FactoryCreator.CreateFactory("Завод C", "Продукция C", 1.2)
        };
            var trucks = new List<Truck>
        {
            new Truck(200),
            new Truck(300)
        };
            var cts = new CancellationTokenSource();
            var tasks = factories.Select(factory => factory.ProduceAsync(warehouse, cts.Token)).ToList();
            tasks.Add(Task.Run(async () =>
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    if (warehouse.IsFull)
                    {
                        foreach (var truck in trucks)
                        {
                            truck.Load(warehouse);
                        }
                    }
                    await Task.Delay(100);
                }
            }));
            Console.WriteLine("Нажмите любую кнопку, чтобы прекратить выполнение программы...");
            Console.ReadKey();
            cts.Cancel();
            await Task.WhenAll(tasks);
        }
    }
}
