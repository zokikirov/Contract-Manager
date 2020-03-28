using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Client.Models
{
	public class DashboardViewModel
	{ 
		[DisplayName("Number of users")]
		public int NumberOfUsers { get; set; }

		[DisplayName("Requests send")]
		public int RequestsSend { get; set; }

		[DisplayName("Signed Contracts")]
		public int SignedContracts { get; set; }
	}
}