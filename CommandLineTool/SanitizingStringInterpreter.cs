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
    internal class SanitizingStringInterpreter : StringInterpreter
    {
        private Boolean strict;

        public SanitizingStringInterpreter(IInterpreter<Byte> interpreter)
            : base(interpreter)
        {
            if (interpreter == null)
            {
                throw new ArgumentNullException();
            }

            this.strict = false;
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
            base.Execute(this.Strict ? instr : instr.Sanitize());
        }
    }
}
