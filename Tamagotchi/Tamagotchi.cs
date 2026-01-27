//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.ComponentModel.Design;
//using System.Linq;
//using System.Reflection.Metadata.Ecma335;
//using System.Security.Cryptography.X509Certificates;
//using System.Text;
//using System.Threading.Tasks;

//namespace Modul24.Test
//{
//    internal class Tamagotchi
//    {
//        // Stats des Tamagotchis
//        public string Name;
//        private int Lebenspunkte = 100;
//        private int Hunger = 50;
//        private int Freude = 40;
//        private int Müdigkeit = 100;
//        private bool alive = true;
//        private string currentAction = "";
//        public int Playtime = 0; // War angedacht als ansatz fürs Alter

//        public Tamagotchi(string name)
//        {
//            Name = name;
//        }
//        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
//        // Hier wird geprüft ob der Tamagotchi lebt
//        public void Death()
//        {
//            if (Lebenspunkte <= 0 && alive)
//            {
//                alive = false;
//                Console.ForegroundColor = ConsoleColor.DarkRed;
//                Console.WriteLine($"{Name} ist gestorben...");
//                Console.ResetColor();
//            }
//        }
//        // Gibt Feedback an alive das er noch lebt oder halt nicht
//        public bool IsAlive()
//        {
//            Death();
//            return alive;
//        }

//        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
//        public void Heal()
//        {
//            if(!alive) return;
//            currentAction = "healing";

//            Lebenspunkte += 20;
//            if (Lebenspunkte >= 100) Lebenspunkte = 100;

//            DisplayStatus();
//            Thread.Sleep(1000);
//            currentAction = "";
//        }
//        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
//        // Bestrafung 
//        public void Punish()
//        {
//            currentAction = "punish";

//            Freude -= 20;
//            Lebenspunkte -= 10;
//            Müdigkeit -= 15;

//            DisplayStatus();
//            Thread.Sleep(1000);
//            currentAction = "";
//        }

//        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
//        // Hier ist die Fütterungsfunktion
//        public void IsHangry(Food food)
//        {
//            if (!alive) return;
            
//            currentAction = "eating";                                   // Status für den essenden Hasen
//                                                                        // Verhindert das Hunger und Freude über 100 geht 
//            Hunger += food.Points;
//            if (Hunger >= 100) Hunger = 100;

//            Freude += food.Freude;
//            if (Freude >= 100) Freude = 100;
            
//            Console.SetCursorPosition(0, 18);
//            Console.WriteLine($"{Name} hat {food.Name} gegessen.                                ");
//            DisplayStatus();                                            // ruft direkt das Update mit dem Essensgesicht auf
//            Thread.Sleep(2000);                                         // einfache Pause
//            currentAction = "";
                                                     
//        }
//        // Verhindert das Hunger ins Minus läuft damit kein OutOfBounce passiert
//        public void HungryNull(int hunger)
//        {
//            Hunger = Math.Max(hunger, 0);
//        }

//        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
//        // Hier wird das schlafen geregelt damit sich Müdigkeit füllt
//        public void IsSleepy()
//        {
//                                                                        // Checkt ob der Tamagotchi lebt
//            if (!alive) return;
//            currentAction = "sleeping";
//                                                                        // Eigentliche Schlaffunktion
//            Console.SetCursorPosition(0, 18);
//            Console.WriteLine($"{Name} schläft...                                               ");
//            Thread.Sleep(3000);
//            Müdigkeit = 100;
//            Freude += 25;
//            if (Freude > 100) Freude = 100;
//            Console.SetCursorPosition(0, 18);
//            Console.WriteLine($"{Name} ist ausgeruht.");
//            DisplayStatus();                                            // ruft direkt das Update mit dem Essensgesicht auf
//            Thread.Sleep(2000);
//            currentAction = "";
//        }
//        // Verhindert das Müdigkeit ins Minus läuft damit kein OutOfBounce passiert
//        public void MüdiNull(int müde)
//        {
//            Müdigkeit = Math.Max(müde, 0);
//        }

