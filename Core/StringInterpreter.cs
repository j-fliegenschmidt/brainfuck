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
    /// A Brainfuck interpreter that takes and emits Strings.
    /// </summary>
    public class StringInterpreter : IInterpreter<String>
    {
        private CharacterInterpreter interpreter;

        public event GetInputHandler<String> InputRequested;
        public event OutputHandler<String> OutputAvailable;

        public StringInterpreter(IInterpreter<Byte> interpreter)
        {
            this.interpreter = new CharacterInterpreter(interpreter);

            this.interpreter.InputRequested += interpreter_InputRequested;
            this.interpreter.OutputAvailable += interpreter_OutputAvailable;
        }

        public virtual void Execute(String instr)
        {
            foreach (Char c in instr)
            {
                this.interpreter.Execute(c);
            }
        }

        public void Execute(Instruction instr)
        {
            this.interpreter.Execute(instr);
        }

        private void interpreter_OutputAvailable(Char output)
        {
            this.OnOutputAvailable(output);
        }

        private Char interpreter_InputRequested()
        {
            return this.OnInputRequested();
        }

        protected virtual void OnOutputAvailable(Char output)
        {
            if (this.OutputAvailable != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(output);

                this.OutputAvailable(sb.ToString());
            }
        }

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
    }
}
