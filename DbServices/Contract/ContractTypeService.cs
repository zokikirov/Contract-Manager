using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseNamespace;
using System.Data.Entity.Migrations;

namespace DbServices
{
	public class ContractTypeService : IContractTypeService
	{
		/// <summary>
		/// Get ContractTypes by user id
		/// </summary>
		/// <param name="userId"></param>
		/// <returns>List of contract types</returns>
		public List<ContractType> GetContractTypesByUser(string userId)
		{
			return ContextFactory.GetContext().ContractTypes.Where(c => c.ManagerId == userId).ToList();
		}

		/// <summary>
		/// Delete contract from database 
		/// </summary>
		/// <param name="contractTypeId"></param>
		/// <returns>boolean</returns>
		public bool Delete(int contractTypeId)
		{
			ContractType contractType = ContextFactory.GetContext().ContractTypes.FirstOrDefault(c => c.ID == contractTypeId);
			if (contractType != null)
			{
				ContextFactory.GetContext().ContractTypes.Remove(contractType);
				return ContextFactory.GetContext().SaveChanges() > 0;
			}

			return false;
		}

		/// <summary>
		/// delete contract types by userId
		/// </summary>
		/// <param name="managerId"></param>
		/// <returns></returns>
		public bool DeleteByUserId(string managerId)
		{
			List<ContractType> contractTypes = ContextFactory.GetContext().ContractTypes.Where(c => c.ManagerId == managerId).ToList();
			if (contractTypes != null)
			{
				ContextFactory.GetContext().ContractTypes.RemoveRange(contractTypes);
				return ContextFactory.GetContext().SaveChanges() > 0;
			}

			return false;
		}

		/// <summary>
		/// Get all contract types
		/// </summary>
		/// <returns></returns>
		public List<ContractType> GetAll()
		{
			return ContextFactory.GetContext().ContractTypes.ToList();
		}

		/// <summary>
		/// Get ContractType by contract type id
		/// </summary>
		/// <param name="contractTypeId"></param>
		/// <returns>ContractType object</returns>
		public ContractType GetById(int contractTypeId)
		{
			return ContextFactory.GetContext().ContractTypes.FirstOrDefault(c => c.ID == contractTypeId);
		}

		/// <summary>
		/// Insert ContractType in database
		/// </summary>
		/// <param name="contractType"></param>
		/// <returns>boolean</returns>
		public bool Insert(ContractType contractType)
		{
			ContextFactory.GetContext().ContractTypes.Add(contractType);
			return ContextFactory.GetContext().SaveChanges() > 0;
		}

		/// <summary>
		/// Update specific contract type
		/// </summary>
		/// <param name="contractType"></param>
		/// <returns></returns>
		public bool Update(ContractType contractType)
		{
			ContextFactory.GetContext().ContractTypes.AddOrUpdate(contractType);
			return ContextFactory.GetContext().SaveChanges() > 0;
		}
	}
}
