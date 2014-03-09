//-----------------------------------------------------------------------
// <copyright file="StringInterpreter.cs">
//     Copyright (c) Janis Fliegenschmidt
// </copyright>
//-----------------------------------------------------------------------

namespace Brainfuck.Interpreter.Core
{
    using Brainfuck.Interpreter.Core.Exceptions;
    using System;
    using System.Text;

    /// <summary>
    /// Acts as a proxy for the BrainfuckInterpreter. Accepts
    /// and emits strings in addition to enum values.
    /// </summary>
    public class StringInterpreter : IInterpreter<String>
    {
        private CharacterInterpreter interpreter;

        public event GetInputHandler<String> InputRequested;
        public event OutputHandler<String> OutputAvailable;

        public StringInterpreter(IInterpreter<Byte> interpreter)
        {
            if (interpreter == null)
            {
                throw new ArgumentNullException();
            }

            this.interpreter = new CharacterInterpreter(interpreter);

            this.interpreter.InputRequested += () => { return this.OnInputRequested(); };
            this.interpreter.OutputAvailable += (output) => this.OnOutputAvailable(output);
        }

        /// <summary>
        /// Executes the commands associated with the characters in the
        /// given string.
        /// </summary>
        /// <param name="instr">The string of instruction characters.</param>
        public virtual void Execute(String instr)
        {
            foreach (Char c in instr)
            {
                this.interpreter.Execute(c);
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
        protected virtual Char OnInputRequested()
        {
            if (this.InputRequested != null)
            {
                String input = this.InputRequested();

                if (input.Length != 1)
                {
                    throw new ArgumentException(
                        "Input String must not consist of more than one character.");
                }

                return input[0];
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
        protected virtual void OnOutputAvailable(Char output)
        {
            if (this.OutputAvailable != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(output);

                this.OutputAvailable(sb.ToString());
            }
        }
    }
}
