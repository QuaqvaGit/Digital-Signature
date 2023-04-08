using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_App.Model
{
    internal interface IEncryptor<T>
    {
        string PrivateKey { get; set; }
        string PublicKey { get; set; }
        T Encrypt(T message);
        T Decrypt(T message);
    }
}
