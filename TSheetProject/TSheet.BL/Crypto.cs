using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSheet.Data;
using TSheet.IBL;

namespace TSheet.BL
{
	public class Crypto
	{

		public static string Hash(string value)
		{
			return Convert.ToBase64String(
				System.Security.Cryptography.SHA256.Create()
				.ComputeHash(Encoding.UTF8.GetBytes(value))
				);
		}
	}


}
