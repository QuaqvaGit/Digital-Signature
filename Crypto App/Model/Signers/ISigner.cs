using System.Collections.Generic;
using System.Numerics;

namespace Crypto_App.Model
{
    interface ISigner<T, V>
    {
        List<V> PrivateKey { get; }
        List<V> PublicKey { get; }
        List<V> Sign(T message);
        bool CheckSign(V messageHash, List<V> keys);
    }
}
