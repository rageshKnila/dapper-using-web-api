using System.ComponentModel.DataAnnotations;

namespace Academy.Model
{
	public class Teacher
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "First Name is required")]
		[MaxLength(50, ErrorMessage = "First Name cannot exceed 50 characters")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Last Name is required")]
		[MaxLength(50, ErrorMessage = "Last Name cannot exceed 50 characters")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Official Mobile is required")]
		[MaxLength(15, ErrorMessage = "Official Mobile cannot exceed 15 characters")]
		public string OfficialMobile { get; set; }

		[Required(ErrorMessage = "Official Email is required")]
		[MaxLength(100, ErrorMessage = "Official Email cannot exceed 100 characters")]
		[EmailAddress(ErrorMessage = "Invalid Official Email Address")]
		public string OfficialEmail { get; set; }

		[Required(ErrorMessage = "Course is required")]
		[MaxLength(50, ErrorMessage = "Course cannot exceed 50 characters")]
		public string Course { get; set; }

		
		public string UploadPhoto { get; set; }
	}
}
