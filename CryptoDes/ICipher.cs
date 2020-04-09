namespace CryptoDes
{
    interface ICipher
    {
        byte[] Key { get; }
        byte[] IV { get; }
        byte[] Encrypt(byte[] data);
        void Encrypt(string fileIn, string fileOut);
        void Decrypt(string fileIn, string fileOut);
        byte[] Decrypt(byte[] data);
    }
}
