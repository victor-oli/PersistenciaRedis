using RedisBoost;
using System;
using System.Configuration;

namespace ExemploPersistenciaRedis
{
    public class Publish
    {
        public static void Execute()
        {
            using (var _pool = RedisClient.CreateClientsPool())
            {
                IRedisClient _redisClient;

                using (_redisClient = _pool.CreateClientAsync(ConfigurationManager
                    .ConnectionStrings["Redis"].ConnectionString).Result)
                {
                    string[] canais = new string[] { "casa", "musica" };
                    string[] mensagens = new string[] { "nova casa por 300,00", "Matanza" };

                    for (int i = 0; i < canais.Length; i++)
                    {
                        _redisClient.PublishAsync(canais[i], mensagens[i]);
                        Console.WriteLine("Publicando no canal {0} a mensagem: {1}", canais[i], mensagens[i]);
                }

                    Console.WriteLine("Mensagens enviadas! :)");
                }
            }
        }
    }
}