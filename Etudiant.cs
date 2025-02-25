using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UsageCollections
{
    public class Etudiant
    {
        public string NO { get; set; }   
        public string Nom { get; set; }
        public string Prénom { get; set; }
        public float NoteCC { get; set; }
        public float NoteDevoir { get; set; }

        public float CalculerMoyenne()
        {
            return (NoteCC * 0.33f) + (NoteDevoir * 0.67f);
        }
    }
}
