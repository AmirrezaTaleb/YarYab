using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using YarYab.Common.Helper;
using System.ComponentModel;

namespace YarYab.Test.Common.Helper
{
    public class SecurityHelper
    {
        [Theory]
        [InlineData(5, typeof(int))]
        [InlineData(0, typeof(int))]
        [InlineData(0, typeof(string))]
        [InlineData(7, typeof(string))]
        [InlineData(9, typeof(int))]
        [InlineData(3, typeof(string))]
        [InlineData(3, typeof(double))]
        public void CreateRandomCode_RandomCodeLengthAndType_CreateRandormCodeLengthAndType(int RandomCodeLength, Type type)
        {
            // Test Length
            string RandomCode = YarYab.Common.Helper.SecurityHelper.CreateRandomCode(RandomCodeLength, type);
            RandomCode.Should().HaveLength(RandomCodeLength);
            // Test Type
            TypeConverter typeConverter = TypeDescriptor.GetConverter(type);
            object propValue = typeConverter.ConvertFromString(RandomCode);
            type.Should().Be(propValue.GetType());
        }
    }
}
