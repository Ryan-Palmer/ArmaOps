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
    public class FireSalvo_Test
    {
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


        // TODO Test GetSolutions and implement / test ApplyCorrection 
    }
}
