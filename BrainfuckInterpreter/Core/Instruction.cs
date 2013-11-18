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
    enum Instruction : byte
    {
        IncrementPointer = 0x00,
        DecrementPointer = 0x01,
        IncrementValue = 0x02,
        DecrementValue = 0x03,
        BeginLoop = 0x04,
        EndLoop = 0x05,
        ReadByte = 0x06,
        PrintByte = 0x07
    }
}
