using System.ComponentModel.DataAnnotations;

namespace Empetz_API.API.Public.RequestObject
{
	public class VerifyObject
	{
		[Required]
		[Phone(ErrorMessage = "Please enter a valid phone number.")]
		[RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number format. It should be 10 digits.")]
		public string Phone { get; set; }
	}
}
