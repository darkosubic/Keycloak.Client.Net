using System;
using Keycloak.Client.Net.Users.DTOs;

namespace Keycloak.Client.Net.Users.Builders.GetUsersByIdsBuilder
{
    public static partial class GetUsersBuilder
    {
        public interface IGetUsersBySearchTermBuilderStart
        {
            /// <summary>
            /// Sets a raw search string (prefix-based by default).
            /// Example: "admin" → matches "admin*", not wrapped with * or quotes.
            /// </summary>
            IGetUsersBySearchTermBuilderReady WithSearch(string rawSearch);

            /// <summary>
            /// Wraps the search term in * to perform infix (contains) search.
            /// Example: "admin" → "*admin*"
            /// </summary>
            IGetUsersBySearchTermBuilderReady WithInfixSearch(string term);

            /// <summary>
            /// Wraps the search term in double quotes to perform exact search.
            /// Example: "admin" → "\"admin\""
            /// </summary>
            IGetUsersBySearchTermBuilderReady WithExactSearch(string exactTerm);
        }

        public interface IGetUsersBySearchTermBuilderReady
        {
            IGetUsersBySearchTermBuilderReady WithExact(bool isExact);
            GetsUserRequestDto Build();
        }

        internal class GetUsersBySearchTermBuilder : IGetUsersBySearchTermBuilderStart, IGetUsersBySearchTermBuilderReady
        {
            private readonly GetsUserRequestDto _dto = new GetsUserRequestDto();

            private GetUsersBySearchTermBuilder() { }

            public static IGetUsersBySearchTermBuilderStart Create() => new GetUsersBySearchTermBuilder();
            public IGetUsersBySearchTermBuilderReady WithSearch(string rawSearch)
            {
                if (!string.IsNullOrWhiteSpace(rawSearch))
                {
                    _dto.Search = rawSearch;
                }

                return this;
            }

            public IGetUsersBySearchTermBuilderReady WithInfixSearch(string term)
            {
                if (!string.IsNullOrWhiteSpace(term))
                {
                    _dto.Search = $"*{term}*";
                }

                return this;
            }

            public IGetUsersBySearchTermBuilderReady WithExactSearch(string exactTerm)
            {
                if (!string.IsNullOrWhiteSpace(exactTerm))
                {
                    _dto.Search = $"\"{exactTerm}\"";
                }
                return this;
            }

            public IGetUsersBySearchTermBuilderReady WithExact(bool isExact)
            {
                _dto.IsExact = isExact;

                return this;
            }

            public GetsUserRequestDto Build()
            {
                if (_dto == null ||
                    string.IsNullOrEmpty(_dto.Search))
                {
                    throw new Exception("Please provide atleast one search term.");
                }

                return _dto;
            }
        }
    }
}
