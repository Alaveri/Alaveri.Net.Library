using FluentAssertions;
using ACL.Data.Test.Mocks;
using ACL.UnitTesting;

namespace ACL.Data.Test
{
    [TestClass]
    public class Tests
    {

        [TestMethod]
        public void DataSourceReaderTest()
        {
            var dataSource = new MockDataSource(new MockDataSourceReader(), new MockDataSourceWriter());
            var item = dataSource.RetrieveItem(42);
            item.Id.Should().Be(42);
        }

        [TestMethod]
        public void DataSourceWriteTest()
        {
            var dataSource = new MockDataSource(new MockDataSourceReader(), new MockDataSourceWriter());
            var item = new GenericItem();
            var id = dataSource.StoreItem(item);
            id.Should().Be(1234);
        }
    }
}