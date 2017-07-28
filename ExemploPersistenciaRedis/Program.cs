using RedisBoost;
using System;
using System.Configuration;

namespace ExemploPersistenciaRedis
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var pool = RedisClient.CreateClientsPool())
            {
                IRedisClient redisClient;

                // chave
                var cadernoHoraAventura = "caderno:hora-de-aventura";
                var cadernoTheJoker = "caderno:the-joker";

                // criar client
                using (redisClient = pool.CreateClientAsync(ConfigurationManager
                    .ConnectionStrings["Redis"].ConnectionString).Result)
                {
                    // insere de forma assincrona no redis
                    redisClient.SetAsync(cadernoHoraAventura, "2000").Wait();

                    Console.WriteLine("Item {0} adicionando com sucesso no carrinho!", cadernoHoraAventura);

                    // get item
                    var redisHoraAventura = redisClient.GetAsync(cadernoHoraAventura).Result.As<string>();

                    Console.WriteLine("Valor {0} da Chave {1}.", redisHoraAventura, cadernoHoraAventura);

                    // deletar item
                    var deleteHoraAventura = redisClient.DelAsync(cadernoHoraAventura).Result;

                    Console.WriteLine("Chave {0} {1} deletada.", cadernoHoraAventura, Convert.ToBoolean(deleteHoraAventura) ? "foi" : "não foi");

                    // adicionando The Joker
                    redisClient.SetAsync(cadernoTheJoker, "2200").Wait();

                    Console.WriteLine("Item {0} adicionando com sucesso no carrinho!", cadernoTheJoker);

                    // adicionando tempo de exiração
                    redisClient.ExpireAsync(cadernoTheJoker, 60);

                    Console.WriteLine("Chave {0} com TTL de 60 segundos.", cadernoTheJoker);
                }
            }

            Console.ReadKey();
        }
    }
}
