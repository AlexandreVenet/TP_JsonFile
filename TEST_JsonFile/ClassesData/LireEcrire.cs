using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TEST_JsonFile.ClassesData
{
	internal class LireEcrire
	{
		[JsonPropertyName("nombre-entier")]
		public int NombreEntier { get; set; }

		[JsonPropertyName("texte")]
		public string Texte { get; set; }

		[JsonPropertyName("ok")]
		public bool OK { get; set; }

		public override string ToString()
		{
			return $"{NombreEntier}, {Texte}, {OK}";
		}
	}
}
