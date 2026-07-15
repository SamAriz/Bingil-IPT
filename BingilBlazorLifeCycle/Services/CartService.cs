namespace BingilBlazorLifeCycle.Services;

public class CartService
{
    public List<CartItem> Items { get; } = new();

    public int TotalQuantity => Items.Sum(item => item.Quantity);

    public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);

    public void AddItem(string name, decimal price, int quantity)
    {
        if (quantity <= 0)
        {
            return;
        }

        var existingItem = Items.FirstOrDefault(item => item.Name == name);

        if (existingItem is null)
        {
            Items.Add(new CartItem
            {
                Name = name,
                Price = price,
                Quantity = quantity
            });
            return;
        }

        existingItem.Quantity += quantity;
    }

    public void Clear() => Items.Clear();
}

public class CartItem
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
