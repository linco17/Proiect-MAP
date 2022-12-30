using System;
using System.Collections.Generic;
using System.Text;

namespace Sistem_de_gestiune_a_unei_biblioteci
{
    struct data
    {
        private int zi;
        private int luna;
        private int an;
        public data(int Zi,int Luna,int An)
        {
            zi = Zi;
            luna = Luna;
            an = An;
        }
        public int Zi
        {
            get
            {
                return zi;
            }
            set
            {
                if(value >= 1 && value <= 31)
                {
                    zi = value;
                }
                else
                {
                    Console.WriteLine("Zi invalida!\n");
                }
            }
        }

        public int Luna
        {
            get
            {
                return luna;
            }
            set
            {
                if (value >= 1 && value <= 12)
                {
                    luna = value;
                }
                else
                {
                    Console.WriteLine("Luna invalida!\n");
                }
            }
        }

        public int An
        {
            get
            {
                return an;
            }
            set
            {
                if (value >= DateTime.Now.Year)
                {
                    an = value;
                }
                else
                {
                    Console.WriteLine("An invalid!\n");
                }
            }
        }
    }
    class Abonat : Client
    {
        public Abonat(string Nume,int Varsta,Buletin Date,data Data_incepere,data Data_expirare)
        {
            data_incepere = Data_incepere;
            data_expirare = Data_expirare;
            this.Nume = Nume;
            this.Varsta = Varsta;
            buletin = Date;
        }
        public data data_incepere;
        public data data_expirare;
        public Buletin buletin;
        
    }
}
