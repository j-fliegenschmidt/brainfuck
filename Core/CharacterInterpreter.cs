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
    /// Acts as a proxy for the BrainfuckInterpreter. Accepts
    /// and emits characters in addition to enum values.
    /// </summary>
    public class CharacterInterpreter : IInterpreter<Char>
    {
        private IInterpreter<Byte> interpreter;

        public event GetInputHandler<Char> InputRequested;
        public event OutputHandler<Char> OutputAvailable;

        public CharacterInterpreter(IInterpreter<Byte> interpreter)
        {
            this.interpreter = interpreter;

            this.interpreter.InputRequested += () => { return this.OnInputRequested(); };
            this.interpreter.OutputAvailable += (output) => this.OnOutputAvailable(output);
        }

        /// <summary>
        /// Executes the command associated with the given character.
        /// </summary>
        /// <param name="instr">The character that represents the 
        /// brainfuck command to be executed.</param>
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

        /// <summary>
        /// Executes the specified instruction.
        /// </summary>
        /// <param name="instr">The instruction.</param>
        public void Execute(Instruction instr)
        {
            this.interpreter.Execute(instr);
        }

        /// <summary>
        /// Internal handler for the OnInputRequested event.
        /// </summary>
        /// <returns>The requested input.</returns>
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

        /// <summary>
        /// Internal handler for the OutputAvailable event.
        /// </summary>
        /// <param name="output">The available output.</param>
        private void OnOutputAvailable(Byte output)
        {
            if (this.OutputAvailable != null)
            {
                this.OutputAvailable((Char)output);
            }
        }
    }
}
