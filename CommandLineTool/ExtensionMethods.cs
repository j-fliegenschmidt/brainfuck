namespace Brainfuck.CommandLineTool
{
    using System;
    using System.Linq;
    using System.Text;

    internal static class ExtensionMethods
    {
        internal static String Sanitize(this String instr)
        {
            Char[] ALLOWED_CHARS = { '+', '-', '>', '<', '.', ',', '[', ']' };

            StringBuilder sanitizedInstr = new StringBuilder();
            Char[] instrArr = instr.ToCharArray();

            for (int i = 0; i < instrArr.Length; i++)
            {
                if (ALLOWED_CHARS.Contains(instrArr[i]))
                {
                    sanitizedInstr.Append(instrArr[i]);
                }
            }

            return sanitizedInstr.ToString();
        }
    }
}
