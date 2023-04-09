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
                    _signer = new RSA_Signer(true);
                    break;
                case 1:
                    _signer = new EGSA_Signer(true);
                    break;
                case 2:
                    _signer = new DSA_Signer(message, true);
                    break;
            }
            _message = message;
        }
        string FormKey(List<BigInteger> keys) => "(" + String.Join(", ", keys) + ")";
        public string GetResults()
        {
            var openKey = FormKey(_signer.PublicKey);
            var sign = "";

            if (_signer is RSA_Signer)
                sign = _signer.Sign(_message)[0].ToString();
            else
                sign = FormKey(_signer.Sign(_message));

            string result = $"Готово!\nОткрытый ключ: {openKey}\nПодпись: {sign}";
            return result;
        }
    }
}
