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
                    _signer = new EGSA_Signer();
                    break;
                case 2:
                  //  _encryptor = new DSA_Encryptor();
                    break;
            }
            _message = message;
        }
        string FormKey(List<BigInteger> keys) => "(" + String.Join(", ", keys) + ")";
        public string GetResults()
        {
            var openKey = FormKey(_signer.PublicKey);
            var privateKey = FormKey(_signer.PrivateKey);
            var hash = _message.GetHashCode().ToString();
            var sign = "";

            if (_signer is RSA_Signer)
                sign = _signer.Sign(_message)[0].ToString();
            else if (_signer is EGSA_Signer)
                sign = FormKey(_signer.Sign(_message));

            string result = $"Готово!\nХеш документа: {hash}\nОткрытый ключ: {openKey}\nЗакрытый ключ: {privateKey}" +
                $"\nПодпись: {sign}";
            return result;
        }
    }
}
