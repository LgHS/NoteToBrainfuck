using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace BachtoBrainfuckTranspiler
{
	class Options
	{
		[Option(HelpText = "Read from stdin", Required = true, SetName = "file", DefaultValue = "file")]
		public string file { get; set; }		
	}

}
