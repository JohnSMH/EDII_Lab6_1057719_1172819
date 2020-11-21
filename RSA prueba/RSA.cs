using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RSA_prueba
{
   public class RSA
    {
        
        public keypair Generatekeys(int p, int q) {
            //por ahora asumir valores de p q y e
            //Por añadir generar e con sus reglas
            int e = 65537;
            

            int n = p * q;
            int phi = (p - 1) * (q - 1);
            //int e = Erastothenes(phi);

            int d = Euclides(phi, e);
            //Llave publica n,e
            //Llave privada n,d
            key publica = new key(n,e);
            key privada = new key(n,d);
            return new keypair(publica,privada);
        }

        public int Euclides(int phi, int e) {
            int upperright = phi;
            int upperleft = phi;
            int lowerleft = e;
            int lowerright = 1;

            while (lowerleft != 1) {
                int div = upperleft / lowerleft;

                int temp = lowerleft;

                lowerleft = upperleft - (div * lowerleft);
                upperleft = temp;

                temp = lowerright;

                lowerright = upperright - (div * lowerright);
                upperright = temp;

                if (lowerright < 0)
                {
                    lowerright = modulo(lowerright,phi);
                }

            }
            return lowerright;
        }

        public byte[] CipherAndDecipher(int m,int e,int n) {
            BigInteger c = BigInteger.ModPow(m, e, n);
            bool le = BitConverter.IsLittleEndian;
            return c.ToByteArray(true,false);
            
        }

        int modulo(int x, int N)
        {
            return (x % N + N) % N;
        }


        //public int Erastothenes(int phi) {
        //    Random randomizer = new Random();



        //    return 0;
        //}

        public int Manualbytetoint(byte[] number) {
            int finalnumber = 0;
            for (int i = 0; i < number.Length; i++)
            {
                if (i != 0)
                    finalnumber += number[i] * (256 * i);
                else
                    finalnumber += number[i];
            }
            return finalnumber;
        }
    }
}
