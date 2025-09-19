using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using TEST_JsonFile.Helpers;

namespace TEST_JsonFile.ClassesOperations
{
	internal class AvecUtf8JsonRW
	{
		public AvecUtf8JsonRW()
		{
			TesterLecteur();
			TesterEcriveur();
		}

		private void TesterLecteur()
		{
			Sortie.Titre("Avec Utf8JsonReader\n");

			// https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/use-utf8jsonreader

			string json = """
				{
					"prenom": "Lex", 
					"nom": "Luthor",
					"age": 999, 
					"actif": true }
				""";

			// Le *parsing* est manuel (pas de modèle de données).
			// Il repose sur la reconnaissance de *tokens* : la plus petite unité JSON rencontrée.
			// {			StartObject
			//   "prenom"	PropertyName
			//   "Lex"		String
			//   ...
			// }			EndObject

			// Convertir en *byte array* encodé en UTF8
			byte[] octetsUtf8 = Encoding.UTF8.GetBytes(json);

			Utf8JsonReader lecteur = new (octetsUtf8);
			while(lecteur.Read())
			{
				switch(lecteur.TokenType)
				{
					case JsonTokenType.PropertyName:
						Console.Write($"Propriété : {lecteur.GetString()} => ");
						break;

					case JsonTokenType.String:
						Console.Write($"Valeur (String) : {lecteur.GetString()}\n");
						break;

					case JsonTokenType.Number:
						Console.Write($"Valeur (Int32) : {lecteur.GetInt32()}\n");
						break;

					case JsonTokenType.True:
					case JsonTokenType.False:
						Console.Write($"Valeur (Bool) : {lecteur.GetBoolean()}\n");
						break;
				}
			}
			//Propriété : prenom => Valeur (String) : Lex
			//Propriété : nom => Valeur (String) : Luthor
			//Propriété : age => Valeur (Int32) : 999
			//Propriété : actif => Valeur (Bool) : True
		}

		private void TesterEcriveur()
		{
			Sortie.Titre("\nAvec Utf8JsonWriter\n");

			// https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/use-utf8jsonwriter

			// Configuration du JSON
			JsonWriterOptions options = new()
			{
				Indented = true,
				Encoder = JavaScriptEncoder.Create(
						UnicodeRanges.BasicLatin,       // Caractères ASCII de base
						UnicodeRanges.Latin1Supplement, // Caractères spéciaux (é, ç...)
						UnicodeRanges.LatinExtendedA    // Autres caractères latins 
						),
			};

			// Utf8JsonWriter rendoie un tableau d'octets (byte[]) en UTF8.
			// Pour cette raison, par exemple utiliser MemoryStream qui permet de le convertir en String.
			// Utf8JsonWriter est rapide et performant mais l'écriture est manuelle (pas d'utilisation de modèle de données).

			Console.WriteLine("Avec MemoryStream");

			using (MemoryStream memoryStream = new())
			{
				// Construire l'objet JSON pas à pas, avec structure de code pour lisibilité.
				using (Utf8JsonWriter jsonWriter = new(memoryStream, options))
				{
					jsonWriter.WriteStartObject(); // objet parent sans nom par obligation
					{
						jsonWriter.WriteCommentValue("Un commentaire par là...");
						jsonWriter.WriteNumber("nombre", 3.2);
						jsonWriter.WriteStartArray("tableau");
						{
							jsonWriter.WriteNumberValue(25.5);
							jsonWriter.WriteStringValue("youpi gné !");
							jsonWriter.WriteBooleanValue(true);
							jsonWriter.WriteNullValue();
						}
						jsonWriter.WriteEndArray();
					}
					jsonWriter.WriteEndObject();
				}

				byte[] octetsDuFlux = memoryStream.ToArray();
				string contenu = Encoding.UTF8.GetString(octetsDuFlux);
				Console.WriteLine(contenu);
				//{
				//  /*Un commentaire par là...*/
				//  "nombre": 3.2,
				//  "tableau": [
				//    25.5,
				//    "youpi gné !",
				//    true,
				//    null
				//  ]
				//}
			}

			Console.WriteLine("\nAvec ArrayBufferWriter");

			// On peut faire la même chose que précédemment avec ArrayBufferWriter plus léger que MemoryStream.

			ArrayBufferWriter<byte> buffer = new();

			using (Utf8JsonWriter jsonWriter = new(buffer, options))
			{
				jsonWriter.WriteStartObject(); // objet parent sans nom par obligation
				{
					jsonWriter.WriteCommentValue("Un commentaire par là...");
					jsonWriter.WriteNumber("nombre", 3.2);
					jsonWriter.WriteStartArray("tableau");
					{
						jsonWriter.WriteNumberValue(25.5);
						jsonWriter.WriteStringValue("youpi gné !");
						jsonWriter.WriteBooleanValue(true);
						jsonWriter.WriteNullValue();
					}
					jsonWriter.WriteEndArray();
				}
				jsonWriter.WriteEndObject();
			}

			ReadOnlySpan<byte> donnees = buffer.WrittenSpan;
			string donneesLisibles = Encoding.UTF8.GetString(donnees);
			Console.WriteLine(donneesLisibles);
			//{
			//  /*Un commentaire par là...*/
			//  "nombre": 3.2,
			//  "tableau": [
			//    25.5,
			//    "youpi gné !",
			//    true,
			//    null
			//  ]
			//}
		}
	}
}
