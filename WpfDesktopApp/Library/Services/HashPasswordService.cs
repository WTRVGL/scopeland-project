﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Core.Services
{
    public class HashPasswordService
    {

        public string HashBase64 { get; }
        public string SaltBase64 { get; }

        public HashPasswordService(string password)
        {
            var rng = new RNGCryptoServiceProvider();
            var saltBytes = new byte[14];
            rng.GetBytes(saltBytes);

            var rfc = new Rfc2898DeriveBytes(password, saltBytes);
            var hashBytes = rfc.GetBytes(20);

            HashBase64 = Convert.ToBase64String(hashBytes);
            SaltBase64 = Convert.ToBase64String(saltBytes);
        }

        public HashPasswordService()
        {

        }

        /// <summary>
        /// Hashes an inputted password using a supplied Salt and compares it to the supplied Hash
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hashBase64"></param>
        /// <param name="saltBase64"></param>
        /// <returns></returns>
        public bool checkHash(string password, string hashBase64, string saltBase64)
        {
            if (password == null)
            {
                return false;
            }

            var rfc = new Rfc2898DeriveBytes(password, Convert.FromBase64String(saltBase64));
            var checkBytes = rfc.GetBytes(20);
            var checkBytesBase64 = Convert.ToBase64String(checkBytes);

            if (hashBase64 == checkBytesBase64)
            {
                return true;
            }

            return false;
        }

        

    }
}
