//-----------------------------------------------------------------------
// <copyright file="ByteList.cs">
//     Copyright (c) Janis Fliegenschmidt
// </copyright>
//-----------------------------------------------------------------------

namespace Brainfuck.Interpreter.Core
{
    using System;
    using System.Collections.Generic;

    class ByteList
    {
        private const int MAX_NODES = 30000;
        private ByteNode current;
        private Int32 nodeCounter;

        public ByteList()
        {
            this.current = new ByteNode();
            this.nodeCounter = 1;
        }

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

        public void IncrementValue()
        {
            this.current.IncrementValue();
        }

        public void DecrementValue()
        {
            this.current.DecrementValue();
        }

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
                    ++this.nodeCounter;
                }
            }
        }
    }
}
