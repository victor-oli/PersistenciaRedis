using System;

namespace ExemploPersistenciaRedis
{
    class Program
    {
        static void Main(string[] args)
        {
            Subscribe.Execute();

            Console.ReadKey();
        }
    }
}
