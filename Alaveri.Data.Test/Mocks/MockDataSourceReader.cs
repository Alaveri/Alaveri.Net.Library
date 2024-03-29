﻿using Alaveri.UnitTesting;
using Alaveri.Core.Extensions.Conversion;

namespace Alaveri.Data.Test.Mocks
{
    public class MockDataSourceReader : DataSourceReader
    {
        private int InternalId { get; set; }

        public GenericItem GetGenericItem(int id)
        {
            InternalId = id;
            var result = new GenericItem()
            {
                Id = InternalId,
                Int32Property = InternalId,
                SingleProperty = Math.PI.AsSingle(),
                StringProperty = "Bob"
            };
            result.SubItems.Add(new GenericSubItem(1, "Item 1"));
            return result;
        }
    }
}
