using ArmaOps.Domain;
using AutoFixture.NUnit3;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Test.Domain
{
    [TestFixture]
    public class Angle_Test
    {
        [Test, AutoData]
        public void EqualsTrueIfValuesMatchMilliRadianConstruction(
            int milliRadians,
            bool nato)
        {
            var sut = new Angle(milliRadians, nato);
            var sameValues = new Angle(milliRadians, nato);

            Assert.That(sameValues, Is.EqualTo(sut));
            Assert.That(sameValues.GetHashCode(), Is.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsFalseIfValuesMismatchMilliRadianConstruction(
            int milliRadians,
            int milliRadians2,
            bool nato)
        {
            var sut = new Angle(milliRadians, nato);
            var other = new Angle(milliRadians2, nato);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsFalseIfValuesMismatchNatoConstruction(
            int milliRadians)
        {
            var sut = new Angle(milliRadians, true);
            var other = new Angle(milliRadians, false);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsTrueIfValuesMatchRadianConstruction(
            double radians)
        {
            var sut = new Angle(radians);
            var sameValues = new Angle(radians);

            Assert.That(sameValues, Is.EqualTo(sut));
            Assert.That(sameValues.GetHashCode(), Is.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsFalseIfValuesMismatchRadianConstruction(
            double radians,
            double radians2)
        {
            var sut = new Angle(radians);
            var other = new Angle(radians2);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void RadianPropertyBalancedWithRadianConstructor(
            int milliRadians)
        {
            var sut = new Angle(milliRadians, true);
            var sameValues = new Angle(sut.Radians);

            Assert.That(sameValues, Is.EqualTo(sut));
            Assert.That(sameValues.GetHashCode(), Is.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void NATOMilisLessThanZeroWraps(string name)
        {
            var overflow = -1;
            var wrapped = 6399;
            Assert.That(new Angle(overflow, true).NATOMils, Is.EqualTo(wrapped));
        }

        [Test, AutoData]
        public void NATOMilisGreaterThanThan6400Wraps(string name)
        {
            var overflow = 6400;
            var wrapped = 0;
            Assert.That(new Angle(overflow, true).NATOMils, Is.EqualTo(wrapped));
        }

        [Test, AutoData]
        public void NATOMilisZeroReturnsNormally(string name)
        {
            var validNeg = 0;
            Assert.That(new Angle(validNeg, true).NATOMils, Is.EqualTo(validNeg));
        }

        [Test, AutoData]
        public void NATOMilis6399ReturnsNormally(string name)
        {
            var valid = 6399;
            Assert.That(new Angle(valid, true).NATOMils, Is.EqualTo(valid));
        }

        [Test, AutoData]
        public void NATOAddMaxValuesReturnsCorrectResult()
        {
            var mil1 = new Angle(int.MaxValue, true);
            var mil2 = new Angle(int.MaxValue, true);
            var expected = new Angle(4094, true);

            Assert.That(mil1.Add(mil2).NATOMils, Is.EqualTo(expected.NATOMils));
        }

        [Test, AutoData]
        public void NATOAddMinValuesReturnsCorrectResult()
        {
            var mil1 = new Angle(int.MinValue, true);
            var mil2 = new Angle(int.MinValue, true);
            var expected = new Angle(2304, true);

            Assert.That(mil1.Add(mil2).NATOMils, Is.EqualTo(expected.NATOMils));
        }

        [Test, AutoData]
        public void NATOAddRolloverValuesReturnsZeroResult()
        {
            var mil1 = new Angle(6300, true);
            var mil2 = new Angle(100, true);
            var expected = new Angle(0, true);

            Assert.That(mil1.Add(mil2).NATOMils, Is.EqualTo(expected.NATOMils));
        }

        [Test, AutoData]
        public void NATOSubtractMaxValuesReturnsCorrectResult()
        {
            var mil1 = new Angle(int.MaxValue, true);
            var mil2 = new Angle(int.MaxValue, true);
            var expected = new Angle(0, true);

            Assert.That(mil1.Sub(mil2).NATOMils, Is.EqualTo(expected.NATOMils));
        }

        [Test, AutoData]
        public void NATOSubtractMinValuesReturnsCorrectResult()
        {
            var mil1 = new Angle(int.MinValue, true);
            var mil2 = new Angle(int.MinValue, true);
            var expected = new Angle(0, true);

            Assert.That(mil1.Sub(mil2).NATOMils, Is.EqualTo(expected.NATOMils));
        }

        [Test, AutoData]
        public void NATOSubtractMinFromMaxValuesReturnsCorrectResult()
        {
            var mil1 = new Angle(int.MinValue, true);
            var mil2 = new Angle(int.MaxValue, true);
            var expected = new Angle(2305, true);

            Assert.That(mil1.Sub(mil2).NATOMils, Is.EqualTo(expected.NATOMils));
        }

        [Test, AutoData]
        public void NATOSubtractMaxFromMinValuesReturnsCorrectResult()
        {
            var mil1 = new Angle(int.MaxValue, true);
            var mil2 = new Angle(int.MinValue, true);
            var expected = new Angle(4095, true);

            Assert.That(mil1.Sub(mil2).NATOMils, Is.EqualTo(expected.NATOMils));
        }

        [Test, AutoData]
        public void NATOSubtractRolloverValuesReturnsCorrectResult()
        {
            var mil1 = new Angle(0, true);
            var mil2 = new Angle(100, true);
            var expected = new Angle(6300, true);

            Assert.That(mil1.Sub(mil2).NATOMils, Is.EqualTo(expected.NATOMils));
        }

        [Test, AutoData]
        public void NATOSubtractZeroValuesReturnsZeroResult()
        {
            var mil1 = new Angle(0, true);
            var mil2 = new Angle(0, true);
            var expected = new Angle(0, true);

            Assert.That(mil1.Sub(mil2).NATOMils, Is.EqualTo(expected.NATOMils));
        }

        [Test, AutoData]
        public void NATOAddAndSubtractBalanced(Angle mil1, Angle mil2)
        {
            Assert.That(mil1.Add(mil2).Sub(mil1), Is.EqualTo(mil2));
        }
    }
}
