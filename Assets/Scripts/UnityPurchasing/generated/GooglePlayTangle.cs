// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("SMyenFjjETyBawhviYLyjztrnIgBx9HSQZfWEeVwCmPhiSQ/+m84kN1v7M/d4Ovkx2ulaxrg7Ozs6O3ucx8rb7j6WWDlgi5SjD7f0Gynev38gPVyqwZLdvdUy1RFoTOH52prQDRwtSap86thAqAdG3QNGoh3tHHnQG/g3wEjXIObDvsqhcbt0yPl+e+x1OhiO027polyyLQvyDEn4trau2/s4u3db+zn72/s7O1FghD/n64oHvZ9Wr14HMxHRqmSfGII74AB0Hc1qvE3qOSm4N0pzSs6Sdl78uOOJtBOlR+gRrv9hzVMSL2a6DgRCLzFrFUWLvBUonJKQCm+Oz/9Lqxw2bNc6w3t0Xpn8efjI3JNVe+/J1GftIaq0EMKMfEB2u/u7O3s");
        private static int[] order = new int[] { 7,8,7,6,12,5,9,11,8,10,12,12,13,13,14 };
        private static int key = 237;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
