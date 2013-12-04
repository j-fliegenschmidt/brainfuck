//-----------------------------------------------------------------------
// <copyright file="CustomStringInterpreter.cs">
//     Copyright (c) Janis Fliegenschmidt
// </copyright>
//-----------------------------------------------------------------------

namespace BrainfuckInterpreter.CommandLineTool
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Brainfuck.Interpreter.Core;

    /// <summary>
    /// </summary>
    internal class CustomStringInterpreter : StringInterpreter
    {
        private static Char[] ALLOWED_CHARS =
            { '+', '-', '>', '<', '.', ',', '[', ']' };

        private Boolean strict;

        public CustomStringInterpreter(IInterpreter<Byte> interpreter)
            : base(interpreter)
        {
            this.strict = true;
        }

        public Boolean Strict
        {
            get
            {
                return this.strict;
            }

            set
            {
                this.strict = value;
            }
        }

        public void Execute(String instr)
        {
            if (this.Strict)
            {
                base.Execute(instr);
            }
            else
            {
                StringBuilder sanitizedInstr = new StringBuilder();
                Char[] instrArr = instr.ToCharArray();

                for (int i = 0; i < instrArr.Length; i++)
                {
                    for (int j = 0; j < ALLOWED_CHARS.Length; j++)
                    {
                        if (ALLOWED_CHARS[j] == (instr[i]))
                        {
                            sanitizedInstr.Append(instrArr[i]);
                            break;
                        }
                    }
                }

                base.Execute(sanitizedInstr.ToString());
            }
        }
    }
}
