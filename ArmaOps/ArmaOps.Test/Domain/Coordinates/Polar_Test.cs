using ArmaOps.Domain;
using ArmaOps.Domain.Coordinates;
using AutoFixture.NUnit3;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Test.Domain.Coordinates
{
    [TestFixture]
    public class Polar_Test
    {
        [Test, AutoData]
        public void OriginPolarConvertsToCartesian(
            string foName)
        {
            var originFO = new ForwardObserver(new Cartesian(0, 0, 0), foName);
            var originPolar = new Polar(originFO, 0, 0, 1);
            var expectedResult = new Cartesian(0, 1, 0);

            var result = originPolar.ToCartesian();

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test, AutoData]
        public void EqualsTrueIfAllValuesMatch(Polar sut)
        {
            var sameValues = new Polar(sut.ForwardObserver, sut.Azimuth, sut.Elevation, sut.Distance);

            Assert.That(sameValues, Is.EqualTo(sut));
        }

        [Test, AutoData]
        public void EqualsFalseIfFOMismatch(
            Polar sut,
            ForwardObserver fo)
        {
            var other = new Polar(fo, sut.Azimuth, sut.Elevation, sut.Distance);

            Assert.That(other, Is.Not.EqualTo(sut));
        }

        [Test, AutoData]
        public void EqualsFalseIfAzimuthMismatch(
            Polar sut,
            double azimuth)
        {
            var other = new Polar(sut.ForwardObserver, azimuth, sut.Elevation, sut.Distance);

            Assert.That(other, Is.Not.EqualTo(sut));
        }

        [Test, AutoData]
        public void EqualsFalseIfElevationMismatch(
            Polar sut,
            double elevation)
        {
            var other = new Polar(sut.ForwardObserver, sut.Azimuth, elevation, sut.Distance);

            Assert.That(other, Is.Not.EqualTo(sut));
        }

        [Test, AutoData]
        public void EqualsFalseIfDistanceMismatch(
            Polar sut,
            double distance)
        {
            var other = new Polar(sut.ForwardObserver, sut.Azimuth, sut.Elevation, distance);

            Assert.That(other, Is.Not.EqualTo(sut));
        }

        [Test, AutoData]
        public void GetHashCodeMatchCorrectlyImplemented(Polar sut)
        {
            var sameValues = new Polar(sut.ForwardObserver, sut.Azimuth, sut.Elevation, sut.Distance);

            Assert.That(sameValues.GetHashCode(), Is.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void GetHashCodeNoMatchCorrectlyImplemented(
            Polar sut,
            Polar other)
        {
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }
    }
}
