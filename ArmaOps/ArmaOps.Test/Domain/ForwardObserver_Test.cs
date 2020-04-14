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
    public class ForwardObserver_Test
    {
        [Test, AutoData]
        public void EqualsTrueIfAllValuesMatch(ForwardObserver sut)
        {
            var sameValues = new ForwardObserver(sut.Location, sut.Name);

            Assert.That(sameValues, Is.EqualTo(sut));
        }

        [Test, AutoData]
        public void EqualsFalseIfLocationMismatch(
            ForwardObserver sut,
            Cartesian location)
        {
            var other = new ForwardObserver(location, sut.Name);

            Assert.That(other, Is.Not.EqualTo(sut));
        }

        [Test, AutoData]
        public void EqualsFalseIfNameMismatch(
            ForwardObserver sut,
            string name)
        {
            var other = new ForwardObserver(sut.Location, name);

            Assert.That(other, Is.Not.EqualTo(sut));
        }

        [Test, AutoData]
        public void GetHashCodeMatchCorrectlyImplemented(ForwardObserver sut)
        {
            var sameValues = new ForwardObserver(sut.Location, sut.Name);

            Assert.That(sameValues.GetHashCode(), Is.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void GetHashCodeNoMatchCorrectlyImplemented(
            ForwardObserver sut,
            ForwardObserver other)
        {
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }
    }
}
