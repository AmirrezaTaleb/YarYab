using YarYab.Common.Helper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace YarYab.Test.Common.Helper
{
    public class StringHelper
    {
        [Theory]
        [InlineData("asdas", false)]
        [InlineData(" ", false)]

        public void HasValue_CheckStringNotEmpthy_ReturnTrue(string value, bool ignoreWhiteSpace = true)
        {
            value.HasValue(ignoreWhiteSpace).Should().BeTrue();
        }
        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData(" ", true)]
        [InlineData(null, true)]
        [InlineData("", true)]

        public void HasValue_CheckStringNotEmpthy_ReturnFalse(string value, bool ignoreWhiteSpace = true)
        {
            value.HasValue(ignoreWhiteSpace).Should().BeFalse();
        }
        [Theory]
        [InlineData("۱۲۳۴۵۶۷۸۹۰")]
        public void Fa2En_CheckAllFaNumber_ReturnEnNumber(string value)
        {
            value.Fa2En().Should().Be("1234567890");
        }
        [Theory]
        [InlineData("1234567890")]
        public void Fa2En_CheckAllEnNumber_ReturnFaNumber(string value)
        {
            value.En2Fa().Should().Be("۱۲۳۴۵۶۷۸۹۰");
        }
        [Theory]
        [InlineData(" AmirReza Taleb   ")]
        [InlineData("Amirreza taleb")]
        [InlineData("amirReza taleb")]
        [InlineData("amirreza taleb   ")]
        [InlineData("amirreza taleb")]
        public void FixText_ExmpleString_TrimAndToLower(string value)
        {
            value.FixText().Should().Be("amirreza taleb");
        }

    }
}
