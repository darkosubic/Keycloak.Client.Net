using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Keycloak.Client.Net.Groups.DTOs;

namespace Keycloak.Client.Net.Groups.Builders
{
    public static partial class GroupDtoBuilder
    {
        public interface IUpdateGroupBuilderStart
        {
            IUpdateGroupBuilderRequireName WithId(string id);
        }

        public interface IUpdateGroupBuilderRequireName
        {
            IUpdateGroupBuilderAttributes WithName(string name);
        }

        public interface IUpdateGroupBuilderAttributes
        {
            IUpdateGroupBuilderReady WithAttributes(Dictionary<string, List<string>> attributes);
            IUpdateGroupBuilderReady WithoNoAttributes();
        }

        public interface IUpdateGroupBuilderReady
        {
            GroupDto Build();
        }

        public class UpdateGroupBuilder : IUpdateGroupBuilderStart, IUpdateGroupBuilderRequireName, IUpdateGroupBuilderAttributes, IUpdateGroupBuilderReady
        {
            private readonly GroupDto _group = new GroupDto();

            private UpdateGroupBuilder() { }

            public static IUpdateGroupBuilderStart Create() => new UpdateGroupBuilder();

            public IUpdateGroupBuilderRequireName WithId(string id)
            {
                _group.Id = id;
                return this;
            }

            public IUpdateGroupBuilderAttributes WithName(string name)
            {
                _group.Name = name;
                return this;
            }

            public IUpdateGroupBuilderReady WithAttributes(Dictionary<string, List<string>> attributes)
            {
                _group.Attributes = attributes;
                return this;
            }

            public IUpdateGroupBuilderReady WithoNoAttributes()
            {
                return this;
            }

            public GroupDto Build()
            {
                if (string.IsNullOrEmpty(_group.Id))
                {
                    throw new InvalidOperationException($"{nameof(GroupDto.Id)} must be set before calling {nameof(IUpdateGroupBuilderReady.Build)}.");
                }
                if (string.IsNullOrEmpty(_group.Name))
                {
                    throw new InvalidOperationException($"{nameof(GroupDto.Name)} must be set before calling {nameof(IUpdateGroupBuilderReady.Build)}.");
                }

                return _group;
            }
        }
    }
}
