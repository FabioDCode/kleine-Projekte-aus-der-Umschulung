using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Modul25.Uebungen
{
    internal class SortToPattern
    {
        // Ermöglicht die eingabe eines Ordners auf dem Desktop. Danach kann man eine Regex Pattern eingeben nach der in 
        // Dateien gesucht wird. Ausgabe zeigt dann an welche Daten das Pattern beinhalten.
        static void Main(string[] args)
        {
            Console.Clear();
            // Name des Ordners auf dem Desktop wird angefragt
            System.Console.WriteLine("Geb den Namen des Verzeichnis an welches auf dem DESKTOP liegt: ");
            string uinput = Console.ReadLine().ToUpper();
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            // Zeigt den aktuelle Pfad an
            string dirPath = Path.Combine(desktop, uinput);
            System.Console.WriteLine(TimeOnly.FromDateTime(DateTime.Now).ToString("HH:mm:ss ") + dirPath);

            string[] openFiles = Directory.GetFiles(dirPath);

            // Regex Pattern wird entgegen genommen
            System.Console.WriteLine("Wonach soll gesucht werden bitte geb ein Regex Pattern an!\n");
            string? pattern = Console.ReadLine();

            // Dateiinfos laden und sortieren
            var sortedFiles = openFiles
                .Select(f => new 
                { 
                    File = f, 
                    Lines = File.ReadAllLines(f),
                    ContainsName = File.ReadAllLines(f).Any(line => Regex.IsMatch(line, pattern))
                })
                .OrderByDescending(f => f.ContainsName) // zuerst mit Name, dann ohne
                .ToList();

            // Ausgabe'
            foreach (var file in sortedFiles)
            {
                Console.WriteLine("==============================================");
                Console.WriteLine($"Datei: {Path.GetFileName(file.File)}");
                Console.WriteLine($"Enthält {pattern}: {file.ContainsName}");
                Console.WriteLine("==============================================");

                // Treffer anzeigen
                Console.WriteLine($"\nZeilen mit '{pattern}':");

                var nameLines = file.Lines
                    .Where(l => Regex.IsMatch(l, pattern))
                    .ToList();

                if (nameLines.Count == 0)
                    Console.WriteLine("(Keine Treffer)");
                else
                    nameLines.ForEach(l => Console.WriteLine($"=> {l}"));
                
                // Dateiinhalt anzeigen
                Console.WriteLine("\nDatei-Inhalt:");
                Console.WriteLine(string.Join(Environment.NewLine, file.Lines));

                Console.WriteLine("\n\n");
            }
        }
    }
}