//-----------------------------------------------------------------------
// <copyright file="Interpreter.cs">
//     Copyright (c) Janis Fliegenschmidt
// </copyright>
//-----------------------------------------------------------------------

namespace Brainfuck.Interpreter.Core
{
    using System;
    using System.Collections.Generic;

    class Interpreter
    {
        public delegate Byte GetInputHandler();
        public event GetInputHandler InputRequested;

        public delegate void OutputHandler(Byte output);
        public event OutputHandler OutputAvailable;

        public Interpreter()
        {
        }

        public void Run(Instruction[] program)
        {
            Stack<int> loopStack = new Stack<int>();
            ByteList programSpace = new ByteList();

            for (int i = 0; i < program.Length; i++)
            {
                switch (program[i])
                {
                    case Instruction.IncrementPointer:
                        programSpace.IncrementPointer();
                        break;
                    case Instruction.DecrementPointer:
                        programSpace.DecrementPointer();
                        break;
                    case Instruction.IncrementValue:
                        programSpace.IncrementValue();
                        break;
                    case Instruction.DecrementValue:
                        programSpace.DecrementValue();
                        break;
                    case Instruction.PrintByte:
                        this.OnOutputAvailable(programSpace.Value);
                        break;
                    case Instruction.ReadByte:
                        programSpace.Value = this.OnInputRequested();
                        break;
                    case Instruction.EndLoop:
                        if (programSpace.Value == 0)
                        {
                            loopStack.Pop();
                            continue;
                        }
                        else
                        {
                            i = loopStack.Peek() + 1;
                        }

                        break;
                    case Instruction.BeginLoop:
                        loopStack.Push(i);
                        break;
                    default:
                        continue;
                }
            }
        }

        private void OnOutputAvailable(Byte output)
        {
            if (this.OutputAvailable != null)
            {
                this.OnOutputAvailable(output);
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
