using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseNamespace;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DbServices.UserServices
{
	public class UserService : IUserService
	{
		/// <summary>
		/// Remove user with user id
		/// </summary>
		/// <param name="userId"></param>
		/// <returns>boolean</returns>
		public bool Delete(string userId)
		{
			UserManager<User> userManager = new UserManager<User>(new UserStore<User>(ContextFactory.GetContext()));
			User userToDelete = ContextFactory.GetContext().Users.FirstOrDefault(c => c.Id == userId);
			return userManager.Delete(userToDelete).Succeeded;
		}

		/// <summary>
		/// delete users by parent userId
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public bool DeleteByParentId(string userId)
		{
			List<User> users = ContextFactory.GetContext().Users.Where(c => c.UserId == userId).ToList();
			if (users != null && users.Count() > 0)
			{
				foreach (var item in users)
				{
					ContextFactory.GetContext().Users.Remove(item);
				}
				return ContextFactory.GetContext().SaveChanges() > 0;
			}

			return false;
		}

		/// <summary>
		/// Get user by specific id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public User Get(string id)
		{
			return ContextFactory.GetContext().Users.Include(c => c.Users).Include(c => c.UserContracts).Include(c => c.ManagerContracts).FirstOrDefault(c => c.Id == id);
		}

		/// <summary>
		/// Get all users from database
		/// </summary>
		/// <returns></returns>
		public List<User> GetAll()
		{
			return ContextFactory.GetContext().Users.Include(c => c.Roles).ToList();
		}

		/// <summary>
		/// Get all child users for specific user id 
		/// </summary>
		/// <param name="userId"></param>
		/// <returns>boolean</returns>
		public List<User> GetByUserID(string userId)
		{
			return ContextFactory.GetContext().Users.Include(c => c.Roles).Where(c => c.UserId == userId).ToList();
		}

		/// <summary>
		/// Insert User to database
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public bool Insert(User user)
		{
			ContextFactory.GetContext().Users.Add(user);
			return ContextFactory.GetContext().SaveChanges() > 0;
		}

		/// <summary>
		/// Update specific user in database
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public bool Update(User user)
		{
			ContextFactory.GetContext().Users.AddOrUpdate(user);
			return ContextFactory.GetContext().SaveChanges() > 0;
		}

		/// <summary>
		/// Get user role by specific name
		/// </summary>
		/// <param name="roleName"></param>
		/// <returns></returns>
		public string GetRoleIdByName(string roleName)
		{
			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ContextFactory.GetContext()));
			return roleManager.FindByName(roleName).Id;

		}
	}
}
