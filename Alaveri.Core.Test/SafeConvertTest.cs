using FluentAssertions;

namespace Alaveri.Core.Test
{
    [TestClass]
    public class SafeConvertTest
    {
        [TestMethod]
        public void ChangeTypeTest()
        {
            var numberString = "1234";
            var number = SafeConvert.ChangeType<int>(numberString, -1);
            number.Should().Be(1234);
        }

        [TestMethod]
        public void ToBoolTest()
        {
            string? boolString = "y";
            var value = SafeConvert.ToBoolean(boolString);
            value.Should().BeTrue();

            boolString = "Y";
            value = SafeConvert.ToBoolean(boolString);
            value.Should().BeTrue();

            boolString = "Yes";
            value = SafeConvert.ToBoolean(boolString);
            value.Should().BeTrue();

            boolString = "yes";
            value = SafeConvert.ToBoolean(boolString);
            value.Should().BeTrue();

            boolString = "yEs";
            value = SafeConvert.ToBoolean(boolString);
            value.Should().BeTrue();

            boolString = "1";
            value = SafeConvert.ToBoolean(boolString);
            value.Should().BeTrue();

            boolString = "yesd";
            value = SafeConvert.ToBoolean(boolString);
            value.Should().BeFalse();

            boolString = "n";
            value = SafeConvert.ToBoolean(boolString);
            value.Should().BeFalse();

            boolString = "no";
            value = SafeConvert.ToBoolean(boolString);
            value.Should().BeFalse();

            boolString = "NO";
            value = SafeConvert.ToBoolean(boolString);
            value.Should().BeFalse();

            boolString = "No";
            value = SafeConvert.ToBoolean(boolString);
            value.Should().BeFalse();

            boolString = "0";
            value = SafeConvert.ToBoolean(boolString);
            value.Should().BeFalse();

            boolString = null;
            value = SafeConvert.ToBoolean(boolString);
            value.Should().BeFalse();

            boolString = null;
            value = SafeConvert.ToBoolean(boolString, true);
            value.Should().BeTrue();

            value = SafeConvert.ToBoolean(DBNull.Value);
            value.Should().BeFalse();
        }

        [TestMethod]
        public void ToByteTest()
        {
            var value = SafeConvert.ToByte("245", 0);
            value.Should().Be(245);

            value = SafeConvert.ToByte("fadsf", 255);
            value.Should().Be(255);

            value = SafeConvert.ToByte("234112", 255);
            value.Should().Be(255);

            value = SafeConvert.ToByte("241.25", 255);
            value.Should().Be(255);
        }

        [TestMethod]
        public void ToCharTest()
        {
            var value = SafeConvert.ToChar("a", 'z');
            value.Should().Be('a');

            value = SafeConvert.ToChar("65", 'z');
            value.Should().Be('z');
        }

        [TestMethod]
        public void ToDateTimeTest()
        {
            var value = SafeConvert.ToDateTime("1/1/2029", default);
            value.Should().Be(new DateTime(2029, 1, 1));

            value = SafeConvert.ToDateTime("13/1/2029", default);
            value.Should().Be(default);
        }

        [TestMethod]
        public void ToDecimalTest()
        {
            var value = SafeConvert.ToDecimal("23.55", -1);
            value.Should().Be(23.55m);
            
            value = SafeConvert.ToDecimal("79228162514264337593543950335", -1);
            value.Should().Be(79228162514264337593543950335m);

            value = SafeConvert.ToDecimal("79228162514264337593543950336", -1);
            value.Should().Be(-1);
        }

        [TestMethod]
        public void ToDoubleTest()
        {
            var value = SafeConvert.ToDouble("23.55", -1);
            value.Should().Be(23.55);

            value = SafeConvert.ToDouble("1.79769313486231e308", -1);
            value.Should().Be(1.79769313486231e308D);

            value = SafeConvert.ToDouble("1.79769313486233e308", -1);
            value.Should().Be(-1);
        }

