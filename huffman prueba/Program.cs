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

            Cifrado hola = new Cifrado();

            //Console.WriteLine(hola.CifrarCesar(message, encoded));
            //Console.WriteLine(hola.DecifrarCesar((hola.CifrarCesar(message, encoded)), key));
            //Console.WriteLine();
            Console.WriteLine(hola.Cifrarzigzag("MY SPIDER SENSE ARE TINGLING", 4));
            Console.WriteLine(hola.Descifrarzigzag(hola.Cifrarzigzag("MY SPIDER SENSE ARE TINGLING", 4), 4));
            Console.WriteLine();
            Console.WriteLine(hola.CifrarRuta("MY SPIDER SENSE ARE TINGLING", 4, 3));
            Console.WriteLine(hola.DecifrarRuta((hola.CifrarRuta("MY SPIDER SENSE ARE TINGLING", 4, 3)), 4, 3));
            Console.ReadKey();
        }

    }

}
