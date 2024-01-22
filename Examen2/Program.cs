using System;

public class Order
{
    public string Customer { get; set; }
    public DateTime Date { get; set; }
    public decimal Total { get; set; }
}

public abstract class PaymentSystem
{
    public abstract void ProcessPayment(decimal amount);
}

public class CardPaymentSystem : PaymentSystem
{
    public override void ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Plata online cu cardul pentru suma de {amount} RON.");
    }
}

public class PayPalPaymentSystem : PaymentSystem
{
    public override void ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Plata prin PayPal pentru suma de {amount} RON.");
    }
}

public class CryptoWalletPaymentSystem : PaymentSystem
{
    public override void ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Plata folosind un wallet crypto pentru suma de {amount} RON.");
    }
}

public class OrderProcessor
{
    public void FulfillOrder(Order order, PaymentSystem paymentSystem)
    {
        Console.WriteLine($"Comanda pentru {order.Customer}, total: {order.Total} RON, data: {order.Date.ToShortDateString()}");

        // Procesează plata folosind sistemul ales
        paymentSystem.ProcessPayment(order.Total);

        Console.WriteLine("Comanda procesată cu succes!");
    }
}

class Program
{
    static void Main()
    {
        // Citirea informațiilor despre comandă de la tastatură
        Order order = new Order();
        Console.Write("Introduceți numele clientului: ");
        order.Customer = Console.ReadLine();
        Console.Write("Introduceți data comenzii (MM/DD/YYYY): ");
        order.Date = DateTime.Parse(Console.ReadLine());
        Console.Write("Introduceți valoarea totală a comenzii: ");
        order.Total = decimal.Parse(Console.ReadLine());

        // Citirea metodei de plată de la tastatură
        Console.Write("Introduceți metoda de plată (Card/PayPal/Crypto): ");
        string metodaPlata = Console.ReadLine();

        // Instantierea corespunzătoare a sistemului de plată
        PaymentSystem system = null;
        switch (metodaPlata.ToLower())
        {
            case "card":
                system = new CardPaymentSystem();
                break;
            case "paypal":
                system = new PayPalPaymentSystem();
                break;
            case "crypto":
                system = new CryptoWalletPaymentSystem();
                break;
            default:
                Console.WriteLine("Metoda de plată invalidă.");
                return;
        }

        // Procesarea comenzii folosind OrderProcessor
        OrderProcessor orderProcessor = new OrderProcessor();
        orderProcessor.FulfillOrder(order, system);
    }
}
