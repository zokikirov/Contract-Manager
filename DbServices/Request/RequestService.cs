using DatabaseNamespace;
using DatabaseNamespace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbServices
{
	public class RequestService : IRequestService
	{
		/// <summary>
		/// Get all requests from database
		/// </summary>
		/// <returns></returns>
		public List<Request> GetAllRequests()
		{
			return ContextFactory.GetContext().Requests.ToList();
		}

		/// <summary>
		/// Get all request for contract id
		/// </summary>
		/// <param name="contractId"></param>
		/// <returns>boolean</returns>
		public List<Request> GetRequestsByContractId(string contractId)
		{
			return ContextFactory.GetContext().Requests.Where(c => c.ContractId == contractId).ToList();
		}

		/// <summary>
		/// Insert request to database
		/// </summary>
		/// <param name="request"></param>
		public void InsertRequest(Request request)
		{
			ContextFactory.GetContext().Requests.Add(request);
			ContextFactory.GetContext().SaveChanges();
		}

		/// <summary>
		/// Delete request from database
		/// </summary>
		/// <param name="requestId"></param>
		public bool RemoveRequest(int requestId)
		{
			Request request = ContextFactory.GetContext().Requests.FirstOrDefault(c => c.Id == requestId);
			if (request != null)
			{
				ContextFactory.GetContext().Requests.Remove(request);
				return ContextFactory.GetContext().SaveChanges() > 0;
			}

			return false;
		}

		public bool RemoveRequestByContractId(string contractId)
		{
			List<Request> requests = ContextFactory.GetContext().Requests.Where(c => c.ContractId == contractId).ToList();
			if (requests != null)
			{
				ContextFactory.GetContext().Requests.RemoveRange(requests);
				return ContextFactory.GetContext().SaveChanges() > 0;
			}

			return false;
		}
	}
}
