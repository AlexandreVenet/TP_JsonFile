using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using TEST_JsonFile.ClassesData;
using TEST_JsonFile.Helpers;

namespace TEST_JsonFile.ClassesOperations
{
	internal class AvecJsonSerializer
	{
		public AvecJsonSerializer()
		{
			Tester();
			TesterTableau();
			TesterListe();
			TesterDictionnaire();
			LirerEtEcrire();
		}

		private void Tester()
		{
			Sortie.Titre("Un modèle de données");

			string fichier = Fichier.CheminFichierData("Vrac.json");
			string texte = File.ReadAllText(fichier);

			Console.WriteLine("\nDésérialisation");

			// Configuration du désérialiseur
			JsonSerializerOptions options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
				WriteIndented = true,
				Encoder = JavaScriptEncoder.Create(
					UnicodeRanges.BasicLatin,       // Caractères ASCII de base
					UnicodeRanges.Latin1Supplement, // Caractères spéciaux (é, ç...)
					UnicodeRanges.LatinExtendedA    // Autres caractères latins 
				),
			};

			// Désérialiser
			Vrac vrac = JsonSerializer.Deserialize<Vrac>(texte, options);
			
			Console.WriteLine(vrac.NombreBizarre);
			// 3.2

			foreach (var elt in vrac.Tableau)
			{
				Console.WriteLine($"{elt} ({elt?.GetType().Name ?? "null"})");
			}
			/*
				25.5 (JsonElement)
				gné ! (JsonElement)
				True (JsonElement)
				 (null)
			*/

			Console.WriteLine("\nSérialisation");

			// On sérialise avec la même configuration
			string vracJson = JsonSerializer.Serialize(vrac, options);
			Console.WriteLine(vracJson);
			/*
				{
				  "nombre-bizarre": 3.2,
				  "tableau": [
					25.5,
					"gné !",
					true,
					null
				  ]
				}
			*/
		}

		private void TesterTableau()
		{
			Sortie.Titre("\nTableau d'objets Temps");

			string fichier = Fichier.CheminFichierData("Temps.json");
			string texte = File.ReadAllText(fichier);

			Temps[] items = JsonSerializer.Deserialize<Temps[]>(texte);
			Console.WriteLine(items[0]);
			Console.WriteLine(items[1]);
			Console.WriteLine(items.GetType());
			// 08/08/2018 00:00:00, -1, Youpi
			// 09/09/2019 11:00:00, 1234, Dji est là !
			// TEST_JsonFile.ClassesData.Temps[]
		}

		private void TesterListe()
		{
			Sortie.Titre("\nListe d'objets Temps");

			string fichier = Fichier.CheminFichierData("Temps.json");
			string texte = File.ReadAllText(fichier);

			List<Temps> itemsList = JsonSerializer.Deserialize<List<Temps>>(texte);
			Console.WriteLine(itemsList[0]);
			Console.WriteLine(itemsList[1]);
			Console.WriteLine(itemsList.GetType());
			// 08/08/2018 00:00:00, -1, Youpi
			// 09/09/2019 11:00:00, 1234, Dji est là !
			// System.Collections.Generic.List`1[TEST_JsonFile.ClassesData.Temps]
		}

		private void TesterDictionnaire()
		{
			Sortie.Titre("\nDictionnaire string-Personne");

			// Maintenant, on traite un JSON représentant un objet contenant des objets nommés.
			// On veut convertir cela en dictionnaire :
			// - clé : le nom de l'objet nommé,
			// - valeur : l'objet correspondant.

			string fichier = Fichier.CheminFichierData("Personne.json");
			string texte = File.ReadAllText(fichier);

			Dictionary<string, Personne> personnes = JsonSerializer.Deserialize<Dictionary<string, Personne>>(texte);

			foreach (KeyValuePair<string, Personne> item in personnes)
			{
				Console.WriteLine($"clé : {item.Key} / valeur : {item.Value}");
			}
			// clé : Toto / valeur : Toto, 0
			// clé : Jean / valeur : Jean, 10
			// clé : Zouzou / valeur : Je ne m'appelle pas Zouzou., -999
			Console.WriteLine(personnes["Zouzou"].Nom);
			// Je ne m'appelle pas Zouzou.
		}

		private void LirerEtEcrire()
		{
			Sortie.Titre("\nLire et écrire");

			string fichier = Fichier.CheminFichierData("LireEcrire.json");

			Console.WriteLine("\nLire les données");
			string texte = Fichier.LireFlux(fichier);
			LireEcrire obj = JsonSerializer.Deserialize<LireEcrire>(texte);
			Console.WriteLine($"{obj.NombreEntier} ({obj.NombreEntier.GetType().Name})");
			Console.WriteLine($"{obj.Texte} ({obj.Texte.GetType().Name})");
			Console.WriteLine($"{obj.OK} ({obj.OK.GetType().Name})");
			// 2 (Int32)
			// Coucou, c'est l'été ! (String)
			// True (Boolean)

			Console.WriteLine("\nModifier les valeurs");
			obj.NombreEntier = 42;
			obj.Texte = "Modifié !";
			obj.OK = false;
			Console.WriteLine("OK");

			Console.WriteLine("\nEnregistrer le fichier");
			string contenuAEnregistrer = JsonSerializer.Serialize(obj);
			Fichier.EcrireFlux(fichier, contenuAEnregistrer);
			Console.WriteLine("OK");

			Console.WriteLine("\nLire pour vérification");
			texte = Fichier.LireFlux(fichier);
			obj = JsonSerializer.Deserialize<LireEcrire>(texte);
			Console.WriteLine($"{obj.NombreEntier} ({obj.NombreEntier.GetType().Name})");
			Console.WriteLine($"{obj.Texte} ({obj.Texte.GetType().Name})");
			Console.WriteLine($"{obj.OK} ({obj.OK.GetType().Name})");
			// 42 (Int32)
			// Modifié ! (String)
			// False (Boolean)
			Console.WriteLine("Contenu texte du fichier :");
			Console.WriteLine(texte);
			// {"nombre-entier":42,"texte":"Modifi\u00E9 !","ok":false}
		}
	}
}
