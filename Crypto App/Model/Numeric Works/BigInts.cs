using System;
using System.Collections.Generic;
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
            byte[] bytes = new byte[bits/2];
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
            var phi = p - 1;
            var factors = Factorize(phi);
            for (BigInteger g = 2; g < p; g++)
            {
                bool isPrimitiveRoot = true;
                foreach (var factor in factors)
                {
                    if (BigInteger.ModPow(g, phi / factor, p) == 1)
                    {
                        isPrimitiveRoot = false;
                        break;
                    }
                }
                if (isPrimitiveRoot)
                {
                    return g;
                }
            }
            return -1; // первообразный корень не найден
        }
        private static List<BigInteger> Factorize(BigInteger n)
        {
            var factors = new List<BigInteger>();
            for (BigInteger i = 2; i * i <= n; i++)
            {
                while (n % i == 0)
                {
                    factors.Add(i);
                    n /= i;
                }
            }
            if (n > 1)
            {
                factors.Add(n);
            }
            return factors;
        }

        public static int GetBits(BigInteger n) => (int)Math.Floor(BigInteger.Log(BigInteger.Abs(n), 2)); 

    }
}
