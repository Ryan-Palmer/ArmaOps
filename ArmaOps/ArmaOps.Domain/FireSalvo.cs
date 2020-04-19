using ArmaOps.Domain.Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmaOps.Domain
{
    public class FireSalvo
    {
        const double GRAVITY = 9.80665;
        public string Name { get; }
        public Cartesian Target { get; }

        public FireSalvo(Cartesian target)
        {
            Name = Guid.NewGuid().ToString();
            Target = target;
        }

        public FireSalvo(string name, Cartesian target)
        {
            Name = name;
            Target = target;
        }

        public FireSalvo(
            ForwardObserver fo, Mils observedAzimuth,
            Mils observedElevation, int observedDistanceMetres)
        {
            Name = $"Fire Mission for {fo.Name}";
            Target = GetCartesianTarget(fo, observedAzimuth, observedElevation, observedDistanceMetres);
        }

        public FireSalvo(
            string name, ForwardObserver fo, Mils observedAzimuth,
            Mils observedElevation, int observedDistanceMetres)
        {
            Name = name;
            Target = GetCartesianTarget(fo, observedAzimuth, observedElevation, observedDistanceMetres);
        }

        Cartesian GetCartesianTarget(ForwardObserver fo, Mils observedAzimuth,
            Mils observedElevation, int observedDistanceMetres)
        {
            return new Polar(fo.Location,
                observedAzimuth.Radians,
                observedElevation.Radians,
                observedDistanceMetres).ToCartesian();
        }

        public BatterySolutionSet GetSolutionSet(Battery battery)
        {
            // *** Needs testing ***
            //var ballistics = new Ballistics(GRAVITY);
            //var vdist = Target.DY(battery.Location);
            //var hdist = Target.ToPolar(battery.Location).Distance;
            //var solutions = new List<FireSolution>();
            //foreach (var cv in battery.Weapon.ChargeVelocities)
            //{
            //    var ballisticSolutions = ballistics?.GetSolutions(cv, hdist, vdist);
            //    if (ballisticSolutions != null)
            //    {
            //        var directSolution = new Mils(ballisticSolutions.NegativeSolution);
            //        var indirectSolution = new Mils(ballisticSolutions.PositiveSolution);
            //        solutions.Add(new FireSolution(cv, directSolution, indirectSolution));
            //    }
            //}
            //return new BatterySolutionSet(battery, Target, solutions);
            throw new NotImplementedException();
        }

        public IEnumerable<BatterySolutionSet> GetSolutionSets(IEnumerable<Battery> batteries)
        {
            // *** Needs testing ***
            //return batteries.Select(b => GetSolutionSet(b));
            throw new NotImplementedException();
        }

        public FireSalvo ApplyCorrection(Cartesian delta)
        {
            // *** Probably wrong, didn't really think about it ***
            //return new FireSalvo(Name, Target.Add(delta));
            throw new NotImplementedException();
        }

        public FireSalvo ApplyCorrection(
            ForwardObserver observer, Mils deltaAzimuth,
            Mils deltaElevation, int deltaDistanceMetres)
        {
            // *** Probably wrong, didn't really think about it ***
            //var delta =
            //    GetCartesianTarget(
            //        observer, deltaAzimuth,
            //        deltaElevation, deltaDistanceMetres);
            //return new FireSalvo(Name, Target.Add(delta));
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                var other = obj as FireSalvo;
                if (other != null)
                {
                    return
                        Name.Equals(other.Name)
                        && Target.Equals(other.Target);
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (Name, Target).GetHashCode();
        }
    }
}
