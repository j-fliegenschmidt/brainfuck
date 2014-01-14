//-----------------------------------------------------------------------
// <copyright file="CharacterInterpreter.cs">
//     Copyright (c) Janis Fliegenschmidt
// </copyright>
//-----------------------------------------------------------------------

namespace Brainfuck.Interpreter.Core
{
    using Brainfuck.Interpreter.Core.Exceptions;
    using System;

    /// <summary>
    /// Brainfuck Interpreter that takes characters instead of enum values.
    /// </summary>
    public class CharacterInterpreter : IInterpreter<Char>
    {
        private IInterpreter<Byte> interpreter;

        public event GetInputHandler<Char> InputRequested;
        public event OutputHandler<Char> OutputAvailable;

        public CharacterInterpreter(IInterpreter<Byte> interpreter)
        {
            this.interpreter = interpreter;

            this.interpreter.InputRequested += interpreter_InputRequested;
            this.interpreter.OutputAvailable += interpreter_OutputAvailable;
        }

        public void Execute(Char instr)
        {
            switch (instr)
            {
                case '+':
                    this.Execute(Instruction.IncrementValue);
                    break;

                case '-':
                    this.Execute(Instruction.DecrementValue);
                    break;

                case '<':
                    this.Execute(Instruction.DecrementPointer);
                    break;

                case '>':
                    this.Execute(Instruction.IncrementPointer);
                    break;

                case '[':
                    this.Execute(Instruction.BeginLoop);
                    break;

                case ']':
                    this.Execute(Instruction.EndLoop);
                    break;

                case ',':
                    this.Execute(Instruction.ReadByte);
                    break;

                case '.':
                    this.Execute(Instruction.PrintByte);
                    break;

                default:
                    throw new ArgumentException(
                        "Not a valid brainfuck command.");
            }
        }

        public void Execute(Instruction instr)
        {
            this.interpreter.Execute(instr);
        }

        private Byte interpreter_InputRequested()
        {
            return this.OnInputRequested();
        }

        private void interpreter_OutputAvailable(Byte output)
        {
            this.OnOutputAvailable(output);
        }

        private Byte OnInputRequested()
        {
            if (this.InputRequested != null)
            {
                Char input = this.InputRequested();

                return (Byte)input;
            }
            else
            {
                throw new NoInputSourceAvailableException();
            }
        }

        private void OnOutputAvailable(Byte output)
        {
            if (this.OutputAvailable != null)
            {
                this.OutputAvailable((Char)output);
            }
        }
    }
}
