using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyoeiSystem.Framework.Core
{
	public class FWThreadManagerException : Exception
	{
		public FWThreadManagerException()
			: base()
		{
		}

		public FWThreadManagerException(string message)
			: base(message)
		{
		}

		public FWThreadManagerException(string message, Exception ex)
			: base(message, ex)
		{
		}
	}

	public class FWThreadCoreDataException : Exception
	{
		public FWThreadCoreDataException()
			: base()
		{
		}

		public FWThreadCoreDataException(string message)
			: base(message)
		{
		}

		public FWThreadCoreDataException(string message, Exception ex)
			: base(message, ex)
		{
		}
	}

}
