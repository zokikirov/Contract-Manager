using System;
using System.Collections.Generic;
using System.Text;
using DatabaseNamespace;
using System.Linq;

namespace DbServices
{
    public class AuditService : IAuditService
    {
		/// <summary>
		/// Get all Audits from database
		/// </summary>
		/// <returns></returns>
		public List<Audit> GetAll()
		{
			return ContextFactory.GetContext().Audits.ToList();
		}


		/// <summary>
		/// Get Audits by user
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public List<Audit> GetByUser(string userId)
		{
			return ContextFactory.GetContext().Audits.Where(c => c.UserId == userId).ToList();
		}


		/// <summary>
		/// Insert audit into database
		/// </summary>
		/// <param name="audit"></param>
		/// <returns></returns>
		public bool Insert(Audit audit)
		{
			ContextFactory.GetContext().Audits.Add(audit);
			return ContextFactory.GetContext().SaveChanges() > 0;
		}

		/// <summary>
		/// delete audits by userId
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public bool DeleteAuditsByUserId(string userId)
		{
			List<Audit> audits = ContextFactory.GetContext().Audits.Where(c => c.UserId == userId).ToList();
			if (audits != null && audits.Count() > 0)
			{
				ContextFactory.GetContext().Audits.RemoveRange(audits);
				return ContextFactory.GetContext().SaveChanges() > 0;
			}
			return false;
		}
	}
}
