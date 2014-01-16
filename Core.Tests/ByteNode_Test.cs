//-----------------------------------------------------------------------
// <copyright file="ByteNode_Test.cs">
//     Copyright (c) Janis Fliegenschmidt
// </copyright>
//-----------------------------------------------------------------------

namespace Brainfuck.Tests.Interpreter.Core
{
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

            Assert.IsFalse(bn.HasNext);

            bn.Next = new ByteNode();

            Assert.IsTrue(bn.HasNext);

            Assert.AreEqual(0, bn.Next.Value);

            bn.Next.IncrementValue();

            Assert.AreEqual(1, bn.Next.Value);            
        }
    }
}
