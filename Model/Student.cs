using System.ComponentModel.DataAnnotations;

namespace Academy.Model
{
	public class Student
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "First Name is required")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Last Name is required")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Course is required")]
		public string Course { get; set; }

		[Required(ErrorMessage = "Batch Details are required")]
		public string BatchDetails { get; set; }

		[Required(ErrorMessage = "Gender is required")]
		public string Gender { get; set; }

		[Required(ErrorMessage = "Date of Birth is required")]
		[DataType(DataType.Date)]
		public DateTime DateOfBirth { get; set; }

		[Required(ErrorMessage = "Admission Number is required")]
		public int AdmissionNo { get; set; }

		[Required(ErrorMessage = "Batch Time is required")]
	
		public string BatchTime { get; set; }

		[Required(ErrorMessage = "Assigned Trainers are required")]
		public string Assignedtrainers { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Prime Phone Number is required")]
		[Phone(ErrorMessage = "Invalid Phone Number")]
		public string PrimePhoneNumbers { get; set; }

	
		public string UploadPhoto { get; set; }

	}
}
