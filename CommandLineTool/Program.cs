//-----------------------------------------------------------------------
// <copyright file="Program.cs">
//     Copyright (c) Janis Fliegenschmidt
// </copyright>
//-----------------------------------------------------------------------

namespace Brainfuck.CommandLineTool
{
    using Brainfuck.Interpreter.Core;
    using System;
    using System.IO;

    /// <summary>
    /// </summary>
    class Program
    {
        public static void Main(String[] args)
        {
            Boolean interactiveMode = false;
            Boolean useFile = false;
            Boolean strict = true;

            String filePath = String.Empty;

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "--help":
                    case "-h":
                        PrintUsage();
                        Console.ReadLine();
                        return;

                    case "--nonStrict":
                        strict = false;
                        break;

                    case "--interactive":
                        interactiveMode = true;
                        break;

                    case "-f":
                        useFile = true;
                        filePath = args[++i];
                        break;

                    default:
                        Console.WriteLine("Unknown argument: " + args[i]);
                        break;
                }
            }

            if (!useFile && !interactiveMode)
            {
                PrintUsage();
                Console.ReadLine();
                return;
            }

            CustomStringInterpreter interpreter = 
                new CustomStringInterpreter(new ByteInterpreter());

            interpreter.Strict = strict;

            interpreter.InputRequested += interpreter_InputRequested;
            interpreter.OutputAvailable += interpreter_OutputAvailable;

            if (useFile)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        interpreter.Execute(sr.ReadToEnd());
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            String input = String.Empty;
            while (interactiveMode)
            {
                input = Console.ReadLine();

                if (input.Equals("exit"))
                {
                    break;
                }

                interpreter.Execute(input);
            }
        }

        static void interpreter_OutputAvailable(string output)
        {
            Console.Write(output);
        }

        static string interpreter_InputRequested()
        {
            return Console.ReadLine();
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("\t--interactive\t\tEnters interactive mode.");
            Console.WriteLine("\t--nonStrict\t\tIgnores all illegal characters.");
        }
    }
}
