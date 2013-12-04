//-----------------------------------------------------------------------
// <copyright file="ByteInterpreter.cs">
//     Copyright (c) Janis Fliegenschmidt
// </copyright>
//-----------------------------------------------------------------------

namespace Brainfuck.Interpreter.Core
{
    using System;
    using System.Collections.Generic;
    using Brainfuck.Interpreter.Core.Exceptions;

    public class ByteInterpreter : IInterpreter<Byte>
    {
        private Stack<List<Instruction>> loopStack;
        private ByteList programSpace;

        public event GetInputHandler<Byte> InputRequested;
        public event OutputHandler<Byte> OutputAvailable;

        public ByteInterpreter()
        {
            this.loopStack = new Stack<List<Instruction>>();
            this.programSpace = new ByteList();
        }

        public void Execute(Byte instr)
        {
            this.Execute((Instruction)instr);
        }

        public void Execute(Instruction instr)
        {
            this.Execute(instr, 0);
        }

        private void Execute(Instruction instr, int depth)
        {
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
                        loopStack.Pop();
                    }
                    else
                    {
                        for (int i = 1; i < this.loopStack.Peek().Count; i++)
                        {
                            this.Execute(this.loopStack.Peek()[i], depth + 1);
                        }

                        this.Execute(Instruction.EndLoop, depth + 1);
                    }

                    break;

                case Instruction.BeginLoop:
                    this.loopStack.Push(new List<Instruction>());

                    break;
            }

            if (this.loopStack.Count == depth + 1)
            {
                this.loopStack.Peek().Add(instr);
            }
        }

        private void OnOutputAvailable(Byte output)
        {
            if (this.OutputAvailable != null)
            {
                this.OutputAvailable(output);
            }
        }

        private Byte OnInputRequested()
        {
            if (this.InputRequested != null)
            {
                return this.InputRequested();
            }
            else
            {
                throw new NoInputSourceAvailableException();
            }
        }
    }
}
