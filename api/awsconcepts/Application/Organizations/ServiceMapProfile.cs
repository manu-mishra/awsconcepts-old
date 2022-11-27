using AutoMapper;

namespace Application.Organizations
{
    public class ServiceMapProfile : Profile
    {
        public ServiceMapProfile()
        {
            CreateMap<Domain.Organizations.Organization, Dto.Organization>();


            CreateMap<Dto.Organization, Domain.Organizations.Organization>();
        }
    }
}
