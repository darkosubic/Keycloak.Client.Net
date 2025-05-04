using System;
using Keycloak.Client.Net.Users.DTOs;

namespace Keycloak.Client.Net.Users.Builders.GetUsersCount
{
    //class UserExistsAndCanViewItBuilder
    //{
    //}

    public static partial class GetUsersCountBuilder
    {
        public interface IUserExistsAndCanViewItBuilderStart
        {
            IUserExistsAndCanViewItBuilderReady WithSearchTerm(string userId);
        }

        public interface IUserExistsAndCanViewItBuilderReady
        {
            GetUsersCountDto Build();
        }

        public class UserExistsAndCanViewItBuilder : IUserExistsAndCanViewItBuilderStart, IUserExistsAndCanViewItBuilderReady
        {
            private readonly GetUsersCountDto _dto = new GetUsersCountDto();
            private const string SearchIdParameter = "id:";
            private const int SearchIdParameterLength = 3;

            private UserExistsAndCanViewItBuilder() { }

            public static IUserExistsAndCanViewItBuilderStart Create() => new UserExistsAndCanViewItBuilder();

            public IUserExistsAndCanViewItBuilderReady WithSearchTerm(string userId)
            {
                if (userId.StartsWith(SearchIdParameter))
                {
                    _dto.Search = userId;
                    return this;
                }

                _dto.Search = SearchIdParameter + userId;
                return this;
            }

            public GetUsersCountDto Build()
            {
                if (string.IsNullOrEmpty(_dto.Search) ||
                    _dto.Search.Length <= SearchIdParameterLength)
                {
                    throw new InvalidOperationException($"{nameof(GetUsersCountDto.Search)} must contain UserId before calling {nameof(IUserExistsAndCanViewItBuilderReady.Build)} method.");
                }

                return _dto;
            }
        }
    }
}
