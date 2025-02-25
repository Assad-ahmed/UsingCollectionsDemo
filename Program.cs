using System;
using System.Collections;
using System.Collections.Generic;

namespace UsageCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            SortedList<string, Etudiant> listeEtudiants = new SortedList<string, Etudiant>();
            float sommeMoyennes = 0;

            Console.Write("Combien d'étudiants voulez-vous enregistrer ? ");
            int nombreEtudiants;
            while (!int.TryParse(Console.ReadLine(), out nombreEtudiants) || nombreEtudiants < 1)
            {
                Console.WriteLine("Veuillez entrer un nombre valide d'étudiants !");
            }

           
            for (int i = 0; i < nombreEtudiants; i++)
            {
                Etudiant etudiant = new Etudiant
                {
                    NO = (i + 1).ToString()
                };

                Console.Write($"Entrez le nom de l'étudiant {etudiant.NO} : ");
                etudiant.Nom = Console.ReadLine();

                Console.Write($"Entrez le prénom de {etudiant.Nom} : ");
                etudiant.Prénom = Console.ReadLine();

                etudiant.NoteCC = LireNote($"Entrez la note de contrôle continu de {etudiant.Nom} : ");
                etudiant.NoteDevoir = LireNote($"Entrez la note de devoir de {etudiant.Nom} : ");

                listeEtudiants.Add(etudiant.NO, etudiant);
                sommeMoyennes += etudiant.CalculerMoyenne();
            }

           
            int lignesParPage = 5;
            Console.Write("Choisissez un nombre de lignes par page (entre 1 et 15) [Entrée pour 5] : ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int valeur) && valeur >= 1 && valeur <= 15)
            {
                lignesParPage = valeur;
            }

            int totalPages = (int)Math.Ceiling((double)nombreEtudiants / lignesParPage);
            int currentPage = 1;
            List<Etudiant> etudiantsList = new List<Etudiant>(listeEtudiants.Values);
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"\nPage {currentPage}/{totalPages} :");

                int start = (currentPage - 1) * lignesParPage;
                int end = Math.Min(start + lignesParPage, nombreEtudiants);

                for (int i = start; i < end; i++)
                {
                    Etudiant etudiant = etudiantsList[i];
                    float moyenne = etudiant.CalculerMoyenne();

                    Console.WriteLine($"NO: {etudiant.NO}, Nom: {etudiant.Nom}, Prénom: {etudiant.Prénom}, " +
                                      $"Note CC: {etudiant.NoteCC}, Note Devoir: {etudiant.NoteDevoir}, Moyenne: {moyenne:F2}");
                }

               
                if (totalPages > 1)
                {
                    Console.WriteLine("\n[S] Page suivante | [D] Page précédente | [W] Quitter");
                    char choix = Console.ReadKey().KeyChar;
                    if (choix == 's' || choix == 'S')
                    {
                        if (currentPage < totalPages) currentPage++;
                    }
                    else if (choix == 'd' || choix == 'D')
                    {
                        if (currentPage > 1) currentPage--;
                    }
                    else if (choix == 'w' || choix == 'W')
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

         
            float moyenneClasse = sommeMoyennes / nombreEtudiants;
            Console.WriteLine($"\nMoyenne générale de la classe : {moyenneClasse:F2}");

            Console.ReadLine();
        }

        static float LireNote(string message)
        {
            float note;
            do
            {
                Console.Write(message);
            } while (!float.TryParse(Console.ReadLine(), out note) || note < 0 || note > 20);
            return note;
        }
    }

}
