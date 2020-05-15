﻿using NUnit.Framework;

namespace NUnit.Project
{
    public class ByAlphabeticalOrder
    {
        public static bool Test1Called;
        public static bool Test2Called;
        public static bool Test3Called;

        [Test]
        public void Test11()
        {
            Test1Called = true;

            Assert.IsFalse(Test2Called);
            Assert.IsFalse(Test3Called);
        }

        [Test]
        public void Test2()
        {
            Test2Called = true;

            Assert.IsTrue(Test1Called);
            Assert.IsFalse(Test3Called);
        }

        [Test]
        public void Test3()
        {
            Test3Called = true;

            Assert.IsTrue(Test1Called);
            Assert.IsTrue(Test2Called);
        }
    }
}
