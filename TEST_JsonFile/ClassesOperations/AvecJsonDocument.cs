using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TEST_JsonFile.Helpers;

namespace TEST_JsonFile.ClassesOperations
{
	internal class AvecJsonDocument
	{
		public AvecJsonDocument()
		{
			Sortie.Titre("Avec JsonDocument\n");

			Tester();
		}

		private void Tester()
		{
			string fichier = Fichier.CheminFichierData("Vrac.json");
			string texte = File.ReadAllText(fichier);

			// S'il faut seulement lire les données, JsonDocument est adéquat car orienté lecture seule.

			// Convertir
			// Ici, les annotations dans Vrac.cs ne sont pas utilisées.
			JsonDocument json = JsonDocument.Parse(texte);

			if (json == null)
			{
				Console.WriteLine("Erreur de conversion en JsonDocument.");
				return;
			}

			// L'élément racine du document JSON
			JsonElement racine = json.RootElement;

			// Obtenir une valeur
			if (racine.TryGetProperty("nombre-bizarre", out JsonElement valeur))
			{
				Console.WriteLine($"nombre-bizarre : {valeur}");
			}
			// Temperature : 25

			// Obtenir un tableau de valeurs
			Console.WriteLine("\nContenu du tableau : ");
			JsonElement tableau = racine.GetProperty("tableau");
			foreach (JsonElement item in tableau.EnumerateArray())
			{
				Console.WriteLine($"- {item} ({item.ValueKind})");
			}
			/*
				Contenu du tableau :
				- 25.5 (Number)
				- gné ! (String)
				- True (True)
				-  (Null)
			*/

			// Détail : https://www.codemag.com/Article/2405031/Manipulating-JSON-Documents-in-.NET-8
		}
	}
}
