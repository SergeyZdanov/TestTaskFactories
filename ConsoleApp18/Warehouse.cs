namespace ConsoleApp18
{
    internal class Warehouse
    {
        private readonly Dictionary<Product, int> _inventory = new();
        private readonly object _lock = new();
        private readonly int _capacity;
        public Warehouse(int capacity)
        {
            _capacity = capacity;
        }
        public int CurrentLoad
        {
            get
            {
                lock (_lock)
                {
                    return _inventory.Values.Sum();
                }
            }
        }
        public void AddProduct(Product product, int quantity)
        {
            lock (_lock)
            {
                if (!_inventory.ContainsKey(product))
                    _inventory[product] = 0;
                _inventory[product] += quantity;
                Console.WriteLine($"Добавлено {quantity} продукции  {product.Name}. Текущая нагрузка: {CurrentLoad}/{_capacity}");
            }
        }
        public Dictionary<Product, int> RemoveProducts(int maxQuantity)
        {
            lock (_lock)
            {
                var removedProducts = new Dictionary<Product, int>();
                int totalRemoved = 0;
                foreach (var entry in _inventory.ToList())
                {
                    if (totalRemoved >= maxQuantity) break;
                    int toRemove = Math.Min(entry.Value, maxQuantity - totalRemoved);
                    _inventory[entry.Key] -= toRemove;
                    totalRemoved += toRemove;
                    if (_inventory[entry.Key] == 0)
                        _inventory.Remove(entry.Key);
                    removedProducts[entry.Key] = toRemove;
                }
                Console.WriteLine($"Удалено {totalRemoved} продукция. Текущая нагрузка: {CurrentLoad}/{_capacity}");
                return removedProducts;
            }
        }
        public bool IsFull => CurrentLoad >= _capacity * 0.95;
    }
}
