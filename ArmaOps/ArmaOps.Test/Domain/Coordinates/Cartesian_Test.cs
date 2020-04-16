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
        public void GreaterThanSquareRootOfMax2DCartesianThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Cartesian(0, Math.Sqrt(double.MaxValue) + double.MinValue, 0));
        }

        [Test, AutoData]
        public void LessThanNegativeSquareRootOfMin2DCartesianThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Cartesian(0, -Math.Sqrt(double.MaxValue) - double.MinValue, 0));
        }

        [Test, AutoData]
        public void Origin2DCartesianConvertsToPolar()
        {
            var origin = new Cartesian(0, 0, 0);
            var target = new Cartesian(0, 1, 0);
            var expectedResult = new Polar(origin, 0, 0, 1);

            var result = target.ToPolar(origin);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test, AutoData]
        public void NearOrigin2DCartesianConvertsToPolar()
        {
            var origin = new Cartesian(0, 0, 0);
            var target = new Cartesian(0, 2, 0);
            var expectedResult = new Polar(origin, 0, 0, 2);

            var result = target.ToPolar(origin);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test, AutoData]
        public void RealWorldMax2DCartesianConvertsToPolar()
        {
            var origin = new Cartesian(0, 0, 0);
            var target = new Cartesian(0, 10000, 0);
            var expectedResult = new Polar(origin, 0, 0, 10000);

            var result = target.ToPolar(origin);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test, AutoData]
        public void RealWorldMin2DCartesianConvertsToPolar()
        {
            var origin = new Cartesian(0, 0, 0);
            var target = new Cartesian(0, -10000, 0);
            var expectedResult = new Polar(origin, Math.PI, 0, 10000);

            var result = target.ToPolar(origin);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test, AutoData]
        public void SquareRootOfMax2DCartesianConvertsToPolar()
        {
            var origin = new Cartesian(0, 0, 0);
            var target = new Cartesian(0, Math.Sqrt(double.MaxValue), 0);
            var expectedResult = new Polar(origin, 0, 0, Math.Sqrt(double.MaxValue));

            var result = target.ToPolar(origin);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test, AutoData]
        public void NegativeSquareRootOfMin2DCartesianConvertsToPolar()
        {
            var origin = new Cartesian(0, 0, 0);
            var target = new Cartesian(0, -Math.Sqrt(double.MaxValue), 0);
            var expectedResult = new Polar(origin, Math.PI, 0, Math.Sqrt(double.MaxValue));

            var result = target.ToPolar(origin);

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