//        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
//        // Hier wird das Spielen geregelt damit die Freude steigt.
//        public void IsHappy()
//        {
//            if (!alive) return;
//            currentAction = "playing";

//            Console.SetCursorPosition(0, 18);
//            Console.WriteLine($"{Name} spielt fröhlich.                                         ");

//            Freude += 30;
//            if (Freude > 100) Freude = 100;

//            Müdigkeit -= 5;
//            if (Müdigkeit < 0) Müdigkeit = 0;

//            DisplayStatus();
//            Thread.Sleep(2000);
//            currentAction = "";
//        }
//        // Verhindert das Freude ins Minus läuft damit kein OutOfBounce passiert
//        public void HappyNull(int happy)
//        {
//            Freude = Math.Max(happy, 0);
//        }

//        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
//        // Hier wird das Spiel "geupdatet" zieht Punkte ab etc
//        public void Update()
//        {
//            if (!alive) return;

//            Hunger -= 5;
//            Müdigkeit -= 3;
//            Freude -= 3;
//            HungryNull(Hunger);
//            MüdiNull(Müdigkeit);
//            HappyNull(Freude);


//            if (Hunger <= 0 || Müdigkeit <= 0 || Freude <= 0)
//            {
//                Lebenspunkte -= 15;
//                if (Lebenspunkte < 0) Lebenspunkte = 0;
//            }
//            Death();
//        }

//        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
//        // Hier werden die Balken auf die Konsole gebracht die ,3 hängt hier mit Bar() zusammen als Wert der abgezogen wird
//        public void DisplayStatus()
//        {
//            Console.SetCursorPosition(0, 3);
//            Console.WriteLine($"TAMAGOTCHI: {Name}");
//            Console.WriteLine(GetFace());
//            Console.WriteLine("---------------------------------");
//            Console.ForegroundColor = ConsoleColor.Green;
//            Console.WriteLine($"Leben:      {Lebenspunkte,3} {Bar(Lebenspunkte)}");
//            Console.ResetColor();
//            Console.ForegroundColor = ConsoleColor.Red;
//            Console.WriteLine($"Hunger:     {Hunger,3} {Bar(Hunger)}");
//            Console.ResetColor();
//            Console.ForegroundColor = ConsoleColor.Magenta;
//            Console.WriteLine($"Freude:     {Freude,3} {Bar(Freude)}");
//            Console.ResetColor();
//            Console.ForegroundColor = ConsoleColor.Blue;
//            Console.WriteLine($"Müdigkeit:  {Müdigkeit,3} {Bar(Müdigkeit)}");
//            Console.ResetColor();
//            Console.WriteLine("---------------------------------");
//            WriteCleanLine();
//            Console.WriteLine($"Stimmung: {GetMood()}");
//            Console.WriteLine();
//            Console.ResetColor();
//        }
//        //Erzeugt einfach eine Leerzeile in der Konsole für die Optik
//        private void WriteCleanLine()
//        {
//            Console.Write(new string(' ', Console.WindowWidth));
//            Console.SetCursorPosition(0, Console.CursorTop );
//        }

//        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
//        // Gemütszustand wird hier geregelt ab welchen Wert welche Stimmung angezeigt werden soll
//        // Ausgabe: Stimmung : Müde z.B
//        private string GetMood()
//        {
//            if (!alive) return "Tot";
//            if (Lebenspunkte < 30) return "Sehr schwach";
//            if (Hunger < 30) return "Hungrig";
//            if (Müdigkeit < 30) return  "Müde";
//            if (Freude < 20) return "Langweilig";
//            return "Glücklich";
//        }

//        // Unterschiedliche Gesichter passend zur Gemütslage des Tamagotchis
//        private string GetFace()
//        {
//            if (!alive)
//                return @"
//              (\_/) 
//              (xᴥx)                                         
//             (  -  )      
//             /O\ /O\";

