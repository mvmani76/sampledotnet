﻿//<Snippet1>
using System;

namespace UsageLibrary
{
    public class TestBadPoint
    {
        public static void Main()
        {
            BadPoint a = new BadPoint(1,1);
            BadPoint b = new BadPoint(2,2);
            BadPoint a1 = a;
            BadPoint bcopy = new BadPoint(2,2);

            Console.WriteLine("a =  {0} and b = {1} are equal? {2}", a, b, a.Equals(b)? "Yes":"No");
            Console.WriteLine("a == b ? {0}", a == b ? "Yes":"No");
            Console.WriteLine("a1 and a are equal? {0}", a1.Equals(a)? "Yes":"No");
            Console.WriteLine("a1 == a ? {0}", a1 == a ? "Yes":"No");

            // This test demonstrates the inconsistent behavior of == and Object.Equals.
            Console.WriteLine("b and bcopy are equal ? {0}", bcopy.Equals(b)? "Yes":"No");
            Console.WriteLine("b == bcopy ? {0}", b == bcopy ? "Yes":"No");
        }
    }
}
//</Snippet1>
namespace UsageLibrary
{
    public class BadPoint
    {
        private int x,y, id;
        private static int NextId;

        static BadPoint()
        {
            NextId = -1;
        }
        public BadPoint(int x, int y)
        {
            this.x = x;
            this.y = y;
            id = ++(BadPoint.NextId);
        }

        public override string ToString()
        {
            return String.Format("([{0}] {1},{2})",id,x,y);
        }

        public int X {get {return x;}}

        public int Y {get {return x;}}
        public int Id {get {return id;}}

        public override int GetHashCode()
        {
            return id;
        }
        // Violates rule: OverrideEqualsOnOverridingOperatorEquals.

        // BadPoint redefines the equality operator to ignore the id value.
        // This is different from how the inherited implementation of
        // System.Object.Equals behaves for value types.
        // It is not safe to exclude the violation for this type.
        public static bool operator== (BadPoint p1, BadPoint p2)
        {
            return ((p1.x == p2.x) && (p1.y == p2.y));
        }
        // The C# compiler and rule OperatorsShouldHaveSymmetricalOverloads require this.
        public static bool operator!= (BadPoint p1, BadPoint p2)
        {
            return !(p1 == p2);
        }
        public override bool Equals(object o)
        {
            return true;
        }
    }
}