using Crypto_App.Model;
using Crypto_App.Model.Encryptors;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;

namespace Crypto_App.ViewModel
{
    public class CheckViewModel
    {
        ISigner<string, BigInteger> _signer;
        BigInteger _hash;
        List<BigInteger> keys;
        public CheckViewModel(int typeOfEncryptor, string message, string sign, string publicKey)
        {
            switch (typeOfEncryptor)
            {
                case 0:
                    _signer = new RSA_Signer();
                    break;
                case 1:
                    _signer = new EGSA_Signer();
                    break;
                case 2:
                    //  _encryptor = new DSA_Encryptor();
                    break;
            }
            _hash = message.GetHashCode();
            var _sign = BigInteger.Parse(sign);
            keys = publicKey
                .Split(new char[] { '(', ')', ',', ' ' })
                .Where(x => x.Length > 0)
                .Select(x => BigInteger.Parse(x))
                .ToList();
            keys.Insert(0, _sign);
        }

        public bool GetResults()
        {
            return _signer.CheckSign(_hash, keys);
        }
    }
}
