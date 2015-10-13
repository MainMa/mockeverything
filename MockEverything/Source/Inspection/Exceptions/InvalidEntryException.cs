﻿// <copyright file="InvalidEntryException.cs">
//      Copyright (c) Arseni Mourzenko 2015. The code is distributed under the MIT License.
// </copyright>
// <author id="5c2316d3-622a-4a8d-816d-5054a48f415f">Arseni Mourzenko</author>

namespace MockEverything.Inspection
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// Represents an exception thrown when the signature of the entry is unexpected.
    /// </summary>
    [Serializable]
    public class InvalidEntryException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidEntryException"/> class.
        /// </summary>
        [ExcludeFromCodeCoverage]
        public InvalidEntryException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidEntryException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        [ExcludeFromCodeCoverage]
        public InvalidEntryException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidEntryException"/> class.
        /// </summary>
        /// <param name="message">A message that describes the current exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        [ExcludeFromCodeCoverage]
        public InvalidEntryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidEntryException"/> class with the specified serialization information and context.
        /// </summary>
        /// <param name="info">The data necessary to serialize or deserialize an object.</param>
        /// <param name="context">Description of the source and destination of the specified serialized stream.</param>
        [ExcludeFromCodeCoverage]
        protected InvalidEntryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="System.Runtime.Serialization.SerializationInfo"/> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="System.ArgumentNullException">The info parameter is a null reference (Nothing in Visual Basic).</exception>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        [ExcludeFromCodeCoverage]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
