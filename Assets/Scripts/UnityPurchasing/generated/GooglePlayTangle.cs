// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("F+6tlUvvGcnx+5IFgIRGlRfLYgjUV1lWZtRXXFTUV1dW/jmrRCQVkwpvU9mA9gAdMslzD5RzipxZYWEARztOyRC98M1M73Dv/hqIPFzR0Pv71FtkupjnOCC1QJE+fVZomF5CVGbUV3RmW1BffNAe0KFbV1dXU1ZVyKSQ1ANB4tteOZXpN4Vka9ccwUZr9S6kG/0ARjyO9/MGIVODqrMHfo/LDp0SSBDauRumoM+2oTPMD8pcunxqafosbapey7HYWjKfhEHUgyvzdyUn41iqhzrQs9QyOUk0gNAnM44RSowTXx1bZpJ2kIHyYsBJWDWdpU3G4QbDp3f8/RIpx9mzVDu6a8znULZWasHcSlxYmMn27lQEnOokDz0Ra/ixikq6YVRVV1ZX");
        private static int[] order = new int[] { 13,1,11,12,9,13,6,12,13,13,12,13,12,13,14 };
        private static int key = 86;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
