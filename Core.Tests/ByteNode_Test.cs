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

        [TestMethod]
        public void ValueTest()
        {
            ByteNode bn = new ByteNode();

            Assert.AreEqual(0, bn.Value);

            bn.IncrementValue();
            Assert.AreEqual(1, bn.Value);

            for (int i = 0; i < 20; i++)
            {
                bn.IncrementValue();
            }

            Assert.AreEqual(21, bn.Value);

            bn.DecrementValue();
            Assert.AreEqual(20, bn.Value);

            for (int i = 0; i < 20; i++)
            {
                bn.DecrementValue();
            }

            Assert.AreEqual(0, bn.Value);

            for (int i = 0; i < byte.MaxValue + 1; i++)
            {
                bn.IncrementValue();
            }

            Assert.AreEqual(0, bn.Value);

            bn.DecrementValue();
            Assert.AreEqual(byte.MaxValue, bn.Value);
        }

        [TestMethod]
        public void LinkTest()
        {
            ByteNode bn = new ByteNode();

            Assert.IsFalse(bn.HasNext);
            Assert.IsNull(bn.Next);
            Assert.IsFalse(bn.HasPrevious);
            Assert.IsNull(bn.Previous);

            bn.Next = new ByteNode();
            Assert.IsTrue(bn.HasNext);
            Assert.IsNotNull(bn.Next);

            bn.Next.Previous = bn;
            bn = bn.Next;
            Assert.IsFalse(bn.HasNext);
            Assert.IsNull(bn.Next);
            Assert.IsTrue(bn.HasPrevious);
            Assert.IsNotNull(bn.Previous);
        }
    }
}
