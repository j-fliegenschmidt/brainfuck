namespace Brainfuck.CommandLineTool
{
    using System;
    using System.Linq;
    using System.Text;

    internal static class ExtensionMethods
    {
        internal static Char[] SANE_CHARS = { '+', '-', '>', '<', '.', ',', '[', ']' };

        internal static String Sanitize(this String instr)
        {
            StringBuilder sanitizedInstr = new StringBuilder();
            Char[] instrArr = instr.ToCharArray();

            for (int i = 0; i < instrArr.Length; i++)
            {
                if (SANE_CHARS.Contains(instrArr[i]))
                {
                    sanitizedInstr.Append(instrArr[i]);
                }
            }

            return sanitizedInstr.ToString();
        }
    }
}
