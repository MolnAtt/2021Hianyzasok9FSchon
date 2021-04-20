using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2021Hianyzasok9FSchon
{
    class Program
    {
        class Adat
        {
            public DateTime datum;
            public string kernev;
            public string veznev;
            public string hianyzas;
        }
        /*
        Függvény hetnapja(honap:egesz, nap:egesz): szöveg
            napnev[]:= ("vasarnap", "hetfo", "kedd", "szerda", "csutortok",
            "pentek", "szombat")
            napszam[]:= (0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 335)
            napsorszam:= (napszam[honap - 1]+nap) MOD 7
            hetnapja:= napnev[napsorszam]
        Függvény vége
        */

        static string hetnapja(int honap, int nap)
        {
            string[] napnev = new string[] { "vasarnap", "hetfo", "kedd", "szerda", "csutortok",
            "pentek", "szombat" };
            int[] napszam = new int[] { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 335 };
            int napsorszam = (napszam[honap - 1] + nap) % 7;
            return napnev[napsorszam];
        }

        static int napihianyzas(Adat a)
        {
            int sum = 0;
            foreach (char kar in a.hianyzas)
            {
                if ("XI".Contains(kar))
                {
                    sum++;
                }
            }
            return sum;
        }

        static void Main(string[] args)
        {
            List<Adat> lista = new List<Adat>();
            using (StreamReader f = new StreamReader("naplo.txt",Encoding.Default))
            {
                DateTime aktdatum = new DateTime();
                while (!f.EndOfStream)
                {
                    string sor = f.ReadLine();
                    string[] sortömb = sor.Split(' ');
                    if (sor[0] == '#')
                    {
                        aktdatum = new DateTime(2021, int.Parse(sortömb[1]), int.Parse(sortömb[2]));
                    }
                    else
                    {
                        lista.Add(new Adat { 
                            datum = aktdatum, 
                            veznev = sortömb[0], 
                            kernev = sortömb[1], 
                            hianyzas = sortömb[2] });
                    }
                }
            }
            Console.WriteLine($"2.feladat\nA naplóban {lista.Count} bejegyzés van.");

            int Xdb = 0;
            int Idb = 0;

            foreach (Adat adat in lista)
            {
                foreach (char betu in adat.hianyzas)
                {

                    /* switch (betu)
                    {
                        case 'X':
                            Xdb++;
                            break;
                        case 'I':
                            Idb++;
                            break;
                        default:
                            break;
                    }*/ 


                    if (betu == 'X')
                    {
                        Xdb++;
                    }
                    else if (betu == 'I')
                    { 
                        Idb++; 
                    }
                }
            }


            Console.WriteLine($"3.feladat\nAz igazolt hiányzások száma {Xdb}, az igazolatlanoké {Idb} óra.");


            //Console.WriteLine(lista.First().datum.ToString(@"yyyy.MM.tdd: dddd"));

            Console.WriteLine($"5. feladat\nA hónap sorszáma = ");
            int userhonap = int.Parse(Console.ReadLine());
            Console.WriteLine($"A nap sorszáma = ");
            int usernap = int.Parse(Console.ReadLine());
            Console.WriteLine($"Azon a napon {hetnapja(userhonap,usernap)} volt.");

            Console.WriteLine("6.feladat");
            Console.WriteLine("A nap neve =");
            string usernapnev = Console.ReadLine();
            Console.WriteLine("Az óra sorszáma =");
            int userhanyadikora = int.Parse(Console.ReadLine());

            int hdb = 0;

            foreach (Adat adat in lista)
            {
                if //(hetnapja(adat.datum.Month, adat.datum.Day) == usernapnev && (adat.hianyzas[userhanyadikora - 1] == 'X' || adat.hianyzas[userhanyadikora - 1] == 'I'))
                    (hetnapja(adat.datum.Month, adat.datum.Day) == usernapnev
                    && "XI".Contains(adat.hianyzas[userhanyadikora - 1]))
                {
                    hdb++;
                }
            }
            Console.WriteLine($"Ekkor összesen {hdb} óra hiányzás történt.");

            // ha group by, akkor DICTIONARY

            Dictionary<string, int> kimennyithiányzott = new Dictionary<string, int>();

            foreach (Adat adat in lista)
            {
                string nev = adat.veznev + " " + adat.kernev;
                if (kimennyithiányzott.ContainsKey(nev))
                {
                    kimennyithiányzott[nev] += napihianyzas(adat);
                }
                else 
                {
                    kimennyithiányzott[nev] = napihianyzas(adat);
                }
            }

            int maxertek = 0; // egyébként a lista 1. elemét illik ennek venni, de most tudható, hogy az értékek nem lehetnek nullánál kisebbek!
            foreach (string nev in kimennyithiányzott.Keys)
            {
                if (kimennyithiányzott[nev] > maxertek)
                {
                    maxertek = kimennyithiányzott[nev];
                }
            }
            // itt most megvan a maximális elem.

            // kiválogatás következik:

            List<string> kiválogatottak = new List<string>();

            foreach (string nev in kimennyithiányzott.Keys)
            {
                if (kimennyithiányzott[nev] == maxertek)
                {
                    kiválogatottak.Add(nev);
                }
            }

            foreach (string nev in kiválogatottak)
            {
                Console.Write(nev+" ");
            }
            Console.WriteLine();
        }
    }
}
