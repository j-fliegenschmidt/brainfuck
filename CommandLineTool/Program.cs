//-----------------------------------------------------------------------
// <copyright file="Program.cs">
//     Copyright (c) Janis Fliegenschmidt
// </copyright>
//-----------------------------------------------------------------------

namespace BrainfuckInterpreter.CommandLineTool
{
    using System;
    using System.Collections.Generic;
    using Brainfuck.Interpreter.Core;

    /// <summary>
    /// </summary>
    class Program
    {
        public static void Main(String[] args)
        {
            String program = args[0];

            StringInterpreter interpreter = new StringInterpreter(new InterpreterBase());

            interpreter.OutputAvailable += interpreter_OutputAvailable;

            interpreter.Execute(program);

            Console.ReadLine();
        }

        static void interpreter_OutputAvailable(String output)
        {
            Console.WriteLine(output);
        }
    }
}
