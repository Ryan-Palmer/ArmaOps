using ArmaOps.Domain;
using ArmaOps.Domain.Coordinates;
using AutoFixture.NUnit3;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ArmaOps.Test.Domain.Coordinates
{
    [TestFixture]
    public class Polar_Test
    {
        [Test, AutoData]
        public void OriginPolarConvertsToCartesian()
        {
            var originFO = new Cartesian(0, 0, 0);
            var originPolar = new Polar(originFO, 0, 0, 1);
            var expectedResult = new Cartesian(0, 0, 1);

            var result = originPolar.ToCartesian();

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test, AutoData]
        public void EqualsTrueIfAllValuesMatch(Polar sut)
        {
            var sameValues = new Polar(sut.Origin, sut.Azimuth, sut.Elevation, sut.Distance);

            Assert.That(sameValues, Is.EqualTo(sut));
        }

        [Test, AutoData]
        public void EqualsFalseIfOriginMismatch(
            Polar sut,
            Cartesian origin)
        {
            var other = new Polar(origin, sut.Azimuth, sut.Elevation, sut.Distance);

            Assert.That(other, Is.Not.EqualTo(sut));
        }

        [Test, AutoData]
        public void EqualsFalseIfAzimuthMismatch(
            Polar sut,
            double azimuth)
        {
            var other = new Polar(sut.Origin, azimuth, sut.Elevation, sut.Distance);

            Assert.That(other, Is.Not.EqualTo(sut));
        }

        [Test, AutoData]
        public void EqualsFalseIfElevationMismatch(
            Polar sut,
            double elevation)
        {
            var other = new Polar(sut.Origin, sut.Azimuth, elevation, sut.Distance);

            Assert.That(other, Is.Not.EqualTo(sut));
        }

        [Test, AutoData]
        public void EqualsFalseIfDistanceMismatch(
            Polar sut,
            double distance)
        {
            var other = new Polar(sut.Origin, sut.Azimuth, sut.Elevation, distance);

            Assert.That(other, Is.Not.EqualTo(sut));
        }

        [Test, AutoData]
        public void GetHashCodeMatchCorrectlyImplemented(Polar sut)
        {
            var sameValues = new Polar(sut.Origin, sut.Azimuth, sut.Elevation, sut.Distance);

            Assert.That(sameValues.GetHashCode(), Is.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void GetHashCodeNoMatchCorrectlyImplemented(
            Polar sut,
            Polar other)
        {
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void OriginPolarToHDist()
        {
            var originFO = new Cartesian(0, 0, 0);
            var originPolar = new Polar(originFO, 0, Math.PI / 4, Math.Sqrt(2));
            var expectedHDist = Math.Sqrt(Math.Pow(Math.Sqrt(2),2) - Math.Pow(1,2));

            var resultHDist = originPolar.HDist;

            Assert.That(resultHDist, Is.EqualTo(expectedHDist));
        }

        [Test, AutoData]
        public void OriginPolarToVDist()
        {
            var originFO = new Cartesian(0, 0, 0);
            var originPolar = new Polar(originFO, 0, Math.PI / 4, Math.Sqrt(2));
            var expectedVDist = Math.Sqrt(Math.Pow(Math.Sqrt(2), 2) - Math.Pow(1, 2));

            var resultVDist = originPolar.VDist;

            Assert.That(resultVDist, Is.EqualTo(expectedVDist));
        }
    }
}
