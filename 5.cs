public class TransactionEventArgs : EventArgs
{
    public Transaction Transaction { get; }
    public TransactionEventArgs(Transaction transaction)
    {
        Transaction = transaction;
    }
}

public class TransactionService
{
    public event EventHandler<TransactionEventArgs> TransactionAdded;
    public decimal TransactionAmount { get; set; }
    
    List<Transaction> transactions = new List<Transaction>();

    public void AddTransaction(Transaction transaction)
    {
        transactions.Add(transaction);
        OnTransactionAdded(new TransactionEventArgs(transaction));
        foreach (var t in transactions)
        {
            if (t.Amount > 100000)
            {
                Console.WriteLine("Alarm! High-value transaction detected.");
            }
        }
    }

    private void OnTransactionAdded(TransactionEventArgs transactionEventArgs)
    {
        TransactionAdded?.Invoke(this, transactionEventArgs);
    }

    public void TotalAmount()
    {
        decimal total = 0;
        foreach (var transaction in transactions)
        {
            total += transaction.Amount;
        }
        TransactionAmount = total;
    }

    public void FindTheBiggestTransaction()
    {
        decimal biggest = decimal.MinValue;
        foreach (var transaction in transactions)
        {
            if (transaction.Amount > biggest)
            {
                biggest = transaction.Amount;
            }
        }
        TransactionAmount = biggest;
    }
}

public class Transaction
{
    public decimal Amount { get; set; }
    public Transaction(decimal amount)
    {
        Amount = amount;
    }
}

class Program
{
    static void Main(string[] args)
    {
        TransactionService transactionService = new TransactionService();
        transactionService.AddTransaction(new Transaction(50000));
        transactionService.AddTransaction(new Transaction(150000));
        transactionService.AddTransaction(new Transaction(75000));
        transactionService.TotalAmount();
        Console.WriteLine($"Total Transaction Amount: {transactionService.TransactionAmount}");
        transactionService.FindTheBiggestTransaction();
        Console.WriteLine($"Biggest Transaction Amount: {transactionService.TransactionAmount}");
    }
}