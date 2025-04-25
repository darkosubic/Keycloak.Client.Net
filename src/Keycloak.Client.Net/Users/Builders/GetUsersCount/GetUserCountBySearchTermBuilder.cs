using System;
using Keycloak.Client.Net.Users.DTOs;

namespace Keycloak.Client.Net.Users.Builders.GetUsersCount
{
    public static partial class GetUsersCountBuilder
    {
        public interface IGetUserCountBySearchTermBuilderStart
        {
            IGetUserCountBySearchTermBuilderReady WithSearchTerm(string searchTerm);
        }

        public interface IGetUserCountBySearchTermBuilderReady
        {
            GetUsersCountDto Build();
        }

        public class GetUserCountBySearchTermBuilder : IGetUserCountBySearchTermBuilderStart, IGetUserCountBySearchTermBuilderReady
        {
            private readonly GetUsersCountDto _dto = new GetUsersCountDto();

            private GetUserCountBySearchTermBuilder() { }

            public static IGetUserCountBySearchTermBuilderStart Create() => new GetUserCountBySearchTermBuilder();

            public IGetUserCountBySearchTermBuilderReady WithSearchTerm(string searchTerm)
            {
                _dto.Search = searchTerm;
                return this;
            }

            public GetUsersCountDto Build()
            {
                if (string.IsNullOrEmpty(_dto.Search))
                {
                    throw new InvalidOperationException($"{nameof(GetUsersCountDto.Search)} must be set before calling {nameof(IGetUserCountBySearchTermBuilderReady.Build)}.");
                }

                return _dto;
            }
        }
    }
}
