using Keycloak.Client.Net.Users.DTOs;

namespace Keycloak.Client.Net.Users.Builders.GetUsersCount
{
    public static partial class GetUsersCountBuilder
    {
        public interface IGetAllUsersCountBuilderStart
        {
            IGetAllUsersCountBuilderReady GetAll();
        }

        public interface IGetAllUsersCountBuilderReady
        {
            GetUsersCountDto Build();
        }

        public class GetAllUsersCountBuilder : IGetAllUsersCountBuilderStart, IGetAllUsersCountBuilderReady
        {
            private readonly GetUsersCountDto _dto = new GetUsersCountDto();

            private GetAllUsersCountBuilder() { }

            public static IGetAllUsersCountBuilderStart Create() => new GetAllUsersCountBuilder();

            public IGetAllUsersCountBuilderReady GetAll()
            {
                return this;
            }

            public GetUsersCountDto Build()
            {                
                return _dto;
            }
        }
    }
}
