using System;
using System.Collections.Generic;
using System.Text;

namespace RSA_prueba
{
    public class key
    {
        public key() { }
        public key(int newn, int newed) {
            n = newn;
            ed = newed;
        }
        public int n {get; set;}
        public int ed {get; set;}
    }
     public class keypair {
        public key llavepublica { get; set; }
        public key llaveprivada { get; set; }

        public keypair() { }
        public keypair(key publica, key privada) {
            llavepublica = publica;
            llaveprivada = privada;
        }
    }
}
