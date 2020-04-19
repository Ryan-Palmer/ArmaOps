using ArmaOps.Domain;
using AutoFixture.NUnit3;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Test.Domain
{
    [TestFixture]
    public class Mils_Test
    {
        [Test, AutoData]
        public void EqualsTrueIfValuesMatchMilliRadianConstruction(
            int milliRadians)
        {
            var sut = new Mils(milliRadians);
            var sameValues = new Mils(milliRadians);

            Assert.That(sameValues, Is.EqualTo(sut));
            Assert.That(sameValues.GetHashCode(), Is.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsFalseIfValuesMismatchMilliRadianConstruction(
            int milliRadians,
            int milliRadians2)
        {
            var sut = new Mils(milliRadians);
            var other = new Mils(milliRadians2);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsTrueIfValuesMatchRadianConstruction(
            double radians)
        {
            var sut = new Mils(radians);
            var sameValues = new Mils(radians);

            Assert.That(sameValues, Is.EqualTo(sut));
            Assert.That(sameValues.GetHashCode(), Is.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsFalseIfValuesMismatchRadianConstruction(
            double radians,
            double radians2)
        {
            var sut = new Mils(radians);
            var other = new Mils(radians2);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void RadianPropertyBalancedWithRadianConstructor(
            int milliRadians)
        {
            var sut = new Mils(milliRadians);
            var sameValues = new Mils(sut.Radians);

            Assert.That(sameValues, Is.EqualTo(sut));
            Assert.That(sameValues.GetHashCode(), Is.EqualTo(sut.GetHashCode()));
        }
    }
}