        [TestMethod]
        public void ToInt16Test()
        {
            var value = SafeConvert.ToInt16("2456", 0);
            value.Should().Be(2456);

            value = SafeConvert.ToInt16("fadsf", 255);
            value.Should().Be(255);

            value = SafeConvert.ToInt16("65536", 255);
            value.Should().Be(255);

            value = SafeConvert.ToInt16("241.25", 255);
            value.Should().Be(255);
        }

        [TestMethod]
        public void ToInt32Test()
        {
            var value = SafeConvert.ToInt32("65536", 0);
            value.Should().Be(65536);

            value = SafeConvert.ToInt32("fadsf", 255);
            value.Should().Be(255);

            value = SafeConvert.ToInt32("3123123123", 255);
            value.Should().Be(255);

            value = SafeConvert.ToInt32("241.25", 255);
            value.Should().Be(255);
        }

        [TestMethod]
        public void ToInt64Test()
        {
            var value = SafeConvert.ToInt64("3123123123", 0);
            value.Should().Be(3123123123L);

            value = SafeConvert.ToInt64("fadsf", 255);
            value.Should().Be(255);

            value = SafeConvert.ToInt64("9223372036854775808", 255);
            value.Should().Be(255);

            value = SafeConvert.ToInt64("241.25", 255);
            value.Should().Be(255);
        }

        [TestMethod]
        public void ToSByteTest()
        {
            var value = SafeConvert.ToSByte("123", 0);
            value.Should().Be(123);

            value = SafeConvert.ToSByte("fadsf", -1);
            value.Should().Be(-1);

            value = SafeConvert.ToSByte("129", -1);
            value.Should().Be(-1);

            value = SafeConvert.ToSByte("241.25", -1);
            value.Should().Be(-1);
        }

        [TestMethod]
        public void ToSingleTest()
        {
            var value = SafeConvert.ToSingle("23.55", -1);
            value.Should().Be(23.55f);

            value = SafeConvert.ToSingle("3.402822e38", -1);
            value.Should().Be(3.402822e38f);

            value = SafeConvert.ToSingle("3.402824e38", -1);
            value.Should().Be(-1);
        }

        [TestMethod]
        public void ToStringTest()
        {
            var test = 12;
            var value = SafeConvert.ToString(test, null);
            value.Should().Be("12");

            value = SafeConvert.ToString(null, "Something");
            value.Should().Be("Something");

            value = SafeConvert.ToString(DBNull.Value, "Something");
            value.Should().Be("Something");
        }

        [TestMethod]
        public void ToUInt16Test()
        {
            var value = SafeConvert.ToUInt16("2456", 0);
            value.Should().Be(2456);

            value = SafeConvert.ToUInt16("fadsf", 255);
            value.Should().Be(255);

            value = SafeConvert.ToUInt16("-1", 255);
            value.Should().Be(255);

            value = SafeConvert.ToUInt16("241.25", 255);
            value.Should().Be(255);
        }

        [TestMethod]
        public void TUInt32Test()
        {
            var value = SafeConvert.ToUInt32("65536", 0);
            value.Should().Be(65536);

            value = SafeConvert.ToUInt32("fadsf", 255);
            value.Should().Be(255);

            value = SafeConvert.ToUInt32("-1", 255);
            value.Should().Be(255);

            value = SafeConvert.ToUInt32("241.25", 255);
            value.Should().Be(255);
        }

        [TestMethod]
        public void ToUInt64Test()
        {
            var value = SafeConvert.ToUInt64("3123123123", 0);
            value.Should().Be(3123123123L);

            value = SafeConvert.ToUInt64("fadsf", 255);
            value.Should().Be(255);

            value = SafeConvert.ToUInt64("-1", 255);
            value.Should().Be(255);

            value = SafeConvert.ToUInt64("241.25", 255);
            value.Should().Be(255);
        }
    }
}