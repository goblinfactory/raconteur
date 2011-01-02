﻿using System.Collections.Generic;

namespace Raconteur
{
    public class XUnit
    {
        public string Name, Namespace, ClassAttr, MethodAttr, Category;
    }

    public class XUnits
    {
        public static readonly Dictionary<string, XUnit> Framework = new Dictionary<string, XUnit>
        {
            { "mstest", new XUnit
            {
                Name = "MsTest",
                Namespace = "Microsoft.VisualStudio.TestTools.UnitTesting",
                ClassAttr = "[TestClass]",
                MethodAttr = "[TestMethod]",
                Category = @"[TestCategory(""{0}"")]",
            }},

            { "nunit", new XUnit
            {
                Name = "NUnit",
                Namespace = "NUnit.Framework",
                ClassAttr = "[TestFixture]",
                MethodAttr = "[Test]",
                Category = @"[Category(""{0}"")]",
            }},

            { "mbunit", new XUnit
            {
                Name = "MbUnit",
                Namespace = "MbUnit.Framework",
                ClassAttr = "[TestFixture]",
                MethodAttr = "[Test]",
                Category = @"[Category(""{0}"")]",
            }},

            { "xunit", new XUnit
            {
                Name = "xUnit",
                Namespace = "Xunit",
                ClassAttr = string.Empty,
                MethodAttr = "[Fact]",
                Category = @"[Trait(""Category"", ""{0}""]",
            }},
        };
    }
}