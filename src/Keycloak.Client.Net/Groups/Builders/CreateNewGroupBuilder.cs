using System;
using System.Collections.Generic;
using System.Text;
using Keycloak.Client.Net.Groups.DTOs;

namespace Keycloak.Client.Net.Groups.Builders
{
    public static partial class GroupDtoBuilder
    {
        public interface ICreateNewGroupBuilderStart
        {
            ICreateNewGroupBuilderOptional WithName(string name);
        }

        public interface ICreateNewGroupBuilderOptional
        {
            ICreateNewGroupBuilderOptional WithAttributes(Dictionary<string, List<string>> attributes);
            GroupDto Build();
        }

        public class CreateNewGroupBuilder : ICreateNewGroupBuilderStart, ICreateNewGroupBuilderOptional
        {
            private readonly GroupDto _group = new GroupDto();

            private CreateNewGroupBuilder() { }

            public static ICreateNewGroupBuilderStart Create() => new CreateNewGroupBuilder();

            public ICreateNewGroupBuilderOptional WithName(string name)
            {
                _group.Name = name;
                return this;
            }

            public ICreateNewGroupBuilderOptional WithAttributes(Dictionary<string, List<string>> attributes)
            {
                _group.Attributes = attributes;
                return this;
            }

            public GroupDto Build()
            {
                if (string.IsNullOrEmpty(_group.Name))
                {
                    throw new InvalidOperationException($"{nameof(GroupDto.Name)} must be set before calling {nameof(ICreateNewGroupBuilderOptional.Build)}.");
                }

                return _group;
            }
        }
    }
}
