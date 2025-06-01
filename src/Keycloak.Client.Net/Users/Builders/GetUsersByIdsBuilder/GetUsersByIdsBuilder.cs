using System;
using System.Collections.Generic;
using System.Text;
using Keycloak.Client.Net.Users.DTOs;

namespace Keycloak.Client.Net.Users.Builders.GetUsersByIdsBuilder
{
    public static partial class GetUsersBuilder
    {
        public interface IGetUsersByIdsBuilderStart
        {
            IGetAllUsersCountBuilderReady WithASingleUserId(string id);
            IGetAllUsersCountBuilderReady WithAListOfUserIds(List<string> id);
        }

        public interface IGetAllUsersCountBuilderReady
        {
            GetsUserRequestDto Build();
        }
        

        public class GetUsersByIdsBuilder : IGetUsersByIdsBuilderStart, IGetAllUsersCountBuilderReady
        {
            private readonly GetsUserRequestDto _dto = new GetsUserRequestDto();
            private const string SearchStart = "id:";

            private GetUsersByIdsBuilder() { }

            public static IGetUsersByIdsBuilderStart Create() => new GetUsersByIdsBuilder();


            public IGetAllUsersCountBuilderReady WithASingleUserId(string id)
            {
                if (string.IsNullOrEmpty(_dto.Search) || _dto.Search.Length <= SearchStart.Length)
                {
                    _dto.Search = SearchStart;
                }

                _dto.Search = _dto.Search + " " + id + " ";

                return this;
            }

            public IGetAllUsersCountBuilderReady WithAListOfUserIds(List<string> ids)
            {
                if (string.IsNullOrEmpty(_dto.Search) || _dto.Search.Length <= SearchStart.Length)
                {
                    _dto.Search = SearchStart;
                }

                if (ids.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (string id in ids)
                    {
                        sb.Append(id + " ");
                    }

                    _dto.Search += sb.ToString();
                }

                return this;
            }

            public GetsUserRequestDto Build()
            {
                if (_dto == null ||
                    !_dto.Search.StartsWith(SearchStart) ||
                    _dto.Search.Length <= SearchStart.Length)
                {
                    throw new Exception("Please provide atleast one user id.");
                }

                return _dto;
            }
        }
    }
}
