using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using ToDoList.BLL.Interfaces;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Interfaces;
using ToDoList.DAL.Repositories;

namespace ToDoList.BLL.Services
{
    public class UserService : IUserService
    {
        private const string EncryptionKey = "dsadasdsadas43";
        private readonly IUnitOfWork _ef;

        public UserService()
        {
            _ef = new EFUnitOfWork();
        }

        public Errors CreateUser(string userName, string password, string repeatedPassword)
        {
            if (password != repeatedPassword)
            {
                return Errors.Password;
            }
            var existingUser = ((UserRepository)_ef.Users).Get(userName);
            if (existingUser != null)
            {
                return Errors.UserExists;
            }

            var user = new User { Password = Encrypt(password), UserName = userName };
            ((UserRepository)_ef.Users).Create(user);
            _ef.Save();

            return Errors.Success;
        }


        public Errors LoginUser(string userName, string password)
        {
            var user = ((UserRepository)_ef.Users).Get(userName);
            if (user == null)
            {
                return Errors.Authentification;
            }
            var decryptedPassword = Decrypt(user.Password);
            if (!decryptedPassword.Equals(password))
            {
                return Errors.Authentification;
            }
            return Errors.Success;
        }

        private static string Encrypt(string clearText)
        {
            var clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                if (encryptor == null) return clearText;

                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }

                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        private static string Decrypt(string cipherText)
        {
            var cipherBytes = Convert.FromBase64String(cipherText);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                if (encryptor == null) return cipherText;

                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }

                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

    }
}
