using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;

namespace Botworx
{

    public abstract class Enum<T, U> where T : Enum<T, U>, new()
    {
        static readonly List<string> names;

        static readonly List<U> values;

        static bool allowInstanceExceptions;

        static Enum()
        {
            Type t = typeof(T);
            Type u = typeof(U);
            if (t == u) throw new InvalidOperationException(String.Format("{0} and its underlying type cannot be the same", t.Name));
            BindingFlags bf = BindingFlags.Static | BindingFlags.Public;
            FieldInfo[] fia = t.GetFields(bf);
            names = new List<string>();
            values = new List<U>();
            for (int i = 0; i < fia.Length; i++)
            {
                if (fia[i].FieldType == u && (fia[i].IsLiteral || fia[i].IsInitOnly))
                {
                    names.Add(fia[i].Name);
                    values.Add((U)fia[i].GetValue(null));
                }
            }
            if (names.Count == 0) throw new InvalidOperationException(String.Format("{0} has no suitable fields", t.Name));
        }

        public static bool AllowInstanceExceptions
        {
            get { return allowInstanceExceptions; }
            set { allowInstanceExceptions = value; }
        }

        public static string[] GetNames()
        {
            return names.ToArray();
        }

        public static string[] GetNames(U value)
        {
            List<string> nameList = new List<string>();
            for (int i = 0; i < values.Count; i++)
            {
                if (values[i].Equals(value)) nameList.Add(names[i]);
            }
            return nameList.ToArray();
        }

        public static U[] GetValues()
        {
            return values.ToArray();
        }

        public static int[] GetIndices(U value)
        {
            List<int> indexList = new List<int>();
            for (int i = 0; i < values.Count; i++)
            {
                if (values[i].Equals(value)) indexList.Add(i);
            }
            return indexList.ToArray();
        }

        public static int IndexOf(string name)
        {
            return names.IndexOf(name);
        }

        public static U ValueOf(string name)
        {
            int index = names.IndexOf(name);
            if (index >= 0)
            {
                return values[index];
            }
            throw new ArgumentException(String.Format("'{0}' is not a defined name of {1}", name, typeof(T).Name));
        }

        public static string FirstNameWith(U value)
        {
            int index = values.IndexOf(value);
            if (index >= 0)
            {
                return names[index];
            }
            throw new ArgumentException(String.Format("'{0}' is not a defined value of {1}", value, typeof(T).Name));
        }

        public static int FirstIndexWith(U value)
        {
            int index = values.IndexOf(value);
            if (index >= 0)
            {
                return index;
            }
            throw new ArgumentException(String.Format("'{0}' is not a defined value of {1}", value, typeof(T).Name));
        }

        public static string NameAt(int index)
        {
            if (index >= 0 && index < Count)
            {
                return names[index];
            }
            throw new IndexOutOfRangeException(String.Format("Index must be between 0 and {0}", Count - 1));
        }

        public static U ValueAt(int index)
        {
            if (index >= 0 && index < Count)
            {
                return values[index];
            }
            throw new IndexOutOfRangeException(String.Format("Index must be between 0 and {0}", Count - 1));
        }


        public static Type UnderlyingType
        {
            get { return typeof(U); }
        }

        public static int Count
        {
            get { return names.Count; }
        }

        public static bool IsDefinedName(string name)
        {
            if (names.IndexOf(name) >= 0) return true;
            return false;
        }

        public static bool IsDefinedValue(U value)
        {
            if (values.IndexOf(value) >= 0) return true;
            return false;
        }

        public static bool IsDefinedIndex(int index)
        {
            if (index >= 0 && index < Count) return true;
            return false;
        }

        public static T ByName(string name)
        {
            if (!IsDefinedName(name))
            {
                if (allowInstanceExceptions) throw new ArgumentException(String.Format("'{0}' is not a defined name of {1}", name, typeof(T).Name));
                return null;
            }
            T t = new T();
            t._index = names.IndexOf(name);
            return t;
        }

        public static T ByValue(U value)
        {
            if (!IsDefinedValue(value))
            {
                if (allowInstanceExceptions) throw new ArgumentException(String.Format("'{0}' is not a defined value of {1}", value, typeof(T).Name));
                return null;
            }
            T t = new T();
            t._index = values.IndexOf(value);
            return t;
        }

        public static T ByIndex(int index)
        {
            if (index < 0 || index >= Count)
            {
                if (allowInstanceExceptions) throw new ArgumentException(String.Format("Index must be between 0 and {0}", Count - 1));
                return null;
            }
            T t = new T();
            t._index = index;
            return t;
        }

        protected int _index;

        public int Index
        {
            get { return _index; }
            set
            {
                if (value < 0 || value >= Count)
                {
                    if (allowInstanceExceptions) throw new ArgumentException(String.Format("Index must be between 0 and {0}", Count - 1));
                    return;
                }
                _index = value;
            }
        }

