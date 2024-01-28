using Alaveri.Core;
using Alaveri.UnitTesting;

namespace Alaveri.Data.Test.Mocks
{
    public class MockDataSourceWriter : DataSourceWriter
    {
        public int InternalId { get; set; }

        public int SetGenericItem(GenericItem item)
        {
            InternalId = 1234;
            item.Id = 1234;
            return item.Id;
        }
    }
}
