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
    public class BattlefieldEntity_Test
    {
        [Test, AutoData]
        public void EqualsTrueIfAllValuesMatch(ForwardObserver sut)
        {
            var sameValues = new BattlefieldEntity(sut.Name, sut.Location);

            Assert.That(sameValues, Is.EqualTo(sut));
        }

        [Test, AutoData]
        public void EqualsFalseIfLocationMismatch(
            BattlefieldEntity sut,
            Cartesian location)
        {
            var other = new BattlefieldEntity(sut.Name, location);

            Assert.That(other, Is.Not.EqualTo(sut));
        }

        [Test, AutoData]
        public void EqualsFalseIfNameMismatch(
            BattlefieldEntity sut,
            string name)
        {
            var other = new BattlefieldEntity(name, sut.Location);

            Assert.That(other, Is.Not.EqualTo(sut));
        }

        [Test, AutoData]
        public void GetHashCodeMatchCorrectlyImplemented(BattlefieldEntity sut)
        {
            var sameValues = new BattlefieldEntity(sut.Name, sut.Location);

            Assert.That(sameValues.GetHashCode(), Is.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void GetHashCodeNoMatchCorrectlyImplemented(
            BattlefieldEntity sut,
            BattlefieldEntity other)
        {
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }
    }
}
