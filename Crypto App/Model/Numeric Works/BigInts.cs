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
            List<BigInteger> factorization = new List<BigInteger>();
            BigInteger phi = p - 1, n = phi;
            for (int i = 2; i * i <= n; ++i)
                if (n % i == 0)
                {
                    factorization.Add(i);
                    while (n % i == 0)
                        n /= i;
                }
            if (n > 1)
                factorization.Add(n);

            for (int res = 2; res <= p; ++res)
            {
                bool ok = true;
                for (int i = 0; i < factorization.Count && ok; ++i)
                    ok &= BigInteger.ModPow(res, phi / factorization[i], p) != 1;
                if (ok) return res;
            }
            return -1;
        }

        public static int GetBits(BigInteger n) => (int)Math.Floor(BigInteger.Log(BigInteger.Abs(n), 2)); 

    }
}
