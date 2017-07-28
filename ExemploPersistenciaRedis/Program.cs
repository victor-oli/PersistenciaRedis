using System;

namespace ExemploPersistenciaRedis
{
    class Program
    {
        static void Main(string[] args)
        {
            PersistindoNaMemoria.Execute();

            Console.ReadKey();
        }
    }
}
