using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TEST_JsonFile.Helpers;

namespace TEST_JsonFile.ClassesOperations
{
	internal class AvecJsonNode
	{
		public AvecJsonNode()
		{
			Sortie.Titre("Avec JsonNode");

			Tester();
		}

		private void Tester()
		{
			// Chemin du fichier : au choix.
			//string fichier = Path.Combine(Directory.GetCurrentDirectory(), "Data/Temperature.json");
			string fichier = Path.GetFullPath("Data/Temperature.json");
			string texte = Fichier.LireFlux(fichier);

			// https://docs.microsoft.com/fr-fr/dotnet/standard/serialization/system-text-json-use-dom-utf8jsonreader-utf8jsonwriter

			// S'il faut lire les données et les modifier, JsonNode est adéquat.

			// Convertir en JsonNode
			JsonNode forecastNode = JsonNode.Parse(texte);

			Console.WriteLine("\nLecture directe d'une donnée du JsonNode");
			JsonNode temperatureNode = forecastNode["Temperature"];
			Console.WriteLine($"\t{temperatureNode.ToJsonString()}"); 
			// 25

			Console.WriteLine("\nLecture directe d'une donnée de type objet");
			JsonNode temperatureRanges = forecastNode["TemperatureRanges"];
			Console.WriteLine($"\t{temperatureRanges.ToJsonString()}");
			// {"Cold":{"High":20,"Low":-10},"Hot":{"High":60,"Low":20}}

			Console.WriteLine("\nLecture directe d'une donnée de type tableau");
			JsonNode datesAvailable = forecastNode["DatesAvailable"];
			Console.WriteLine($"\t{datesAvailable.ToJsonString()}");
			// ["2019-08-01T00:00:00","2019-08-02T00:00:00"]

			// Le tableau est renseigné en tant que JsonNode.
			// On peut l'explorer.
			Console.WriteLine("\nLecture à l'index 0 du tableau JsonNode");
			JsonNode entree0 = datesAvailable[0];
			Console.WriteLine($"\t{entree0}");
			// 2019-08-01T00:00:00

			Console.WriteLine("\nExploration d'un objet par chaînage de propriétés");
			int coldHighTemperature = (int)temperatureRanges["Cold"]["High"];
			Console.WriteLine($"\t{coldHighTemperature}");
			// 20

			Console.WriteLine("\nLecture d'une valeur convertie en son type attendu");
			int temperatureInt = (int)forecastNode["Temperature"];
			Console.WriteLine($"\t{temperatureInt} (int)");
			// 25 (int)

			Console.WriteLine("\nLecture avec GetValue<T>()");
			temperatureInt = forecastNode["Temperature"].GetValue<int>();
			Console.WriteLine($"\t{temperatureInt} (int)");
			// 25(int)

			Console.WriteLine("\nLecture avec GetValue<T>() en explorant un tableau");
			JsonNode firstDate = datesAvailable[0].GetValue<DateTime>();
			Console.WriteLine($"\tPremière date = {firstDate}");
			// Première date = "2019-08-01T00:00:00"
		}
	}
}
