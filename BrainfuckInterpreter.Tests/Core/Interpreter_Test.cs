//-----------------------------------------------------------------------
// <copyright file="Interpreter.cs">
//     Copyright (c) Janis Fliegenschmidt
// </copyright>
//-----------------------------------------------------------------------

namespace Tests.BrainfuckInterpreter.Core
{
    using System;
    using Brainfuck.Interpreter.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// </summary>
    [TestClass]
    class Interpreter_Test
    {
        [TestMethod]
        public static void SmokeTest()
        {
            IInterpreter interpreter = new InterpreterBase();

            try
            {
                interpreter.Execute(Instruction.ReadByte);
                Assert.Fail("Execute should fail if no input event handler is bound!");
            }
            catch (NoInputSourceAvailableException)
            {
                // This is expected
            }
            catch (Exception)
            {
                Assert.Fail("Execute threw an unexpected exception!");
            }
        }
    }
}
