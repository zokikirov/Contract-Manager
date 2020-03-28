using DatabaseNamespace;
using DatabaseNamespace.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbServices
{
	public class MailConfigurationService : IMailConfigurationService
	{
		/// <summary>
		/// Add mail configuration to database
		/// </summary>
		/// <param name="configuration"></param>
		public void Add(MailConfiguration configuration)
		{
			ContextFactory.GetContext().MailConfigurations.Add(configuration);
			ContextFactory.GetContext().SaveChanges();
		}

		/// <summary>
		/// Get all mail configurations
		/// </summary>
		/// <returns></returns>
		public List<MailConfiguration> GetAll()
		{
			return ContextFactory.GetContext().MailConfigurations.ToList();
		}

		/// <summary>
		/// Get mail configuration by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public MailConfiguration GetById(int id)
		{
			return ContextFactory.GetContext().MailConfigurations.FirstOrDefault(c => c.Id == id);
		}

		/// <summary>
		/// Get default mail configuration in database
		/// </summary>
		/// <returns></returns>
		public MailConfiguration GetMailConfiguration()
		{
			return ContextFactory.GetContext().MailConfigurations.FirstOrDefault();
		}

		/// <summary>
		/// Remove mail configuration
		/// </summary>
		/// <param name="mailConfigurationId"></param>
		public void Remove(int mailConfigurationId)
		{
			MailConfiguration config = ContextFactory.GetContext().MailConfigurations.FirstOrDefault(c => c.Id == mailConfigurationId);
			if (config != null)
			{
				ContextFactory.GetContext().MailConfigurations.Remove(config);
				ContextFactory.GetContext().SaveChanges();
			}
		}

		/// <summary>
		/// Update specific mail configuration 
		/// </summary>
		/// <param name="configuration"></param>
		public void Update(MailConfiguration configuration)
		{
			ContextFactory.GetContext().MailConfigurations.AddOrUpdate(configuration);
			ContextFactory.GetContext().SaveChanges();
		}
	}
}
