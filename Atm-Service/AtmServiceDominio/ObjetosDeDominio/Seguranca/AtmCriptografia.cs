using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AtmServiceDominio.ObjetosDeDominio.Seguranca
{
    public class AtmCriptografia
    {
        private static byte[] bIV = { 0x50, 0x08, 0xF1, 0xDD, 0xDE, 0x3C, 0xF2, 0x18,
                                      0x44, 0x74, 0x19, 0x2C, 0x53, 0x49, 0xAB, 0xBC };

        public string ValueToCrypt { get; set; }
        public string ValueCrypted { get { return this.GetCryptedHash(); } }

        private string GetCryptKey()
        {
            return "khshgdHHghhhspoKcOnghhgHytgg690a35gEfgfjKll=";
        }

        private string GetCryptedHash()
        {
            if (!string.IsNullOrEmpty(this.ValueToCrypt))
            {
                // Cria instancias de vetores de bytes com as chaves                
                byte[] bKey = Convert.FromBase64String(GetCryptKey());
                byte[] bText = new UTF8Encoding().GetBytes(this.ValueToCrypt);

                // Instancia a classe de criptografia Rijndael
                Rijndael rijndael = new RijndaelManaged();

                // Define o tamanho da chave "256 = 8 * 32"                
                // Lembre-se: chaves possíves:                
                // 128 (16 caracteres), 192 (24 caracteres) e 256 (32 caracteres)                
                rijndael.KeySize = 256;

                MemoryStream mStream = new MemoryStream();

                CryptoStream encryptor = new CryptoStream(
                    mStream,
                    rijndael.CreateEncryptor(bKey, bIV),
                    CryptoStreamMode.Write);

                encryptor.Write(bText, 0, bText.Length);

                encryptor.FlushFinalBlock();

                return Convert.ToBase64String(mStream.ToArray());
            }
            else
            {
                return null;
            }
        }

        public string Decrypt(string CryptedText)
        {
            if (!string.IsNullOrEmpty(CryptedText))
            {
                byte[] bKey = Convert.FromBase64String(this.GetCryptKey());
                byte[] bText = Convert.FromBase64String(CryptedText);

                Rijndael rijndael = new RijndaelManaged();

                // Define o tamanho da chave "256 = 8 * 32"                
                // Lembre-se: chaves possíves:                
                // 128 (16 caracteres), 192 (24 caracteres) e 256 (32 caracteres)                
                rijndael.KeySize = 256;

                MemoryStream mStream = new MemoryStream();

                CryptoStream decryptor = new CryptoStream(
                    mStream,
                    rijndael.CreateDecryptor(bKey, bIV),
                    CryptoStreamMode.Write);

                decryptor.Write(bText, 0, bText.Length);
                decryptor.FlushFinalBlock();

                UTF8Encoding utf8 = new UTF8Encoding();
                return utf8.GetString(mStream.ToArray());
            }
            else
            {
                return null;
            }
        }
    }
}
