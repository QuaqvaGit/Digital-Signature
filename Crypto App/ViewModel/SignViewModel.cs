using System;
using System.Collections.Generic;
using System.Numerics;
using Crypto_App.Model;
using Crypto_App.Model.Encryptors;

namespace Crypto_App.ViewModel
{
    public class SignViewModel
    {
        ISigner<string, BigInteger> _signer;
        string _message;
        public SignViewModel(int typeOfEncryptor, string message)
        {
            switch(typeOfEncryptor)
            {
                case 0:
                    _signer = new RSA_Signer();
                    break;
                case 1:
                  //  _encryptor = new EGSA_Encryptor();
                    break;
                case 2:
                  //  _encryptor = new DSA_Encryptor();
                    break;
            }
            _message = message;
        }
        string FormKey(List<BigInteger> keys) => "(" + String.Join(", ", keys) + ")";
        public bool GetResults(out string openKey, out string privateKey, out string hash, out string sign)
        {
            try
            {
                openKey = FormKey(_signer.PublicKey);
                privateKey = FormKey(_signer.PrivateKey);
                hash = _message.GetHashCode().ToString();
                sign = _signer.Sign(_message).ToString();
                return true;
            }
            catch
            {
                openKey = privateKey = hash = sign = null;
                return false;
            }
        }
    }
}
