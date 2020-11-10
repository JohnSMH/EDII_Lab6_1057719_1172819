using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace huffman_prueba
{
    public class Cifrado
    {
        string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        char []key;
        String letrasno = "[\\]^_`";

        public Cifrado()
        {
        }

         public String encoder(string clave)
        {
            key = clave.ToCharArray();
            StringBuilder encoded = new StringBuilder();
           
            Boolean[] arr = new Boolean[letras.Length + 6];

            for (int i = 0; i < key.Length; i++)
            {
                if (letras.Contains(key[i]))
                {
                    if (arr[key[i] - 65] == false)
                    {
                        encoded.Append((char)key[i]);
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
                    encoded.Append((char)(i + 65));
                }
            }
            return encoded.ToString();
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

        public String CifrarCesar(string msg, string encoded)
        {
            StringBuilder cifrado = new StringBuilder();
            int pos = 0;


            for (int i = 0; i < msg.Length; i++)
            {
                if (msg[i] >= 'A' && msg[i] <= 'z' && !letrasno.Contains(msg[i]))
                {
                    pos = getpos(msg[i]);
                    cifrado.Append(encoded[pos]);
                }
                else 
                {
                    cifrado.Append(msg[i]);
                }
            }
            return cifrado.ToString();
        }

       public string DecifrarCesar(string msg, string clave)
        {
            int pos = 0;
            StringBuilder descifrado = new StringBuilder();
            string claves = encoder(clave);
            for (int i = 0; i < msg.Length; i++)
            {
                if (msg[i] >= 'A' && msg[i] <= 'z' && !letrasno.Contains(msg[i]))
                {
                    pos = getposl(msg[i], claves);
                    descifrado.Append(letras[pos]);
                }
                else
                {
                    descifrado.Append(msg[i]);
                }

            }

            return descifrado.ToString();
        }

        public string Cifrarzigzag(string msg, int nivel)
        {
            int n = 0;
            if ((msg.Length + 1) % nivel != 0)
            {
                n = (msg.Length + 1) % nivel;
                for (int i = 0; i < n + 1; i++)
                {
                    msg += "-";
                }
            }
            char[,] matriz = new char[nivel, (msg.Length)];

            for (int i = 0; i < nivel; i++)
                for (int j = 0; j < msg.Length; j++)
                    matriz[i, j] = '*';

            bool abajo = false;
            int col = 0;
            int rows = 0;
            for (int i = 0; i < msg.Length; i++)
            {
                if (rows == 0 || rows == nivel - 1)
                    abajo = !abajo;
                matriz[rows, col++] = msg[i];

                if (abajo)
                    rows += 1;
                else
                    rows -= 1;
            }

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < nivel; i++)
                for (int j = 0; j < msg.Length; j++)
                    if (matriz[i, j] != '*')
                        result.Append(matriz[i, j]);

            return result.ToString();
        }

        public string Descifrarzigzag(string msg, int nivel)
        {
            char[,] matriz = new char[nivel, (msg.Length)];
            for (int i = 0; i < nivel; i++)
                for (int j = 0; j < msg.Length; j++)
                    matriz[i, j] = '*';
            bool abajo = false;

            int row = 0, col = 0;

            for (int i = 0; i < msg.Length; i++)
            {
                if (row == 0)
                    abajo = true;
                if (row == nivel - 1)
                    abajo = false;

                matriz[row, col++] = '$';

                if (abajo)
                    row += 1;
                else
                    row -= 1;

            }

            int index = 0;
            for (int i = 0; i < nivel; i++)
                for (int j = 0; j < msg.Length; j++)
                    if (matriz[i, j] == '$' && index < msg.Length)
                        matriz[i, j] = msg[index++];

            StringBuilder result = new StringBuilder();

            row = 0; col = 0;
            for (int i = 0; i < msg.Length; i++)
            {
                if (row == 0)
                    abajo = true;
                if (row == nivel - 1)
                    abajo = false;

                if (matriz[row, col] != '$')
                    result.Append(matriz[row, col++]);

                if (abajo)
                    row += 1;
                else
                    row -= 1;

            }
            StringBuilder resultado = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] != '-')
                {
                    resultado.Append(result[i]);
                }
                else
                {
                    break;
                }

            }

            return resultado.ToString();
        }

        public string CifrarRuta(string msg, int row, int col)
        {
            char[,] matrix = new char[row, col];
            char[] characters = msg.ToCharArray();
            StringBuilder cipher = new StringBuilder();
          
            int o = 0;
            int p = 0;
                while (o < msg.Length)
                {
                    for (int l = 0; l < row; l++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            if (o <msg.Length)
                            {
                                matrix[l, j] = characters[o];
                                o++;
                            }
                            else { matrix[l, j] = '*'; }

                        }
                    }
                    for (int u = 0; u < col; u++)
                    {
                        while (p < row)
                        {
                            cipher.Append(matrix[p, u]);
                            p++;
                        }
                            p = 0;
                    }

                } 

            return cipher.ToString();
        }

        public string DecifrarRuta(string msg, int row, int col)
        {
            char[,] matrix = new char[col, row];
            char[] characters = msg.ToCharArray();
            StringBuilder descipher = new StringBuilder();

            int o = 0;
            while (o < msg.Length)
            {
                for (int l = 0; l < col; l++)
                {
                    for (int j = 0; j < row; j++)
                    {
                        if (o < msg.Length)
                        {
                            matrix[l, j] = characters[o];
                            o++;
                        }
                        else { matrix[l, j] = '*'; }

                    }
                }
                for (int u = 0; u < row; u++)
                {
                    for (int l = 0; l < col; l++)
                    {
                        descipher.Append(matrix[l, u]);
                    }
                }

            }
            StringBuilder resultado = new StringBuilder();
            for (int i = 0; i < descipher.Length; i++)
            {
                if (descipher[i] != '*')
                {
                    resultado.Append(descipher[i]);
                }
                else
                {
                    break;
                }

            }

            return resultado.ToString();
        }
    }
}






