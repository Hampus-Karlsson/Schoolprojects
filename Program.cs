using System.Collections;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

bool isruning =true;
List<Unit> units= new ();
List<Repport> repports=new();
List<Call> calls=new();

while (isruning)
{
    Console.WriteLine("----------------------Polisens Rapportsystem 80-----------------------");
    Console.WriteLine("Välj ett av följande:");
    Console.WriteLine("[R]egistrera ny Utryckning\n[N]y rapport\n[P]ersonal\n[I]nformationssammanställning\n[A]vsluta");
    string svar = Console.ReadLine();

    switch (svar)
    {
        case "r"://Registrera
        string temptyp;
        string tempplats;
        int temptid;
        int tempUnits;
        bool go=true;
        Console.Clear();
        while(go)
            {
                
                Console.WriteLine("Ny utryckning eller [A] för att avsluta");
                Console.Write("Typ av utryckning: ");
                Console.WriteLine();
                temptyp=Console.ReadLine();
                 if (temptyp=="a")
                {
                    go=false;
                }
                Console.Write("Plats:");
                tempplats=Console.ReadLine();
                if (tempplats=="a")
                {   
                    go=false;
                }
                Console.WriteLine();
                Console.Write("Agne tid i militär tids format:");
                temptid=int.Parse(Console.ReadLine());
                Console.WriteLine();
                Console.Write("Aktiva enheter:");
                tempUnits=int.Parse(Console.ReadLine());
                Console.WriteLine($" {temptyp}, {tempplats}, {temptid}, {tempUnits}");
                Call newCalls =new Call (temptyp,tempplats,temptid,tempUnits);
                calls.Add(newCalls);
                Console.ReadLine();
                
            }
        break;

        case "n"://ny rapport
        Console.Clear();
        string tempstation;
        string tempdesc;
        int tempnr;
        int tempdate;
        bool runing=true;
        while (runing)
            {
                Console.WriteLine("Skapa ny rapport eller skriv bara [A] för att avsluta");
                Console.Write("Sation: ");
                tempstation=Console.ReadLine();
                Console.WriteLine("Beskrivning: ");
                tempdesc=Console.ReadLine();
                Console.WriteLine("Rapportnummer: ");
                tempnr=int.Parse(Console.ReadLine());
                Console.WriteLine("datum: ");
                tempdate=int.Parse(Console.ReadLine());

                Repport newRepport = new Repport(tempstation,tempdesc,tempnr,tempdate);
                repports.Add(newRepport);
                if (tempstation =="A" || tempdesc =="A")
                {
                    runing=false;
                }
            }   
        break;

        case "p"://personal
        Console.Clear();
        Console.WriteLine("[R]egistrera ny personal\n[V]isa enheter\n[T]illbaka");
        svar=Console.ReadLine();
            if (svar=="r")
            {    
                bool run=true;
                while(run)
                {
                    
                    string temp;
                    int nr;
                    Console.WriteLine("Lägg till enhet eller tryck [A]vbryt för att gå tillbaka till menyn");
                    Console.WriteLine("Agne namn");
                    temp =Console.ReadLine();
                  if(temp=="a")
                    {
                     
                      run=false;
                    
                    }
                  else
                    {
                        Console.WriteLine("Ange tjänstnummer");
                        nr =int.Parse(Console.ReadLine());
                        Unit newUnit=new Unit(temp,nr);
                        units.Add(newUnit);
                        Console.WriteLine("Enhet tillagd.");
                    }
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
        
    }

}
public class Repport
{
    public string Station;
    public string Desc;
    public int Repnr;
    public int Date;
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
    public string Typ;
    public string Plats;
    public int Tid;
    public int Units;

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