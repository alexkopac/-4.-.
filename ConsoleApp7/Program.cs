public class PrimeThreadSynchronizer
{
    private static readonly object _lock = new object();
    private static int _nextPrimeToPrint = 1;


    //1
    public static void PrintPrime(int primeId)
    {
       
        Console.WriteLine(primeId);
    }
    //2
    public static void PrintPrimeInOrder(int primeId)
    {
        lock (_lock)
        {
            while (primeId != _nextPrimeToPrint)
            {
                Monitor.Wait(_lock);
            }

            Console.WriteLine(primeId);
            _nextPrimeToPrint++;
            Monitor.PulseAll(_lock);
        }
    }

    public static void Main(string[] args)
    {
        //2
        Thread t1 = new Thread(() => PrintPrimeInOrder(3));
        Thread t2 = new Thread(() => PrintPrimeInOrder(1));
        Thread t3 = new Thread(() => PrintPrimeInOrder(2));


        //1
        //Thread t1 = new Thread(() => PrintPrime(3));
        //Thread t2 = new Thread(() => PrintPrime(1));
        //Thread t3 = new Thread(() => PrintPrime(2));


        t1.Start();
        t2.Start();
        t3.Start();
        
        t1.Join();
        t2.Join();
        t3.Join();

        Console.WriteLine("All threads have finished.");
    }
}