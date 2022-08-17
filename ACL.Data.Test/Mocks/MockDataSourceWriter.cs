using ACL.Core;
using ACL.UnitTesting;

namespace ACL.Data.Test.Mocks
{
    public class MockDataSourceWriter : DataSourceWriter
    {
        public int InternalId { get; set; }

        public int SetGenericItem(GenericItem? item)
        {
            item = item ?? throw ExceptionFactory.CreateArgumentNullException(item);
            InternalId = 1234;
            item.Id = 1234;
            return item.Id;
        }
    }
}
