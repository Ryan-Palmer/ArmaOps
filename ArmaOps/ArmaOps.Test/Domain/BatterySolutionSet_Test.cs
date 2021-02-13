﻿using ArmaOps.Domain;
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
            var weapon = new Weapon(name, new Angle(1, true), new Angle(2, true), charges);
            var battery = new Battery(name, batteryLoc, weapon);
            var azToTarget = new Angle(0, true);

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
            var weapon = new Weapon(name, new Angle(1, true), new Angle(2, true), charges);
            var battery = new Battery(name, batteryLoc, weapon);
            var battery2 = new Battery(name2, batteryLoc, weapon);
            var azToTarget = new Angle(0, true);

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
            var weapon = new Weapon(name, new Angle(1, true), new Angle(2, true), charges);
            var battery = new Battery(name, batteryLoc, weapon);
            var azToTarget = new Angle(0, true);

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
            var weapon = new Weapon(name, new Angle(1, true), new Angle(2, true), charges);
            var battery = new Battery(name, batteryLoc, weapon);
            var azToTarget = new Angle(0, true);

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
            var weapon = new Weapon(name, new Angle(1, true), new Angle(2, true), charges);
            var battery = new Battery(name, batteryLoc, weapon);
            var azToTarget = new Angle(0, true);
            var azToTarget2 = new Angle(1, true);

            var sut = new BatterySolutionSet(battery, target, azToTarget, fireSolutions);
            var other = new BatterySolutionSet(battery, target, azToTarget2, fireSolutions);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }


        //[Test, AutoData]
        //public void CartesianCorrectionIsCorrect(string name)
        //{
        //    var charges = new List<double> { 0, 1, 2 };
        //    var weapon = new Weapon(name, new Angle(1, true), new Angle(2, true), charges);
        //    var battery = new Battery(name, new Cartesian(0, 0, 0), weapon);
        //    var sut = new FireMission(name, new Cartesian(0, 0, 1000)).GetSolutionSet(battery);
        //    var actualResult = sut.ApplyCorrection(new Cartesian(10, 20, 50));
        //    var expectedResult = new FireMission(name, new Cartesian(10, 20, 1050)).GetSolutionSet(battery);

        //    Assert.That(actualResult, Is.EqualTo(expectedResult));
        //}

        //[Test, AutoData]
        //public void PolarCorrectionIsCorrectlyBalanced(string name)
        //{
        //    var charges = new List<double> { 0, 1, 2 };
        //    var weapon = new Weapon(name, new Angle(1, true), new Angle(2, true), charges);
        //    var battery = new Battery(name, new Cartesian(0, 0, 0), weapon);
        //    var sut = new FireMission(name, new Cartesian(0, 0, 1000)).GetSolutionSet(battery);
        //    var fo = new ForwardObserver(name, new Cartesian(0, 0, 0));
        //    var deltaAzimuth = new Angle(1600, true);
        //    var deltaElevation = new Angle(0, true);
        //    var dDistance = 0.0;
        //    var negDeltaAzimuth = new Angle(-1600, true);
        //    var negDeltaElevation = new Angle(0, true);
        //    var negDistance = 0.0;
        //    var moved = sut.ApplyCorrection(fo, deltaAzimuth, deltaElevation, dDistance);
        //    var movedBack = moved.ApplyCorrection(fo, negDeltaAzimuth, negDeltaElevation, negDistance);

        //    Assert.That(sut, Is.EqualTo(movedBack));
        //}

        //[Test, AutoData]
        //public void PolarCorrectionIsCorrectlyBalancedErrorTest(string name)
        //{
        //    var charges = new List<double> { 0, 1, 2 };
        //    var weapon = new Weapon(name, new Angle(1, true), new Angle(2, true), charges);
        //    var battery = new Battery(name, new Cartesian(0, 0, 0), weapon);
        //    var sut = new FireMission(name, new Cartesian(0, 0, 1000)).GetSolutionSet(battery);
        //    var fo = new ForwardObserver(name, new Cartesian(0, 0, 0));
        //    var deltaAzimuth = new Angle(1600, true);
        //    var deltaElevation = new Angle(0, true);
        //    var dDistance = 0.0;
        //    var negDeltaAzimuth = new Angle(-1601, true);
        //    var negDeltaElevation = new Angle(0, true);
        //    var negDistance = 0.0;
        //    var moved = sut.ApplyCorrection(fo, deltaAzimuth, deltaElevation, dDistance);
        //    var movedBack = moved.ApplyCorrection(fo, negDeltaAzimuth, negDeltaElevation, negDistance);

        //    Assert.That(sut, Is.Not.EqualTo(movedBack));
        //}

        //[Test]
        //[InlineAutoData(1600)]
        //[InlineAutoData(8000)]
        //[InlineAutoData(1234)]
        //public void PolarCorrectionIsCorrectlyBalancedDoubleApply(
        //    int Mils,
        //    string name
        //    )
        //{
        //    var charges = new List<double> { 0, 1, 2 };
        //    var weapon = new Weapon(name, new Angle(1, true), new Angle(2, true), charges);
        //    var battery = new Battery(name, new Cartesian(0, 0, 0), weapon);
        //    var sut = new FireMission(name, new Cartesian(0, 0, 1000)).GetSolutionSet(battery);
        //    var fo = new ForwardObserver(name, new Cartesian(0, 0, 0));
        //    var deltaAzimuth = new Angle(Mils, true);
        //    var deltaElevation = new Angle(0, true);
        //    var dDistance = 0.0;
        //    var negDeltaAzimuth = new Angle(-Mils, true);
        //    var negDeltaElevation = new Angle(0, true);
        //    var negDistance = 0.0;
        //    var moved = sut.ApplyCorrection(fo, deltaAzimuth, deltaElevation, dDistance);
        //    var movedAgain = moved.ApplyCorrection(fo, deltaAzimuth, deltaElevation, dDistance);
        //    var movedBack = movedAgain.ApplyCorrection(fo, negDeltaAzimuth, negDeltaElevation, negDistance);
        //    var movedBackAgain = movedBack.ApplyCorrection(fo, negDeltaAzimuth, negDeltaElevation, negDistance);

        //    Assert.That(sut, Is.EqualTo(movedBackAgain));
        //}

        //[Test]
        //[InlineAutoData(5678)]
        //public void PolarCorrectionHasSomeRoundingErrorsAtArbitraryInputValues(
        //    int Mils,
        //    string name
        //    )
        //{
        //    var charges = new List<double> { 0, 1, 2 };
        //    var weapon = new Weapon(name, new Angle(1, true), new Angle(2, true), charges);
        //    var battery = new Battery(name, new Cartesian(0, 0, 0), weapon);
        //    var sut = new FireMission(name, new Cartesian(0, 0, 1000)).GetSolutionSet(battery);
        //    var fo = new ForwardObserver(name, new Cartesian(0, 0, 0));
        //    var deltaAzimuth = new Angle(Mils, true);
        //    var deltaElevation = new Angle(0, true);
        //    var dDistance = 0.0;
        //    var negDeltaAzimuth = new Angle(-Mils, true);
        //    var negDeltaElevation = new Angle(0, true);
        //    var negDistance = 0.0;
        //    var moved = sut.ApplyCorrection(fo, deltaAzimuth, deltaElevation, dDistance);
        //    var movedAgain = moved.ApplyCorrection(fo, deltaAzimuth, deltaElevation, dDistance);
        //    var movedBack = movedAgain.ApplyCorrection(fo, negDeltaAzimuth, negDeltaElevation, negDistance);
        //    var movedBackAgain = movedBack.ApplyCorrection(fo, negDeltaAzimuth, negDeltaElevation, negDistance);

        //    Assert.That(sut, Is.Not.EqualTo(movedBackAgain));
        //}
    }
}
