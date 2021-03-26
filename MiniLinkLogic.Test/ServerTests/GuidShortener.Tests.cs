using MiniLink.Server.Utilities;
using System;
using Xunit;

namespace MiniLink.Tests
{
    public class TestGuidEncoder
    {

        /// <summary>
        /// proves that encoder encodes and decodes correctly
        /// 
        /// </summary>
        [Fact]
        public void ValidCase()
        {
            Guid guidTotest = Guid.NewGuid();

            var encoded = GuidShortener.EncodeGuid(guidTotest);
            var decoded = GuidShortener.DecodeGuid(encoded);

            Assert.Equal(guidTotest, decoded);          
        }
        /// <summary>
        /// Ensure string length is 22
        /// </summary>
        [Fact]
        public void EncodedIdLengthIs22()
        {
            Guid guidTotest = Guid.NewGuid();

            var encoded = GuidShortener.EncodeGuid(guidTotest);

            Assert.Equal(22, encoded.Length);
        }
    }
}
