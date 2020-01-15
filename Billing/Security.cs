using System.Text;
using Java.Security;
using Android.Util;
using Java.Security.Spec;
using Java.Lang;

namespace InAppBilling.Billing
{
    public sealed class Security
    {
        /// <summary>
		/// Verifies the purchase.
		/// </summary>
		/// <returns><c>true</c>, if purchase was verified, <c>false</c> otherwise.</returns>
		/// <param name="publicKey">Public key.</param>
		/// <param name="signedData">Signed data.</param>
		/// <param name="signature">Signature.</param>
		public static bool VerifyPurchase(string publicKey, string signedData, string signature)
        {
            if (signedData == null)
            {                
                return false;
            }

            if (!string.IsNullOrEmpty(signature))
            {
                var key = Security.GeneratePublicKey(publicKey);
                bool verified = Security.Verify(key, signedData, signature);

                if (!verified)
                {                    
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Generates the public key.
        /// </summary>
        /// <returns>The public key.</returns>
        /// <param name="encodedPublicKey">Encoded public key.</param>
        public static IPublicKey GeneratePublicKey(string encodedPublicKey)
        {
            try
            {
                var keyFactory = KeyFactory.GetInstance(KeyFactoryAlgorithm);
                return keyFactory.GeneratePublic(new X509EncodedKeySpec(Base64.Decode(encodedPublicKey, 0)));
            }
            catch (NoSuchAlgorithmException e)
            {                
                throw new RuntimeException(e);
            }
            catch (Exception e)
            {                
                throw new IllegalArgumentException(e.Message);
            }
        }

        /// <summary>
        /// Verify the specified publicKey, signedData and signature.
        /// </summary>
        /// <param name="publicKey">Public key.</param>
        /// <param name="signedData">Signed data.</param>
        /// <param name="signature">Signature.</param>
        public static bool Verify(IPublicKey publicKey, string signedData, string signature)
        {            
            try
            {
                var sign = Signature.GetInstance(SignatureAlgorithm);
                sign.InitVerify(publicKey);
                sign.Update(Encoding.UTF8.GetBytes(signedData));

                if (!sign.Verify(Base64.Decode(signature, 0)))
                {                    
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                var msg = e.Message;
            }

            return false;
        }

        const string KeyFactoryAlgorithm = "RSA";
        const string SignatureAlgorithm = "SHA1withRSA";

    }
}
