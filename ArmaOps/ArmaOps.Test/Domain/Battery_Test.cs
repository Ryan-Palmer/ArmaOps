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
    public class Battery_Test
    {
        [Test, AutoData]
        public void EqualsTrueIfAllValuesMatch(string name, Cartesian loc)
        {
            var charges = new List<double> { 0, 1, 2 };
            var weapon = new Weapon(name, new Angle(1, true), new Angle(2, true), charges);
            var sut = new Battery(name, loc, weapon);
            var sameValues = new Battery(name, loc, weapon);

            Assert.That(sameValues, Is.EqualTo(sut));
        }

        [Test, AutoData]
        public void EqualsFalseIfNameMismatch(
            string name,
            string name2,
            Cartesian loc)
        {
            var charges = new List<double> { 0, 1, 2 };
            var weapon = new Weapon(name, new Angle(1, true), new Angle(2, true), charges);
            var sut = new Battery(name, loc, weapon);
            var other = new Battery(name2, loc, weapon);

            Assert.That(other, Is.Not.EqualTo(sut));
        }

        [Test, AutoData]
        public void EqualsFalseIfLocationMismatch(
            string name, 
            Cartesian loc,
            Cartesian loc2)
        {
            var charges = new List<double> { 0, 1, 2 };
            var weapon = new Weapon(name, new Angle(1, true), new Angle(2, true), charges);
            var sut = new Battery(name, loc, weapon);
            var other = new Battery(name, loc2, weapon);

            Assert.That(other, Is.Not.EqualTo(sut));
        }

        [Test, AutoData]
        public void EqualsFalseIfWeaponMismatch(
            string name,
            string name2,
            Cartesian loc)
        {
            var charges = new List<double> { 0, 1, 2 };
            var weapon = new Weapon(name, new Angle(1, true), new Angle(2, true), charges);
            var weapon2 = new Weapon(name2, new Angle(1, true), new Angle(2, true), charges);
            var sut = new Battery(name, loc, weapon);
            var other = new Battery(name, loc, weapon2);

            Assert.That(other, Is.Not.EqualTo(sut));
        }

        [Test, AutoData]
        public void HashCodeMatchIfAllValuesMatch(string name, Cartesian loc)
        {
            var charges = new List<double> { 0, 1, 2 };
            var weapon = new Weapon(name, new Angle(1, true), new Angle(2, true), charges);
            var sut = new Battery(name, loc, weapon);
            var sameValues = new Battery(name, loc, weapon);

            Assert.That(sameValues.GetHashCode(), Is.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void HashCodeMatchIfBaseMismatch(
            string name,
            string name2,
            Cartesian loc)
        {
            var charges = new List<double> { 0, 1, 2 };
            var weapon = new Weapon(name, new Angle(1, true), new Angle(2, true), charges);
            var sut = new Battery(name, loc, weapon);
            var other = new Battery(name2, loc, weapon);

            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void HashCodeFalseIfWeaponMismatch(
            string name,
            Cartesian loc,
            string name2)
        {
            var charges = new List<double> { 0, 1, 2 };
            var weapon = new Weapon(name, new Angle(1, true), new Angle(2, true), charges);
            var weapon2 = new Weapon(name2, new Angle(1, true), new Angle(2, true), charges);
            var sut = new Battery(name, loc, weapon);
            var other = new Battery(name, loc, weapon2);

            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }
    }
}
