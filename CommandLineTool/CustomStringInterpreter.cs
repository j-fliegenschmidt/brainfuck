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
    using System.Linq;
    using Brainfuck.Interpreter.Core;

    /// <summary>
    /// </summary>
    internal class CustomStringInterpreter : StringInterpreter
    {
        private readonly Char[] ALLOWED_CHARS = { '+', '-', '>', '<', '.', ',', '[', ']' };

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

        new public void Execute(String instr)
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
                    if (ALLOWED_CHARS.Contains(instrArr[i]))
                    {
                        sanitizedInstr.Append(instrArr[i]);
                        break;
                    }
                }

                base.Execute(sanitizedInstr.ToString());
            }
        }
    }
}
