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


            string key;
            key = "HOLAiamoru";
            Cifrado hola = new Cifrado();

            string encoded = hola.encoder(key);

            string salida= "";
            salida = hola.CifrarCesar(prueba, encoded);

            foreach (char item in salida)
            {
                writer.Write(item);
            }

            writer.Close();
            BfileWrite.Close();
            fileWrite.Close();

            //READ
            var fileRead1 = new FileStream("cuento.csr", FileMode.OpenOrCreate);
            var BfileRead1 = new BufferedStream(fileRead1);
            var reader1 = new StreamReader(BfileRead1);

            string prueba1 = reader1.ReadToEnd();

            reader1.Close();
            BfileRead1.Close();
            fileRead1.Close();

            //WRITE
            var fileWrite1 = new FileStream("cuentoa.txt", FileMode.OpenOrCreate);
            var BfileWrite1 = new BufferedStream(fileWrite1);
            var writer1 = new StreamWriter(BfileWrite1);


            string salida1 = "";
            salida1 = hola.DecifrarCesar(prueba1, encoded);

            foreach (char item in salida1)
            {
                writer1.Write(item);
            }

            writer1.Close();
            BfileWrite1.Close();
            fileWrite1.Close();



            //Console.WriteLine(hola.CifrarCesar(message, encoded));
            //Console.WriteLine(hola.DecifrarCesar((hola.CifrarCesar(message, encoded)), key));
            //Console.WriteLine();
            //Console.WriteLine(hola.Cifrarzigzag("MY SPIDER SENSE ARE TINGLING", 4));
            //Console.WriteLine(hola.Descifrarzigzag(hola.Cifrarzigzag("MY SPIDER SENSE ARE TINGLING", 4), 4));
            //Console.WriteLine();
            //Console.WriteLine(hola.CifrarRuta("MY SPIDER SENSE ARE TINGLING", 4, 3));
            //Console.WriteLine(hola.DecifrarRuta((hola.CifrarRuta("MY SPIDER SENSE ARE TINGLING", 4, 3)), 4, 3));
            Console.ReadKey();
        }

    }

}
