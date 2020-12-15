namespace ToDoList.BLL.Interfaces
{
    public interface ICryptService
    {
        string Encrypt(string clearText);
        string Decrypt(string cipherText);
    }
}
