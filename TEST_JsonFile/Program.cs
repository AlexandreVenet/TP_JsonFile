// See https://aka.ms/new-console-template for more information

using System.Text.Json.Nodes;
using TEST_APICall.ClassesData;
using System.Text.Json;

#region Début programme

Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine(new String('-',50));
Console.WriteLine("Traitement JSON local");
Console.WriteLine(new String('-', 50));
Console.WriteLine();
Console.ResetColor();

#endregion

// --------------------------------



#region Chemins vers fichiers

// https://docs.microsoft.com/fr-fr/dotnet/standard/serialization/system-text-json-use-dom-utf8jsonreader-utf8jsonwriter?pivots=dotnet-6-0

//string pathTemperature = Directory.GetCurrentDirectory() + @"/Data/Temperature.json"; // problèmes de séparateurs
string pathTemperature = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Temperature.json"); // ok
//string pathTemperature = System.IO.Path.GetFullPath("/Data/Temperature.json");
//string pathTemperature = Environment.CurrentDirectory + "Data/Temperature.json";

string pathTemps = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Temps.json"); 
string pathPersonne = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Personne.json"); 

#endregion



#region Avec JsonNode

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("---- Avec JsonNode");
Console.ResetColor();

// Lire un flux de données au chemin défini précédemment
using (StreamReader r = new StreamReader(pathTemperature))
//using (StreamReader r = File.OpenText(pathTemperature)) // autre façon de faire
{
	// lecture de tout le flux jusqu'à la fin
	string jsonString = r.ReadToEnd();

	// https://docs.microsoft.com/fr-fr/dotnet/standard/serialization/system-text-json-use-dom-utf8jsonreader-utf8jsonwriter?pivots=dotnet-6-0

	// Convertir en JsonNode
	JsonNode forecastNode = JsonNode.Parse(jsonString); 


	Console.WriteLine("\nLecture directe d'une donnée du JsonNode :");
	JsonNode temperatureNode = forecastNode["Temperature"];
	//Console.WriteLine($"\tType = {temperatureNode.GetType()}");
	Console.WriteLine($"\tValeur = {temperatureNode.ToJsonString()}"); // 25


	Console.WriteLine("\nIdem avec une donnée de type objet : ");
	JsonNode temperatureRanges = forecastNode["TemperatureRanges"];
	//Console.WriteLine($"\tType = {temperatureRanges.GetType()}");
	Console.WriteLine($"\tValeur = {temperatureRanges.ToJsonString()}"); // l'objet en string


	Console.WriteLine("\nIdem avec une donnée de type tableau : ");
	JsonNode datesAvailable = forecastNode["DatesAvailable"];
	//Console.WriteLine($"\tType = {datesAvailable.GetType()}");
	Console.WriteLine($"\tValeur = {datesAvailable.ToJsonString()}"); // le tableau en string

	// Le tableau est renseigné en tant que JsonNode. On peut l'explorer.
	Console.WriteLine("\nLecture à l'index 0 du tableau JsonNode : ");
	JsonNode entree0 = datesAvailable[0];
	Console.WriteLine($"\tA l'index 0 : {entree0}");


	Console.WriteLine("\nExploration d'un objet par chaînage de propriétés : ");
	int coldHighTemperature = (int)temperatureRanges["Cold"]["High"];
	Console.WriteLine($"\tTemperatureRanges.Cold.High = {coldHighTemperature}"); // 20


	Console.WriteLine("\nLecture d'une valeur convertie en son type attendu (ici int) :");
	int temperatureInt = (int)forecastNode["Temperature"];
	Console.WriteLine($"\tValeur = {temperatureInt}"); // 25


	Console.WriteLine("\nLecture à partir de GetValue<T>() : ");
	temperatureInt = forecastNode["Temperature"].GetValue<int>();
	Console.WriteLine($"\tValeur = {temperatureInt}"); // 25


	Console.WriteLine("\nIdem en explorant un tableau : ");
	JsonNode firstDate = datesAvailable[0].GetValue<DateTime>();
	Console.WriteLine($"\tPremière date = {firstDate}");
}

#endregion



#region Avec classe de données

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("\n---- Avec classe de données");
Console.ResetColor();

using (StreamReader r = new StreamReader(pathTemps))
{
	string jsonString = r.ReadToEnd();

	Console.WriteLine("\nUne liste d'objets Temps : ");
	List<Temps> itemsList = JsonSerializer.Deserialize<List<Temps>>(jsonString);
	Console.WriteLine("\t" + itemsList[0]);
	Console.WriteLine("\t" + itemsList[1]);


	Console.WriteLine("\nUn tableau d'objets Temps : ");
	Temps[] items = JsonSerializer.Deserialize<Temps[]>(jsonString);
	Console.WriteLine("\t" + items[0]);
	Console.WriteLine("\t" + items[1]);
}
#endregion



#region Modifier et enregistrer

// https://docs.microsoft.com/fr-fr/dotnet/standard/io/how-to-write-text-to-a-file

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("\n---- Avec classe de données : modifier\n");
Console.ResetColor();

List<Temps> listeTemps = null;

// Lire pour renseigner la liste 
Console.WriteLine("Lire le fichier json : ");
using (StreamReader r = new StreamReader(pathTemps))
{
	string jsonString = r.ReadToEnd();
	listeTemps = JsonSerializer.Deserialize<List<Temps>>(jsonString);
	Console.WriteLine("\t" + listeTemps[0]);
	Console.WriteLine("\t" + listeTemps[1]);
}

// Modifier les valeurs
Console.WriteLine("\nModifier des valeurs de l'item à l'index 1...\n");
listeTemps[1].BabyDate = DateTime.Now;
listeTemps[1].Entier = 0;
listeTemps[1].Summary = "TOTO";

// Enregistrer le fichier json
using (StreamWriter r = new StreamWriter(pathTemps))
{
	string toWrite = JsonSerializer.Serialize(listeTemps);
	r.Write(toWrite);
}

// Lire pour cette fois vérifier que l'enregistrement a fonctionné
Console.WriteLine("Lire le fichier json pour vérifier la modification : ");
using (StreamReader r = new StreamReader(pathTemps))
{
	string jsonString = r.ReadToEnd();
	List<Temps> itemsList = JsonSerializer.Deserialize<List<Temps>>(jsonString);
	Console.WriteLine("\t" + itemsList[0]);
	Console.WriteLine("\t" + itemsList[1]);
}

// Version .NET 5 (source : Anthony)
//static List<T> Deserialize<T>(this string SerializedJSONString)
//{
//	var stuff = JsonConvert.DeserializeObject<List<T>>(SerializedJSONString);
//	return stuff;
//}

#endregion



#region Json et dictionary

// Ici, on traite un json représentant un objet contenant des objets nommés.
// On veut convertir cela en dictionnaire :
// - clé : le nom de l'objet nommé,
// - valeur : l'objet correspondant.

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("\n---- Avec classe de données : dictionary et objets\n");
Console.ResetColor();

Dictionary<string, Personne> personnes = null;

using (StreamReader r = new StreamReader(pathPersonne))
{
	string jsonString = r.ReadToEnd();
	personnes = JsonSerializer.Deserialize<Dictionary<string, Personne>>(jsonString);
}

foreach (KeyValuePair<string, Personne> item in personnes)
{
	Console.WriteLine($"clé : {item.Key} / valeur : {item.Value}");
}

#endregion



// --------------------------------

#region Fin programme

Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine();
Console.WriteLine(new String('-', 50));
Console.WriteLine("Fin de programme");
Console.WriteLine(new String('-', 50));
Console.Read();

#endregion