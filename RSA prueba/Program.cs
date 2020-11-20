using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace RSA_prueba
{
    class Program
    {
        static void Main(string[] args)
        {

            RSA prueba = new RSA();
            prueba.Generatekeys(0,0);
            byte m = 32;
            List<byte[]> bytelist = new List<byte[]>();
            int ahoraadecifrar =prueba.Manualbytetoint(prueba.CipherAndDecipher(m,17,3233));
            List<byte> finaltest = new List<byte>();
            finaltest.AddRange(prueba.CipherAndDecipher(ahoraadecifrar, 2753,3233));

            Console.ReadKey();
        }

    }

}
