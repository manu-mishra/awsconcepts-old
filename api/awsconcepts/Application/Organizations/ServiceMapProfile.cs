using AutoMapper;

namespace Application.Organizations
{
    public class ServiceMapProfile : Profile
    {
        public ServiceMapProfile()
        {
            CreateMap<Domain.Organizations.Organization, Dto.Organization>().ForAllMembers(x => x.AllowNull());
            CreateMap<Dto.Organization, Domain.Organizations.Organization>().ForAllMembers(x => x.AllowNull());
        }
    }
}
