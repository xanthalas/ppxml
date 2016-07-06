/*****************************************************************************
/* Copyright (c) 2016 xanthalas.co.uk                                       */
/*                                                                          */
/* Author: Xanthalas                                                        */
/* Date  : July 2016                                                        */
/*                                                                          */
/*  This file is part of ppxml.                                             */
/*                                                                          */
/*  ppxml is free software: you can redistribute it and/or modify           */
/*  it under the terms of the GNU General Public License as published by    */
/*  the Free Software Foundation, either version 3 of the License, or       */
/*  (at your option) any later version.                                     */
/*                                                                          */
/*  ppxml is distributed in the hope that it will be useful,                */
/*  but WITHOUT ANY WARRANTY; without even the implied warranty of          */
/*  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the           */
/*  GNU General Public License for more details.                            */
/*                                                                          */
/*  You should have received a copy of the GNU General Public License       */
/*  along with ppxml.  If not, see <http://www.gnu.org/licenses/>.          */
/*                                                                          */
/****************************************************************************/
using System;
using System.IO;
using System.Xml;

public class PpXml
{
    private static string inputFile;
    private static string outputFile;

    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("No input file specified");
            return;
        }

        if (args.Length == 1)
        {
            if (args[0] == "-h" || args[0] == "--help" || args[0] == "/?")
            {
                displayHelp();
                return;
            }
            else
            {
                inputFile = args[0];
                outputFile = string.Empty;
            }
        }

        if (args.Length == 2)
        {
            inputFile = args[0];
            outputFile = args[1];
        }

        if (!File.Exists(inputFile))
        {
            Console.WriteLine("Input file doesn't exist: " + inputFile);
        }

        XmlDocument doc = new XmlDocument();
        doc.PreserveWhitespace = false;

        try
        {
            doc.Load(inputFile);
        }
        catch (Exception e)
        {
            Console.WriteLine("Input file is not a valid XML file. Error returned was: ");
            Console.WriteLine(e.Message);
            return;
        }

        if (outputFile.Length > 0)
        {
            try
            {
                doc.Save(outputFile);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot save output file. Error returned was: ");
                Console.WriteLine(e.Message);
                return;
            }
        }
        else
        {
            MemoryStream ms = new MemoryStream();
            doc.Save(ms);
            ms.Position = 0;
            
            using (StreamReader sr = new StreamReader(ms))
            {
                string line = sr.ReadToEnd();
                Console.WriteLine(line);
            }
        }

    }

    private static void displayHelp()
    {
        Console.WriteLine("Pretty Print XML utility. (c) Xanthalas, 2016");
        Console.WriteLine("");
        Console.WriteLine("Usage: ppxml.exe InputFile <OutputFile>");
        Console.WriteLine("");
        Console.WriteLine("OutputFile is optional. If it is omitted then output is written to the terminal.");
    }
}
