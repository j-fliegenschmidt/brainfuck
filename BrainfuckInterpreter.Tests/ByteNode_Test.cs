//-----------------------------------------------------------------------
// <copyright file="ByteNode_Test.cs">
//     Copyright (c) Janis Fliegenschmidt
// </copyright>
//-----------------------------------------------------------------------

namespace Tests.BrainfuckInterpreter
{
    using System;
    using System.Collections.Generic;
    using Brainfuck.Interpreter.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// </summary>
    [TestClass]
    public class ByteNode_Test
    {
        [TestMethod]
        public void SmokeTest()
        {
            ByteNode bn = new ByteNode();

            Assert.AreEqual(0, bn.Value);

            bn.IncrementValue();

            Assert.AreEqual(1, bn.Value);

            bn.Next = new ByteNode();

            Assert.IsTrue(bn.HasNext);

            Assert.AreEqual(0, bn.Next.Value);

            bn.Next.IncrementValue();

            Assert.AreEqual(1, bn.Next.Value);            
        }
    }
}
