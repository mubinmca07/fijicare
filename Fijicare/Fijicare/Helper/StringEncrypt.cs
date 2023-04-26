using System;
using System.Text;

namespace Fijicare.Helper
{
    public class StringEncrypt
    {
        public static string Encrypt(string plainText, string passPhrase)
        {
            int encryptionKey = 0x53;
            StringBuilder szInputStringBuild = new StringBuilder(plainText);
            StringBuilder szOutStringBuild = new StringBuilder(plainText.Length);
            char Textch;
            for (int iCount = 0; iCount < plainText.Length; iCount++)
            {
                Textch = szInputStringBuild[iCount];
                Textch = (char)(Textch ^ encryptionKey);
                szOutStringBuild.Append(Textch);
            }
            return szOutStringBuild.ToString();
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {

            int encryptionKey = 0x53;
            StringBuilder szInputStringBuild = new StringBuilder(cipherText);
            StringBuilder szOutStringBuild = new StringBuilder(cipherText.Length);
            char Textch;
            for (int iCount = 0; iCount < cipherText.Length; iCount++)
            {
                Textch = szInputStringBuild[iCount];
                Textch = (char)(Textch ^ encryptionKey);
                szOutStringBuild.Append(Textch);
            }
            return szOutStringBuild.ToString();
        }
    }
}
