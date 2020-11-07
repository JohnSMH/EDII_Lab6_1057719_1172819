using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
namespace Lab1.Models
{
    public class Datos
    {
        public string Word { get; set; }
        public int Levels { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        public Datos(string word, int levels, int rows, int columns)
        {
            Word = word;
            Levels = levels;
            Rows = rows;
            Columns = columns;
        }
        public Datos() { }


    }
}
