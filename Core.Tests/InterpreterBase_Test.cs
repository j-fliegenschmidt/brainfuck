//-----------------------------------------------------------------------
// <copyright file="Interpreter_Test.cs">
//     Copyright (c) Janis Fliegenschmidt
// </copyright>
//-----------------------------------------------------------------------

namespace Tests.BrainfuckInterpreter.Core
{
    using System;
    using System.Collections.Generic;
    using Brainfuck.Interpreter.Core;
    using Brainfuck.Interpreter.Core.Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// </summary>
    [TestClass]
    public class InterpreterBase_Test
    {
        [TestMethod]
        public void SmokeTest()
        {
            IInterpreter<Byte> interpreter = new ByteInterpreter();

            try
            {
                interpreter.Execute(Instruction.ReadByte);
                Assert.Fail("Execute ReadByte should fail if no input event handler is bound!");
            }
            catch (NoInputSourceAvailableException)
            {
                // This is expected
            }
            catch (Exception)
            {
                Assert.Fail("Execute threw an unexpected exception!");
            }

            List<Byte> output = new List<Byte>();

            interpreter.OutputAvailable += (o) =>
            {
                output.Add(o);
            };

            const int RUNS = 20;

            for (int i = 0; i < RUNS; i++)
            {
                interpreter.Execute(Instruction.IncrementValue);
                interpreter.Execute(Instruction.IncrementPointer);
            }

            for (int i = 0; i < RUNS; i++)
            {
                interpreter.Execute(Instruction.DecrementPointer);
            }

            interpreter.Execute(Instruction.BeginLoop);
            interpreter.Execute(Instruction.PrintByte);
            interpreter.Execute(Instruction.IncrementPointer);
            interpreter.Execute(Instruction.EndLoop);

            Assert.AreEqual(RUNS, output.Count);

            for (int i = 0; i < output.Count; i++)
            {
                Assert.AreEqual(1, output[i]);
            }
        }
    }
}
