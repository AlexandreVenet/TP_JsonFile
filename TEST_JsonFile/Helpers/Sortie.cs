using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST_JsonFile.Helpers
{
	internal class Sortie
	{
		static public void ProgrammeDebut()
		{
			Console.ForegroundColor = ConsoleColor.Blue;

			Console.WriteLine(new String('=', 50));
			Console.WriteLine("Traitement JSON local");
			Console.WriteLine(new String('=', 50));
			Console.WriteLine();

			Console.ResetColor();
		}

		static public void ProgrammeFin()
		{
			Console.ForegroundColor = ConsoleColor.Blue;

			Console.WriteLine();
			Console.WriteLine(new String('=', 50));
			Console.WriteLine("Fin de programme");
			Console.WriteLine(new String('=', 50));

			Console.Read();
		}

		static public void Titre(string titre)
		{
			Console.ForegroundColor = ConsoleColor.DarkYellow;

			Console.WriteLine(titre);

			Console.ResetColor();
		}
	}
}
