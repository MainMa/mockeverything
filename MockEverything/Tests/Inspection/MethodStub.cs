﻿namespace MockEverythingTests.Inspection
{
    using System.Collections.Generic;
    using System.Linq;
    using CommonStubs;
    using MockEverything.Inspection;

    public class MethodStub : IMethod
    {
        public MethodStub(string name, IType returnType = null, IEnumerable<Parameter> parameters = null, IEnumerable<string> genericTypes = null)
        {
            this.Name = name;
            this.ReturnType = returnType ?? new TypeStub("Void") { FullName = "System.Void" };
            this.Parameters = parameters ?? Enumerable.Empty<Parameter>();
            this.GenericTypes = genericTypes ?? Enumerable.Empty<string>();
        }

        public IEnumerable<string> GenericTypes { get; private set; }

        public string Name { get; private set; }

        public IEnumerable<Parameter> Parameters { get; private set; }

        public IType ReturnType { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is IMethod))
            {
                return false;
            }

            var other = (IMethod)obj;
            return
                this.Name == other.Name &&
                this.ReturnType.Equals(other.ReturnType) &&
                this.Parameters.SequenceEqual(other.Parameters) &&
                this.GenericTypes.SequenceEqual(other.GenericTypes);
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
