using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace huffman_prueba
{
    public class LZW
    {
        string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        char []key;
        String letrasno = "[\\]^_`";
        Dictionary<int,int> keymap;

        public LZW()
        {
        }

         public String encoder(string clave)
        {
            key = clave.ToCharArray();
            String encoded = "";

            Boolean[] arr = new Boolean[letras.Length + 6];

            for (int i = 0; i < key.Length; i++)
            {
                if (letras.Contains(key[i]))
                {
                    if (arr[key[i] - 65] == false)
                    {
                        encoded += (char)key[i];
                        arr[key[i] - 65] = true;
                    }
                }
            }
            for (int i = 0; i < letras.Length + 6; i++)
            {
                if (letrasno.Contains((char)(i + 65)))
                {
                    arr[i] = true;
                }
                if (arr[i] == false)
                {
                    arr[i] = true;
                    encoded += (char)(i + 65);
                }
            }
            return encoded;
        }

        public int getpos(char x)
        {
            for (int i = 0; i < letras.Length; i++)
            {
                if (x == letras[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public int getposl(char x, string letras)
        {
            for (int i = 0; i < letras.Length; i++)
            {
                if (x == letras[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public String cipheredIt(string msg, string encoded)
        {
            String cipher = "";
            int pos = 0;


            for (int i = 0; i < msg.Length; i++)
            {
                if (msg[i] >= 'A' && msg[i] <= 'z' && !letrasno.Contains(msg[i]))
                {
                    pos = getpos(msg[i]);
                    cipher += encoded[pos];
                }
                else 
                {
                    cipher += msg[i];
                }
            }
            return cipher;
        }

       public string decipheredIt(string msg, string clave)
        {
            int pos = 0;
            string decipher = "";
            string claves = encoder(clave);
            for (int i = 0; i < msg.Length; i++)
            {
                if (msg[i] >= 'A' && msg[i] <= 'z' && !letrasno.Contains(msg[i]))
                {
                    pos = getposl(msg[i], claves);
                    decipher += letras[pos];
                }
                else
                {
                    decipher += msg[i];
                }

            }

            return decipher;
        }

        public string Cifrarzigzag(string msg, int nivel)
        {
            int n = 0;
            if ((msg.Length + 1) % nivel != 0)
            {
                n = (msg.Length + 1) % nivel;
                for (int i = 0; i < n + 1; i++)
                {
                    msg += "*";
                }
            }
            char[,] rail = new char[nivel, (msg.Length)] ;

            for (int i = 0; i < nivel; i++)
                for (int j = 0; j < msg.Length; j++)
                    rail[i,j] = '\n';
            bool dir_down = false;
            int col = 0;
            int rows = 0;
            for (int i = 0; i < msg.Length; i++)
            {
                if (rows == 0 || rows == nivel - 1)
                    dir_down = !dir_down;
                rail[rows, col++] = msg[i];



                if (dir_down)
                    rows += 1;
                else
                    rows -= 1;
            }

            string result = "";
            for (int i = 0; i < nivel; i++)
                for (int j = 0; j < msg.Length; j++)
                    if (rail[i,j] != '\n')
                        result += rail[i,j];

            return result;
        }

        public string Descifrarzigzag(string cipher, int nivel)
        {
            char[,] rail = new char[nivel, (cipher.Length)];
            for (int i = 0; i < nivel; i++)
                for (int j = 0; j < cipher.Length; j++)
                    rail[i, j] = '\n';
            bool dir_down = false;

            int row = 0, col = 0;

            for (int i = 0; i < cipher.Length; i++)
            {
                if (row == 0)
                    dir_down = true;
                if (row == nivel - 1)
                    dir_down = false;

                rail[row, col++] = '^';

                if (dir_down)
                    row += 1;
                else
                    row -= 1;

            }

            int index = 0;
            for (int i = 0; i < nivel; i++)
                for (int j = 0; j < cipher.Length; j++)
                    if (rail[i, j] == '^' && index < cipher.Length)
                        rail[i, j] = cipher[index++];

            string result = "";

            row = 0; col = 0;
            for (int i = 0; i < cipher.Length; i++)
            {
                if (row == 0)
                    dir_down = true;
                if (row == nivel - 1)
                    dir_down = false;

                // place the marker 
                if (rail[row, col] != '^')
                    result += rail[row, col++];

                if (dir_down)
                    row += 1;
                else
                    row -= 1;

            }
            string resultado = "";
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i]!='*')
                {
                    resultado += result[i]; 
                }
                else
                {
                    break;
                }

            }

         return resultado;
        }

        public string CifrarRuta(string msg, int row, int col)
        {

            char[,] matrix = new char[row, col];
            char[] characters = msg.ToCharArray();
            string cipher = "";
            List<char[,]> hola;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    matrix[i, j] = characters[i + j + i * 2];
                }
            }
          

            return cipher;
        }
    }
}






