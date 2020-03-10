namespace ProvisionData.ResourceHelpr.UnitTests
{
    using FluentAssertions;
    using ProvisionData.ResourceHelpr.UnitTests.Resources;
    using Xunit;

    public class Specifications
    {
        [Fact]
        public void Must_return_a_String_this_assembly()
        {
            RH.GS<Specifications>("TextFile.txt").Should().Be("This is a text file.");
        }

        [Fact]
        public void Must_return_a_String_from_a_different_assembly()
        {
            RH.GS<Key>("TextFile.txt").Should().Be("This is a different text file.");
        }
    }
}
