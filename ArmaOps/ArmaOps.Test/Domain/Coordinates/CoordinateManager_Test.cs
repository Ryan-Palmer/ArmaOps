using ArmaOps.Domain.Coordinates;
using AutoFixture.NUnit3;
using NUnit.Framework;

namespace ArmaOps.Test.Domain.Coordinates
{
    [TestFixture]
    public class CoordinateManager_Test
    {
        [Test, AutoData]
        public void OriginPolarConvertsToCartesian 
            (CoordinateManager sut)
        {
            var originFO = new Cartesian(0, 0, 0);
            var originPolar = new Polar(originFO, 0, 0, 1);
            var expectedResult = new Cartesian(1, 0, 0);
            var result = sut.ToCartesian(originPolar);

            Assert.That(result.X, Is.EqualTo(expectedResult));
        }
    }
}
