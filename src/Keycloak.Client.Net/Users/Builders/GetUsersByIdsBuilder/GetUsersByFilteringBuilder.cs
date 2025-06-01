using System;
using Keycloak.Client.Net.Users.DTOs;

namespace Keycloak.Client.Net.Users.Builders.GetUsersByIdsBuilder
{
    public static partial class GetUsersBuilder
    {
        public interface IGetUsersByFilteringBuilderStart
        {
            IGetUsersByFilteringBuilderReady WithEmail(string email);
            IGetUsersByFilteringBuilderReady WithUsername(string username);
            IGetUsersByFilteringBuilderReady WithFirstName(string firstName);
            IGetUsersByFilteringBuilderReady WithLastName(string lastName);
            IGetUsersByFilteringBuilderReady WithUserEnabled(bool enabled);
            IGetUsersByFilteringBuilderReady WithEmailVerified(bool isEmailVerified);
            IGetUsersByFilteringBuilderReady WithBriefRepresentation(bool brief);
            IGetUsersByFilteringBuilderReady WithExact(bool isExact);
            IGetUsersByFilteringBuilderReady WithPaginationOffset(int paginationOffset);
            IGetUsersByFilteringBuilderReady WithMax(int max);
            IGetUsersByFilteringBuilderReady WithIdpAlias(string alias);
            IGetUsersByFilteringBuilderReady WithIdpUserId(string id);
        }

        public interface IGetUsersByFilteringBuilderReady : IGetUsersByFilteringBuilderStart
        {
            GetsUserRequestDto Build();
        }

        internal class GetUsersByFilteringBuilder : IGetUsersByFilteringBuilderReady
        {
            private readonly GetsUserRequestDto _dto = new GetsUserRequestDto();

            private GetUsersByFilteringBuilder() { }

            public static IGetUsersByFilteringBuilderStart Create() => new GetUsersByFilteringBuilder();
            public IGetUsersByFilteringBuilderReady WithEmail(string email)
            {
                if (!string.IsNullOrWhiteSpace(email))
                    _dto.Email = email;

                return this;
            }

            public IGetUsersByFilteringBuilderReady WithUsername(string username)
            {
                if (!string.IsNullOrWhiteSpace(username))
                    _dto.Username = username;

                return this;
            }

            public IGetUsersByFilteringBuilderReady WithFirstName(string firstName)
            {
                if (!string.IsNullOrWhiteSpace(firstName))
                    _dto.FirstName = firstName;

                return this;
            }

            public IGetUsersByFilteringBuilderReady WithLastName(string lastName)
            {
                if (!string.IsNullOrWhiteSpace(lastName))
                    _dto.LastName = lastName;

                return this;
            }

            public IGetUsersByFilteringBuilderReady WithIdpAlias(string alias)
            {
                if (!string.IsNullOrWhiteSpace(alias))
                    _dto.IdpAlias = alias;

                return this;
            }

            public IGetUsersByFilteringBuilderReady WithIdpUserId(string id)
            {
                if (!string.IsNullOrWhiteSpace(id))
                    _dto.IdpUserId = id;

                return this;
            }

            public IGetUsersByFilteringBuilderReady WithUserEnabled(bool isEnabled)
            {
                _dto.UserEnabled = isEnabled;
                return this;
            }

            public IGetUsersByFilteringBuilderReady WithEmailVerified(bool isEmailVerified)
            {
                _dto.IsEmailVerified = isEmailVerified;
                return this;
            }

            public IGetUsersByFilteringBuilderReady WithBriefRepresentation(bool brief)
            {
                _dto.BriefRepresentation = brief;
                return this;
            }

            public IGetUsersByFilteringBuilderReady WithExact(bool isExact)
            {
                _dto.IsExact = isExact;
                return this;
            }

            public IGetUsersByFilteringBuilderReady WithPaginationOffset(int paginationOffset)
            {
                _dto.PaginationOffset = paginationOffset;
                return this;
            }

            public IGetUsersByFilteringBuilderReady WithMax(int max)
            {
                _dto.MaxResultCount = max;
                return this;
            }

            public GetsUserRequestDto Build()
            {
                if (_dto == null)
                {
                    throw new Exception("Please populate atleast one search property.");
                }

                return _dto;
            }
        }
    }
}
