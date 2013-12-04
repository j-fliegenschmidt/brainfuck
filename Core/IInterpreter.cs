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
    /// The generic Interpreter interface.
    /// </summary>
    public interface IInterpreter<T> : IInterpreter
    {
        event GetInputHandler<T> InputRequested;

        event OutputHandler<T> OutputAvailable;

        void Execute(T instr);
    }

    /// <summary>
    /// The non-generic Interpreter base interface
    /// </summary>
    public interface IInterpreter
    {
        void Execute(Instruction instr);
    }
}
