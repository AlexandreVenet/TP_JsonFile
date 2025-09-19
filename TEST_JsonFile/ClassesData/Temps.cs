using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST_JsonFile.ClassesData
{
	internal class Temps
	{
		public DateTime BabyDate { get; set; }
		public int Entier { get; set; }
		public string Summary { get; set; }


		public override string ToString()
		{
			return $"{BabyDate}, {Entier}, {Summary}";
		}
	}
}
