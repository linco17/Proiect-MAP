using System;
using System.Collections.Generic;
using System.Text;

namespace Sistem_de_gestiune_a_unei_biblioteci
{
    class Buletin
    {
        private string cnp;
        private string seria;

        public Buletin(string cnp,string seria)
        {
            Cnp = cnp;
            Seria = seria;
        }
        public string Cnp
        {
            get
            {
                return cnp;
            }
            set
            {
                
                if(Buletin.CnpIsValid(value))
                {
                    cnp = value;
                }
                else
                {
                    Console.WriteLine("CNP invalid!\n");
                }
            }
        }
        public string Seria
        {
            get
            {
                return seria;
            }
            set
            {
              
                if (Buletin.SeriaIsValid(value))
                {
                    seria = value;
                }
                else
                {
                    Console.WriteLine("Serie invalida!\n");
                }
            }
        }
        static public bool CnpIsValid(string verif)
        {
            bool numere = true;
            for (int i = 0; i < verif.Length; i++)
            {
                if (verif[i] > 57 || verif[i] < 48)
                {
                    numere = false;
                    break;
                }
            }
            if (verif.Length == 13 && numere)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static public bool SeriaIsValid(string verif)
        {
            bool litere = true;
            for (int i = 0; i < verif.Length; i++)
            {
                if (verif[i] < 65 || verif[i] > 90)
                {
                    litere = false;
                }
            }
            if (verif.Length == 2 && litere)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
