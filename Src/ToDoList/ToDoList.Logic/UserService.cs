using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using ToDoList.Database.Entities;
using ToDoList.Database.Repositories;
using ToDoList.Database.EF;

namespace ToDoList.Logic
{
    public class UserService
    {
        private static string EncryptionKey = "dsadasdsadas43";
        private UserRepository userRepository;

        public UserService()
        {
            DataBase db = new DataBase();
           
            userRepository = new UserRepository(db);
        }

        public void CreateUser(String userName, String password1, String password2)
        {
            if (password1 != password2)
            {
                throw new Exception("PasswordError");
            }
            User existingUser = userRepository.Get(userName);
            if (existingUser != null)
            {
                throw new Exception("UserExistsError");
            }

            User user = new User();
            user.Password = Encrypt(password1);
            user.UserName = userName;
            userRepository.Create(user);
        }


        public User LoginUser(string login, string password)
        {
            User user = userRepository.Get(login);
            if(user == null)
            {
                throw new Exception("AuthError");
            }
            string decryptedPassword = Decrypt(user.Password);
            if (!decryptedPassword.Equals(password))
            {
                throw new Exception("AuthError");
            }
            return user;
        } 

        private static string Encrypt(string clearText)
        {
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

        private static string Decrypt(string cipherText)
        {
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

    }
}
