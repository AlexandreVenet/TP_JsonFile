using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST_JsonFile.ClassesData
{
	internal class Personne
	{
		public string Nom { get; set; }
		public int Valeur { get; set; }


		public override string ToString()
		{
			return $"{Nom}, {Valeur}";
		}
	}
}
