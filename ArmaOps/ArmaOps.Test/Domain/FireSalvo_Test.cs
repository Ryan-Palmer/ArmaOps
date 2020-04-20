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
    public class FireSalvo_Test
    {
        const double EARTH_G = 9.80665;

        [Test, AutoData]
        public void EqualsTrueIfAllValuesMatchCartesianConstructor(
            string name,
            Cartesian target)
        {
            var sut = new FireSalvo(name, target);
            var sameValues = new FireSalvo(name, target);

            Assert.That(sameValues, Is.EqualTo(sut));
            Assert.That(sameValues.GetHashCode(), Is.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsTrueIfAllValuesMatchFoWithNameConstructor(
            string name, ForwardObserver fo, Mils observedAzimuth,
            Mils observedElevation, int observedDistanceMetres)
        {
            var sut = new FireSalvo(name, fo, observedAzimuth, observedElevation, observedDistanceMetres);
            var sameValues = new FireSalvo(name, fo, observedAzimuth, observedElevation, observedDistanceMetres);

            Assert.That(sameValues, Is.EqualTo(sut));
            Assert.That(sameValues.GetHashCode(), Is.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsTrueIfAllValuesMatchFoConstructor(
            ForwardObserver fo, Mils observedAzimuth,
            Mils observedElevation, int observedDistanceMetres)
        {
            var sut = new FireSalvo(fo, observedAzimuth, observedElevation, observedDistanceMetres);
            var sameValues = new FireSalvo(fo, observedAzimuth, observedElevation, observedDistanceMetres);

            Assert.That(sameValues, Is.EqualTo(sut));
            Assert.That(sameValues.GetHashCode(), Is.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsTrueIfAllValuesMatchMixedConstructor(
            string name,
            ForwardObserver fo, Mils observedAzimuth,
            Mils observedElevation, int observedDistanceMetres)
        {
            var cartesian = new Polar(fo.Location,
                observedAzimuth.Radians,
                observedElevation.Radians,
                observedDistanceMetres).ToCartesian();

            var sut = new FireSalvo(name, cartesian);
            var sameValues = new FireSalvo(name, fo, observedAzimuth, observedElevation, observedDistanceMetres);

            Assert.That(sameValues, Is.EqualTo(sut));
            Assert.That(sameValues.GetHashCode(), Is.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsFalseIfMismatchedName(
            string name,
            string name2,
            Cartesian target)
        {
            var sut = new FireSalvo(name, target);
            var sameValues = new FireSalvo(name2, target);

            Assert.That(sameValues, Is.Not.EqualTo(sut));
            Assert.That(sameValues.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }


        [Test, AutoData]
        public void EqualsFalseIfMismatchedTarget(
            string name,
            Cartesian target,
            Cartesian target2)
        {
            var sut = new FireSalvo(name, target);
            var sameValues = new FireSalvo(name, target2);

            Assert.That(sameValues, Is.Not.EqualTo(sut));
            Assert.That(sameValues.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }


        [Test]
        [InlineAutoData(2000, 0, 0, 0, 0, 0, 942, 1547, 96.8, 120.4, 141.9)]
        [InlineAutoData(0, 0, 2000, 0, 0, 0, 942, 1547, 96.8, 120.4, 141.9)]
        [InlineAutoData(2050, 0, 0, 0, 0, 0, 942, 1547, 96.8, 120.4, 141.9)]
        [InlineAutoData(0, 0, 2050, 0, 0, 0, 942, 1547, 96.8, 120.4, 141.9)]
        [InlineAutoData(3000, 0,    0, 0, 0, 0, 942, 1547, 96.8, 120.4, 141.9)]
        [InlineAutoData(0,    0, 3000, 0, 0, 0, 942, 1547, 96.8, 120.4, 141.9)]
        [InlineAutoData(2121, 0, 2121, 0, 0, 0, 942, 1547, 96.8, 120.4, 141.9)]
        public void OutOfRangeCartesianTargetsReturnNoSolutions(
            int targetX,
            int targetY,
            int targetZ,
            int locX,
            int locY,
            int locZ,
            int minElev,
            int maxElev,
            double charge1,
            double charge2,
            double charge3,
            string name)
        {
            var weapon = new Weapon(name, new Mils(minElev), new Mils(maxElev), new List<double> { charge1, charge2, charge3 });
            var battery = new Battery(name, new Cartesian(locX, locY, locZ), weapon);
            var sut = new FireSalvo(new Cartesian(targetX, targetY, targetZ));

            var result = sut.GetSolutionSet(battery);

            Assert.That(result.FireSolutions.Count(), Is.EqualTo(0));
        }

        [Test]
        [InlineAutoData(   0, 0,   50, 0, 0, 0, 942, 1547, 96.8, 120.4, 141.9)]
        [InlineAutoData(1600, 0,   50, 0, 0, 0, 942, 1547, 96.8, 120.4, 141.9)]
        [InlineAutoData(3200, 0,   50, 0, 0, 0, 942, 1547, 96.8, 120.4, 141.9)]
        [InlineAutoData(4800, 0,   50, 0, 0, 0, 942, 1547, 96.8, 120.4, 141.9)]
        [InlineAutoData(   0, 0, 2000, 0, 0, 0, 942, 1547, 96.8, 120.4, 141.9)]
        [InlineAutoData(1600, 0, 2000, 0, 0, 0, 942, 1547, 96.8, 120.4, 141.9)]
        [InlineAutoData(3200, 0, 2000, 0, 0, 0, 942, 1547, 96.8, 120.4, 141.9)]
        [InlineAutoData(4800, 0, 2000, 0, 0, 0, 942, 1547, 96.8, 120.4, 141.9)]
        [InlineAutoData(   0, 0, 2050, 0, 0, 0, 942, 1547, 96.8, 120.4, 141.9)]
        [InlineAutoData(1600, 0, 2050, 0, 0, 0, 942, 1547, 96.8, 120.4, 141.9)]
        [InlineAutoData(3200, 0, 2050, 0, 0, 0, 942, 1547, 96.8, 120.4, 141.9)]
        [InlineAutoData(4800, 0, 2050, 0, 0, 0, 942, 1547, 96.8, 120.4, 141.9)]
        [InlineAutoData(   0, 0, 3000, 0, 0, 0, 942, 1547, 96.8, 120.4, 141.9)]
        [InlineAutoData(1600, 0, 3000, 0, 0, 0, 942, 1547, 96.8, 120.4, 141.9)]
        [InlineAutoData(3200, 0, 3000, 0, 0, 0, 942, 1547, 96.8, 120.4, 141.9)]
        [InlineAutoData(4800, 0, 3000, 0, 0, 0, 942, 1547, 96.8, 120.4, 141.9)]
        public void OutOfRangePolarTargetsReturnNoSolutions(
            int observedAzimuth,
            int observedElevation,
            int observedDistanceMetres,
            int locX,
            int locY,
            int locZ,
            int minElev,
            int maxElev,
            double charge1,
            double charge2,
            double charge3,
            string name)
        {
            var weapon = new Weapon(name, new Mils(minElev), new Mils(maxElev), new List<double> { charge1, charge2, charge3 });
            var battery = new Battery(name, new Cartesian(locX, locY, locZ), weapon);
            var fo = new ForwardObserver(name, new Cartesian(0, 0, 0));
            var sut = new FireSalvo(fo, new Mils(observedAzimuth), new Mils(observedElevation), observedDistanceMetres);

            var result = sut.GetSolutionSet(battery);

            Assert.That(result.FireSolutions.Count(), Is.EqualTo(0));
        }
    }
}
