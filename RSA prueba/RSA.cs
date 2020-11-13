using System;
using System.Collections.Generic;
using System.Text;

namespace huffman_prueba
{
    class RSA
    {
        
        public keypair Generatekeys(int p, int q) {
            //por ahora asumir valores de p q y e
            p = 61;
            q = 53;
            //Por añadir generar e con sus reglas
            int e = 17;
            

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
                    lowerright = lowerright + phi;
                }

            }
            return lowerright;
        }

        public byte[] CipherAndDecipher(int m,int e,int n) {
            double c = (Math.Pow(m,e))%n;
            return BitConverter.GetBytes(c);
        }

        

        public int Erastothenes(int phi) {
            Random randomizer = new Random();

            

            return 0;
        }
        
    }
}
