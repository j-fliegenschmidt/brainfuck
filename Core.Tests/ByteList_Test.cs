//-----------------------------------------------------------------------
// <copyright file="ByteList_Test.cs">
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
    public class ByteList_Test
    {
        [TestMethod]
        public void SmokeTest()
        {
            ByteList bl = new ByteList();

            Assert.AreEqual(0, bl.Value);

            bl.IncrementValue();
            Assert.AreEqual(1, bl.Value);

            bl.IncrementPointer();
            Assert.AreEqual(0, bl.Value);
        }
    }
}