//            if (Lebenspunkte < 30)
//                return @"
//              (\_/) 
//              (*ᴥ*)                                     
//             (U - U)      
//             /O\ /O\";
            
//            if (currentAction == "eating")
//                return @"
//              (\_/) 
//              (^ᴥ^)                                 
//             (=>Ü<=)      
//             /O\ /O\";

//            if (Hunger < 30)
//                return @"
//              (\_/) 
//              (O_O)         Ich hab Hunger!
//             (U s U)      
//             /O\ /O\";
            
//            if (currentAction == "punish")
//                return @"
//              (\_/) 
//              (ˣᴥˣ) 
//             (Z z Z)      
//             /O\ /O\";

//            if (Müdigkeit < 30)
//                return @"
//              (\_/) 
//              (=_=)         Ich bin Müde!
//             (U - U)      
//             /O\ /O\";

//            if (currentAction == "sleeping")
//                return @"
//              (\_/) 
//             ( -ᴥ- )         zZzZzZzZ           
//            ((u)_(u))      
//                    ";

//            if (Freude < 20)
//                return @"
//              (\_/) 
//              (●ᴥ●)         Laangweilig!
//             (U - U)      
//             /O\ /O\";

//            if (currentAction == "playing")
//                return @"
//              (\_/) 
//              (^ᴥ^)
//             ( Ƿ-  Ƿ   Օ
//             /O\ /O\";      

//            if (currentAction == "healing")
//                return @"
//              (\_/)+
//            + (+ᴥ+)   +                                                              
//             (U + U)+      
//             /O\ /O\";

//            if (GetMood() == "Glücklich")
//                return @" 
//              (\_/) 
//              (^ᴥ^)                                     
//             (U - U)      
//             /O\ /O\";

//            // Standard glücklich
//            return @"
//              (\_/) 
//              (^ᴥ^)                                     
//             (U - U)      
//             /O\ /O\";

//        }
//        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
//        public void Ereignisse()
//        {
//            Random rng = new Random();
//            int zufall = rng.Next(0,100);

            
//            switch (zufall)
//            {
//                case 17:
//                    Console.SetCursorPosition(0, 2);
//                    Console.WriteLine($"{Name} hat sich den kleinen Zeh gestoßen.       ");
//                    Lebenspunkte -= 10;
//                    break;
//                case 52:
//                    Console.SetCursorPosition(0, 2);
//                    Console.WriteLine($"{Name} hat sich verschluckt.                    ");
//                    Lebenspunkte -= 5;
//                    break;
//                case 0:
//                    Console.SetCursorPosition(0, 2);
//                    Console.WriteLine($"{Name} hat eine Funk Doku geschaut!             ");
//                    Lebenspunkte -= 99;
//                    break;
//                case 21:
//                    Console.SetCursorPosition(0, 2);
//                    Console.WriteLine($"{Name} GEZ Rechnung ist angekommen!             ");
//                    Lebenspunkte -= 30;
//                    break;

//            }
//        }

//        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
//        // Anzeige Balken für LP, Müdigkeit, Freude und Hunger. 
//        // filled wird anhand Prozentrechnung berechnet und dargestellt
//        // OutpudEncoding nötig für die "Anzeige"
//        private string Bar(int value)
//        {
//            Console.OutputEncoding = System.Text.Encoding.UTF8;
//            int barLength = 20;
//            int filled = value * barLength / 100;
//            filled = Math.Clamp(filled, 0, barLength);
//            return "|" + new string('\u2588', filled) + new string('\u2591', barLength - filled) + "|";
//        }

//        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
        
//    }

//    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//    // Hier wird das essen geregelt name, points also wie viel das jeweilige essen an Hunger füllen soll
//    // und freude das nach dem essen gefüllt wird
//    internal class Food
//    {
//        public string Name;
//        public int Points;
//        public int Freude;

