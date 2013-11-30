//-----------------------------------------------------------------------
// <copyright file="NoInputSourceAvailableException.cs">
//     Copyright (c) Janis Fliegenschmidt
// </copyright>
//-----------------------------------------------------------------------

namespace Brainfuck.Interpreter.Core.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception thrown by the Interpreter when no Handler is registered to
    /// request input from.
    /// </summary>
    [Serializable]
    public class NoInputSourceAvailableException : Exception
    {
        public NoInputSourceAvailableException()
            : base()
        {
        }

        public NoInputSourceAvailableException(String message)
            : base(message)
        {
        }

        public NoInputSourceAvailableException(
            String message, Exception innerException)
            : base(message, innerException)
        {
        }

        public NoInputSourceAvailableException(
            SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}
