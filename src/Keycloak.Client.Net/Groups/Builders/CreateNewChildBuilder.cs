using System;
using System.Collections.Generic;
using Keycloak.Client.Net.Groups.DTOs;

namespace Keycloak.Client.Net.Groups.Builders
{
    public static partial class GroupDtoBuilder
    {
        public interface ICreateNewChildBuilderStart
        {
            ICreateNewChildBuilderOptional WithName(string name);
        }

        public interface ICreateNewChildBuilderOptional
        {
            ICreateNewChildBuilderOptional WithAttributes(Dictionary<string, List<string>> attributes);
            GroupDto Build();
        }

        public class CreateNewChildBuilder : ICreateNewChildBuilderStart, ICreateNewChildBuilderOptional
        {
            private readonly GroupDto _group = new GroupDto();

            private CreateNewChildBuilder() { }

            public static ICreateNewChildBuilderStart Create() => new CreateNewChildBuilder();

            public ICreateNewChildBuilderOptional WithName(string name)
            {
                _group.Name = name;
                return this;
            }

            public ICreateNewChildBuilderOptional WithAttributes(Dictionary<string, List<string>> attributes)
            {
                _group.Attributes = attributes;
                return this;
            }

            public GroupDto Build()
            {
                if (string.IsNullOrEmpty(_group.Name))
                {
                    throw new InvalidOperationException($"{nameof(GroupDto.Name)} must be set before calling {nameof(ICreateNewChildBuilderOptional.Build)}.");
                }

                return _group;
            }
        }
    }
}
