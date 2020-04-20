using ArmaOps.Domain;
using AutoFixture.NUnit3;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Test.Domain
{
    [TestFixture]
    public class Mils_Test
    {
        [Test, AutoData]
        public void EqualsTrueIfValuesMatchMilliRadianConstruction(
            int milliRadians)
        {
            var sut = new Mils(milliRadians);
            var sameValues = new Mils(milliRadians);

            Assert.That(sameValues, Is.EqualTo(sut));
            Assert.That(sameValues.GetHashCode(), Is.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsFalseIfValuesMismatchMilliRadianConstruction(
            int milliRadians,
            int milliRadians2)
        {
            var sut = new Mils(milliRadians);
            var other = new Mils(milliRadians2);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsTrueIfValuesMatchRadianConstruction(
            double radians)
        {
            var sut = new Mils(radians);
            var sameValues = new Mils(radians);

            Assert.That(sameValues, Is.EqualTo(sut));
            Assert.That(sameValues.GetHashCode(), Is.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void EqualsFalseIfValuesMismatchRadianConstruction(
            double radians,
            double radians2)
        {
            var sut = new Mils(radians);
            var other = new Mils(radians2);

            Assert.That(other, Is.Not.EqualTo(sut));
            Assert.That(other.GetHashCode(), Is.Not.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void RadianPropertyBalancedWithRadianConstructor(
            int milliRadians)
        {
            var sut = new Mils(milliRadians);
            var sameValues = new Mils(sut.Radians);

            Assert.That(sameValues, Is.EqualTo(sut));
            Assert.That(sameValues.GetHashCode(), Is.EqualTo(sut.GetHashCode()));
        }

        [Test, AutoData]
        public void MilisLessThanNeg3200Wraps(string name)
        {
            var overflow = -3201;
            var wrapped = 3199;
            Assert.That(new Mils(overflow).Value, Is.EqualTo(wrapped));
        }

        [Test, AutoData]
        public void MilisGreaterThanThan3200Wraps(string name)
        {
            var overflow = 3201;
            var wrapped = -3199;
            Assert.That(new Mils(overflow).Value, Is.EqualTo(wrapped));
        }

        [Test, AutoData]
        public void MilisNeg3200ReturnsNormally(string name)
        {
            var validNeg = -3200;
            Assert.That(new Mils(validNeg).Value, Is.EqualTo(validNeg));
        }

        [Test, AutoData]
        public void Milis3200ReturnsNormally(string name)
        {
            var valid = 3200;
            Assert.That(new Mils(valid).Value, Is.EqualTo(valid));
        }

        [Test, AutoData]
        public void AddMaxValuesReturnsCorrectResult()
        {
            var mil1 = new Mils(int.MaxValue);
            var mil2 = new Mils(int.MaxValue);
            var expected = new Mils(-2306);

            Assert.That(mil1.Add(mil2).Value, Is.EqualTo(expected.Value));
        }

        [Test, AutoData]
        public void AddMinValuesReturnsCorrectResult()
        {
            var mil1 = new Mils(int.MinValue);
            var mil2 = new Mils(int.MinValue);
            var expected = new Mils(2304);

            Assert.That(mil1.Add(mil2).Value, Is.EqualTo(expected.Value));
        }

        [Test, AutoData]
        public void AddRolloverValuesReturnsZeroResult()
        {
            var mil1 = new Mils(6300);
            var mil2 = new Mils(100);
            var expected = new Mils(0);

            Assert.That(mil1.Add(mil2).Value, Is.EqualTo(expected.Value));
        }

        [Test, AutoData]
        public void SubtractMaxValuesReturnsCorrectResult()
        {
            var mil1 = new Mils(int.MaxValue);
            var mil2 = new Mils(int.MaxValue);
            var expected = new Mils(0);

            Assert.That(mil1.Sub(mil2).Value, Is.EqualTo(expected.Value));
        }

        [Test, AutoData]
        public void SubtractMinValuesReturnsCorrectResult()
        {
            var mil1 = new Mils(int.MinValue);
            var mil2 = new Mils(int.MinValue);
            var expected = new Mils(0);

            Assert.That(mil1.Sub(mil2).Value, Is.EqualTo(expected.Value));
        }

        [Test, AutoData]
        public void SubtractMinFromMaxValuesReturnsCorrectResult()
        {
            var mil1 = new Mils(int.MinValue);
            var mil2 = new Mils(int.MaxValue);
            var expected = new Mils(2305);

            Assert.That(mil1.Sub(mil2).Value, Is.EqualTo(expected.Value));
        }

        [Test, AutoData]
        public void SubtractMaxFromMinValuesReturnsCorrectResult()
        {
            var mil1 = new Mils(int.MaxValue);
            var mil2 = new Mils(int.MinValue);
            var expected = new Mils(-2305);

            Assert.That(mil1.Sub(mil2).Value, Is.EqualTo(expected.Value));
        }

        [Test, AutoData]
        public void SubtractRolloverValuesReturnsCorrectResult()
        {
            var mil1 = new Mils(0);
            var mil2 = new Mils(100);
            var expected = new Mils(6300);

            Assert.That(mil1.Sub(mil2).Value, Is.EqualTo(expected.Value));
        }

        [Test, AutoData]
        public void SubtractZeroValuesReturnsZeroResult()
        {
            var mil1 = new Mils(0);
            var mil2 = new Mils(0);
            var expected = new Mils(0);

            Assert.That(mil1.Sub(mil2).Value, Is.EqualTo(expected.Value));
        }

        [Test, AutoData]
        public void AddAndSubtractBalanced(Mils mil1, Mils mil2)
        {
            Assert.That(mil1.Add(mil2).Sub(mil1), Is.EqualTo(mil2));
        }
    }
}
