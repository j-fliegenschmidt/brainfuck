//-----------------------------------------------------------------------
// <copyright file="Instruction.cs">
//     Copyright (c) Janis Fliegenschmidt
// </copyright>
//-----------------------------------------------------------------------

namespace Brainfuck.Interpreter.Core
{
    /// <summary>
    /// Enumeration that represents the Brainfuck commands.
    /// </summary>
    public enum Instruction : byte
    {
        /// <summary>
        /// '&gt;' in Brainfuck source code.
        /// </summary>
        IncrementPointer = 0x00,

        /// <summary>
        /// '&lt;' in Brainfuck source code.
        /// </summary>
        DecrementPointer = 0x01,

        /// <summary>
        /// '+' in Brainfuck source code.
        /// </summary>
        IncrementValue = 0x02,

        /// <summary>
        /// '-' in Brainfuck source code.
        /// </summary>
        DecrementValue = 0x03,

        /// <summary>
        /// '[' in Brainfuck source code.
        /// </summary>
        BeginLoop = 0x04,

        /// <summary>
        /// ']' in Brainfuck source code.
        /// </summary>
        EndLoop = 0x05,

        /// <summary>
        /// ',' in Brainfuck source code.
        /// </summary>
        ReadByte = 0x06,

        /// <summary>
        /// '.' in Brainfuck source code.
        /// </summary>
        PrintByte = 0x07
    }
}
