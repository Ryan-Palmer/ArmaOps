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
            Mils directSolution,
            Mils indirectSolution)
        {
            var sut = new FireSolution(chargeVelocity, directSolution, indirectSolution);
            var sameValues = new FireSolution(chargeVelocity, directSolution, indirectSolution);

            Assert.That(sameValues, Is.EqualTo(sut));
            Assert.That(sameValues.GetHashCode(), Is.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsFalseIfChargeVelocityMismatch(
            double chargeVelocity,
            double chargeVelocity2,
            Mils directSolution,
            Mils indirectSolution)
        {
            var sut = new FireSolution(chargeVelocity, directSolution, indirectSolution);
            var other = new FireSolution(chargeVelocity2, directSolution, indirectSolution);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsFalseIfDirectSolutionMismatch(
            double chargeVelocity,
            Mils directSolution,
            Mils indirectSolution,
            Mils otherSolution)
        {
            var sut = new FireSolution(chargeVelocity, directSolution, indirectSolution);
            var other = new FireSolution(chargeVelocity, otherSolution, indirectSolution);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsFalseIfIndirectSolutionMismatch(
            double chargeVelocity,
            Mils directSolution,
            Mils indirectSolution,
            Mils otherSolution)
        {
            var sut = new FireSolution(chargeVelocity, directSolution, indirectSolution);
            var other = new FireSolution(chargeVelocity, directSolution, otherSolution);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }
    }
}
