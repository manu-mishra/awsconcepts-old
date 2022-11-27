//using Application.Common.Interfaces;
//using Application.Values.Queries;
//using Domain.Applicants;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Application.Applicant.Queries
//{
//    public class ListUserProfilesQuery : IRequest<List<ApplicantProfile>>
//    {
//        ApplicantProfile _user;
//        public ListUserProfilesQuery(ICurrentUser currentUser)
//        {
//            _user = new ApplicantProfile();
//        }
//    }
//    public class GetValuesQueryHandler : IRequestHandler<ListUserProfilesQuery, List<ApplicantProfile>>
//    {

//        public Task<IEnumerable<string>> Handle(ListUserProfilesQuery request, CancellationToken cancellationToken)
//        {
//            return Task.FromResult<IEnumerable<string>>(new List<string> { "hello", "world" });
//        }
//    }
//}
