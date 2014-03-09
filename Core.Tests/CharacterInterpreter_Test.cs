//-----------------------------------------------------------------------
// <copyright file="CharacterInterpreter_Test.cs">
//     Copyright (c) Janis Fliegenschmidt
// </copyright>
//-----------------------------------------------------------------------

namespace Brainfuck.Tests.Interpreter.Core
{
    using Brainfuck.Interpreter.Core;
    using Brainfuck.Interpreter.Core.Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Test class containing CharacterInterpreter related tests.
    /// </summary>
    [TestClass]
    public class CharacterInterpreter_Test
    {
        [TestMethod]
        public void SmokeTest()
        {
            CharacterInterpreter interpreter = new CharacterInterpreter(new ByteInterpreter());

            List<char> output = new List<char>();

            interpreter.OutputAvailable += (o) => output.Add(o);

            const int RUNS = 20;

            for (int i = 0; i < RUNS; i++)
            {
                interpreter.Execute('+');
                interpreter.Execute('>');
            }

            for (int i = 0; i < RUNS; i++)
            {
                interpreter.Execute('<');
            }

            interpreter.Execute('[');
            interpreter.Execute('.');
            interpreter.Execute('>');
            interpreter.Execute(']');

            Assert.AreEqual(RUNS, output.Count);

            for (int i = 0; i < output.Count; i++)
            {
                Assert.AreEqual((char)1, output[i]);
            }
        }
    }
}
