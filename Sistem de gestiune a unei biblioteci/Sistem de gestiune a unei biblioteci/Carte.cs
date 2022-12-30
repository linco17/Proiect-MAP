using System;
using System.Collections.Generic;
using System.Text;

namespace Sistem_de_gestiune_a_unei_biblioteci
{
    class Carte
    {
            private string titlu;
            private string autor;
            private uint anul_aparitiei;
            private uint nr_pagini;
            private float pret_inchiriere;
            static public int id = -1;
            public Carte(string Titlu,string Autor,uint Anul_aparitiei,uint Nr_pagini,float Pret_inchiriere)
            {
               this.Titlu = Titlu;
               this.Autor = Autor;
               this.Anul_aparitiei = Anul_aparitiei;
               this.Nr_pagini = Nr_pagini;
               this.Pret_inchiriere = Pret_inchiriere;
               id++;
            }    

            public string Titlu
            {
                get
                {
                    return titlu;
                }
                set
                {
                    titlu = value;
                }
            }
            public string Autor
            {
                get
                {
                    return autor;
                }
                set
                {
                    autor = value;
                }
            }
            public uint Anul_aparitiei
            {
                get
                {
                    return anul_aparitiei;
                }
                set
                {
                    if (Carte.AnulAparitieiIsValid(value))
                    {
                        anul_aparitiei = value;
                    }
                    else
                    {
                        Console.WriteLine("Anul aparitiei este invalid!\n");
                    }
                }
            }
            public uint Nr_pagini
            {
                get
                {
                    return nr_pagini;
                }
                set
                {
                    if (value > 0)
                    {
                        nr_pagini = value;
                    }
                    else
                    {
                        Console.WriteLine("Valoare negativa!\n");
                    }
                }
            }
            public float Pret_inchiriere
            {
                get
                {
                    return pret_inchiriere;
                }
                set
                {
                    if (value > 0)
                    {
                        pret_inchiriere = value;
                    }
                    else
                    {
                        Console.WriteLine("Valoare negativa!\n");
                    }
                }
            }
            static public bool AnulAparitieiIsValid(uint verif)
            {
                if(verif <= DateTime.Now.Year && verif >= 0)
                {
                    return true;
                }
                return false;
            } 
        }
    }
