using FluentAssertions;
using Alaveri.Core.Extensions.Conversion;

namespace Alaveri.Core.Test
{
    [TestClass]
    public class SafeConvertExtensionsTest
    {
        [TestMethod]
        public void ChangeTypeTest()
        {
            var numberString = "1234";
            var number = numberString.AsInt32(-1);
            number.Should().Be(1234);

            numberString = "123s4";
            number = numberString.AsInt32(-1);
            number.Should().Be(-1);
        }

        [TestMethod]
        public void AsBoolTest()
        {
            string boolString = "y";
            var value = boolString.AsBoolean();
            value.Should().BeTrue();

            boolString = "Y";
            value = boolString.AsBoolean();
            value.Should().BeTrue();

            boolString = "Yes";
            value = boolString.AsBoolean();
            value.Should().BeTrue();

            boolString = "yes";
            value = boolString.AsBoolean();
            value.Should().BeTrue();

            boolString = "yEs";
            value = boolString.AsBoolean();
            value.Should().BeTrue();

            boolString = "1";
            value = boolString.AsBoolean();
            value.Should().BeTrue();

            boolString = "yesd";
            value = boolString.AsBoolean();
            value.Should().BeFalse();

            boolString = "n";
            value = boolString.AsBoolean();
            value.Should().BeFalse();

            boolString = "no";
            value = boolString.AsBoolean();
            value.Should().BeFalse();

            boolString = "NO";
            value = boolString.AsBoolean();
            value.Should().BeFalse();

            boolString = "No";
            value = boolString.AsBoolean();
            value.Should().BeFalse();

            boolString = "0";
            value = boolString.AsBoolean();
            value.Should().BeFalse();

            value = DBNull.Value.AsBoolean();
            value.Should().BeFalse();
        }

        [TestMethod]
        public void AsByteTest()
        {
            var value = "245".AsByte(0);
            value.Should().Be(245);

            value = "fadsf".AsByte(255);
            value.Should().Be(255);

            value = SafeConvert.ToByte("234112", 255);
            value.Should().Be(255);

            value = SafeConvert.ToByte("241.25", 255);
            value.Should().Be(255);
        }

        [TestMethod]
        public void AsCharTest()
        {
            var value = "a".AsChar('z');
            value.Should().Be('a');

            value = "65".AsChar('z');
            value.Should().Be('z');
        }

        [TestMethod]
        public void AsDateTimeTest()
        {
            var value = "1/1/2029".AsDateTime(default);
            value.Should().Be(new DateTime(2029, 1, 1));

            value = "13/1/2029".AsDateTime(default);
            value.Should().Be(default);
        }

        [TestMethod]
        public void AsDecimalTest()
        {
            var value = "23.55".AsDecimal(-1);
            value.Should().Be(23.55m);

            value = "79228162514264337593543950335".AsDecimal(-1);
            value.Should().Be(79228162514264337593543950335m);

            value = "79228162514264337593543950336".AsDecimal(-1);
            value.Should().Be(-1);
        }

        [TestMethod]
        public void AsDoubleTest()
        {
            var value = "23.55".AsDouble(-1);
            value.Should().Be(23.55);

            value = "1.79769313486231e308".AsDouble(-1);
            value.Should().Be(1.79769313486231e308D);

            value = "1.79769313486233e308".AsDouble(-1);
            value.Should().Be(-1);
        }

        [TestMethod]
        public void AsInt16Test()
        {
            var value = "2456".AsInt16(0);
            value.Should().Be(2456);

            value = "fadsf".AsInt16(255);
            value.Should().Be(255);

            value = "65536".AsInt16(255);
            value.Should().Be(255);

            value = "241.25".AsInt16(255);
            value.Should().Be(255);
        }

        [TestMethod]
        public void AsInt32Test()
        {
            var value = "65536".AsInt32(0);
            value.Should().Be(65536);

            value = "fadsf".AsInt32(255);
            value.Should().Be(255);

            value = "3123123123".AsInt32(255);
            value.Should().Be(255);

            value = "241.25".AsInt32(255);
            value.Should().Be(255);
        }

        [TestMethod]
        public void AsInt64Test()
        {
            var value = "3123123123".AsInt64(0);
            value.Should().Be(3123123123L);

            value = "fadsf".AsInt64(255);
            value.Should().Be(255);

            value = "9223372036854775808".AsInt64(255);
            value.Should().Be(255);

            value = "241.25".AsInt64(255);
            value.Should().Be(255);
        }

        [TestMethod]
        public void AsSByteTest()
        {
            var value = "123".AsSByte(0);
            value.Should().Be(123);

            value = "fadsf".AsSByte(-1);
            value.Should().Be(-1);

            value = "129".AsSByte(-1);
            value.Should().Be(-1);

            value ="241.25".AsSByte(-1);
            value.Should().Be(-1);
        }

        [TestMethod]
        public void AsSingleTest()
        {
            var value = "23.55".AsSingle(-1);
            value.Should().Be(23.55f);

            value = "3.402822e38".AsSingle(-1);
            value.Should().Be(3.402822e38f);

            value = "3.402824e38".AsSingle(-1);
            value.Should().Be(-1);
        }

        [TestMethod]
        public void AsStringTest()
        {
            var test = 12;
            var value = test.AsString(null);
            value.Should().Be("12");

            object obj = null;
            value = obj.AsString("Something");
            value.Should().Be("Something");

            value = DBNull.Value.AsString("Something");
            value.Should().Be("Something");
        }

        [TestMethod]
        public void AsUInt16Test()
        {
            var value = "2456".AsUInt16(0);
            value.Should().Be(2456);

            value = "fadsf".AsUInt16(255);
            value.Should().Be(255);

            value = "-1".AsUInt16(255);
            value.Should().Be(255);

            value = "241.25".AsUInt16(255);
            value.Should().Be(255);
        }

        [TestMethod]
        public void AsUInt32Test()
        {
            var value = "65536".AsUInt32(0);
            value.Should().Be(65536);

            value = "fadsf".AsUInt32(255);
            value.Should().Be(255);

            value = "-1".AsUInt32(255);
            value.Should().Be(255);

            value = "241.25".AsUInt32(255);
            value.Should().Be(255);
        }

        [TestMethod]
        public void AsUInt64Test()
        {
            var value = "3123123123".AsUInt64(0);
            value.Should().Be(3123123123L);

            value = "fadsf".AsUInt64(255);
            value.Should().Be(255);

            value = "-1".AsUInt64(255);
            value.Should().Be(255);

            value = "241.25".AsUInt64(255);
            value.Should().Be(255);
        }
    }
}