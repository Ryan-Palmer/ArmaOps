using ArmaOps.Domain;
using ArmaOps.Domain.Coordinates;
using AutoFixture.NUnit3;
using NUnit.Framework;
using System.Collections.Generic;

namespace ArmaOps.Test.Domain
{
    [TestFixture]
    public class BatterySolutionSet_Test
    {
        [Test, AutoData]
        public void EqualsTrueIfAllValuesMatch(
            string name,
            Cartesian target,
            Cartesian batteryLoc,
            IEnumerable<FireSolution> fireSolutions)
        {
            var charges = new List<double> { 0, 1, 2 };
            var weapon = new Weapon(name, new Mils(1), new Mils(2), charges);
            var battery = new Battery(name, batteryLoc, weapon);
            var azToTarget = new Mils(0);

            var sut = new BatterySolutionSet(battery, target, azToTarget, fireSolutions);
            var sameValues = new BatterySolutionSet(battery, target, azToTarget, fireSolutions);

            Assert.That(sameValues, Is.EqualTo(sut));
            Assert.That(sameValues.GetHashCode(), Is.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsFalseIfBatteryMismatch(
            string name,
            string name2,
            Cartesian target,
            Cartesian batteryLoc,
            IEnumerable<FireSolution> fireSolutions)
        {
            var charges = new List<double> { 0, 1, 2 };
            var weapon = new Weapon(name, new Mils(1), new Mils(2), charges);
            var battery = new Battery(name, batteryLoc, weapon);
            var battery2 = new Battery(name2, batteryLoc, weapon);
            var azToTarget = new Mils(0);

            var sut = new BatterySolutionSet(battery, target, azToTarget, fireSolutions);
            var other = new BatterySolutionSet(battery2, target, azToTarget, fireSolutions);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsFalseIfTargetMismatch(
            string name,
            Cartesian target,
            Cartesian target2,
            Cartesian batteryLoc,
            IEnumerable<FireSolution> fireSolutions)
        {
            var charges = new List<double> { 0, 1, 2 };
            var weapon = new Weapon(name, new Mils(1), new Mils(2), charges);
            var battery = new Battery(name, batteryLoc, weapon);
            var azToTarget = new Mils(0);

            var sut = new BatterySolutionSet(battery, target, azToTarget, fireSolutions);
            var other = new BatterySolutionSet(battery, target2, azToTarget, fireSolutions);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsFalseIfFireSolutionsMismatch(
            string name,
            Cartesian target,
            Cartesian batteryLoc,
            IEnumerable<FireSolution> fireSolutions,
            IEnumerable<FireSolution> fireSolutions2)
        {
            var charges = new List<double> { 0, 1, 2 };
            var weapon = new Weapon(name, new Mils(1), new Mils(2), charges);
            var battery = new Battery(name, batteryLoc, weapon);
            var azToTarget = new Mils(0);

            var sut = new BatterySolutionSet(battery, target, azToTarget, fireSolutions);
            var other = new BatterySolutionSet(battery, target, azToTarget, fireSolutions2);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsFalseIfAzimuthsMismatch(
            string name,
            Cartesian target,
            Cartesian batteryLoc,
            IEnumerable<FireSolution> fireSolutions)
        {
            var charges = new List<double> { 0, 1, 2 };
            var weapon = new Weapon(name, new Mils(1), new Mils(2), charges);
            var battery = new Battery(name, batteryLoc, weapon);
            var azToTarget = new Mils(0);
            var azToTarget2 = new Mils(1);

            var sut = new BatterySolutionSet(battery, target, azToTarget, fireSolutions);
            var other = new BatterySolutionSet(battery, target, azToTarget2, fireSolutions);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }
    }
}