//        public Food(string name, int points, int freude)
//        {
//            Name = name;
//            Points = points;
//            Freude = freude;
//        }
//        // Hier kann weiteres Essen hinzugefügt werden muss aber unten angepasst werden im switch und in der Liste
//        public static Food Doener() => new Food("Döner", 60, 10);

//    }

//    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//    internal class Gameloop // Automatische Loop wiederholt sich im eingestellten abstand jedes mal
//    {
//        static void Main(string[] args)
//        {
//            Console.SetWindowSize(100, 30);
//            Console.OutputEncoding = System.Text.Encoding.UTF8;
//            Console.WriteLine("Wie soll dein Tamagotchi heißen?");
//            Tamagotchi kim = new Tamagotchi(Console.ReadLine());
//            Console.Clear();
//            bool running = true;
            
//            Console.WriteLine($"Ein neuer Diktator Namens {kim.Name} ist geboren!");
//            Thread.Sleep(1000);

//            //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
//            Thread updateThread = new Thread(() =>                  // Erstellt ein neues "Nebenprogramm"
//            {                                                       // Somit aktualisiert das Programm und User kann gleichzeitig Eingaben machen.  
//                while (kim.IsAlive())
//                {
//                    kim.Update();                                   // Update wird erneuert
//                    kim.Ereignisse();
//                    kim.DisplayStatus();                            // Displaystatus wird erneuert
//                    Thread.Sleep(2000);                             // Alle 2 Sekunden entsteht ein neues update
//                }
//            });

//            updateThread.IsBackground = true;                       // Wenn Hauptprogramm beendet wird darf Thread beendet werden.
//            updateThread.Start();                                   // Startet das Nebenprogramm 

//            //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
//            while (running && kim.IsAlive())
//            {   
//                                                                    // Anzeige für den Spieler welche Möglichkeiten er hat
//                Console.SetCursorPosition(0, 22);
//                Console.WriteLine("Was möchtest du tun?");
//                Console.WriteLine("1 - Füttern (Döner)");
//                Console.WriteLine("2 - Spielen");
//                Console.WriteLine("3 - Schlafen lassen");
//                Console.WriteLine("4 - Bestrafen");
//                Console.WriteLine("5 - Heilen");
//                Console.WriteLine("6 - Beenden");
//                Console.WriteLine(">> ");

//                char eingabe = Console.ReadKey().KeyChar;           // KeyChar lässt nur eine Eingabe zu
//                //Console.WriteLine();
                
//                                                                    // switch für die einzelnen Funktionen
//                switch (eingabe)
//                {
//                    case '1': kim.IsHangry(Food.Doener()); break;
//                    case '2': kim.IsHappy(); break;
//                    case '3': kim.IsSleepy(); break;
//                    case '4':
//                        kim.Punish();
//                        Console.SetCursorPosition(0, 18);
//                        Console.WriteLine($"Du hast {kim.Name} geschockt mit dem Schockhalsband."); break;
//                    case '5':
//                        kim.Heal();
//                        Console.SetCursorPosition(0, 18);
//                        Console.WriteLine($"Du hast {kim.Name} wunden behandelt."); 
//                        break;
//                    case '6':
//                        running = false;
//                        Console.SetCursorPosition(0, 18);
//                        Console.WriteLine($"{kim.Name} ist gestorben du Monster!");
//                        break;
//                    default:
//                        Console.SetCursorPosition(0, 18);
//                        Console.WriteLine("Ungültige Eingabe!");
//                        break;
//                }
//            }

//        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
//        Console.Clear();
//        Console.WriteLine("GAME OVER!");
//        Console.WriteLine("....................../´¯/) \r\n....................,/¯../ \r\n.................../..../ \r\n............./´¯/'...'/´¯¯`·¸ \r\n........../'/.../..../......./¨¯\\ " +
//            "\r\n........('(...´...´.... ¯~/'...') \r\n.........\\.................'...../ \r\n..........''...\\.......... _.·´ " +
//            "\r\n............\\..............( \r\n..............\\.............\\...");
//        }
//    }
//}
