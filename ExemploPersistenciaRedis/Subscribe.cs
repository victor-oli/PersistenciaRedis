using RedisBoost;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploPersistenciaRedis
{
    public class Subscribe
    {
        public static void Execute()
        {
            using (var _pool = RedisClient.CreateClientsPool())
            {
                IRedisClient _redisClient;

                using (_redisClient = _pool.CreateClientAsync(ConfigurationManager
                    .ConnectionStrings["Redis"].ConnectionString).Result)
                {
                    var canal = Console.ReadLine();

                    using (var _psbuscriber = _redisClient.PSubscribeAsync(canal).Result)
                    {
                        Console.WriteLine("Ouvindo o canal {0}.", canal);

                        // obtendo a primeira mensagem
                        // Se não tiver nenhuma mensagem, fica esperando...
                        var mensagem = _psbuscriber
                            .ReadMessageAsync(ChannelMessageType.Message | ChannelMessageType.PMessage).Result;

                        while (mensagem.Value.As<string>() != null)
                        {
                            var msgDoCanal = mensagem.Value.As<string>();

                            Console.WriteLine("Mensagem: {0}", msgDoCanal);

                            // começa a escutar novamente...
                            // Se não tiver nenhuma mensagem, fica esperando
                            mensagem = _psbuscriber
                            .ReadMessageAsync(ChannelMessageType.Message | ChannelMessageType.PMessage).Result;
                        }
                    }
                }
            }
        }
    }
}