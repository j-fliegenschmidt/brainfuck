//-----------------------------------------------------------------------
// <copyright file="IInterpreter.cs">
//     Copyright (c) Janis Fliegenschmidt
// </copyright>
//-----------------------------------------------------------------------

namespace Brainfuck.Interpreter.Core
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public delegate Byte GetInputHandler();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="output"></param>
    public delegate void OutputHandler(Byte output);

    /// <summary>
    /// The Interpreter interface. All decorator classes must inherit this interface.
    /// </summary>
    public interface IInterpreter
    {
        event GetInputHandler InputRequested;

        event OutputHandler OutputAvailable;

        void Execute(Instruction instr);
    }
}
