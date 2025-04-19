using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using APICsv.Database.Models;

namespace APICsv.Database
{
    public class DBContext
    {

        private const string Pathname =
            "C:\\Users\\floss\\OneDrive\\Área de Trabalho\\PARADIGMAS DE LINGUAGENS\\projetos\\API.net\\APICsv\\animais.txt";

        private readonly List<Animal> _animais = new();
        

        public DBContext() 
        { 
           string[] lines =
                File.ReadAllLines(Pathname);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] coluns =
                lines[i].Split(';');        

                Animal animal = new ();
                animal.Id = int.Parse(coluns[0]);
                animal.Name = coluns[1];
                animal.Classification = coluns[2];
                animal.Origin = coluns[3];
                animal.Reproduction = coluns[4];
                animal.Feeding = coluns[5];

                _animais.Add(animal);
            }
        } //propriedade
        public List<Animal> Animals => _animais;
    }
    
}
