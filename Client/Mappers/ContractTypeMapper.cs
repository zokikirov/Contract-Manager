using Client.Models;
using DatabaseNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client.Mappers
{
	public class ContractTypeMapper
	{
		public static ContractTypeViewModel ToViewModel(ContractType contractType, bool withData = true)
		{
			return new ContractTypeViewModel
			{
				ID = contractType.ID,
				Data = (withData && contractType.Data != null) ? (HttpPostedFileBase)new MemoryPostedFile(contractType.Data) : null,
				Name = contractType.Name,
				ManagerId = contractType.ManagerId,
			};
		}
	}
}