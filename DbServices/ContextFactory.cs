using DatabaseNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbServices
{
	public class ContextFactory
	{
		private static DatabaseContext instance;
		private static DatabaseContext Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new DatabaseContext();
				}
				return instance;
			}
		}

		public static int PageSize = 20;
		private static bool testing = false;
		private static DatabaseContext context = null;

		public static void SetContext(DatabaseContext test)
		{
			testing = true;
			context = test;
		}

		public static DatabaseContext GetContext()
		{
			if (!testing)
			{
				return Instance;
			}

			return context;
		}
	}
}