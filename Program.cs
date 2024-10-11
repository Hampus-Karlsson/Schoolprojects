using System.Collections;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.IO;
bool isruning =true;
List<Unit> units= new ();
List<Repport> repports=new();
List<Call> calls=new();
while (isruning)
{
    Console.WriteLine("----------------------Polisens Rapportsystem 80-----------------------");
    Console.WriteLine("Välj ett av följande:");
    Console.WriteLine("[R]egistrera ny Utryckning\n[N]y rapport\n[P]ersonal\n[I]nformationssammanställning\n[A]vsluta");
    string? svar = Console.ReadLine();
    if(string.IsNullOrWhiteSpace(svar))
    {
        Console.WriteLine("Felakting inmatning, försök igen");
        continue;
    }
    switch (svar.ToLower())
    {
        case "r"://Registrera
        string? temptyp;
        string? tempplats;
        int temptid;
        int tempUnits;
        Console.Clear();
        while(true)
        {
            Console.WriteLine("Ny utryckning eller [A] för att avsluta");
            Console.Write("Typ av utryckning: ");
            Console.WriteLine();
            temptyp=Console.ReadLine();
            if(string.IsNullOrWhiteSpace(temptyp))
            {
                Console.WriteLine("Felakting inmatning, försök igen");
                continue;
            }
            if (temptyp.Equals("A", StringComparison.OrdinalIgnoreCase))
            {
                
                break;
            }
            Console.Write("Plats:");
            tempplats =Console.ReadLine();
            if (string.IsNullOrWhiteSpace(tempplats))//validering av inmatning
            {
                Console.WriteLine("Felakting inmatning, försök igen");
                continue;
            }
            if (tempplats=="a")
            {   
                break;
            }
            Console.WriteLine();
            Console.Write("Agne tid i militär tids format:");
            if(!int.TryParse(Console.ReadLine(), out temptid) || temptid < 0 || temptid>2359)//validering av tid
            {
                Console.WriteLine("Felaktig inmatning, försök igen");
            }
            Console.WriteLine();
            Console.Write("Aktiva enheter:");
            if(!int.TryParse(Console.ReadLine(),out tempUnits)|| tempUnits<0)
            {
                Console.WriteLine("Felaktigt enhetsnummer, försök igen.");
                continue;
            }
            Console.WriteLine($" Uttryckning registrerad: {temptyp}, {tempplats}, {temptid}, {tempUnits}");
            Call newCalls =new Call (temptyp,tempplats,temptid,tempUnits);
            calls.Add(newCalls);
            Console.ReadLine();  
        }
        break;
        case "n"://ny rapport
        Console.Clear();
        string? tempstation;
        string? tempdesc;
        int tempnr;
        int tempdate;
        while (true)
            {
                Console.WriteLine("Skapa ny rapport eller skriv bara [A] för att avsluta");
                Console.Write("Sation: ");
                tempstation=Console.ReadLine();
                if (string.IsNullOrWhiteSpace(tempstation))
                {
                 Console.WriteLine("Inmatningen kan inte vara tom. Försök igen.");
                continue;
                }
                if (tempstation=="A"|| tempstation == "a")
                {
                    break;
                }
                Console.WriteLine("Beskrivning: ");
                tempdesc=Console.ReadLine();
                if (string.IsNullOrWhiteSpace(tempdesc))
                {
                    Console.WriteLine("Inmatningen kan inte vara tom. Försök igen.");
                    continue;
                }
                if (tempdesc =="A" || tempdesc =="a")
                {
                    break;
                }
                Console.WriteLine("Rapportnummer: ");
                if(!int.TryParse(Console.ReadLine(), out tempnr) || tempnr<0)
                {
                    Console.WriteLine("Felaktigt rapportnummer,försök igen.");
                    continue;
                }
                Console.WriteLine("datum (ÅÅÅMMDD): ");
                if(!int.TryParse(Console.ReadLine(),out tempdate)|| tempdate<0)
                {   
                    Console.WriteLine("Felaktigt datum, försök igen");
                    continue;
                }
                Repport newRepport = new Repport(tempstation,tempdesc,tempnr,tempdate);
                repports.Add(newRepport);
                System.Console.WriteLine("Ny Rapport tillagd.");
            }   
        break;
        case "p"://personal
         Console.Clear();
         Console.WriteLine("[R]egistrera ny personal\n[V]isa enheter\n[T]illbaka");
         svar=Console.ReadLine();
            if (svar=="r")
            {    
                while(true)
                {
                    string? temp;
                    int nr;
                    Console.WriteLine("Lägg till enhet eller tryck [A]vbryt för att gå tillbaka till menyn");
                    Console.WriteLine("Agne namn");
                    temp =Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(temp))
                    {
                        Console.WriteLine("Inmatningen kan inte vara tom. Försök igen.");
                        continue;
                    }
                    if(temp=="a")
                    {
                      break;       
                    }
                        Console.WriteLine("Ange tjänstnummer eller [0] för att avbryta");
                    if(!int.TryParse(Console.ReadLine(), out nr) || nr <0)
                    {
                        Console.WriteLine("Felaktigt tjänstnummer, försök igen.");
                    }
                    if (nr==0)
                    {
                        break;
                    }
                        Unit newUnit=new Unit(temp,nr);
                        units.Add(newUnit);
                        Console.WriteLine("Enhet tillagd.");
                }
            }
            else if (svar=="v")
            {
                units.Sort((x, y) => x.Namn.CompareTo(y.Namn));
                foreach(Unit i in units)
                {
                //printa sorterad lista
                i.Print();  
                continue;
                }
            }
            else if (svar=="t")
            {
                continue;
            }
        break;
        case "i"://infosamman
        Console.Clear();
        Console.WriteLine("[U]ttryckningar\n[P]ersonal\n[R]apporter");
        svar=Console.ReadLine();
            if (svar=="u")
            {
                foreach(Call i in calls)
                {
                    i.Print();
                }
                //lista alla uttryckningar
            }
            else if (svar=="p")
            {   
                //lista personal
                units.Sort((x, y) => x.Namn.CompareTo(y.Namn));
                foreach(Unit i in units)
                {
                    i.Print();
                }
            }
            else if (svar=="r")
            {
                foreach(Repport i in repports)
                {
                    i.Print();
                }
                //Lista alla rapporter
            }
        break;
            case "a"://avsluta
            isruning=false;
            break; 

        default:
        Console.WriteLine("Ogiltigt val, försök igen");
        break;

    }
}
public class Repport
{
    private string Station;
    private string Desc;
    private int Repnr;
    private int Date;
        public Repport(string station, string desc, int repnr, int date)
        {
            this.Station=station;
            this.Desc=desc;
            this.Repnr=repnr;
            this.Date=date;
        }
        public void Print()
        {
            Console.WriteLine($"Station:{Station}\nBeskrivning:{Desc}\nRapportnummer:{Repnr}\nDatum:{Date}");
        }
}
public class Unit
{
 public string Namn;
 public int Tjänstenummer;
    public Unit(string namn,int tjänstenummer)
    {
        this.Namn=namn;
        this.Tjänstenummer=tjänstenummer;
    }
    public void Print()
    {
        Console.WriteLine($"Namn:{Namn}, Tjänstenummer:{Tjänstenummer}");
    }   
}
public class Call   
{
    private string Typ;
    private string Plats;
    private int Tid;
    private int Units;
        public Call(string typ,string plats, int tid, int units)
        {
            this.Typ=typ;
            this.Plats=plats;
            this.Tid=tid;
            this.Units=units;
        }
        public void Print()
        {
            Console.WriteLine($"Typ av utryckning:{Typ}\nPlats:{Plats}\nTid:{Tid}\nEnhter:{Units}");
        }
}