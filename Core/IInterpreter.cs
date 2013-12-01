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
    public delegate T GetInputHandler<T>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="output"></param>
    public delegate void OutputHandler<T>(T output);

    /// <summary>
    /// The Interpreter interface. All decorator classes must inherit this interface.
    /// </summary>
    public interface IInterpreter
    {
        event GetInputHandler<Byte> InputRequested;

        event OutputHandler<Byte> OutputAvailable;

        void Execute(Instruction instr);
    }
}
