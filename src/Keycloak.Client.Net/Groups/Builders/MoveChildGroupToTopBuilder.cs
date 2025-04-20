using System;
using Keycloak.Client.Net.Groups.DTOs;

namespace Keycloak.Client.Net.Groups.Builders
{
    public static partial class GroupDtoBuilder
    {
        public interface IMoveChildGroupToTopBuilderStart
        {
            IMoveChildGroupToTopBuilderReady WithId(string id);
        }

        public interface IMoveChildGroupToTopBuilderReady
        {
            GroupDto Build();
        }

        public class MoveChildGroupToTopBuilder : IMoveChildGroupToTopBuilderStart, IMoveChildGroupToTopBuilderReady
        {
            private readonly GroupDto _group = new GroupDto();

            private MoveChildGroupToTopBuilder() { }

            public static IMoveChildGroupToTopBuilderStart Create() => new MoveChildGroupToTopBuilder();

            public IMoveChildGroupToTopBuilderReady WithId(string id)
            {
                _group.Id = id;
                return this;
            }

            public GroupDto Build()
            {
                if (string.IsNullOrEmpty(_group.Id))
                {
                    throw new InvalidOperationException($"{nameof(GroupDto.Id)} must be set before calling {nameof(IMoveChildGroupToTopBuilderReady.Build)}.");
                }

                //BUGFIX, group name is not used when moving child to the top but is required
                _group.Name = "placeholder";

                return _group;
            }
        }
    }
}
