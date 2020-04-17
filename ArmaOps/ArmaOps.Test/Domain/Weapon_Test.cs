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
    public class Weapon_Test
    {
        [Test, AutoData]
        public void EqualsTrueIfAllValuesMatch(string name)
        {
            var charges = new List<int> { 0, 1, 2 };
            var sut = new Weapon(name, 1, 2, charges);
            var sameValues = new Weapon(sut.Name, sut.MinElevation, sut.MaxElevation,sut.ChargeVelocity);

            Assert.That(sameValues, Is.EqualTo(sut));
            Assert.That(sameValues.GetHashCode(), Is.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsFalseIfNameMismatch(
            string name,
            string name2)
        {
            var charges = new List<int> { 0, 1, 2 };
            var sut = new Weapon(name, 1, 2, charges);
            var other = new Weapon(name2, sut.MinElevation, sut.MaxElevation, sut.ChargeVelocity);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsFalseIfMaxElevationMismatch(string name)
        {
            var charges = new List<int> { 0, 1, 2 };
            var sut = new Weapon(name, 1, 2, charges);
            var other = new Weapon(sut.Name, sut.MinElevation, 3, sut.ChargeVelocity);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsFalseIfMinElevationMismatch(string name)
        {
            var charges = new List<int> { 0, 1, 2 };
            var sut = new Weapon(name, 1, 2, charges);
            var other = new Weapon(sut.Name, 0, sut.MaxElevation, sut.ChargeVelocity);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }


        [Test, AutoData]
        public void EqualsFalseIfChargeVelocityMismatch(
            string name,
            IEnumerable<int> chargeVelocity)
        {
            var charges = new List<int> { 0, 1, 2 };
            var sut = new Weapon(name, 1, 2, charges);
            var other = new Weapon(sut.Name, sut.MinElevation, sut.MaxElevation, chargeVelocity);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EmptyNameThrowsArgumentException()
        {
            var charges = new List<int> { 0, 1, 2 };

            Assert.Throws<ArgumentException>(() =>
                new Weapon(string.Empty, -1, 1, charges));
        }

        [Test, AutoData]
        public void MinElevationLessThanNeg6400ThrowsArgumentOutOfRangeException(string name)
        {
            var charges = new List<int> { 0, 1, 2 };

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Weapon(name, -6401, 1, charges));
        }

        [Test, AutoData]
        public void MinElevationGreaterThanThan6400ThrowsArgumentOutOfRangeException(string name)
        {
            var charges = new List<int> { 0, 1, 2 };

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Weapon(name, 6401, 1, charges));
        }

        [Test, AutoData]
        public void MaxElevationLessThanNeg6400ThrowsArgumentOutOfRangeException(string name)
        {
            var charges = new List<int> { 0, 1, 2 };

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Weapon(name, 1, -6401, charges));
        }

        [Test, AutoData]
        public void MaxElevationGreaterThanThan6400ThrowsArgumentOutOfRangeException(string name)
        {
            var charges = new List<int> { 0, 1, 2 };

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Weapon(name, 1, 6401, charges));
        }


        [Test, AutoData]
        public void EmptyChargeCollectionThrowsArgumentException(string name)
        {
            Assert.Throws<ArgumentException>(() =>
                new Weapon(name, -1, 1, new List<int>()));
        }

        [Test, AutoData]
        public void ChargeLessThanZeroThrowsArgumentOutOfRangeException(string name)
        {
            var charges = new List<int> { -1 };

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Weapon(name, -1, 1, charges));
        }

        [Test, AutoData]
        public void MinElevationGreaterThanMaxThrowsArgumentException(string name)
        {
            var charges = new List<int> { 0, 1, 2 };

            Assert.Throws<ArgumentException>(() =>
                new Weapon(name, 2, 1, charges));
        }

        [Test, AutoData]
        public void MinElevationNeg6400DoesntThrowArgumentOutOfRangeException(string name)
        {
            var charges = new List<int> { 0, 1, 2 };

            Assert.DoesNotThrow(() =>
                new Weapon(name, -6400, 1, charges));
        }

        [Test, AutoData]
        public void MinElevation6400DoesntThrowArgumentOutOfRangeException(string name)
        {
            var charges = new List<int> { 0, 1, 2 };

            Assert.DoesNotThrow(() =>
                new Weapon(name, 6400, 6400, charges));
        }

        [Test, AutoData]
        public void MaxElevationNeg6400DoesntThrowArgumentOutOfRangeException(string name)
        {
            var charges = new List<int> { 0, 1, 2 };

            Assert.DoesNotThrow(() =>
                new Weapon(name, -6400, -6400, charges));
        }

        [Test, AutoData]
        public void MaxElevation6400DoesntThrowArgumentOutOfRangeException(string name)
        {
            var charges = new List<int> { 0, 1, 2 };

            Assert.DoesNotThrow(() =>
                new Weapon(name, 1, 6400, charges));
        }
    }
}
