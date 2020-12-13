using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.BLL.Interfaces
{
    interface ICryptService
    {
        string Encrypt(string clearText);
        string Decrypt(string cipherText);
    }
}
