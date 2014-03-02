//-----------------------------------------------------------------------
// <copyright file="ByteList.cs">
//     Copyright (c) Janis Fliegenschmidt
// </copyright>
//-----------------------------------------------------------------------

namespace Brainfuck.Interpreter.Core
{
    using System;

    /// <summary>
    /// ByteList is a wrapper around a list of bytes that handles
    /// creation and navigation between nodes.
    /// </summary>
    internal class ByteList
    {
        private const int MAX_NODES = 30000;
        private ByteNode current;
        private Int32 nodeCounter;

        public ByteList()
        {
            this.current = new ByteNode();
            this.nodeCounter = 1;
        }

        /// <summary>
        /// Gets or sets the value of the the current node.
        /// </summary>
        public Byte Value
        {
            get
            {
                return this.current.Value;
            }

            set
            {
                this.current.Value = value;
            }
        }

        /// <summary>
        /// Increments the value of the current node.
        /// </summary>
        public void IncrementValue()
        {
            this.current.IncrementValue();
        }

        /// <summary>
        /// Decrements the value of the curret node.
        /// </summary>
        public void DecrementValue()
        {
            this.current.DecrementValue();
        }

        /// <summary>
        /// Increments the pointer, i.e. sets the next node as the current.
        /// </summary>
        public void IncrementPointer()
        {
            if (this.current.HasNext)
            {
                this.current = this.current.Next;
            }
            else
            {
                try
                {
                    this.AddNode();
                    this.IncrementPointer();
                }
                catch (InvalidOperationException ex)
                {
                    throw new OutOfMemoryException(
                        "Not allowed to allocate more memory!", ex);
                }
            }
        }

        /// <summary>
        /// Decrements the pointer, i.e. sets the previous node as the current.
        /// </summary>
        public void DecrementPointer()
        {
            if (this.current.HasPrevious)
            {
                this.current = this.current.Previous;
            }
            else
            {
                throw new InvalidOperationException(
                    "The pointer can not be decremented below 0.");
            }
        }

        /// <summary>
        /// Private wrapper for the logic responsible for adding
        /// a new ByteNode.
        /// </summary>
        private void AddNode()
        {
            if (this.current.HasNext)
            {
                throw new InvalidOperationException(
                    "Can only insert nodes at the end of the chain.");
            }
            else
            {
                if (this.nodeCounter >= MAX_NODES)
                {
                    throw new InvalidOperationException(
                        "The number of nodes must not exceed " + MAX_NODES + ".");
                }
                else
                {
                    this.current.Next = new ByteNode();
                    this.current.Next.Previous = this.current;
                    ++this.nodeCounter;
                }
            }
        }
    }
}
