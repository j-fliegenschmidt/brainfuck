//-----------------------------------------------------------------------
// <copyright file="StringInterpreter.cs">
//     Copyright (c) Janis Fliegenschmidt
// </copyright>
//-----------------------------------------------------------------------

namespace Brainfuck.Interpreter.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Brainfuck.Interpreter.Core.Exceptions;

    /// <summary>
    /// A Brainfuck interpreter that takes and emits Strings.
    /// </summary>
    public class StringInterpreter
    {
        private CharacterInterpreter interpreter;

        public event GetInputHandler<String> InputRequested;
        public event OutputHandler<String> OutputAvailable;

        public StringInterpreter(IInterpreter interpreter)
        {
            this.interpreter = new CharacterInterpreter(interpreter);

            this.interpreter.InputRequested += interpreter_InputRequested;
            this.interpreter.OutputAvailable += interpreter_OutputAvailable;
        }

        public void Execute(String instr)
        {
            foreach (Char c in instr)
            {
                this.interpreter.Execute(c);
            }
        }

        private void interpreter_OutputAvailable(Char output)
        {
            this.OnOutputAvailable(output);
        }

        private Char interpreter_InputRequested()
        {
            return this.OnInputRequested();
        }

        private void OnOutputAvailable(Char output)
        {
            if (this.OutputAvailable != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(output);

                this.OutputAvailable(sb.ToString());
            }
        }

        private Char OnInputRequested()
        {
            if (this.InputRequested != null)
            {
                String input = this.InputRequested();

                if (input.Length != 1)
                {
                    throw new ArgumentException(
                        "Input String may not consist of more than one character.");
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
