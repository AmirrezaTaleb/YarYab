using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YarYab.Common.Helper
{
    public static class SecurityHelper
    {
        /// <summary>
        /// Summary description for Cryptography
        /// </summary>
        public sealed class Cryptography
        {
            public enum EncryptionAlgorithmType
            {
                DES,
                RC2,
                Rijndael,
                TripleDES
            };

            public enum CryptoDirection
            {
                Encrypt,
                Decrypt
            };

            public Cryptography()
            {
                //
                //TODO: Add constructor logic here
                //
            }

            public static SymmetricAlgorithm GetAlgorithm(EncryptionAlgorithmType type)
            {
                // Determine the type and return the approrpiate Symmetric Algorithm Class
                switch (type)
                {
                    case EncryptionAlgorithmType.DES:
                        return new DESCryptoServiceProvider();

                    case EncryptionAlgorithmType.RC2:
                        return new RC2CryptoServiceProvider();

                    case EncryptionAlgorithmType.Rijndael:
                        return new RijndaelManaged();

                    case EncryptionAlgorithmType.TripleDES:
                        return new TripleDESCryptoServiceProvider();

                    default:
                        throw new ArgumentException("Invalid Algorithm Type");
                }
            }

            public static byte[] GenerateKey(EncryptionAlgorithmType type)
            {
                SymmetricAlgorithm algorithm = GetAlgorithm(type);
                algorithm.GenerateKey();
                return algorithm.Key;
            }

            public static byte[] GenerateIV(EncryptionAlgorithmType type)
            {
                SymmetricAlgorithm algorithm = GetAlgorithm(type);
                algorithm.GenerateIV();
                return algorithm.IV;
            }

            public static ICryptoTransform GetCryptoTransformer(EncryptionAlgorithmType type,
                CryptoDirection direction, byte[] key, byte[] IV)
            {
                SymmetricAlgorithm algorithm = GetAlgorithm(type);
                algorithm.Mode = CipherMode.CBC;

                // Give key to algorithm, or get auto-generated key from algorithm
                if (key == null)
                    key = algorithm.Key;   // get generated key
                else
                    algorithm.Key = key;   // set key

                if (IV == null)
                    IV = algorithm.IV;     // get generated Initialization vector
                else
                    algorithm.IV = IV;     // set key

                // Return the appropriate ICryptoTransformer for the Direction
                switch (direction)
                {
                    case CryptoDirection.Encrypt:
                        return algorithm.CreateEncryptor();

                    case CryptoDirection.Decrypt:
                        return algorithm.CreateDecryptor();

                    default:
                        throw new ArgumentException("Invalid Crypto Direction");
                }
            }

            public static string EncryptString(string valueToEncrypt,
                EncryptionAlgorithmType type, byte[] key, byte[] IV)
            {
                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] value = encoder.GetBytes(valueToEncrypt);
                byte[] encrypted = EncryptByteArray(value, type, key, IV);
                return Convert.ToBase64String(encrypted);
            }

            public static byte[] EncryptByteArray(byte[] byteArrayToEncrypt,
                EncryptionAlgorithmType type, byte[] key, byte[] IV)
            {
                ICryptoTransform transformer = GetCryptoTransformer(type,
                    CryptoDirection.Encrypt, key, IV);
                MemoryStream buffer = new MemoryStream();
                CryptoStream encStream = new CryptoStream(buffer, transformer, CryptoStreamMode.Write);

                // Write data to encryption stream which stores it in the buffer
                try
                {
                    encStream.Write(byteArrayToEncrypt, 0, byteArrayToEncrypt.Length);
                    encStream.FlushFinalBlock();

                }
                catch (Exception ex)
                {
                    throw new IOException("Could not Encrypt data", ex);

                }
                finally
                {
                    encStream.Close();
                }

                return buffer.ToArray();
            }

            public static string DecryptString(string valueToDecrypt, EncryptionAlgorithmType type, byte[] key, byte[] IV)
            {
                ASCIIEncoding decoder = new ASCIIEncoding();
                byte[] value = Convert.FromBase64String(valueToDecrypt);
                byte[] decrypted = DecryptByteArray(value, type, key, IV);
                return decoder.GetString(decrypted);
            }

            public static byte[] DecryptByteArray(byte[] byteArrayToDecrypt,
                EncryptionAlgorithmType type, byte[] key, byte[] IV)
            {
                ICryptoTransform transformer = GetCryptoTransformer(type,
                    CryptoDirection.Decrypt, key, IV);
                MemoryStream buffer = new MemoryStream();
                CryptoStream decStream = new CryptoStream(buffer, transformer, CryptoStreamMode.Write);

                // Write data to decryption stream which stores it in the buffer
                try
                {
                    decStream.Write(byteArrayToDecrypt, 0, byteArrayToDecrypt.Length);
                    decStream.FlushFinalBlock();

                }
                catch (Exception ex)
                {
                    throw new IOException("Could not Decrypt data", ex);

                }
                finally
                {
                    decStream.Close();
                }

                return buffer.ToArray();
            }
        }

        /// <summary>
        /// Summary description for Hashing
        /// </summary>
        public sealed class Hashing
        {
            // algorithm enumeration
            public enum HashAlgorithmType
            {
                MD5,
                SHA1,
                SHA256,
                SHA384,
                SHA512
            };

            public Hashing()
            {
                // TODO : Add constructor logic here
            }

            public static string CreateHash(string valueToHash, HashAlgorithmType type)
            {
                // Setup variables
                HashAlgorithm algorithm;
                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] valueByteArray = encoder.GetBytes(valueToHash);
                string Hashedvalue = string.Empty;
                byte[] HashvalueByteArray;

                // Acquire algorithm object
                switch (type)
                {
                    case HashAlgorithmType.MD5:
                        algorithm = new MD5CryptoServiceProvider();
                        break;
                    case HashAlgorithmType.SHA1:
                        algorithm = new SHA1Managed();
                        break;
                    case HashAlgorithmType.SHA256:
                        algorithm = new SHA256Managed();
                        break;
                    case HashAlgorithmType.SHA384:
                        algorithm = new SHA384Managed();
                        break;
                    case HashAlgorithmType.SHA512:
                        algorithm = new SHA512Managed();
                        break;
                    default:
                        throw new ArgumentException("Invalid Hasing Algorithm Type");

                }

                // create binary hash
                HashvalueByteArray = algorithm.ComputeHash(valueByteArray);

                // convert binary hash to hex
                foreach (byte b in HashvalueByteArray)
                {
                    Hashedvalue += String.Format("{0:x2}", b);
                }

                // return...
                return Hashedvalue;
            }

        }


        // algorithm enumeration
        public enum EncryptionKey
        {
            Infra3MANAGE2021,
            IMS0912Infrastructure1400ForCookie,
        };

        //public SecureZone()
        //{
        //    // TODO : Add constructor logic here
        //}

        public static string Encrypt(string clearText, EncryptionKey encryptionKey)
        {
            string EncryptionKey = encryptionKey.ToString();
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Encrypt(string clearText, string encryptionKey)
        {
            string EncryptionKey = encryptionKey.ToString();
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }


        public static string Decrypt(string cipherText, EncryptionKey encryptionKey)
        {
            string EncryptionKey = encryptionKey.ToString();
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        public static string Decrypt(string cipherText, string encryptionKey)
        {
            string EncryptionKey = encryptionKey.ToString();
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        /// <summary>
        ///    
        /// </summary>
        /// <param name="PasswordLength"></param>
        /// <returns>Create Random Code with custom length and custom type ex)int or string</returns>
        public static string CreateRandomCode(int PasswordLength, Type type)
        {
            string _allowedChars;
            if (type == typeof(int))
                _allowedChars = "0123456789";
            else
                _allowedChars = "0123456789abcdefgHJKLMNPQRSTUVWXYZ";


            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pass"></param>
        /// <returns>Encode Password With Md5</returns>
        public static string EncodePasswordMd5(string pass) //Encrypt using MD5   
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)   
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(pass);
            encodedBytes = md5.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string   
            return BitConverter.ToString(encodedBytes);
        }

        public static string GetSha256Hash(string input)
        {
            //using (var sha256 = new SHA256CryptoServiceProvider())
            using (var sha256 = SHA256.Create())
            {
                var byteValue = Encoding.UTF8.GetBytes(input);
                var byteHash = sha256.ComputeHash(byteValue);
                return Convert.ToBase64String(byteHash);
                //return BitConverter.ToString(byteHash).Replace("-", "").ToLower();
            }
        }
    }
}
