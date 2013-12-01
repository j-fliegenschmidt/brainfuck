//-----------------------------------------------------------------------
// <copyright file="ByteNode.cs">
//     Copyright (c) Janis Fliegenschmidt
// </copyright>
//-----------------------------------------------------------------------

namespace Brainfuck.Interpreter.Core
{
    using System;
    using System.Collections.Generic;

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

        public Boolean HasNext
        {
            get
            {
                return this.next != null;
            }
        }

        public Boolean HasPrevious
        {
            get
            {
                return this.previous != null;
            }
        }

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

        public void IncrementValue()
        {
            ++this.value;
        }

        public void DecrementValue()
        {
            --this.value;
        }
    }
}
