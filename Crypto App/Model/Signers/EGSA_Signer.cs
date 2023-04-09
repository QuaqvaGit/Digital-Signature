using System.Numerics;
using System.Collections.Generic;
using Crypto_App.Model.Numeric_Works;
using System;

namespace Crypto_App.Model.Encryptors
{
    public class EGSA_Signer: ISigner<string, BigInteger>
    {
        static int MAX_BITS = 20;
        public List<BigInteger> PrivateKey { get; }
        public List<BigInteger> PublicKey { get; }

        public EGSA_Signer(bool generateKeys)
        {
            PublicKey = new List<BigInteger>();
            PrivateKey = new List<BigInteger>();
            //Генерация ключей
            if (generateKeys)
            {
                BigInteger p = Primes.GenerateRandomPrime(MAX_BITS),
                g = BigInts.GeneratePrimitiveRoot(p),
                x = BigInts.RandBigInt(Primes.random.Next(2, BigInts.GetBits(p))),
                y = BigInteger.ModPow(g, x, p);
                
                PublicKey.Add(y);
                PublicKey.Add(g);
                PublicKey.Add(p);
                PrivateKey.Add(x);
            }
            
        }

        /// <summary>
        /// Метод подписи сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <returns>Подпись вида {r, s}</returns>
        public List<BigInteger> Sign(string message)
        {
            BigInteger hash = Math.Abs(message.GetHashCode()),
                k = Primes.GetCoprime(2, PublicKey[2] - 2, PublicKey[2] - 1),
                r = BigInteger.ModPow(PublicKey[1], k, PublicKey[2]),
                s = (hash - PrivateKey[0] * r) * BigInts.ModInverse(k, PublicKey[2] - 1) % (PublicKey[2] - 1);
            if (s < 0)
                s += PublicKey[2] - 1;
            List<BigInteger> result = new List<BigInteger>();
            result.Add(r); 
            result.Add(s);
            return result;
        }

        /// <summary>
        /// Метод проверки корректности подписи
        /// </summary>
        /// <param name="hash">Хеш сообщения</param>
        /// <param name="keys">Список вида {подпись r, s, ключ y,g,p}</param>
        /// <returns></returns>
        public bool CheckSign(BigInteger hash, List<BigInteger> keys)
        {
            return keys[0] > 0 && keys[0] < keys[4] && keys[1] > 0 && keys[1] < keys[4] - 1
                && BigInteger.ModPow(keys[2], keys[0], keys[4]) * 
                BigInteger.ModPow(keys[0], keys[1], keys[4]) % keys[4] ==
                BigInteger.ModPow(keys[3], BigInteger.Abs(hash), keys[4]);
        }
    }
}
