﻿//-----------------------------------------------------------------------
// <copyright file="ByteInterpreter.cs">
//     Copyright (c) Janis Fliegenschmidt
// </copyright>
//-----------------------------------------------------------------------

namespace Brainfuck.Interpreter.Core
{
    using Brainfuck.Interpreter.Core.Exceptions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The base interpreter class providing the actual interpretation logic.
    /// </summary>
    public class ByteInterpreter : IInterpreter<Byte>
    {
        private readonly int MAX_DEPTH = 7000;
        private Stack<List<Instruction>> loopStack;
        private ByteList programSpace;

        public event GetInputHandler<Byte> InputRequested;
        public event OutputHandler<Byte> OutputAvailable;

        public ByteInterpreter()
        {
            this.loopStack = new Stack<List<Instruction>>();
            this.programSpace = new ByteList();
        }

        /// <summary>
        /// Executes the instruction that is mapped to the input byte.
        /// </summary>
        /// <param name="instr">The instruction byte.</param>
        public void Execute(Byte instr)
        {
            this.Execute((Instruction)instr);
        }

        /// <summary>
        /// Executes the passed instruction.
        /// </summary>
        /// <param name="instr">The instruction.</param>
        public void Execute(Instruction instr)
        {
            this.Execute(instr, 0, 0);
        }

        /// <summary>
        /// A private wrapper for the Execute method that also takes
        /// recursion depth parameters, which are needed to preempt
        /// stack overflow exceptions and for instruction caching 
        /// when looping.
        /// </summary>
        /// <param name="instr">The instruction.</param>
        /// <param name="loopDepth">The current loop depth.</param>
        /// <param name="recursionDepth">The current recursion depth.</param>
        private void Execute(Instruction instr, int loopDepth, int recursionDepth)
        {
            if (recursionDepth > MAX_DEPTH)
            {
                throw new StackOverflowException("Recursion may not exceed depth of " + MAX_DEPTH);
            }

            switch (instr)
            {
                case Instruction.IncrementPointer:
                    this.programSpace.IncrementPointer();
                    break;

                case Instruction.DecrementPointer:
                    this.programSpace.DecrementPointer();
                    break;

                case Instruction.IncrementValue:
                    this.programSpace.IncrementValue();
                    break;

                case Instruction.DecrementValue:
                    this.programSpace.DecrementValue();
                    break;

                case Instruction.PrintByte:
                    this.OnOutputAvailable(this.programSpace.Value);
                    break;

                case Instruction.ReadByte:
                    this.programSpace.Value = this.OnInputRequested();
                    break;

                case Instruction.EndLoop:
                    if (this.loopStack.Count < 1)
                    {
                        throw new InvalidOperationException();
                    }

                    if (this.programSpace.Value == 0)
                    {
                        this.loopStack.Pop();
                    }
                    else
                    {
                        for (int i = 1; i < this.loopStack.Peek().Count; i++)
                        {
                            this.Execute(this.loopStack.Peek()[i], loopDepth + 1, loopDepth + 1);
                        }

                        this.Execute(Instruction.EndLoop, loopDepth, recursionDepth + 1);
                    }

                    break;

                case Instruction.BeginLoop:
                    this.loopStack.Push(new List<Instruction>());

                    break;
            }

            if (this.loopStack.Count == loopDepth + 1)
            {
                this.loopStack.Peek().Add(instr);
            }
        }

        /// <summary>
        /// Internal handling of the OutputAvailable event.
        /// </summary>
        /// <param name="output">The output to be exposed.</param>
        private void OnOutputAvailable(Byte output)
        {
            OutputHandler<Byte> handler = this.OutputAvailable;

            if (handler != null)
            {
                handler(output);
            }
        }

        /// <summary>
        /// Internal handling of the InputRequested event.
        /// </summary>
        /// <returns>Returns the requested input.</returns>
        private Byte OnInputRequested()
        {
            GetInputHandler<Byte> handler = this.InputRequested;

            if (handler != null)
            {
                return handler();
            }
            else
            {
                throw new NoInputSourceAvailableException();
            }
        }
    }
}
