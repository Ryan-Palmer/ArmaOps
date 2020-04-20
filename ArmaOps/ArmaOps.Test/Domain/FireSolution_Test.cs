using ArmaOps.Domain;
using AutoFixture.NUnit3;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Test.Domain
{
    [TestFixture]
    public class FireSolution_Test
    {
        [Test, AutoData]
        public void EqualsTrueIfAllValuesMatch(
            double chargeVelocity,
            Mils elevation,
            SolutionType sType)
        {
            var sut = new FireSolution(chargeVelocity, elevation, sType);
            var sameValues = new FireSolution(chargeVelocity, elevation, sType);

            Assert.That(sameValues, Is.EqualTo(sut));
            Assert.That(sameValues.GetHashCode(), Is.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsFalseIfChargeVelocityMismatch(
            double chargeVelocity,
            double chargeVelocity2,
            Mils elevation,
            SolutionType sType)
        {
            var sut = new FireSolution(chargeVelocity, elevation, sType);
            var other = new FireSolution(chargeVelocity2, elevation, sType);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsFalseIfElevationMismatch(
            double chargeVelocity,
            Mils elevation,
            Mils elevation2,
            SolutionType sType)
        {
            var sut = new FireSolution(chargeVelocity, elevation, sType);
            var other = new FireSolution(chargeVelocity, elevation2, sType);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsFalseIfSolutionTypeMismatch(
            double chargeVelocity,
            Mils elevation,
            SolutionType sType,
            SolutionType sType2)
        {
            var sut = new FireSolution(chargeVelocity, elevation, sType);
            var other = new FireSolution(chargeVelocity, elevation, sType2);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }
    }
}
