using System.Collections.Generic;
using System.Numerics;
using Crypto_App.Model.Numeric_Works;

namespace Crypto_App.Model.Encryptors
{
    public class RSA_Signer : ISigner<string, BigInteger>
    {
        static int MAX_BITS = 512;
        public List<BigInteger> PrivateKey { get; }
        public List<BigInteger> PublicKey { get; }

        /// <summary>
        /// Конструктор шифровальщика, в котором происходит генерация ключей
        /// </summary>
        public RSA_Signer()
        {
            int bits = Primes.random.Next(MAX_BITS);
            BigInteger p = Primes.GenerateRandomPrime(bits),
                q = Primes.GenerateRandomPrime(bits),
                n = p * q,
                phi_n = (p-1)*(q-1),
                e = Primes.GetCoprime(3, phi_n-1, phi_n),
                d = BigInts.ModInverse(e, phi_n);
            PublicKey = new List<BigInteger>();
            PublicKey.Add(e);
            PublicKey.Add(n);
            PrivateKey = new List<BigInteger>();
            PrivateKey.Add(d);
        }

        /// <summary>
        /// Метод шифрования и подписывания сообщения
        /// </summary>
        /// <param name="message">Шифруемое и подписываемое сообщение</param>
        /// <returns>Подпись</returns>
        public BigInteger Sign(string message)
        {
            BigInteger hash = message.GetHashCode();
            BigInteger sign = BigInteger.ModPow(hash, PrivateKey[0], PublicKey[1]);
            return sign;
        }

        /// <summary>
        /// Метод проверки корректности подписи
        /// </summary>
        /// <param name="hash">Хешкод сообщения</param>
        /// <param name="keys">Список ключей вида {sign, e, n}</param>
        /// <returns></returns>
        public bool CheckSign(BigInteger hash, List<BigInteger> keys)
        {
            BigInteger h = BigInteger.ModPow(keys[0], keys[1], keys[2]);
            return h == hash;
        }
    }
}
