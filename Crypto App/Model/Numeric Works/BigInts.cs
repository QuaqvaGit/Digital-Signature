using System;
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
        public static BigInteger ModInverse(BigInteger a, BigInteger m)
        {
            BigInteger m0 = m;
            BigInteger y = 0, x = 1;

            if (m == 1)
                return 0;

            while (a > 1)
            {
                // q - частное, t - остаток
                BigInteger q = a / m;
                BigInteger t = m;

                // m - остаток, a - делитель
                m = a % m;
                a = t;
                t = y;

                // Вычисляем коэффициенты x и y с помощью расширенного алгоритма Евклида
                y = x - q * y;
                x = t;
            }

            if (x < 0)
                x += m0;

            return x;

        }

        /// <summary>
        /// Метод генерации случайного BigInteger
        /// </summary>
        /// <param name="bits">Количество бит в числе</param>
        /// <returns>Случайное число</returns>
        public static BigInteger RandBigInt(int bits)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[bits / 8 + 1];
            rng.GetBytes(bytes);
            bytes[bytes.Length - 1] = 0;
            BigInteger result = new BigInteger(bytes);
            return result;
        }

        /// <summary>
        /// Метод генерации первообразного корня числа
        /// </summary>
        /// <param name="p"></param>
        /// <returns>Первообразный корень числа</returns>
        public static BigInteger GeneratePrimitiveRoot(BigInteger p)
        {
            BigInteger g = 0;
            while (g == 1 || g == p - 1 || BigInteger.ModPow(g, (p-1)/2, p) == 1)
                g = RandBigInt((int)Math.Floor(BigInteger.Log(p, 2)));
            return g;
        }
    }
}
