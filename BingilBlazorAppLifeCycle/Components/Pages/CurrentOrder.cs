public static class CurrentOrder
{
    public static List<OrderItem> Items { get; private set; } = new();

    public static void SetOrder(List<OrderItem> items)
    {
        Items.Clear();
        Items.AddRange(items);
    }

    public static decimal TotalAmount => Items.Sum(x => x.Price * x.Quantity);

    public static void Clear() => Items.Clear();

    public class OrderItem
    {
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}