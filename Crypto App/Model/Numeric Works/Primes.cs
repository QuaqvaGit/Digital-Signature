using System;
using System.Numerics;

namespace Crypto_App.Model.Numeric_Works
{
    public static class Primes
    {
        public static Random random = new Random();
        /// <summary>
        /// Метод генерации случайного простого числа длиной N бит
        /// </summary>
        /// <param name="n">Число бит</param>
        /// <returns>Случайное простое число</returns>
        public static BigInteger GenerateRandomPrime (int n) {
            BigInteger candidate = BigInts.RandBigInt(n);
            while (!IsMillerRabinPassed(candidate))
                candidate = BigInts.RandBigInt(n);
            return candidate;
        }

        public static BigInteger GetCoprime(BigInteger lowerBound, BigInteger upperBound, BigInteger a)
        {
            for (BigInteger i = lowerBound; i <= upperBound; i++)
                if (BigInteger.GreatestCommonDivisor(i, a) == 1)
                    return i;
            throw new Exception("Нет взаимно простого (???)");
        }

        static bool TrialComposite(BigInteger round_tester, 
            BigInteger evenComponent, BigInteger miller_rabin_candidate, BigInteger maxDivisionsByTwo)
        {
            if (BigInteger.ModPow(round_tester, evenComponent, miller_rabin_candidate) == 1)
                return false;
            for (int i = 0; i < maxDivisionsByTwo; i++)
                if (BigInteger.ModPow(round_tester, (1 << i) * evenComponent,
                           miller_rabin_candidate) == miller_rabin_candidate - 1)
                    return false;
            return true;
        }

        static bool IsMillerRabinPassed(BigInteger miller_rabin_candidate)
        {
            BigInteger maxDivisionsByTwo = 0;
            BigInteger evenComponent = miller_rabin_candidate - 1;

            while (evenComponent % 2 == 0)
            {
                evenComponent >>= 1;
                maxDivisionsByTwo += 1;
            }

            for (int i = 0; i < BigInteger.Log(miller_rabin_candidate, 2); i++)
            {
                BigInteger round_tester = BigInts.RandBigInt(
                    (int)Math.Floor(BigInteger.Log(miller_rabin_candidate)));

                if (TrialComposite(round_tester, evenComponent,
                                   miller_rabin_candidate, maxDivisionsByTwo))
                    return false;
            }
            return true;
        }
    }
}
