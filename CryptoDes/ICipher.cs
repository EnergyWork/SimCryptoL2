namespace CryptoDes
{
    interface ICipher
    {
        byte[] Key { get; }
        byte[] IV { get; }
        byte[] Encrypt(byte[] data);
        byte[] Decrypt(byte[] data);
    }
}
