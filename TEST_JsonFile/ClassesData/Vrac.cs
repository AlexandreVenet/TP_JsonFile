using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TEST_JsonFile.ClassesData
{
	internal class Vrac
	{
		[JsonPropertyName("nombre-bizarre")]
		public double NombreBizarre { get; set; }

		[JsonPropertyName("tableau")]
		public object[] Tableau { get; set; }
	}
}
