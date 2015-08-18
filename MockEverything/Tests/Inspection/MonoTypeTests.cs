﻿namespace MockEverythingTests.Inspection
{
    using Demo;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MockEverything.Inspection;
    using MockEverything.Inspection.MonoCecil;
    using System.Linq;

    [TestClass]
    public class MonoTypeTests
    {
        [TestMethod]
        public void TestGetName()
        {
            var actual = this.SampleAssembly.FindType("MockEverythingTests.Inspection.Demo.SimpleClass").Name;
            var expected = "SimpleClass";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetFullName()
        {
            var actual = this.SampleAssembly.FindType("MockEverythingTests.Inspection.Demo.SimpleClass").FullName;
            var expected = "MockEverythingTests.Inspection.Demo.SimpleClass";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestFindMethods()
        {
            Assert.IsTrue(this.MethodExists("SimpleClass", "SimpleMethod"));
        }

        [TestMethod]
        public void TestFindMethodsMissing()
        {
            Assert.IsFalse(this.MethodExists("SimpleClass", "MissingMethod"));
        }

        [TestMethod]
        public void TestFindMethodsPrivate()
        {
            Assert.IsTrue(this.MethodExists("SimpleClass", "PrivateMethod"));
        }

        [TestMethod]
        public void TestFindMethodsProtected()
        {
            Assert.IsTrue(this.MethodExists("SimpleClass", "ProtectedMethod"));
        }

        [TestMethod]
        public void TestFindMethodsInternal()
        {
            Assert.IsTrue(this.MethodExists("SimpleClass", "InternalMethod"));
        }

        [TestMethod]
        public void TestFindMethodsStatic()
        {
            Assert.IsTrue(this.MethodExists("SimpleClass", "StaticMethod"));
        }

        [TestMethod]
        public void TestFindMethodsPrivateStatic()
        {
            Assert.IsTrue(this.MethodExists("SimpleClass", "PrivateStaticMethod"));
        }

        [TestMethod]
        public void TestFindMethodsProtectedStatic()
        {
            Assert.IsTrue(this.MethodExists("SimpleClass", "ProtectedStaticMethod"));
        }

        [TestMethod]
        public void TestFindMethodsInternalStatic()
        {
            Assert.IsTrue(this.MethodExists("SimpleClass", "InternalStaticMethod"));
        }

        [TestMethod]
        public void TestFindMethodsStaticWithStaticFilter()
        {
            Assert.IsTrue(this.MethodExists("SimpleClass", "StaticMethod", MemberType.Static));
        }

        [TestMethod]
        public void TestFindMethodsStaticWithInstanceFilter()
        {
            Assert.IsFalse(this.MethodExists("SimpleClass", "StaticMethod", MemberType.Instance));
        }

        [TestMethod]
        public void TestFindMethodsInstanceWithStaticFilter()
        {
            Assert.IsFalse(this.MethodExists("SimpleClass", "SimpleMethod", MemberType.Static));
        }

        [TestMethod]
        public void TestFindMethodsInstanceWithInstanceFilter()
        {
            Assert.IsTrue(this.MethodExists("SimpleClass", "SimpleMethod", MemberType.Instance));
        }

        [TestMethod]
        public void TestFindMethodsAttributeMissing()
        {
            Assert.IsFalse(this.MethodExists("SimpleClass", "SimpleMethod", MemberType.Instance, typeof(DemoAttribute)));
        }

        [TestMethod]
        public void TestFindMethodsSingleAttribute()
        {
            Assert.IsTrue(this.MethodExists("SimpleClass", "DecoratedMethod", MemberType.Instance, typeof(DemoAttribute)));
        }

        [TestMethod]
        public void TestFindMethodsAllAttributes()
        {
            Assert.IsTrue(this.MethodExists("SimpleClass", "DecoratedMethod", MemberType.Instance, typeof(DemoAttribute), typeof(DemoSecondAttribute)));
        }

        [TestMethod]
        public void TestFindMethodsMoreAttributes()
        {
            Assert.IsFalse(this.MethodExists("SimpleClass", "DecoratedMethod", MemberType.Instance, typeof(DemoAttribute), typeof(DemoSecondAttribute), typeof(System.SerializableAttribute)));
        }

        [TestMethod]
        public void TestFindMethodsProperty()
        {
            Assert.IsTrue(this.MethodExists("SimpleClass", "get_SimpleProperty"));
        }

        [TestMethod]
        public void TestFindMethodsPropertyPrivate()
        {
            Assert.IsTrue(this.MethodExists("SimpleClass", "get_PrivateProperty"));
        }

        [TestMethod]
        public void TestFindMethodsPropertyPrivateGetter()
        {
            Assert.IsTrue(this.MethodExists("SimpleClass", "get_PropertyPrivateGetter"));
        }

        [TestMethod]
        public void TestFindMethodsPropertyInstanceFilterStatic()
        {
            Assert.IsFalse(this.MethodExists("SimpleClass", "get_SimpleProperty", MemberType.Static));
        }

        [TestMethod]
        public void TestFindMethodsPropertyStaticFilterStatic()
        {
            Assert.IsTrue(this.MethodExists("SimpleClass", "get_StaticProperty", MemberType.Static));
        }

        [TestMethod]
        public void TestFindMethodsPropertyStaticFilterInstance()
        {
            Assert.IsFalse(this.MethodExists("SimpleClass", "get_StaticProperty", MemberType.Instance));
        }

        [TestMethod]
        public void TestFindMethodsPropertyAttributeMissing()
        {
            Assert.IsFalse(this.MethodExists("SimpleClass", "get_SimpleProperty", MemberType.Instance, typeof(DemoAttribute)));
        }

        [TestMethod]
        public void TestFindMethodsPropertySingleAttribute()
        {
            Assert.IsTrue(this.MethodExists("SimpleClass", "get_DecoratedProperty", MemberType.Instance, typeof(DemoAttribute)));
        }

        [TestMethod]
        public void TestFindMethodsPropertyAllAttributes()
        {
            Assert.IsTrue(this.MethodExists("SimpleClass", "get_DecoratedProperty", MemberType.Instance, typeof(DemoAttribute), typeof(DemoSecondAttribute)));
        }

        [TestMethod]
        public void TestFindMethodsPropertyMoreAttributes()
        {
            Assert.IsFalse(this.MethodExists("SimpleClass", "get_DecoratedProperty", MemberType.Instance, typeof(DemoAttribute), typeof(DemoSecondAttribute), typeof(System.SerializableAttribute)));
        }

        [TestMethod]
        public void TestFindMethodsPropertyGetterSingleAttribute()
        {
            Assert.IsTrue(this.MethodExists("SimpleClass", "get_PropertyDecoratedGetter", MemberType.Instance, typeof(DemoAttribute)));
        }

        [TestMethod]
        public void TestFindMethodsPropertyGetterSingleAttributeMatchingSetter()
        {
            Assert.IsFalse(this.MethodExists("SimpleClass", "set_PropertyDecoratedGetter", MemberType.Instance, typeof(DemoAttribute)));
        }

        [TestMethod]
        public void TestFindMethodsPropertyGetterAllAttributes()
        {
            Assert.IsTrue(this.MethodExists("SimpleClass", "get_PropertyDecoratedGetter", MemberType.Instance, typeof(DemoAttribute), typeof(DemoSecondAttribute)));
        }

        [TestMethod]
        public void TestFindMethodsPropertyGetterMoreAttributes()
        {
            Assert.IsFalse(this.MethodExists("SimpleClass", "get_PropertyDecoratedGetter", MemberType.Instance, typeof(DemoAttribute), typeof(DemoSecondAttribute), typeof(System.SerializableAttribute)));
        }

        [TestMethod]
        public void TestFindAttributeCompareTypes()
        {
            var actual = this.SampleAssembly
                .FindType("MockEverythingTests.Inspection.Demo.DecoratedCustomClass")
                .FindAttribute<DemoAttribute>()
                .GetType();

            var expected = typeof(DemoAttribute);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestFindAttribute()
        {
            var actual = this.SampleAssembly
                .FindType("MockEverythingTests.Inspection.Demo.DecoratedCustomClass")
                .FindAttribute<DemoAttribute>()
                .Text;

            var expected = "Hello, World!";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(AttributeNotFoundException))]
        public void TestFindAttributeMissing()
        {
            this.SampleAssembly
                .FindType("MockEverythingTests.Inspection.Demo.DecoratedCustomClass")
                .FindAttribute<DemoSecondAttribute>();
        }

        private bool MethodExists(string typeName, string methodName)
        {
            return this.SampleAssembly
                .FindTypes()
                .Single(t => t.Name == typeName)
                .FindMethods()
                .Select(m => m.Name)
                .Contains(methodName);
        }

        private bool MethodExists(string typeName, string methodName, MemberType filter, params System.Type[] attributes)
        {
            return this.SampleAssembly
                .FindTypes()
                .Single(t => t.Name == typeName)
                .FindMethods(filter, attributes)
                .Select(m => m.Name)
                .Contains(methodName);
        }

        private Assembly SampleAssembly
        {
            get
            {
                return new Assembly(this.SampleAssemblyPath);
            }
        }

        private string SampleAssemblyPath
        {
            get
            {
                var codeBase = typeof(SimpleClass).Assembly.CodeBase;
                return System.Uri.UnescapeDataString(new System.UriBuilder(codeBase).Path);
            }
        }
    }
}
