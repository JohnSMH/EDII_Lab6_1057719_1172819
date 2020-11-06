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
            //READ
            var fileRead = new FileStream("cuento.txt", FileMode.OpenOrCreate);
            var BfileRead = new BufferedStream(fileRead);
            var reader = new StreamReader(BfileRead);

            string prueba = reader.ReadToEnd();

            reader.Close();
            BfileRead.Close();
            fileRead.Close();

            //WRITE
            var fileWrite = new FileStream("cuento.csr",FileMode.OpenOrCreate);
            var BfileWrite = new BufferedStream(fileWrite);
            var writer = new StreamWriter(BfileWrite);

            string salida="";
            foreach (char item in salida)
            {
                writer.Write(item);
            }

            writer.Close();
            BfileWrite.Close();
            fileWrite.Close();

            string key;
            key = "MARYJ";
            LZW hola = new LZW();

            string encoded = hola.encoder(key);
            string message = "MY SPIDER SENSE ARE TINGLING";
            Console.WriteLine(hola.CifrarCesar(message, encoded));
            Console.WriteLine(hola.DecifrarCesar((hola.CifrarCesar(message, encoded)), key));
            Console.WriteLine();
            Console.WriteLine(hola.Cifrarzigzag("MY SPIDER SENSE ARE TINGLING", 4));
            Console.WriteLine(hola.Descifrarzigzag(hola.Cifrarzigzag("MY SPIDER SENSE ARE TINGLING", 4), 4));
            Console.WriteLine();
            Console.WriteLine(hola.CifrarRuta("MY SPIDER SENSE ARE TINGLING", 4, 3));
            Console.WriteLine(hola.DecifrarRuta((hola.CifrarRuta("MY SPIDER SENSE ARE TINGLING", 4, 3)), 4, 3));
            Console.ReadKey();
        }

    }

}
