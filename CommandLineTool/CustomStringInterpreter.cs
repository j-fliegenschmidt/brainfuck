//-----------------------------------------------------------------------
// <copyright file="CustomStringInterpreter.cs">
//     Copyright (c) Janis Fliegenschmidt
// </copyright>
//-----------------------------------------------------------------------

namespace Brainfuck.CommandLineTool
{
    using Brainfuck.Interpreter.Core;
    using System;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// </summary>
    internal class CustomStringInterpreter : StringInterpreter
    {
        private static readonly Char[] ALLOWED_CHARS = { '+', '-', '>', '<', '.', ',', '[', ']' };

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
                    }
                }

                base.Execute(sanitizedInstr.ToString());
            }
        }
    }
}
