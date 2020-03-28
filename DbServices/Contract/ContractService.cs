using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseNamespace;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using DatabaseNamespace.Models;

namespace DbServices
{
	public class ContractService : IContractService
	{
		/// <summary>
		/// Delete contract from database
		/// </summary>
		/// <param name="contractId"></param>
		/// <returns>boolean</returns>
		public bool Delete(string contractId)
		{
			Contract contract = ContextFactory.GetContext().Contracts.FirstOrDefault(c => c.Id == contractId);
			if (contract != null)
			{
				ContextFactory.GetContext().Contracts.Remove(contract);
				return ContextFactory.GetContext().SaveChanges() > 0;
			}
			return false;
		}

		public void RemoveAllForManagerAndSubUSers(string managerId, string userId)
		{
			List<Contract> contractsForRemove = ContextFactory.GetContext().Contracts.Where(c => c.UserID == userId || c.ManagerID == managerId).ToList();

			foreach (Contract contract in contractsForRemove)
			{
				ContractType type = ContextFactory.GetContext().ContractTypes.FirstOrDefault(c => c.ID == contract.ContractTypeId);
				if (type != null)
				{
					ContextFactory.GetContext().ContractTypes.Remove(type);
				}

				Request request = ContextFactory.GetContext().Requests.FirstOrDefault(c => c.ContractId == contract.Id);
				if (request != null)
				{
					ContextFactory.GetContext().Requests.Remove(ContextFactory.GetContext().Requests.FirstOrDefault(c => c.ContractId == contract.Id));
				}

				ContextFactory.GetContext().Contracts.Remove(contract);
			}

			ContextFactory.GetContext().SaveChanges();
		}

		public bool DeleteContractsByManagerId(string managerId)
		{
			List<Contract> contracts = ContextFactory.GetContext().Contracts.Where(c => c.ManagerID == managerId).ToList();
			if (contracts != null)
			{
				ContextFactory.GetContext().Contracts.RemoveRange(contracts);
				return ContextFactory.GetContext().SaveChanges() > 0;
			}
			return false;
		}

		/// <summary>
		/// delete contracts by userId
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public bool DeleteContractsByUserId(string userId)
		{
			List<Contract> contracts = ContextFactory.GetContext().Contracts.Where(c => c.UserID == userId).ToList();
			if (contracts != null)
			{
				ContextFactory.GetContext().Contracts.RemoveRange(contracts);
				return ContextFactory.GetContext().SaveChanges() > 0;
			}


			return false;
		}

		/// <summary>
		/// Get all contracts from database
		/// </summary>
		/// <returns>List of contracts</returns>
		public List<Contract> GetAll()
		{
			return ContextFactory.GetContext().Contracts.ToList();
		}

		/// <summary>
		/// Get contracts by user
		/// </summary>
		/// <param name="userID"></param>
		/// <returns>List of contracts</returns>
		public List<Contract> GetContractsByManagerID(string managerId)
		{
			return ContextFactory.GetContext().Contracts.Include(c => c.Requests).Where(c => c.ManagerID == managerId).ToList();
		}

		/// <summary>
		/// Get contracts by manager id
		/// </summary>
		/// <param name="managerId"></param>
		/// <returns>List of contracts</returns>
		public List<Contract> GetContractsByUserID(string userId)
		{
			return ContextFactory.GetContext().Contracts.Include(c => c.Requests).Where(c => c.UserID == userId).ToList();
		}

		/// <summary>
		/// Insert contract in database
		/// </summary>
		/// <param name="contract"></param>
		/// <returns></returns>
		public Contract Insert(Contract contract)
		{
			Contract newContract = ContextFactory.GetContext().Contracts.Add(contract);
			if (ContextFactory.GetContext().SaveChanges() > 0)
			{
				return newContract;
			}
			return null;
		}

		/// <summary>
		/// Update contract in database
		/// </summary>
		/// <param name="contract"></param>
		/// <returns>boolean</returns>
		public bool Update(Contract contract)
		{
			ContextFactory.GetContext().Contracts.AddOrUpdate(contract);
			return ContextFactory.GetContext().SaveChanges() > 0;
		}

		/// <summary>
		/// Get contracts for specific users send by manager id
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="managerID"></param>
		/// <returns>List sof contracts</returns>
		public List<Contract> GetContractsByUserAndManager(string userID, string managerID)
		{
			return ContextFactory.GetContext().Contracts.Where(c => c.UserID == userID && c.ManagerID == managerID).ToList();
		}

		/// <summary>
		/// Get contract by contract id
		/// </summary>
		/// <param name="contractId"></param>
		/// <returns>Contract object</returns>
		public Contract GetContractById(string contractId)
		{
			return ContextFactory.GetContext().Contracts.FirstOrDefault(c => c.Id == contractId);
		}
	}
}
