//-----------------------------------------------------------------------
// <copyright file="ByteNode.cs">
//     Copyright (c) Janis Fliegenschmidt
// </copyright>
//-----------------------------------------------------------------------

namespace Brainfuck.Interpreter.Core
{
    using System;

    internal class ByteNode
    {
        private Byte value;
        private ByteNode previous;
        private ByteNode next;

        public ByteNode()
        {
            this.value = 0;
            this.previous = null;
            this.next = null;
        }

        /// <summary>
        /// Returns true if the NextNode reference is set.
        /// </summary>
        public Boolean HasNext
        {
            get
            {
                return this.next != null;
            }
        }

        /// <summary>
        /// Returns true if the PreviousNode reference is set.
        /// </summary>
        public Boolean HasPrevious
        {
            get
            {
                return this.previous != null;
            }
        }

        /// <summary>
        /// Gets or sets the NextNode.
        /// </summary>
        public ByteNode Next
        {
            get
            {
                return this.next;
            }

            set
            {
                if (this.next == null)
                {
                    this.next = value;
                }
                else
                {
                    throw new InvalidOperationException(
                        "This ByteNode already has a next node.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the Previous node.
        /// </summary>
        public ByteNode Previous
        {
            get
            {
                return this.previous;
            }

            set
            {
                if (this.previous == null)
                {
                    this.previous = value;
                }
                else
                {
                    throw new InvalidOperationException(
                        "This ByteNode already has a previous node.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the nodes value.
        /// </summary>
        public Byte Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Increments the node's value.
        /// </summary>
        public void IncrementValue()
        {
            ++this.value;
        }

        /// <summary>
        /// Decrements the node's value.
        /// </summary>
        public void DecrementValue()
        {
            --this.value;
        }
    }
}
