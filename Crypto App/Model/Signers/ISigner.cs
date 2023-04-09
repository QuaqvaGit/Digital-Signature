using System.Collections.Generic;
using System.Numerics;

namespace Crypto_App.Model
{
    interface ISigner<T, V>
    {
        List<BigInteger> PrivateKey { get; }
        List<BigInteger> PublicKey { get; }
        V Sign(T message);
        bool CheckSign(V messageHash, List<V> keys);
    }
}