        public string Name
        {
            get { return names[_index]; }
            set
            {
                int index = names.IndexOf(value);
                if (index == -1)
                {
                    if (allowInstanceExceptions) throw new ArgumentException(String.Format("'{0}' is not a defined name of {1}", value, typeof(T).Name));
                    return;
                }
                _index = index;
            }
        }

        public U Value
        {
            get { return values[_index]; }
            set
            {
                int index = values.IndexOf(value);
                if (index == -1)
                {
                    if (allowInstanceExceptions) throw new ArgumentException(String.Format("'{0}' is not a defined value of {1}", value, typeof(T).Name));
                    return;
                }
                _index = index;
            }
        }

        public override string ToString()
        {
            return names[_index];
        }
    }

    class Fractions : Enum<Fractions, double>
    {
        public static readonly double Sixth = 1.0 / 6.0;
        public static readonly double Fifth = 0.2;
        public static readonly double Quarter = 0.25;
        public static readonly double Third = 1.0 / 3.0;
        public static readonly double Half = 0.5;

        public double FractionOf(double amount)
        {
            return this.Value * amount;
        }
    }


    class Seasons : Enum<Seasons, DateTime>
    {
        public static readonly DateTime Spring = new DateTime(2011, 3, 1);
        public static readonly DateTime Summer = new DateTime(2011, 6, 1);
        public static readonly DateTime Autumn = new DateTime(2011, 9, 1);
        public static readonly DateTime Winter = new DateTime(2011, 12, 1);
    }

    public class Planets : Enum<Planets, Planet>
    {
        public static readonly Planet Mercury = new Planet(3.303e+23, 2.4397e6);
        public static readonly Planet Venus = new Planet(4.869e+24, 6.0518e6);
        public static readonly Planet Earth = new Planet(5.976e+24, 6.37814e6);
        public static readonly Planet Mars = new Planet(6.421e+23, 3.3972e6);
        public static readonly Planet Jupiter = new Planet(1.9e+27, 7.1492e7);
        public static readonly Planet Saturn = new Planet(5.688e+26, 6.0268e7);
        public static readonly Planet Uranus = new Planet(8.686e+25, 2.5559e7);
        public static readonly Planet Neptune = new Planet(1.024e+26, 2.4746e7);

        public bool IsCloserToSunThan(Planets p)
        {
            if (this.Index < p.Index) return true;
            return false;
        }
    }


    public class Planet
    {
        public double Mass { get; private set; }  // in kilograms
        public double Radius { get; private set; } // in meters

        public Planet(double mass, double radius)
        {
            Mass = mass;
            Radius = radius;
        }

        // universal gravitational constant  (m^3 kg^-1 s^-2)
        public static double G = 6.67300E-11;

        public double SurfaceGravity()
        {
            return G * Mass / (Radius * Radius);
        }

        public double SurfaceWeight(double otherMass)
        {
            return otherMass * SurfaceGravity();
        }
    }


    class Test
    {
        static void Main()
        {
            Console.Clear();

            Fractions f = new Fractions();
            Console.WriteLine(f); // Sixth
            f.Value = Fractions.Fifth;
            Console.WriteLine(f.Index); // 1
            Fractions f2 = Fractions.ByName("Third");
            Console.WriteLine(f2.FractionOf(30)); // 10 
            f2.Index = 4;
            Console.WriteLine(f2); // Half
            string name = Fractions.FirstNameWith(0.25);
            Console.WriteLine(name); // Quarter
            Fractions f3 = Fractions.ByName("Tenth"); // no exception by default
            Console.WriteLine(f3 == null); // true
            f3 = Fractions.ByValue(1.0 / 3.0);
            Console.WriteLine(f3); // Third

            Console.WriteLine();

            foreach (string season in Seasons.GetNames())
            {
                Console.WriteLine("{0} starts on {1}", season, Seasons.ValueOf(season).ToString("d MMMM"));
            }

            Console.WriteLine();

            double earthWeight = 80;
            double mass = earthWeight / Planets.Earth.SurfaceGravity();
            foreach (Planet p in Planets.GetValues())
            {
                Console.WriteLine("Weight on {0} is {1:F2} kg", Planets.FirstNameWith(p), p.SurfaceWeight(mass));
            }

            Console.WriteLine();

            Planets mercury = Planets.ByName("Mercury");
            Planets earth = Planets.ByIndex(2);
            Planets jupiter = new Planets();
            jupiter.Value = Planets.Jupiter;
            Console.WriteLine("It is {0} that Mercury is closer to the Sun than the Earth", mercury.IsCloserToSunThan(earth)); // True    
            Console.WriteLine("It is {0} that Jupiter is closer to the Sun than the Earth", jupiter.IsCloserToSunThan(earth)); // False    
            Console.ReadKey();
        }
    }
}
