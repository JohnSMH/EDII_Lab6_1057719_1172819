using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace huffman_prueba
{
    class Program
    {
        static void Main(string[] args)
        {

            RSA prueba = new RSA();
            prueba.Generatekeys(0,0);
            byte m = 32;
            //prueba.Cipher(m,17,3233);
            Console.ReadKey();
        }

    }

}
