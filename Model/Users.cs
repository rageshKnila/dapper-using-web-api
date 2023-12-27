using Sieve.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Academy.Model
{
	public class Users
	{

		public int Id { get; set; }

		public string Name { get; set; }

		public string Phone { get; set; }

		public string Email { get; set; }

		public string UserName { get; set; }

		public string OldPassword { get; set; }

		public string NewPassword { get; set; }

		public string UpdateProfilePicture { get; set; }
	}
}
