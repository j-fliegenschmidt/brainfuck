//-----------------------------------------------------------------------
// <copyright file="Interpreter.cs">
//     Copyright (c) Janis Fliegenschmidt
// </copyright>
//-----------------------------------------------------------------------

namespace Brainfuck.Interpreter.Core
{
    using System;
    using System.Collections.Generic;

    public class InterpreterBase : IInterpreter
    {
        private Stack<int> loopStack;
        private ByteList programSpace;
        private List<Instruction> loopCache;
        private int loopCachePointer;

        public event GetInputHandler InputRequested;

        public event OutputHandler OutputAvailable;

        public InterpreterBase()
        {
            this.loopStack = new Stack<int>();
            this.programSpace = new ByteList();
            this.loopCache = new List<Instruction>();
            this.loopCachePointer = 0;
        }

        public void Execute(Instruction instr)
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
                    if (this.programSpace.Value == 0)
                    {
                        loopStack.Pop();

                        if (loopStack.Count < 1)
                        {
                            this.loopCache.Clear();
                            this.loopCachePointer = 0;
                        }
                    }
                    else
                    {
                        int i = this.loopStack.Peek();

                        do
                        {
                            this.Execute(this.loopCache[++i]);
                        } while (loopCache[i] != Instruction.EndLoop);
                    }

                    break;

                case Instruction.BeginLoop:
                    loopStack.Push(this.loopCachePointer);
                    break;
            }

            if (this.loopStack.Count > 0)
            {
                this.loopCache.Add(instr);
                ++this.loopCachePointer;
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
