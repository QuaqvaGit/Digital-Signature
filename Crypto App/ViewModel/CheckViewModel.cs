using Crypto_App.Model;
using Crypto_App.Model.Encryptors;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Crypto_App.ViewModel
{
    public class CheckViewModel
    {
        ISigner<string, BigInteger> _signer;
        BigInteger _hash;
        List<BigInteger> keys;

        List<BigInteger> ParseInput(string input)
        {
            return input
                .Split(new char[] { '(', ')', ',', ' ' })
                .Where(x => x.Length > 0)
                .Select(x => BigInteger.Parse(x))
                .ToList();
        }

        public CheckViewModel(int typeOfEncryptor, string message, string sign, string publicKey)
        {
            _hash = Math.Abs(message.GetHashCode());
            keys = ParseInput(publicKey);

            switch (typeOfEncryptor)
            {
                case 0:
                    {
                        _signer = new RSA_Signer(false);
                        BigInteger _sign = BigInteger.Parse(sign);
                        keys.Insert(0, _sign);
                        break;
                    }
                case 1:
                    {
                        _signer = new EGSA_Signer(false);
                        List<BigInteger> _sign = ParseInput(sign);
                        _sign.AddRange(keys);
                        keys = _sign;
                    break;
                    }
                case 2:
                    {
                        _signer = new DSA_Signer(message, false);
                        List<BigInteger> _sign = ParseInput(sign);
                        keys.AddRange(_sign);
                        break;
                    }
            }
            
        }

        public string GetResults(out bool succeed)
        {
            succeed = _signer.CheckSign(_hash, keys);
            return succeed?"Подпись корректна":"Подпись некорректна";
        }
    }
}
