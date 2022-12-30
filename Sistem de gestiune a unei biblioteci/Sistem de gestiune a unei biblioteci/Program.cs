using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;


namespace Sistem_de_gestiune_a_unei_biblioteci
{
    class Program
    {
        static void Main(string[] args)
        {
            File.WriteAllText("Abonati.txt","|     Nume     |  Varsta  |           CNP           | Seria |     Data incepere     |     Data expirare    |\n");
            File.WriteAllText("Clienti.txt","|     Nume     |  Varsta  | Suma platita |\n");
            File.WriteAllText("Carti.txt","|ID|     Titlu     |      Autor       |Anul aparitiei|Numar pagini|Pret inchiriere|\n");

            List<Carte> Carti = new List<Carte>();
            List<Abonat> Abonati = new List<Abonat>();
            List<Client> Clienti = new List<Client>();
            while (true)
            {
                Console.WriteLine("1.Introducere carte in baza de date\n2.Introducere abonat in baza de date\n" +
                    "3.Introducere client in baza de date\n4.Stergere carte din baza de date\n" +
                    "5.Stergere abonat din baza de date\n6.Stergere client din baza de date\n7.Modificare carte\n" +
                    "8.Modificare abonat\n9.Modificare client\n10.Afisare carti\n11.Afisare abonati\n12.Afisare clienti\n13.Iesire aplicatie\n");
                if(Abonati.Count != 0)//stergem abonatii carora le-a expirat abonamentul
                {
                    string[] randuri = File.ReadAllLines("Abonati.txt");
                    int index_expirat = 1;
                    File.WriteAllText("Abonati.txt", randuri[0] + "\n");
                    foreach (Abonat abonat in Abonati)
                    {
                        if((abonat.data_expirare.An < DateTime.Now.Year) || 
                            (abonat.data_expirare.Luna < DateTime.Now.Month && abonat.data_expirare.An == DateTime.Now.Year) ||
                            (abonat.data_expirare.Luna == DateTime.Now.Month && abonat.data_expirare.An == DateTime.Now.Year &&
                            abonat.data_expirare.Zi < DateTime.Now.Day))
                        {
                            Abonati.Remove(abonat);
                            index_expirat++;
                        }
                        else
                        {
                            File.AppendAllText("Abonati.txt",randuri[index_expirat] + "\n");
                            index_expirat++;
                        }
                    }
                }
                Console.Write("Alegeti o comanda:");
                string alegere = Console.ReadLine();
                Console.Clear();
                switch (alegere)
                {
                    case "1":
                        string titlu, autor;
                        uint anul_aparitiei, nr_pagini;
                        float pret_inchiriere;
                        bool dublura_carte = false;
                        try
                        {
                            Console.Write("Titlu:");
                            titlu = Console.ReadLine();
                            Console.Write("Autor:");
                            autor = Console.ReadLine();
                            do
                            {
                                Console.Write("Anul aparitiei:");
                                anul_aparitiei = Convert.ToUInt32(Console.ReadLine());
                            } while (!Carte.AnulAparitieiIsValid(anul_aparitiei));
                            Console.Write("Numar pagini:");
                            nr_pagini = Convert.ToUInt32(Console.ReadLine());
                            do
                            {
                                Console.Write("Pret inchiriere:");
                                pret_inchiriere = Convert.ToInt32(Console.ReadLine());
                            } while (pret_inchiriere <= 0);
                            foreach (Carte carte in Carti)
                            {
                                if (carte.Titlu == titlu && carte.Autor == autor && 
                                    carte.Anul_aparitiei == anul_aparitiei && carte.Nr_pagini == nr_pagini &&
                                    carte.Pret_inchiriere == pret_inchiriere)
                                {
                                    dublura_carte = true;
                                    break;
                                }

                            }
                            if (!dublura_carte)
                            {
                                Carti.Add(new Carte(titlu, autor, anul_aparitiei, nr_pagini, pret_inchiriere));

                                //scriere in fisier
                                File.AppendAllText("Carti.txt", Carte.id + "       " + titlu + "             " +
                                    autor + "               " + anul_aparitiei.ToString() +
                                    "           " + nr_pagini.ToString() +
                                    "           " + pret_inchiriere.ToString() + "\n");
                                Console.Clear();
                                Console.WriteLine("Carte inregistrata cu succes!\n");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Inregistrarea exista deja!\n");
                            }
                        }
                        catch(Exception e)
                        {
                            Console.Clear();
                            Console.WriteLine("Date de intrare invalide.Operatie incheiata cu esec!\n");
                        }
                        break;
                    case "2":
                        string nume,cnp,seria;
                        int varsta,luni;
                        bool dublura_abonat = false;
                        try
                        {
                            Console.Write("Nume:");
                            nume = Console.ReadLine();
                            do
                            {
                                Console.Write("Varsta:");
                                varsta = Convert.ToInt32(Console.ReadLine());
                            } while (!Client.VarstaIsValid(varsta));
                            do
                            {
                                Console.Write("Durata in luni:");
                                luni = Convert.ToInt32(Console.ReadLine());
                            } while (luni < 0);
                            data data_incepere = new data(DateTime.Today.Day,DateTime.Today.Month,DateTime.Today.Year);
                            DateTime increment = DateTime.Today.AddMonths(luni);
                            data data_expirare = new data(increment.Day, increment.Month, increment.Year);

                            do
                            {
                                Console.Write("CNP:");
                                cnp = Console.ReadLine();
                            } while (!Buletin.CnpIsValid(cnp));
                            do
                            {
                                Console.Write("Seria:");
                                seria = Console.ReadLine();
                            } while (!Buletin.SeriaIsValid(seria));
                            foreach(Abonat ab in Abonati) {
                                if(ab.Nume == nume && ab.Varsta == varsta && ab.buletin.Cnp == cnp && ab.buletin.Seria == seria)
                                {
                                    dublura_abonat = true;
                                    break;
                                }
                                
                            }
                            if (!dublura_abonat)
                            {
                                Abonati.Add(new Abonat(nume, varsta, new Buletin(cnp, seria), data_incepere, data_expirare));
                                //scriere in fisier
                                File.AppendAllText("Abonati.txt", nume + "              " + varsta + "            " + cnp +
                                    "         " + seria + "            " + data_incepere.Zi + "-" +
                                    data_incepere.Luna + "-" + data_incepere.An + "                 " + data_expirare.Zi + "-" +
                                    data_expirare.Luna + "-" + data_expirare.An + "\n");
                                Console.Clear();
                                Console.WriteLine("Abonat inregistrat cu succes!\n");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Inregistrarea exista deja!\n");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.Clear();
                            Console.WriteLine("Date de intrare invalide!Introduceti datele din nou!\n");
                        }
                        break;
                    case "3":
                        string nume_client;
                        int varsta_client,numar_carti_inchiriate,aux;
                        string cnp_client;
                        double suma_platita = 0.0;
                        bool abonat = false;
                        List<int> id_uri = new List<int>();
                        if(Carte.id == -1)
                        {
                            Console.WriteLine("Nici o carte introdusa in baza de date!\n");
                            break;
                        }
                        try
                        {
                            Console.Write("Nume:");
                            nume_client = Console.ReadLine();
                            do
                            {
                                Console.Write("Varsta:");
                                varsta_client = Convert.ToInt32(Console.ReadLine());
                            } while (!Client.VarstaIsValid(varsta_client));
                            do
                            {
                                Console.Write("CNP:");
                                cnp_client = Console.ReadLine();
                            } while (!Buletin.CnpIsValid(cnp_client));
                            do
                            {
                                Console.Write("Numar de carti inchiriate:");
                                numar_carti_inchiriate = Convert.ToInt32(Console.ReadLine());
                            } while (numar_carti_inchiriate <= 0);
                            for (int i = 0; i < numar_carti_inchiriate; i++)
                            {
                                do
                                {
                                    Console.Write("   Id-ul cartii " + (i + 1) + " :");
                                    aux = Convert.ToInt32(Console.ReadLine());
                                    id_uri.Add(aux);
                                } while (id_uri[i] < 0);
                                suma_platita += Carti[id_uri[i]].Pret_inchiriere;
                            }
                            foreach(Abonat abt in Abonati)
                            {
                                if(abt.Nume == nume_client && abt.buletin.Cnp == cnp_client)
                                {
                                    abonat = true;
                                    break;
                                }
                            }
                            if (abonat)
                            {
                                suma_platita *= 0.5;
                            }

                            Clienti.Add(new Client(nume_client, varsta_client, suma_platita));

                            //scriere in fisier
                            File.AppendAllText("Clienti.txt", nume_client + "              " +
                                varsta_client + "           " + suma_platita + "\n");
                            Console.Clear();
                            Console.WriteLine("Client inregistrat cu succes!\n");

                        }
                        catch (Exception e)
                        {
                            Console.Clear();
                            Console.WriteLine("Date de intrare invalide!Introduceti datele din nou!\n");
                        }
                        break;
                    case "4":
                        string titlu_carte, autor_carte;
                        bool carte_stearsa = false;
                        Console.Write("Titlul cartii:");
                        titlu_carte = Console.ReadLine();
                        Console.Write("Autorul cartii:");
                        autor_carte = Console.ReadLine();

                        foreach(Carte carte in Carti)
                        {
                            if(titlu_carte == carte.Titlu && autor_carte == carte.Autor)
                            {
                                Carti.Remove(carte);
                                carte_stearsa = true;
                                break;
                            }
                        }
                        if (carte_stearsa)
                        {
                            string[] linii = File.ReadAllLines("Carti.txt");
                            File.WriteAllText("Carti.txt", "");//stergem ce era anterior
                            for (int i = 0; i < linii.Length; i++)
                            {

                                if (linii[i].Contains(titlu_carte) && linii[i].Contains(autor_carte))
                                {
                                    continue;
                                }
                                else
                                {
                                    File.AppendAllText("Carti.txt", linii[i] + "\n");
                                }
                            }
                            Console.Clear();
                            Console.WriteLine("Carte stearsa cu succes din baza de date!\n");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Cartea nu exista!\n");
                        }
                        break;
                    case "5":
                        string cnp_abonat, nume_abonat,serie_abonat;
                        bool abonat_sters = false;

                        Console.Write("Numele abonatului:");
                        nume_abonat = Console.ReadLine();
                        do
                        {
                            Console.Write("CNP-ul abonatului:");
                            cnp_abonat = Console.ReadLine();
                        } while (!Buletin.CnpIsValid(cnp_abonat));
                        do
                        {
                            Console.Write("Seria abonatului:");
                            serie_abonat = Console.ReadLine();
                        } while (!Buletin.SeriaIsValid(serie_abonat));

                        foreach (Abonat abt in Abonati)
                        {
                                if (nume_abonat == abt.Nume && serie_abonat == abt.buletin.Seria && cnp_abonat == abt.buletin.Cnp)
                                {
                                    Abonati.Remove(abt);
                                    abonat_sters = true;
                                    break;
                                }
                        }
                        if (abonat_sters)
                        {
                            string[] linii = File.ReadAllLines("Abonati.txt");
                            File.WriteAllText("Abonati.txt", "");//stergem ce era anterior
                            for (int i = 0; i < linii.Length; i++)
                            {

                                if (linii[i].Contains(nume_abonat) && linii[i].Contains(cnp_abonat) && linii[i].Contains(serie_abonat))
                                {
                                    continue;
                                }
                                else
                                {
                                    File.AppendAllText("Abonati.txt", linii[i] + "\n");
                                }
                            }
                            Console.Clear();
                            Console.WriteLine("Abonatul sters cu succes din baza de date!\n");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Abonatul nu exista in baza de date!\n");
                        }
                        break;
                    case "6":
                        string nume_client_sters;
                        double suma_platita_client;
                        int varsta_client_sters;
                        bool client_sters = false;
                        Console.Write("Numele clientului:");
                        nume_client_sters = Console.ReadLine();
                        try
                        {
                            do
                            {
                                Console.Write("Varsta clientului:");
                                varsta_client_sters = Convert.ToInt32(Console.ReadLine());
                            } while (Client.VarstaIsValid(varsta_client_sters));
                            do
                            {
                                Console.Write("Suma platita de client:");
                                suma_platita_client = Convert.ToDouble(Console.ReadLine());
                            } while (suma_platita_client <= 0);
                        }
                        catch(Exception e)
                        {
                            Console.Clear();
                            Console.WriteLine("Date invalide!\n");
                            break;
                        }

                        foreach (Client client in Clienti)
                        {
                            if (nume_client_sters == client.Nume && varsta_client_sters == client.Varsta && suma_platita_client == client.Suma_platita)
                            {
                                Clienti.Remove(client);
                                client_sters = true;
                                break;
                            }
                        }
                        if (client_sters)
                        {
                            string[] linii = File.ReadAllLines("Clienti.txt");
                            File.WriteAllText("Clienti.txt", "");//stergem ce era anterior
                            for (int i = 0; i < linii.Length; i++)
                            {

                                if (linii[i].Contains(nume_client_sters) && linii[i].Contains(varsta_client_sters.ToString()) && linii[i].Contains(suma_platita_client.ToString()))
                                {
                                    continue;
                                }
                                else
                                {
                                    File.AppendAllText("Clienti.txt", linii[i] + "\n");
                                }
                            }
                            Console.Clear();
                            Console.WriteLine("Client sters cu succes din baza de date!\n");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Clientul nu exista!\n");
                        }
                        break;
                    case "7":
                        Console.Clear();
                        string titlu_carte_modificata, autor_carte_modificata;
                        bool exista_carte = false;
                        int index_carte_modificata = 0;
                        string[] linii_carti_modificare = File.ReadAllLines("Carti.txt");
                        Console.Write("Titlul cartii modificate:");
                        titlu_carte_modificata = Console.ReadLine();
                        Console.Write("Autorul cartii modificate:");
                        autor_carte_modificata = Console.ReadLine();
                        Console.Clear();
                        foreach(Carte carte in Carti)
                        {
                            if(carte.Titlu == titlu_carte_modificata && carte.Autor == autor_carte_modificata)
                            { 
                                exista_carte = true;
                                break;
                            }
                            index_carte_modificata++;
                        }
                        if (exista_carte)
                        {
                            Console.WriteLine("1.Titlu\n2.Autor\n3.Anul aparitiei\n4.Numar pagini\n5.Pret inchiriere\n");
                            string alegere_modificare_carte;
                            Console.Write("Alege campul modificat:");
                            alegere_modificare_carte = Console.ReadLine();
                            Console.Clear();
                            switch (alegere_modificare_carte)
                            {
                                case "1":
                                    Console.Write("Noul titlu:");
                                    string noul_titlu_carte = Console.ReadLine();
                                    string titlu_vechi = Carti[index_carte_modificata].Titlu;
                                    Carti[index_carte_modificata].Titlu = noul_titlu_carte;
                                    linii_carti_modificare[index_carte_modificata + 1] = linii_carti_modificare[index_carte_modificata + 1].Replace(titlu_vechi, noul_titlu_carte);
                                    File.WriteAllText("Carti.txt", "");//stergem ce era inainte
                                    foreach(string linie in linii_carti_modificare)
                                    {
                                        File.AppendAllText("Carti.txt", linie + "\n");
                                    }
                                    Console.Clear();
                                    Console.WriteLine("Titlu modificat cu succes!\n");
                                    break;
                                case "2":
                                    Console.Write("Noul autor:");
                                    string noul_autor_carte = Console.ReadLine();
                                    string autor_vechi = Carti[index_carte_modificata].Autor;
                                    Carti[index_carte_modificata].Autor = noul_autor_carte;
                                    linii_carti_modificare[index_carte_modificata + 1] = linii_carti_modificare[index_carte_modificata + 1].Replace(autor_vechi, noul_autor_carte);
                                    File.WriteAllText("Carti.txt", "");//stergem ce era inainte
                                    foreach (string linie in linii_carti_modificare)
                                    {
                                        File.AppendAllText("Carti.txt", linie + "\n");
                                    }
                                    Console.Clear();
                                    Console.WriteLine("Autor modificat cu succes!\n");
                                    break;
                                case "3":
                                    Console.Write("Noul an al aparitiei:");
                                    try
                                    {
                                        uint noul_an_carte = Convert.ToUInt32(Console.ReadLine());
                                        uint anul_vechi = Carti[index_carte_modificata].Anul_aparitiei;
                                        Carti[index_carte_modificata].Anul_aparitiei = noul_an_carte;
                                        linii_carti_modificare[index_carte_modificata + 1] = linii_carti_modificare[index_carte_modificata + 1].Replace(anul_vechi.ToString(), noul_an_carte.ToString());
                                        File.WriteAllText("Carti.txt", "");//stergem ce era inainte
                                        foreach (string linie in linii_carti_modificare)
                                        {
                                            File.AppendAllText("Carti.txt", linie + "\n");
                                        }
                                        Console.Clear();
                                        Console.WriteLine("An modificat cu succes!\n");
                                    }
                                    catch (Exception e)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Input invalid!\n");
                                    }
                                    break;
                                case "4":
                                    Console.Write("Noul numar de pagini:");
                                    try
                                    {
                                        uint noul_nr_pagini_carte = Convert.ToUInt32(Console.ReadLine());
                                        uint nr_pagini_vechi = Carti[index_carte_modificata].Nr_pagini;
                                        Carti[index_carte_modificata].Nr_pagini = noul_nr_pagini_carte;
                                        linii_carti_modificare[index_carte_modificata + 1] = linii_carti_modificare[index_carte_modificata + 1].Replace(nr_pagini_vechi.ToString(), noul_nr_pagini_carte.ToString());
                                        File.WriteAllText("Carti.txt", "");//stergem ce era inainte
                                        foreach (string linie in linii_carti_modificare)
                                        {
                                            File.AppendAllText("Carti.txt", linie + "\n");
                                        }
                                        Console.Clear();
                                        Console.WriteLine("Numarul paginilor modificat cu succes!\n");
                                    }
                                    catch (Exception e)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Input invalid!\n");
                                    }
                                    break;
                                case "5":
                                    Console.Write("Noul pret de inchiriere:");
                                    try
                                    {
                                        float noul_pret_inchiriere_carte = Convert.ToSingle(Console.ReadLine());
                                        float pret_vechi = Carti[index_carte_modificata].Pret_inchiriere;
                                        Carti[index_carte_modificata].Pret_inchiriere = noul_pret_inchiriere_carte;
                                        linii_carti_modificare[index_carte_modificata + 1] = linii_carti_modificare[index_carte_modificata + 1].Replace(pret_vechi.ToString(), noul_pret_inchiriere_carte.ToString());
                                        File.WriteAllText("Carti.txt", "");//stergem ce era inainte
                                        foreach (string linie in linii_carti_modificare)
                                        {
                                            File.AppendAllText("Carti.txt", linie + "\n");
                                        }
                                        Console.Clear();
                                        Console.WriteLine("Pretul de inchiriere modificat cu succes!\n");
                                    }
                                    catch (Exception e)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Input invalid!\n");
                                    }
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Comanda invalida!\n");
                                    break;
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Titlul si autorul introdusi nu exista in baza de date!\n");
                        }
                        break;
                    case "8":
                        Console.Clear();
                        string nume_abonat_modificat, cnp_abonat_modificat,seria_abonat_modificat;
                        bool exista_abonat = false;
                        int index_abonat_modificat = 0;
                        string[] linii_abonati_modificare = File.ReadAllLines("Abonati.txt");
                        Console.Write("Numele abonatului modificat:");
                        nume_abonat_modificat = Console.ReadLine();
                        do
                        {
                            Console.Write("CNP-ul abonatului modificat:");
                            cnp_abonat_modificat = Console.ReadLine();
                        } while (!Buletin.CnpIsValid(cnp_abonat_modificat));
                        do
                        {
                            Console.Write("Seria abonatului modificat:");
                            seria_abonat_modificat = Console.ReadLine();
                        } while (!Buletin.SeriaIsValid(seria_abonat_modificat));
                        Console.Clear();
                        foreach (Abonat abt in Abonati)
                        {
                            if (abt.Nume == nume_abonat_modificat && abt.buletin.Cnp == cnp_abonat_modificat && abt.buletin.Seria == seria_abonat_modificat)
                            {
                                exista_abonat = true;
                                break;
                            }
                            index_abonat_modificat++;
                        }
                        if (exista_abonat)
                        {
                            Console.WriteLine("1.Nume\n2.Varsta\n3.CNP\n4.Seria\n");
                            string alegere_modificare_abonat;
                            Console.Write("Alege campul modificat:");
                            alegere_modificare_abonat = Console.ReadLine();
                            Console.Clear();
                            switch (alegere_modificare_abonat)
                            {
                                case "1":
                                    Console.Write("Noul nume:");
                                    string noul_nume_abonat = Console.ReadLine();
                                    string nume_vechi = Abonati[index_abonat_modificat].Nume;
                                    Abonati[index_abonat_modificat].Nume = noul_nume_abonat;
                                    linii_abonati_modificare[index_abonat_modificat + 1] = linii_abonati_modificare[index_abonat_modificat + 1].Replace(nume_vechi, noul_nume_abonat);
                                    File.WriteAllText("Abonati.txt", "");//stergem ce era inainte
                                    foreach (string linie in linii_abonati_modificare)
                                    {
                                        File.AppendAllText("Abonati.txt", linie + "\n");
                                    }
                                    Console.Clear();
                                    Console.WriteLine("Nume modificat cu succes!\n");
                                    break;
                                case "2":
                                    Console.Write("Noua varsta:");
                                    try
                                    {
                                        int noua_varsta_abonat = Convert.ToInt32(Console.ReadLine());
                                        int varsta_veche = Abonati[index_abonat_modificat].Varsta;
                                        Abonati[index_abonat_modificat].Varsta = noua_varsta_abonat;
                                        linii_abonati_modificare[index_abonat_modificat + 1] = linii_abonati_modificare[index_abonat_modificat + 1].Replace(varsta_veche.ToString(), noua_varsta_abonat.ToString());
                                        File.WriteAllText("Abonati.txt", "");//stergem ce era inainte
                                        foreach (string linie in linii_abonati_modificare)
                                        {
                                            File.AppendAllText("Abonati.txt", linie + "\n");
                                        }
                                        Console.Clear();
                                        Console.WriteLine("Varsta modificata cu succes!\n");
                                    }
                                    catch(Exception e)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Input invalid!\n");
                                    }
                                    break;
                                case "3":
                                    string noul_cnp_abonat;
                                    do
                                    {
                                        Console.Write("Noul CNP:");
                                        noul_cnp_abonat = Console.ReadLine();
                                    } while (!Buletin.CnpIsValid(noul_cnp_abonat));
                                    string cnp_vechi = Abonati[index_abonat_modificat].buletin.Cnp;
                                    Abonati[index_abonat_modificat].buletin.Cnp = noul_cnp_abonat;
                                    linii_abonati_modificare[index_abonat_modificat + 1] = linii_abonati_modificare[index_abonat_modificat + 1].Replace(cnp_vechi, noul_cnp_abonat);
                                    File.WriteAllText("Abonati.txt", "");//stergem ce era inainte
                                    foreach (string linie in linii_abonati_modificare)
                                    {
                                        File.AppendAllText("Abonati.txt", linie + "\n");
                                    }
                                    Console.Clear();
                                    Console.WriteLine("CNP modificat cu succes!\n");
                                    break;
                                case "4":
                                    string noua_serie_abonat;
                                    do
                                    {
                                        Console.Write("Noua serie:");
                                        noua_serie_abonat = Console.ReadLine();
                                    } while (!Buletin.SeriaIsValid(noua_serie_abonat));
                                    string serie_veche = Abonati[index_abonat_modificat].buletin.Seria;
                                    Abonati[index_abonat_modificat].buletin.Seria = noua_serie_abonat;
                                    linii_abonati_modificare[index_abonat_modificat + 1] = linii_abonati_modificare[index_abonat_modificat + 1].Replace(serie_veche, noua_serie_abonat);
                                    File.WriteAllText("Abonati.txt", "");//stergem ce era inainte
                                    foreach (string linie in linii_abonati_modificare)
                                    {
                                        File.AppendAllText("Abonati.txt", linie + "\n");
                                    }
                                    Console.Clear();
                                    Console.WriteLine("Seria modificata cu succes!\n");
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Comanda invalida!\n");
                                    break;
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Titlul si autorul introdusi nu exista in baza de date!\n");
                        }
                        break;
                    case "9":
                        Console.Clear();
                        string nume_client_modificat;
                        int varsta_client_modificat; 
                        double plata_client_modificat;
                        bool exista_client = false;
                        int index_client_modificat = 0;
                        string[] linii_clienti_modificare = File.ReadAllLines("Clienti.txt");
                        Console.Write("Numele clientului modificat:");
                        nume_client_modificat = Console.ReadLine();
                        try
                        {
                            do
                            {
                                Console.Write("Varsta client modificat:");
                                varsta_client_modificat = Convert.ToInt32(Console.ReadLine());
                            } while (!Client.VarstaIsValid(varsta_client_modificat));
                            do
                            {
                                Console.Write("Plata clientului modificat:");
                                plata_client_modificat = Convert.ToDouble(Console.ReadLine());
                            } while (plata_client_modificat <= 0);
                        }
                        catch(Exception e)
                        {
                            Console.Clear();
                            Console.WriteLine("Input invalid!\n");
                            break;
                        }
                        Console.Clear();
                        foreach (Client client in Clienti)
                        {
                            if (client.Nume == nume_client_modificat && client.Varsta == varsta_client_modificat && client.Suma_platita == plata_client_modificat)
                            {
                                exista_client = true;
                                break;
                            }
                            index_client_modificat++;
                        }
                        if (exista_client)
                        {
                            Console.WriteLine("1.Nume\n2.Varsta\n3.Suma platita\n");
                            string alegere_modificare_abonat;
                            Console.Write("Alege campul modificat:");
                            alegere_modificare_abonat = Console.ReadLine();
                            Console.Clear();
                            switch (alegere_modificare_abonat)
                            {
                                case "1":
                                    Console.Write("Noul nume:");
                                    string noul_nume_client = Console.ReadLine();
                                    string nume_vechi = Clienti[index_client_modificat].Nume;
                                    Clienti[index_client_modificat].Nume = noul_nume_client;
                                    linii_clienti_modificare[index_client_modificat + 1] = linii_clienti_modificare[index_client_modificat + 1].Replace(nume_vechi, noul_nume_client);
                                    File.WriteAllText("Clienti.txt", "");//stergem ce era inainte
                                    foreach (string linie in linii_clienti_modificare)
                                    {
                                        File.AppendAllText("Clienti.txt", linie + "\n");
                                    }
                                    Console.Clear();
                                    Console.WriteLine("Nume modificat cu succes!\n");
                                    break;
                                case "2":
                                    Console.Write("Noua varsta:");
                                    try
                                    {
                                        int noua_varsta_client = Convert.ToInt32(Console.ReadLine());
                                        int varsta_veche = Clienti[index_client_modificat].Varsta;
                                        Clienti[index_client_modificat].Varsta = noua_varsta_client;
                                        linii_clienti_modificare[index_client_modificat + 1] = linii_clienti_modificare[index_client_modificat + 1].Replace(varsta_veche.ToString(), noua_varsta_client.ToString());
                                        File.WriteAllText("Clienti.txt", "");//stergem ce era inainte
                                        foreach (string linie in linii_clienti_modificare)
                                        {
                                            File.AppendAllText("Clienti.txt", linie + "\n");
                                        }
                                        Console.Clear();
                                        Console.WriteLine("Varsta modificata cu succes!\n");
                                    }
                                    catch (Exception e)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Input invalid!\n");
                                    }
                                    break;
                                case "3":
                                    Console.Write("Noua plata:");
                                    try
                                    {
                                        double noua_plata_client = Convert.ToDouble(Console.ReadLine());
                                        double plata_veche = Clienti[index_client_modificat].Suma_platita;
                                        Clienti[index_client_modificat].Suma_platita = noua_plata_client;
                                        linii_clienti_modificare[index_client_modificat + 1] = linii_clienti_modificare[index_client_modificat + 1].Replace(plata_veche.ToString(), noua_plata_client.ToString());
                                        File.WriteAllText("Clienti.txt", "");//stergem ce era inainte
                                        foreach (string linie in linii_clienti_modificare)
                                        {
                                            File.AppendAllText("Clienti.txt", linie + "\n");
                                        }
                                        Console.Clear();
                                        Console.WriteLine("Suma platita modificata cu succes!\n");
                                    }
                                    catch (Exception e)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Input invalid!\n");
                                    }
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Comanda invalida!\n");
                                    break;
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Titlul si autorul introdusi nu exista in baza de date!\n");
                        }
                        break;
                    case "10":
                        Console.WriteLine(File.ReadAllText("Carti.txt"));
                        Console.Write("\n\nApasati un buton pentru a iesi...\n");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "11":
                        Console.WriteLine(File.ReadAllText("Abonati.txt"));
                        Console.Write("\n\nApasati un buton pentru a iesi...\n");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "12":
                        Console.WriteLine(File.ReadAllText("Clienti.txt"));
                        Console.Write("\n\nApasati un buton pentru a iesi...\n");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "13":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Comanda invalida!\n");
                        break;
                }
            }
        }
    }
}
