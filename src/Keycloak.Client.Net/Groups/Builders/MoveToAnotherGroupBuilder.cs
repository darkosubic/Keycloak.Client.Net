using System;
using System.Collections.Generic;
using System.Text;
using Keycloak.Client.Net.Groups.DTOs;

namespace Keycloak.Client.Net.Groups.Builders
{
    public static partial class GroupDtoBuilder
    {
        public interface IMoveToAnotherGroupBuilderStart
        {
            IMoveToAnotherGroupBuilderReady WithId(string id);
        }

        public interface IMoveToAnotherGroupBuilderReady
        {
            GroupDto Build();
        }

        public class MoveToAnotherGroupBuilder : IMoveToAnotherGroupBuilderStart, IMoveToAnotherGroupBuilderReady
        {
            private readonly GroupDto _group = new GroupDto();

            private MoveToAnotherGroupBuilder() { }

            public static IMoveToAnotherGroupBuilderStart Create() => new MoveToAnotherGroupBuilder();

            public IMoveToAnotherGroupBuilderReady WithId(string id)
            {
                _group.Id = id;
                return this;
            }

            public GroupDto Build()
            {
                if (string.IsNullOrEmpty(_group.Id))
                {
                    throw new InvalidOperationException($"{nameof(GroupDto.Id)} must be set before calling {nameof(IMoveToAnotherGroupBuilderReady.Build)}.");
                }

                //BUGFIX, group name is not used when moving child to the top but is required
                _group.Name = "placeholder";

                return _group;
            }
        }
    }
}
