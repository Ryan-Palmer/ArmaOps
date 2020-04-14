using ArmaOps.Domain;
using ArmaOps.Domain.Coordinates;
using AutoFixture.NUnit3;
using NUnit.Framework;

namespace ArmaOps.Test.Domain.Coordinates
{
    [TestFixture]
    public class CoordinateManager_Test
    {
        [Test, AutoData]
        public void OriginPolarConvertsToCartesian(
            CoordinateManager sut,
            string foName)
        {
            var originFO = new ForwardObserver(new Cartesian(0, 0, 0), foName);
            var originPolar = new Polar(originFO, 0, 0, 1);
            var expectedResult = new Cartesian(0, 1, 0);

            var result = sut.ToCartesian(originPolar);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test, AutoData]
        public void Origin2DCartesianConvertsToPolar(
            CoordinateManager sut,
            string foName)
        {
            var originFO = new ForwardObserver(new Cartesian(0, 0, 0), foName);
            var originTarget = new Cartesian(0, 1, 0);
            var expectedResult = new Polar(originFO, 0, 1, 0);

            var result = sut.ToPolar(originFO, originTarget);

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
