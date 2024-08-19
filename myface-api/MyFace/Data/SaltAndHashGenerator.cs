using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace MyFace.Data
{
    public static class SaltAndHashGenerator
    {
        private static string _salt { get; set; }
        private static string _hash { get; set; }   
        public static List<string> getSaltAndHash(string password)
        {
            List<string> saltHash = new List<string>();
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new  RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            _salt = Convert.ToBase64String(salt);

            _hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

            saltHash.Add(_salt);
            saltHash.Add(_hash);

            return saltHash;
        }
    }
    // {

    //     private static string _salt { get; set; }
    //     private static string _hash { get; set; }


    //     public static string getSalt(){
    //         // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
    //         byte[] salt = new byte[128 / 8];
    //         using (var rngCsp = new  RNGCryptoServiceProvider())
    //         {
    //             rngCsp.GetNonZeroBytes(salt);
    //         }
    //         _salt = Convert.ToBase64String(salt);
    //         return _salt;
    //     }

    //     public static string getHash(string password, string salt)
    //     {
    //         _hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
    //             password: password,
    //             salt:  Convert.FromBase64String(salt),
    //             prf: KeyDerivationPrf.HMACSHA256,
    //             iterationCount: 100000,
    //             numBytesRequested: 256 / 8));
    //         return _hash;
    //     }
    // }
}