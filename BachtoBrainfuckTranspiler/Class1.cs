using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace BachtoBrainfuckTranspiler
{
	public class Program
    {
		static List<Note> parsedNotes;

		static void Main(string[] args)
		{
			parsedNotes = new List<Note>();

			var regexExpression = @"((do|re|mi|fa|sol|la|si)[#b]*)";

			var result = CommandLine.Parser.Default.ParseArguments<Options>(args);

			if (String.IsNullOrEmpty(result.Value.file.ToString()))
			{
				Console.WriteLine("No input file");
				return;
			}

			if (!File.Exists(result.Value.file.ToString()))
			{
				Console.WriteLine("File does not exist");
			}

			var fileContent = System.IO.File.ReadAllText(result.Value.file.ToString());

			var notes = new List<string>() { "do", "re", "mi", "fa", "sol", "la", "si" } ;

			for(int i = 0; i < fileContent.ToCharArray().Length; i ++)
			{
				foreach (var note in notes)
				CheckMusicalInstruction(note, i, fileContent);
			}

			MusicToBrainfuckTranslation musicToBrainfuckTranslation = new MusicToBrainfuckTranslation();
			Console.WriteLine(musicToBrainfuckTranslation.Parse(parsedNotes));

			Console.ReadKey();
		}

		private static void AddNote(string note, char alteration, int ocateve)
		{
			parsedNotes.Add(new Note()
			{
				Pitch = Enum.Parse<Notes>(note, true),
				Alteration = (alteration == ' ' ? (alteration == '#' ? Alteration.Sharp : Alteration.Flat) : Alteration.None),
				Ocatve = ocateve
			});
		}

		private static int CheckMusicalInstruction(string note, int position, string content)
		{
			if (note.Length < 2) return 1;
			if (position +1 >= content.Length) return 1;
			int offset = position;
			int octave = 0;
			char alteration = ' ';
			if (content[position] == note[0] && (content[position + 1] == note[1]))
			{
				offset += 2;

				// Check Ocatave
				if (Char.IsNumber(content[offset]))
				{
					octave = Int32.Parse(content[offset].ToString());
					offset++;
				}

				if (offset < content.Length && CheckForalteration(content[offset]))
				{
					alteration = content[offset];
					offset++;
				}

				AddNote(note, alteration, octave);
			}
			else
			{
				//Console.WriteLine("Unrecognized charactère {0}", content[position]);
			}
			return offset;
		}

		private static bool CheckForalteration(char c)
		{
			return (c == 'b' || c == '#'); 
		}
	}
}
