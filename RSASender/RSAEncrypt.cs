using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RSASender
{
    class RSAEncrypt
    {
        public RSAParameters _publicKey;
        public RSAParameters _privateKey;

        /// <summary>
        /// creates a private and a public key  
        /// </summary>
        public void AssignNewKey()
        {
            using var rsa = new RSACryptoServiceProvider(2048);
            rsa.PersistKeyInCsp = false;
            _publicKey = rsa.ExportParameters(false);
            _privateKey = rsa.ExportParameters(true);
        }

        /// <summary>
        /// Encrypts the data from the Message text
        /// </summary>
        /// <param name="dataToEncrypt"></param>
        /// <returns></returns>
        public byte[] EncryptData(byte[] dataToEncrypt)
        {
            byte[] cipherbytes;

            using var rsa = new RSACryptoServiceProvider(2048);
            rsa.PersistKeyInCsp = false;
            rsa.ImportParameters(_publicKey);

            cipherbytes = rsa.Encrypt(dataToEncrypt, true);

            return cipherbytes;
        }

       
    }
}

