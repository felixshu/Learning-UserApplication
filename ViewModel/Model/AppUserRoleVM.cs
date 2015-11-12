using System.ComponentModel.DataAnnotations;

namespace ViewModel.Model
{
	public class AppUserRoleVm
	{
		public string Id { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "You must enter the name for Role.")]
		[StringLength(120, ErrorMessage = "The role name must be 120 characters or shorter.", MinimumLength = 2)]
		[Display(Name = "Role Name")]
		public string Name { get; set; }
	}
}