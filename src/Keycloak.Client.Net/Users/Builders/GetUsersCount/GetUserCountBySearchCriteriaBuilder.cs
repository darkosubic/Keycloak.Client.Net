using System;
using Keycloak.Client.Net.Users.DTOs;

namespace Keycloak.Client.Net.Users.Builders.GetUsersCount
{
    public static partial class GetUsersCountBuilder
    {
        public interface ICriteriaBuilder : ICriteriaBuilderStart
        {
            GetUsersCountDto Build();
        }

        public interface ICriteriaBuilderStart
        {
            ICriteriaBuilder WithFirstName(string firstName);
            ICriteriaBuilder WithLastName(string lastName);
            ICriteriaBuilder WithEmail(string email);
            ICriteriaBuilder WithEmailVerified(bool emailVerified);
            ICriteriaBuilder WithUsername(string username);
            ICriteriaBuilder WithEnabled(bool enabled);
            ICriteriaBuilder WithQuery(string query);
        }

        public class GetUserCountBySearchCriteria : ICriteriaBuilderStart, ICriteriaBuilder
        {
            private readonly GetUsersCountDto _dto = new GetUsersCountDto();

            private GetUserCountBySearchCriteria() { }

            public static ICriteriaBuilderStart Create() => new GetUserCountBySearchCriteria();

            public ICriteriaBuilder WithFirstName(string firstName)
            {
                _dto.FirstName = firstName;
                return this;
            }

            public ICriteriaBuilder WithLastName(string lastName)
            {
                _dto.LastName = lastName;
                return this;
            }

            public ICriteriaBuilder WithEmail(string email)
            {
                _dto.Email = email;
                return this;
            }

            public ICriteriaBuilder WithEmailVerified(bool emailVerified)
            {
                _dto.EmailVerified = emailVerified;
                return this;
            }

            public ICriteriaBuilder WithUsername(string username)
            {
                _dto.Username = username;
                return this;
            }

            public ICriteriaBuilder WithEnabled(bool enabled)
            {
                _dto.Enabled = enabled;
                return this;
            }

            public ICriteriaBuilder WithQuery(string query)
            {
                _dto.SearchQuery = query;
                return this;
            }

            public GetUsersCountDto Build()
            {
                bool hasCriteriaBeenSet =
                            _dto.FirstName != null ||
                            _dto.LastName != null ||
                            _dto.Email != null ||
                            _dto.EmailVerified.HasValue ||
                            _dto.Username != null ||
                            _dto.Enabled.HasValue ||
                            _dto.SearchQuery != null;

                if (!hasCriteriaBeenSet)
                {
                    throw new InvalidOperationException($"At least one filter criteria must be set before calling {nameof(ICriteriaBuilder.Build)}.");
                }

                return _dto;
            }
        }
    }
}
