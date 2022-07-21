using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEST_APICall.ClassesData
{
	/// <summary>
	/// Classe modélisant les données de l'API externe
	/// </summary>
	internal class Todo
	{
		#region Properties

		public string UserId { get; set; }

		public string Id { get; set; }

		public string Title { get; set; }

		//public string Body { get; set; }

		public bool Completed { get; set; }	

		#endregion
	}
}
