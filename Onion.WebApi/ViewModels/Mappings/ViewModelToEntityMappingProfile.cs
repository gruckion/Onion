using AutoMapper;
using Onion.Data.Identity;

namespace Onion.WebApi.ViewModels.Mappings
{
	public class ViewModelToEntityMappingProfile : Profile
	{
		public ViewModelToEntityMappingProfile()
		{
			CreateMap<RegistrationViewModel, AppUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
		}
	}
}
