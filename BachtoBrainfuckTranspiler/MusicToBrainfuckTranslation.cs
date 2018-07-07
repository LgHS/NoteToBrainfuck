using System.Collections.Generic;
using System.Text;

namespace BachtoBrainfuckTranspiler
{
	public class MusicToBrainfuckTranslation
	{
		int currentBracket = 0;
		public string Parse(List<Note> notes)
		{
			StringBuilder builder = new StringBuilder();
			foreach(var note in notes)
			{
				if(note.Pitch == Notes.Si)
				{
					builder.Append(GetSymbols(note.Pitch));
					this.currentBracket++;
				}
				else
				{
					for (int i = 0; i < note.Ocatve; i++)
					{
						builder.Append(GetSymbols(note.Pitch));
						builder.Append(GetSymbols(note.Alteration));
					}
				}
			}
			if (currentBracket % 2 != 0)
				builder.Append("]");
			return builder.ToString();
		}

		private string GetSymbols(Notes note)
		{
			switch(note)
			{
				case Notes.Do: return "-";
				case Notes.Re: return "+";
				case Notes.Mi: return ">";
				case Notes.Fa: return "<";
				case Notes.Sol: return ",";
				case Notes.La: return ".";
				case Notes.Si: return this.currentBracket % 2 == 0 ? "[" : "]";
				default: return "";
			}
		}

		private string GetSymbols(Alteration alteration)
		{
			switch(alteration)
			{
				case Alteration.Flat: return "-";
				case Alteration.Sharp: return "+";
				default: return "";
			}
		}
	}
}
