﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Globalization;

using NetLibrary;
using Utils;
using System.Threading;

namespace ConsoleTester
{
	static public class FileReadWriteTest
	{
		static public void MappedFilesTest()
		{
			//Thread t = new Thread();

			//t.ThreadState == ThreadState.


			MemoryMappedFile f = MemoryMappedFile.CreateFromFile("C:\file.txt");

			var a = f.CreateViewAccessor();


			//    a.Read<


			//MemoryMappedFileSecurity s = new MemoryMappedFileSecurity();


			//f.SetAccessControl(
		}

		static public void Run()
		{
			string readableFilename;

			do
			{
				//Net.Recreate();

				Console.Write("File to read: ");
				readableFilename = Console.ReadLine().Trim('"');

				if (!string.IsNullOrWhiteSpace(readableFilename))
				{
					//Net.Recreate();

					//int countersCyclesToEquality = 0;

					//while (true)
					//{
					long linksBefore = Net.And.ReferersByLinkerCount;
					//long charsLinksBefore = Net.Character.ReferersBySourceCount;

					int totalBytesRead = 0;

					char[] chars = FileHelpers.ReadAllChars(readableFilename);

					SmartTextParsing(chars, 0, chars.Length);

					//string xmlSnapshotFilename = Path.Combine(Path.GetDirectoryName(readableFilename), Path.GetFileNameWithoutExtension(readableFilename) + "." + countersCyclesToEquality + ".gexf");
					//XmlGenerator.ToFile(xmlSnapshotFilename, link =>
					//{
					//    return (link.ReferersByLinkerCount < (link.ReferersBySourceCount + link.ReferersByTargetCount)) && ((link.ReferersBySourceCount + link.ReferersByTargetCount + link.ReferersByLinkerCount) >= 0);
					//});

					totalBytesRead += chars.Length;

					long linksAfter = Net.And.ReferersByLinkerCount - linksBefore;
					//long charsLinksAfter = Net.Character.ReferersBySourceCount - charsLinksBefore;

					Console.Write(totalBytesRead);
					Console.Write(" ~ ");
					Console.WriteLine(linksAfter);

					//if (linksAfter == 0)
					//{
					//    break;
					//}
					//   countersCyclesToEquality++;
					//}

					//Console.WriteLine("Total cycles: {0}.", countersCyclesToEquality);

					//Console.WriteLine("Total 'and' links used: {0}", linksAfter);
					//Console.WriteLine("Total 'char' links used: {0}", charsLinksAfter);
				}

			} while (!string.IsNullOrWhiteSpace(readableFilename));

			Console.ReadLine();

			Console.WriteLine("Xml export started.");

			readableFilename = @"C:\Texts\result.txt";

			string xmlFilename = Path.Combine(Path.GetDirectoryName(readableFilename), Path.GetFileNameWithoutExtension(readableFilename) + ".gexf");

			XmlGenerator.ToFile(xmlFilename, link =>
			{
				return (link.ReferersByLinkerCount < (link.ReferersBySourceCount + link.ReferersByTargetCount)) && ((link.ReferersBySourceCount + link.ReferersByTargetCount + link.ReferersByLinkerCount) >= 5);
			});

			Console.WriteLine("Xml export finished");

			Console.ReadLine();
		}

		static public Link SmartTextParsing(char[] text, int takeFrom, int takeUntil)
		{
			// Переменная result должна быть со ссылкой от другой связи, чтобы защитить её от удаления,
			// иначе произойдёт исключение

			Link result = null;
			Link group = null;

			Link holder = Net.CreateThing();
			Link holderPair = holder & holder;


			UnicodeCategory? currentUnicodeCategory = null;
			int currentUnicodeCategoryStartIndex = takeFrom;

			for (int i = takeFrom; i < takeUntil; i++)
			{
				char c = text[i];

				var charCategory = char.GetUnicodeCategory(c);

				if (charCategory != currentUnicodeCategory)
				{
					if (currentUnicodeCategory != null)
					{
						group = CreateCharactersGroup(text, currentUnicodeCategoryStartIndex, i);

						result = result == null ? group : result & group; // LinkConverter.CombinedJoin(ref result, ref group);

						Link.Update(ref holderPair, holder, Net.And, result);
					}

					currentUnicodeCategory = charCategory;
					currentUnicodeCategoryStartIndex = i;
				}
			}

			group = CreateCharactersGroup(text, currentUnicodeCategoryStartIndex, takeUntil);
			result = result == null ? group : result & group; // LinkConverter.CombinedJoin(ref result, ref group);

			return result;
		}

		static private Link CreateCharactersGroup(char[] text, int takeFrom, int takeUntil)
		{
			if (takeFrom < (takeUntil - 1))
			{
				return Link.Create(Net.Group, Net.ThatConsistsOf, LinkConverter.FromChars(text, takeFrom, takeUntil));
			}
			else
			{
				return LinkConverter.FromChar(text[takeFrom]);
			}
		}
	}
}
