using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TEST_JsonFile.Helpers
{
	internal class Fichier
	{
		static private string _dossierData = "Data";

		static public string CheminFichierData(string nomFichier)
		{
			return Path.Combine(Directory.GetCurrentDirectory(), _dossierData, nomFichier);
		}

		static public string LireFlux(string chemin)
		{
			string texte = null;

			// Lire un flux de données au chemin fourni
			using (StreamReader lecteur = new(chemin))
			//using (StreamReader lecteur = File.OpenText(chemin)) // autre façon de faire
			{
				texte = lecteur.ReadToEnd();
			}

			return texte;
		}

		static public void EcrireFlux(string chemin, string contenu)
		{
			using (StreamWriter ecriveur = new (chemin))
			{
				ecriveur.Write(contenu);
			}
		}
	}
}
