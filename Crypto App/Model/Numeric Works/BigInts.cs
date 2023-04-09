using System.Numerics;
using System.Security.Cryptography;

namespace Crypto_App.Model.Numeric_Works
{
    public static class BigInts
    {
        /// <summary>
        /// Вспомогательная функция для вычисления обратного элемента в кольце по модулю.
        /// </summary>
        /// <param name="a">Число</param>
        /// <param name="m">Модуль</param>
        /// <returns>Число, обратное по модулю</returns>
        public static BigInteger ModInverse(BigInteger a, BigInteger m) => BigInteger.ModPow(a, m - 2, m);
        public static BigInteger RandBigInt(int bits)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[bits / 8 + 1];
            rng.GetBytes(bytes);
            bytes[bytes.Length - 1] = 0;
            BigInteger result = new BigInteger(bytes);
            return result;
        }
    }
}
