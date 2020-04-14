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
    public class Cartesian_Test
    {
        [Test, AutoData]
        public void Origin2DCartesianConvertsToPolar(
            string foName)
        {
            var originFO = new ForwardObserver(new Cartesian(0, 0, 0), foName);
            var originTarget = new Cartesian(0, 1, 0);
            var expectedResult = new Polar(originFO, 0, 1, 0);

            var result = originTarget.ToPolar(originFO);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test, AutoData]
        public void EqualsTrueIfAllValuesMatch(Cartesian sut)
        {
            var sameValues = new Cartesian(sut.X, sut.Y, sut.Z);

            Assert.That(sameValues, Is.EqualTo(sut));
        }

        [Test, AutoData]
        public void EqualsFalseIfXMismatch(
            Cartesian sut,
            double x)
        {
            var other = new Cartesian(x, sut.Y, sut.Z);

            Assert.That(other, Is.Not.EqualTo(sut));
        }

        [Test, AutoData]
        public void EqualsFalseIfYMismatch(
            Cartesian sut,
            double y)
        {
            var other = new Cartesian(sut.X, y, sut.Z);

            Assert.That(other, Is.Not.EqualTo(sut));
        }

        [Test, AutoData]
        public void EqualsFalseIfZMismatch(
            Cartesian sut,
            double z)
        {
            var other = new Cartesian(sut.X, sut.Y, z);

            Assert.That(other, Is.Not.EqualTo(sut));
        }

        [Test, AutoData]
        public void GetHashCodeMatchCorrectlyImplemented(Cartesian sut)
        {
            var sameValues = new Cartesian(sut.X, sut.Y, sut.Z);

            Assert.That(sameValues.GetHashCode(), Is.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void GetHashCodeNoMatchCorrectlyImplemented(
            Cartesian sut,
            Cartesian other)
        {
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void AddAndSubtractBalanced(Cartesian a, Cartesian b)
        {
            var result = a.Add(b).Subtract(a);

            Assert.That(result, Is.EqualTo(b));
        }
    }
}
