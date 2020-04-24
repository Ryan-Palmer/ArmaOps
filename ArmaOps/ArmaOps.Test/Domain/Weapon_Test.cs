using ArmaOps.Domain;
using ArmaOps.Domain.Coordinates;
using AutoFixture.NUnit3;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmaOps.Test.Domain
{
    [TestFixture]
    public class Weapon_Test
    {
        [Test, AutoData]
        public void EditNameReturnsNewWeapon(string name, string name2)
        {
            var charges = new List<double> { 0, 1, 2 };
            var sut = new Weapon(name, new Mils(1), new Mils(2), charges);
            var result = sut.EditName(name2);

            Assert.That(result.Name, Is.EqualTo(name2));
            Assert.That(result.MinElevation, Is.EqualTo(sut.MinElevation));
            Assert.That(result.MaxElevation, Is.EqualTo(sut.MaxElevation));
            Assert.That(result.ChargeVelocities.SequenceEqual(sut.ChargeVelocities), Is.True);
            Assert.That(result == sut, Is.False);
        }

        [Test, AutoData]
        public void EditMinElevationReturnsNewWeapon(string name)
        {
            var charges = new List<double> { 0, 1, 2 };
            var sut = new Weapon(name, new Mils(2), new Mils(3), charges);
            var result = sut.EditMinElevation(new Mils(1));

            Assert.That(result.Name, Is.EqualTo(sut.Name));
            Assert.That(result.MinElevation, Is.EqualTo(new Mils(1)));
            Assert.That(result.MaxElevation, Is.EqualTo(sut.MaxElevation));
            Assert.That(result.ChargeVelocities.SequenceEqual(sut.ChargeVelocities), Is.True);
            Assert.That(result == sut, Is.False);
        }

        [Test, AutoData]
        public void EditMaxElevationReturnsNewWeapon(string name)
        {
            var charges = new List<double> { 0, 1, 2 };
            var sut = new Weapon(name, new Mils(1), new Mils(2), charges);
            var result = sut.EditMaxElevation(new Mils(3));

            Assert.That(result.Name, Is.EqualTo(sut.Name));
            Assert.That(result.MinElevation, Is.EqualTo(sut.MinElevation));
            Assert.That(result.MaxElevation, Is.EqualTo(new Mils(3)));
            Assert.That(result.ChargeVelocities.SequenceEqual(sut.ChargeVelocities), Is.True);
            Assert.That(result == sut, Is.False);
        }

        [Test, AutoData]
        public void EditChargeVelocitiesReturnsNewWeapon(string name)
        {
            var charges = new List<double> { 0, 1, 2 };
            var newCharges = new List<double> { 1, 2, 3 };
            var sut = new Weapon(name, new Mils(1), new Mils(2), charges);
            var result = sut.EditChargeVelocities(newCharges);

            Assert.That(result.Name, Is.EqualTo(sut.Name));
            Assert.That(result.MinElevation, Is.EqualTo(sut.MinElevation));
            Assert.That(result.MaxElevation, Is.EqualTo(sut.MaxElevation));
            Assert.That(result.ChargeVelocities.SequenceEqual(sut.ChargeVelocities), Is.False);
            Assert.That(result == sut, Is.False);
        }


        [Test, AutoData]
        public void EqualsTrueIfAllValuesMatch(string name)
        {
            var charges = new List<double> { 0, 1, 2 };
            var sut = new Weapon(name, new Mils(1), new Mils(2), charges);
            var sameValues = new Weapon(sut.Name, sut.MinElevation, sut.MaxElevation,sut.ChargeVelocities);

            Assert.That(sameValues, Is.EqualTo(sut));
            Assert.That(sameValues.GetHashCode(), Is.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsFalseIfNameMismatch(
            string name,
            string name2)
        {
            var charges = new List<double> { 0, 1, 2 };
            var sut = new Weapon(name, new Mils(1), new Mils(2), charges);
            var other = new Weapon(name2, sut.MinElevation, sut.MaxElevation, sut.ChargeVelocities);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsFalseIfMaxElevationMismatch(string name)
        {
            var charges = new List<double> { 0, 1, 2 };
            var sut = new Weapon(name, new Mils(1), new Mils(2), charges);
            var other = new Weapon(sut.Name, sut.MinElevation, new Mils(3), sut.ChargeVelocities);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsFalseIfMinElevationMismatch(string name)
        {
            var charges = new List<double> { 0, 1, 2 };
            var sut = new Weapon(name, new Mils(1), new Mils(2), charges);
            var other = new Weapon(sut.Name, new Mils(0), sut.MaxElevation, sut.ChargeVelocities);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }


        [Test, AutoData]
        public void EqualsFalseIfChargeVelocitiesMismatch(
            string name,
            IEnumerable<double> chargeVelocities)
        {
            var charges = new List<double> { 0, 1, 2 };
            var sut = new Weapon(name, new Mils(1), new Mils(2), charges);
            var other = new Weapon(sut.Name, sut.MinElevation, sut.MaxElevation, chargeVelocities);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EmptyNameThrowsArgumentException()
        {
            var charges = new List<double> { 0, 1, 2 };

            Assert.Throws<ArgumentException>(() =>
                new Weapon(string.Empty, new Mils(-1), new Mils(1), charges));
        }

        [Test, AutoData]
        public void EmptyChargeCollectionThrowsArgumentException(string name)
        {
            Assert.Throws<ArgumentException>(() =>
                new Weapon(name, new Mils(-1), new Mils(1), new List<double>()));
        }

        [Test, AutoData]
        public void ChargeLessThanZeroThrowsArgumentOutOfRangeException(string name)
        {
            var charges = new List<double> { -1 };

            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Weapon(name, new Mils(-1), new Mils(1), charges));
        }

        [Test, AutoData]
        public void MinElevationGreaterThanMaxThrowsArgumentException(string name)
        {
            var charges = new List<double> { 0, 1, 2 };

            Assert.Throws<ArgumentException>(() =>
                new Weapon(name, new Mils(2), new Mils(1), charges));
        }

        [Test, AutoData]
        public void ValidElevationReturnsAllowed(string name)
        {
            var charges = new List<double> { 0, 1, 2 };
            var minElev = new Mils(1);
            var maxElev = new Mils(3);
            var inRangeElev = new Mils(2);

            var sut = new Weapon(name, minElev, maxElev, charges);

            Assert.That(sut.ElevationIsAllowed(inRangeElev), Is.True);
        }

        [Test, AutoData]
        public void GreaterInvalidElevationReturnsNotAllowed(string name)
        {
            var charges = new List<double> { 0, 1, 2 };
            var minElev = new Mils(1);
            var maxElev = new Mils(2);
            var outOfRangeElev = new Mils(3);

            var sut = new Weapon(name, minElev, maxElev, charges);

            Assert.That(sut.ElevationIsAllowed(outOfRangeElev), Is.False);
        }

        [Test, AutoData]
        public void LesserInvalidElevationReturnsNotAllowed(string name)
        {
            var charges = new List<double> { 0, 1, 2 };
            var minElev = new Mils(1);
            var maxElev = new Mils(2);
            var outOfRangeElev = new Mils(0);

            var sut = new Weapon(name, minElev, maxElev, charges);

            Assert.That(sut.ElevationIsAllowed(outOfRangeElev), Is.False);
        }
    }
}
