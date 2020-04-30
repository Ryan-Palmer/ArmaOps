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
    public class FireMission_Test
    {
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
            string name,
            FireMission sut)
        {
            var weapon = new Weapon(name, new Mils(minElev), new Mils(maxElev), new List<double> { charge1, charge2, charge3 });
            var battery = new Battery(name, new Cartesian(locX, locY, locZ), weapon);
            var result = sut.GetSolutionSets(new List<Battery> { battery }, new Cartesian(targetX, targetY, targetZ));

            Assert.That(result.First().FireSolutions.Count(), Is.EqualTo(0));
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
            string name,
            FireMission sut)
        {
            var weapon = new Weapon(name, new Mils(minElev), new Mils(maxElev), new List<double> { charge1, charge2, charge3 });
            var battery = new Battery(name, new Cartesian(locX, locY, locZ), weapon);
            var fo = new ForwardObserver(name, new Cartesian(0, 0, 0));
            var result = sut.GetSolutionSets(new List<Battery> { battery }, fo, new Mils(observedAzimuth), new Mils(observedElevation), observedDistanceMetres);

            Assert.That(result.First().FireSolutions.Count(), Is.EqualTo(0));
        }


        [Test, AutoData]
        public void SolutionIsViableForIronFrontIfNearestTriple(string name,
            FireMission sut)
        {
            var weapon = new Weapon(name, new Mils(942), new Mils(1547), new List<double> { 96.8, 120.4, 141.9 });
            var battery = new Battery(name, new Cartesian(0, 0, 0), weapon);
            var fo = new ForwardObserver(name, new Cartesian(0, 0, 0));
            var result = sut.GetSolutionSets(new List<Battery> { battery }, fo, new Mils(0), new Mils(0), 250);
            var expectedResult = new BatterySolutionSet(battery, new Cartesian(0, 0, 250), new Mils(0),
                new List<FireSolution> {
                    new FireSolution(96.8, new Mils(1465), SolutionType.Indirect),
                    new FireSolution(120.4, new Mils(1513), SolutionType.Indirect),
                    new FireSolution(141.9, new Mils(1538), SolutionType.Indirect),
                });

            Assert.That(result.First(), Is.EqualTo(expectedResult));
        }

        [Test, AutoData]
        public void SolutionIsViableForIronFrontIfFurthestTriple(string name, FireMission sut)
        {
            var weapon = new Weapon(name, new Mils(942), new Mils(1547), new List<double> { 96.8, 120.4, 141.9 });
            var battery = new Battery(name, new Cartesian(0, 0, 0), weapon);
            var fo = new ForwardObserver(name, new Cartesian(0, 0, 0));
            var result = sut.GetSolutionSets(new List<Battery> { battery }, fo, new Mils(0), new Mils(0), 900);
            var expectedResult = new BatterySolutionSet(battery, new Cartesian(0, 0, 900), new Mils(0),
                new List<FireSolution> {
                    new FireSolution(96.8,  new Mils(974),  SolutionType.Indirect),
                    new FireSolution(120.4, new Mils(1267), SolutionType.Indirect),
                    new FireSolution(141.9, new Mils(1369), SolutionType.Indirect),
                });

            Assert.That(result.First(), Is.EqualTo(expectedResult));
        }

        [Test, AutoData]
        public void SolutionIsViableForIronFrontIfFurthestTriplePlusOne(string name, FireMission sut)
        {
            var weapon = new Weapon(name, new Mils(942), new Mils(1547), new List<double> { 96.8, 120.4, 141.9 });
            var battery = new Battery(name, new Cartesian(0, 0, 0), weapon);
            var fo = new ForwardObserver(name, new Cartesian(0, 0, 0));
            var result = sut.GetSolutionSets(new List<Battery> { battery }, fo, new Mils(0), new Mils(0), 950);
            var expectedResult = new BatterySolutionSet(battery, new Cartesian(0, 0, 950), new Mils(0),
                new List<FireSolution> {
                    new FireSolution(120.4, new Mils(1245), SolutionType.Indirect),
                    new FireSolution(141.9, new Mils(1355), SolutionType.Indirect),
                });

            Assert.That(result.First(), Is.EqualTo(expectedResult));
        }

        [Test, AutoData]
        public void SolutionIsViableForIronFrontIfFurthestDouble(string name, FireMission sut)
        {
            var weapon = new Weapon(name, new Mils(942), new Mils(1547), new List<double> { 96.8, 120.4, 141.9 });
            var battery = new Battery(name, new Cartesian(0, 0, 0), weapon);
            var fo = new ForwardObserver(name, new Cartesian(0, 0, 0));
            var result = sut.GetSolutionSets(new List<Battery> { battery }, fo, new Mils(0), new Mils(0), 1400);
            var expectedResult = new BatterySolutionSet(battery, new Cartesian(0, 0, 1400), new Mils(0),
                new List<FireSolution> {
                    new FireSolution(120.4, new Mils(966), SolutionType.Indirect),
                    new FireSolution(141.9, new Mils(1218), SolutionType.Indirect),
                });

            Assert.That(result.First(), Is.EqualTo(expectedResult));
        }

        [Test, AutoData]
        public void SolutionIsViableForIronFrontIfFurthestDoublePlusOne(string name, FireMission sut)
        {
            var weapon = new Weapon(name, new Mils(942), new Mils(1547), new List<double> { 96.8, 120.4, 141.9 });
            var battery = new Battery(name, new Cartesian(0, 0, 0), weapon);
            var fo = new ForwardObserver(name, new Cartesian(0, 0, 0));
            var result = sut.GetSolutionSets(new List<Battery> { battery }, fo, new Mils(0), new Mils(0), 1450);
            var expectedResult = new BatterySolutionSet(battery, new Cartesian(0, 0, 1450), new Mils(0),
                new List<FireSolution> {
                    new FireSolution(141.9, new Mils(1201), SolutionType.Indirect),
                });

            Assert.That(result.First(), Is.EqualTo(expectedResult));
        }

        [Test, AutoData]
        public void SolutionIsViableForIronFrontIfFurthestSingle(string name, FireMission sut)
        {
            var weapon = new Weapon(name, new Mils(942), new Mils(1547), new List<double> { 96.8, 120.4, 141.9 });
            var battery = new Battery(name, new Cartesian(0, 0, 0), weapon);
            var fo = new ForwardObserver(name, new Cartesian(0, 0, 0));
            var result = sut.GetSolutionSets(new List<Battery> { battery }, fo, new Mils(0), new Mils(0), 1950);
            var expectedResult = new BatterySolutionSet(battery, new Cartesian(0, 0, 1950), new Mils(0),
                new List<FireSolution> {
                    new FireSolution(141.9, new Mils(962), SolutionType.Indirect),
                });

            Assert.That(result.First(), Is.EqualTo(expectedResult));
        }

        [Test, AutoData]
        public void SolutionIsViableForIronFrontIfFurthestSinglePlusOne(string name, FireMission sut)
        {
            var weapon = new Weapon(name, new Mils(942), new Mils(1547), new List<double> { 96.8, 120.4, 141.9 });
            var battery = new Battery(name, new Cartesian(0, 0, 0), weapon);
            var fo = new ForwardObserver(name, new Cartesian(0, 0, 0));
            var result = sut.GetSolutionSets(new List<Battery> { battery }, fo, new Mils(0), new Mils(0), 2000);
            var expectedResult = new BatterySolutionSet(battery, new Cartesian(0, 0, 2000), new Mils(0),
                new List<FireSolution> {
                });

            Assert.That(result.First(), Is.EqualTo(expectedResult));
        }

        [Test, AutoData]
        public void MultipleSolutionSetsCalculatedCorrectly(string name, FireMission sut)
        {
            var weapon = new Weapon(name, new Mils(942), new Mils(1547), new List<double> { 96.8, 120.4, 141.9 });
            var battery1 = new Battery(name, new Cartesian(0, 0, 0), weapon);
            var battery2 = new Battery(name, new Cartesian(0, 0, 2000), weapon);
            var battery3 = new Battery(name, new Cartesian(0, 0, 20000), weapon);
            var result = sut.GetSolutionSets(new List<Battery> { battery1, battery2, battery3 }, new Cartesian(0, 0, 1000)).ToList();
            var expectedResult1 = new BatterySolutionSet(battery1, new Cartesian(0, 0, 1000), new Mils(0),
                new List<FireSolution>
                {
                    new FireSolution(120.4, new Mils(1222), SolutionType.Indirect),
                    new FireSolution(141.9, new Mils(1341), SolutionType.Indirect)
                });
            var expectedResult2 = new BatterySolutionSet(battery2, new Cartesian(0, 0, 1000), new Mils(3200),
                new List<FireSolution>
                {
                    new FireSolution(120.4, new Mils(1222), SolutionType.Indirect),
                    new FireSolution(141.9, new Mils(1341), SolutionType.Indirect)
                });
            var expectedResult3 = new BatterySolutionSet(battery3, new Cartesian(0, 0, 1000), new Mils(3200),
                new List<FireSolution>
                {
                });

            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(result[0], Is.EqualTo(expectedResult1));
            Assert.That(result[1], Is.EqualTo(expectedResult2));
            Assert.That(result[2], Is.EqualTo(expectedResult3));
        }

        [Test, AutoData]
        public void CartesianCorrectionIsCorrect(string name, FireMission sut)
        {
            var weapon = new Weapon(name, new Mils(942), new Mils(1547), new List<double> { 96.8, 120.4, 141.9 });
            var battery = new Battery(name, new Cartesian(0, 0, 0), weapon);
            var solutionSet = new BatterySolutionSet(battery, new Cartesian(0, 0, 1000), new Mils(0), new List<FireSolution> { });
            var actualResult = sut.ApplyCorrection(solutionSet, new Cartesian(0, 0, 100));
            var expectedResult = new BatterySolutionSet(battery, new Cartesian(0, 0, 1100), new Mils(0), new List<FireSolution> { 
                new FireSolution(120.4, new Mils(1173), SolutionType.Indirect), 
                new FireSolution(141.9, new Mils(1312), SolutionType.Indirect) });

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test, AutoData]
        public void PolarCorrectionIsCorrectlyBalanced(string name, FireMission sut)
        {
            var weapon = new Weapon(name, new Mils(942), new Mils(1547), new List<double> { 96.8, 120.4, 141.9 });
            var battery = new Battery(name, new Cartesian(0, 0, 0), weapon);
            var solutionSet = new BatterySolutionSet(battery, new Cartesian(0, 0, 1000), new Mils(0), new List<FireSolution> {
                new FireSolution(120.4, new Mils(1222), SolutionType.Indirect),
                new FireSolution(141.9, new Mils(1341), SolutionType.Indirect) });
            var fo = new ForwardObserver(name, new Cartesian(0, 0, 0));
            var deltaAzimuth = new Mils(1600);
            var deltaElevation = new Mils(0);
            var dDistance = 0.0;
            var negDeltaAzimuth = new Mils(-1600);
            var negDeltaElevation = new Mils(0);
            var negDistance = 0.0;
            var moved = sut.ApplyCorrection(solutionSet, fo, deltaAzimuth, deltaElevation, dDistance);
            var movedBack = sut.ApplyCorrection(moved, fo, negDeltaAzimuth, negDeltaElevation, negDistance);

            Assert.That(movedBack, Is.EqualTo(solutionSet));
        }

        [Test, AutoData]
        public void PolarCorrectionIsCorrectlyBalancedErrorTest(string name, FireMission sut)
        {
            var weapon = new Weapon(name, new Mils(942), new Mils(1547), new List<double> { 96.8, 120.4, 141.9 });
            var battery = new Battery(name, new Cartesian(0, 0, 0), weapon);
            var solutionSet = new BatterySolutionSet(battery, new Cartesian(0, 0, 1000), new Mils(0), new List<FireSolution> {
                new FireSolution(120.4, new Mils(1222), SolutionType.Indirect),
                new FireSolution(141.9, new Mils(1341), SolutionType.Indirect) });
            var fo = new ForwardObserver(name, new Cartesian(0, 0, 0));
            var deltaAzimuth = new Mils(1600);
            var deltaElevation = new Mils(0);
            var dDistance = 0.0;
            var negDeltaAzimuth = new Mils(-1601);
            var negDeltaElevation = new Mils(0);
            var negDistance = 0.0;
            var moved = sut.ApplyCorrection(solutionSet, fo, deltaAzimuth, deltaElevation, dDistance);
            var movedBack = sut.ApplyCorrection(moved, fo, negDeltaAzimuth, negDeltaElevation, negDistance);

            Assert.That(movedBack, Is.Not.EqualTo(solutionSet));
        }

        [Test]
        [InlineAutoData(1600)]
        [InlineAutoData(8000)]
        [InlineAutoData(1234)]
        public void PolarCorrectionIsCorrectlyBalancedDoubleApply(
            int mils,
            string name, FireMission sut
            )
        {
            var weapon = new Weapon(name, new Mils(942), new Mils(1547), new List<double> { 96.8, 120.4, 141.9 });
            var battery = new Battery(name, new Cartesian(0, 0, 0), weapon);
            var solutionSet = new BatterySolutionSet(battery, new Cartesian(0, 0, 1000), new Mils(0), new List<FireSolution> {
                new FireSolution(120.4, new Mils(1222), SolutionType.Indirect),
                new FireSolution(141.9, new Mils(1341), SolutionType.Indirect) });
            var fo = new ForwardObserver(name, new Cartesian(0, 0, 0));
            var deltaAzimuth = new Mils(mils);
            var deltaElevation = new Mils(0);
            var dDistance = 0.0;
            var negDeltaAzimuth = new Mils(-mils);
            var negDeltaElevation = new Mils(0);
            var negDistance = 0.0;
            var moved = sut.ApplyCorrection(solutionSet, fo, deltaAzimuth, deltaElevation, dDistance);
            var movedAgain = sut.ApplyCorrection(moved, fo, deltaAzimuth, deltaElevation, dDistance);
            var movedBack = sut.ApplyCorrection(movedAgain, fo, negDeltaAzimuth, negDeltaElevation, negDistance);
            var movedBackAgain = sut.ApplyCorrection(movedBack, fo, negDeltaAzimuth, negDeltaElevation, negDistance);

            Assert.That(movedBackAgain, Is.EqualTo(solutionSet));
        }

        [Test]
        [InlineAutoData(5678)]
        public void PolarCorrectionHasSomeRoundingErrorsAtArbitraryInputValues(
            int mils,
            string name, FireMission sut
            )
        {
            var weapon = new Weapon(name, new Mils(942), new Mils(1547), new List<double> { 96.8, 120.4, 141.9 });
            var battery = new Battery(name, new Cartesian(0, 0, 0), weapon);
            var solutionSet = new BatterySolutionSet(battery, new Cartesian(0, 0, 1000), new Mils(0), new List<FireSolution> {
                new FireSolution(120.4, new Mils(1222), SolutionType.Indirect),
                new FireSolution(141.9, new Mils(1341), SolutionType.Indirect) });
            var fo = new ForwardObserver(name, new Cartesian(0, 0, 0));
            var deltaAzimuth = new Mils(mils);
            var deltaElevation = new Mils(0);
            var dDistance = 0.0;
            var negDeltaAzimuth = new Mils(-mils);
            var negDeltaElevation = new Mils(0);
            var negDistance = 0.0;
            var moved = sut.ApplyCorrection(solutionSet, fo, deltaAzimuth, deltaElevation, dDistance);
            var movedAgain = sut.ApplyCorrection(moved, fo, deltaAzimuth, deltaElevation, dDistance);
            var movedBack = sut.ApplyCorrection(movedAgain, fo, negDeltaAzimuth, negDeltaElevation, negDistance);
            var movedBackAgain = sut.ApplyCorrection(movedBack, fo, negDeltaAzimuth, negDeltaElevation, negDistance);

            Assert.That(movedBackAgain, Is.Not.EqualTo(solutionSet));
        }
    }
}
