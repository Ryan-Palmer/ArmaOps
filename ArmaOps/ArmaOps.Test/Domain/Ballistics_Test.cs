using ArmaOps.Domain;
using ArmaOps.Domain.Coordinates;
using AutoFixture.NUnit3;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Test.Domain
{
    [TestFixture]
    public class Ballistics_Test
    {
        const double EARTH_G = 9.80665;

        [Test]
        [InlineAutoData(96.8, 1000, 0)]
        [InlineAutoData(96.8, 1259, 0)]
        [InlineAutoData(96.8, 1585, 0)]
        [InlineAutoData(96.8, 1995, 0)]
        [InlineAutoData(96.8, 2512, 0)]
        [InlineAutoData(96.8, 3162, 0)]
        [InlineAutoData(96.8, 3981, 0)]
        [InlineAutoData(96.8, 5012, 0)]
        [InlineAutoData(96.8, 6310, 0)]
        [InlineAutoData(96.8, 7943, 0)]
        [InlineAutoData(96.8, 10000, 0)]
        [InlineAutoData(96.8, 12589, 0)]
        [InlineAutoData(96.8, 15849, 0)]
        [InlineAutoData(96.8, 19953, 0)]
        [InlineAutoData(96.8, 25119, 0)]
        [InlineAutoData(96.8, 31623, 0)]
        [InlineAutoData(96.8, 39811, 0)]
        [InlineAutoData(96.8, 50119, 0)]
        [InlineAutoData(96.8, 63096, 0)]
        [InlineAutoData(96.8, 79433, 0)]
        [InlineAutoData(96.8, 100000, 0)]
        [InlineAutoData(120.4, 100000, 0)]
        [InlineAutoData(141.9, 100000, 0)]
        [InlineAutoData(106.7, 100000, 0)]
        [InlineAutoData(115.5, 100000, 0)]
        [InlineAutoData(124.3, 100000, 0)]
        [InlineAutoData(133.5, 100000, 0)]
        [InlineAutoData(138.0, 100000, 0)]
        [InlineAutoData(207.0, 100000, 0)]
        [InlineAutoData(299.0, 100000, 0)]
        [InlineAutoData(153.9, 100000, 0)]
        [InlineAutoData(243.0, 100000, 0)]
        [InlineAutoData(388.8, 100000, 0)]
        [InlineAutoData(648.0, 100000, 0)]
        [InlineAutoData(810.0, 100000, 0)]
        public void UnviableValuesOnEarthReturnNull(double v, double x, double y)
        {
            var sut = new Ballistics(EARTH_G);
            var result = sut.GetSolutions(v, x, y);

            Assert.That(result, Is.Null);
        }

        [Test]
        [InlineAutoData( 96.8,   100, 0)]
        [InlineAutoData(120.4,   100, 0)]
        [InlineAutoData(141.9,   100, 0)]
        [InlineAutoData(106.7,   100, 0)]
        [InlineAutoData(115.5,   100, 0)]
        [InlineAutoData(124.3,   100, 0)]
        [InlineAutoData(133.5,   100, 0)]
        [InlineAutoData(138.0,   100, 0)]
        [InlineAutoData(207.0,   100, 0)]
        [InlineAutoData(299.0,   100, 0)]
        [InlineAutoData(153.9,   100, 0)]
        [InlineAutoData(243.0,   100, 0)]
        [InlineAutoData(388.8,   100, 0)]
        [InlineAutoData(648.0,   100, 0)]
        [InlineAutoData(810.0,   100, 0)]
        [InlineAutoData(810.0,   126, 0)]
        [InlineAutoData(810.0,   158, 0)]
        [InlineAutoData(810.0,   200, 0)]
        [InlineAutoData(810.0,   251, 0)]
        [InlineAutoData(810.0,   316, 0)]
        [InlineAutoData(810.0,   398, 0)]
        [InlineAutoData(810.0,   501, 0)]
        [InlineAutoData(810.0,   631, 0)]
        [InlineAutoData(810.0,   794, 0)]
        [InlineAutoData(810.0,  1000, 0)]
        [InlineAutoData(810.0,  1259, 0)]
        [InlineAutoData(810.0,  1585, 0)]
        [InlineAutoData(810.0,  1995, 0)]
        [InlineAutoData(810.0,  2512, 0)]
        [InlineAutoData(810.0,  3162, 0)]
        [InlineAutoData(810.0,  3981, 0)]
        [InlineAutoData(810.0,  5012, 0)]
        [InlineAutoData(810.0,  6310, 0)]
        [InlineAutoData(810.0,  7943, 0)]
        [InlineAutoData(810.0, 10000, 0)]
        [InlineAutoData(810.0, 12589, 0)]
        [InlineAutoData(810.0, 15849, 0)]
        [InlineAutoData(810.0, 19953, 0)]
        [InlineAutoData(810.0, 25119, 0)]
        [InlineAutoData(810.0, 31623, 0)]
        [InlineAutoData(810.0, 39811, 0)]
        [InlineAutoData(810.0, 50119, 0)]
        [InlineAutoData(810.0, 63096, 0)]
        public void ViableValuesOnEarthReturnSolutions(double v, double x, double y)
        {
            var sut = new Ballistics(EARTH_G);
            var result = sut.GetSolutions(v, x, y);

            Assert.That(result, Is.Not.Null);
        }
    }
}
