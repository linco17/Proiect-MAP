using System;
using System.Collections.Generic;
using System.Text;

namespace Sistem_de_gestiune_a_unei_biblioteci
{
    class Client
    {
        protected int varsta;
        protected string nume;
        protected double suma_platita; //in lei 

        public Client()
        {}
        public Client(string Nume,int Varsta,double Suma_platita)
        {
            nume = Nume;
            varsta = Varsta;
            suma_platita  = Suma_platita;
        }
       public int Varsta
       {
            get
            {
                return varsta;
            }
            set
            {
                if(Client.VarstaIsValid(value))
                {
                    varsta = value;
                }
                else
                {
                    Console.WriteLine("Varsta invalida!\n");
                }
            }
       }
       public string Nume
       {
            get
            {
                return nume;
            }
            set
            {
                nume = value;
            }

       }
        public double Suma_platita
        {
            get
            {
                return suma_platita;
            }
            set
            {
                if(suma_platita > 0)
                {
                    suma_platita = value;
                }
                else
                {
                    Console.WriteLine("Suma negativa!\n");
                }
            }
        }
        static public bool VarstaIsValid(int verif)
        {
            if(verif >= 7 && verif <= 80)
            {
                return true;
            }
            return false;
        }
    }
}
