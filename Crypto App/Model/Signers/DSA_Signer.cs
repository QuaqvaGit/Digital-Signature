using Crypto_App.Model.Numeric_Works;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Crypto_App.Model.Encryptors
{
    public class DSA_Signer: ISigner<string, BigInteger>
    {
        public List<BigInteger> PublicKey { get; }
        public List<BigInteger> PrivateKey { get; }
        public DSA_Signer(string message, bool generateKeys) { 
            PublicKey = new List<BigInteger>();
            PrivateKey = new List<BigInteger>();

            if (generateKeys)
            {
                BigInteger hash = message.GetHashCode();
                int bits = BigInts.GetBits(hash);
                BigInteger q = Primes.GenerateRandomPrime(bits),
                           p = 2 * q + 1;
                int n = 3;
                while (!Primes.IsMillerRabinPassed(p))
                {
                    p = n * q + 1;
                    n++;
                }
                BigInteger h = Primes.GenerateRandomPrime(Primes.random.Next(2, BigInts.GetBits(p - 1))),
                           g = BigInteger.ModPow(h, (p - 1) / q, p),
                           x = Primes.GenerateRandomPrime(Primes.random.Next(1, BigInts.GetBits(q))),
                           y = BigInteger.ModPow(g, x, p);
                PrivateKey.Add(x);
                PublicKey.AddRange(new BigInteger[] { p, q, g, y });
            }   
        }

        public List<BigInteger> Sign(string message)
        {
            BigInteger r = 0, k = 0, s = 0;
            while (r == 0 || s == 0)
            {
                k = Primes.GenerateRandomPrime(Primes.random.Next(1, BigInts.GetBits(PublicKey[1])));
                r = BigInteger.ModPow(PublicKey[2], k, PublicKey[0]) % PublicKey[1];
                s = BigInts.ModInverse(k, PublicKey[1]) * (message.GetHashCode() + PrivateKey[0] * r);
            }
            List<BigInteger> result = new List<BigInteger>();
            result.Add(r);
            result.Add(s);
            return result;
        }

        /// <summary>
        /// Метод проверки подписи DSA
        /// </summary>
        /// <param name="hash">Хэш сообщения</param>
        /// <param name="keys">Список вида {p, q, g, y, r, s}</param>
        /// <returns></returns>
        public bool CheckSign(BigInteger hash, List<BigInteger> keys)
        {
            BigInteger w = BigInts.ModInverse(keys[5], keys[1]),
                u1 = (hash*w) % keys[1],
                u2 = (keys[4] * w) % keys[1],
                v = BigInteger.ModPow(keys[2], u1, keys[0]) * BigInteger.ModPow(keys[3], u2, keys[0]) % keys[0] % keys[1];
            return v == keys[4];
        }
    }
}
