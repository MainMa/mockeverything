﻿// <copyright file="TypeMatching.cs">
//      Copyright (c) Arseni Mourzenko 2015. The code is distributed under the MIT License.
// </copyright>
// <author id="5c2316d3-622a-4a8d-816d-5054a48f415f">Arseni Mourzenko</author>

namespace MockEverything.Engine.Browsers
{
    using System.Diagnostics.Contracts;
    using System.Linq;
    using Attributes;
    using Inspection;

    /// <summary>
    /// Represents a comparer which matches proxy types to target types.
    /// </summary>
    public class TypeMatching : IMatching<IType, IAssembly>
    {
        /// <summary>
        /// Finds, within the target assembly, a type which corresponds to the proxy type.
        /// </summary>
        /// <param name="proxy">The proxy type.</param>
        /// <param name="targetAssembly">The assembly expected to contain the target type.</param>
        /// <returns>The type from the target assembly which matches the specified proxy type.</returns>
        /// <exception cref="MatchNotFoundException">The match doesn't exist.</exception>
        public IType FindMatch(IType proxy, IAssembly targetAssembly)
        {
            Contract.Requires(proxy != null);
            Contract.Requires(targetAssembly != null);
            Contract.Ensures(Contract.Result<IType>() != null);

            var values = proxy.FindAttributeValues<ProxyOfAttribute>().ToList();
            var type = (dynamic)values.Single();
            string fullName = type.FullName;
            return targetAssembly.FindType(fullName);
        }
    }
}