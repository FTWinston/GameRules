using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GameParser
{
    static class Colors
    {
        public static bool IsValidColor(string color)
        {
            return HexColor.IsMatch(color) || DefaultColors.Contains(color);
        }

        private static Regex HexColor { get; } = new Regex("#[a-f\\d]{6}", RegexOptions.IgnoreCase);

        public static string[] DefaultColors { get; } = new[] {
            "red",
            "blue",
            "yellow",
            "green",
            "purple",
            "orange",
            "pink",
            "brown",
            "teal",
            "cyan",
            "fuschia",
            "magenta",
            "olive",
            "maroon",
            "silver",
            "indigo",
            "coral",
            "gold",
            "sienna",
            "lime",
            "plum",
            "tan",
            "beige",
            "lavendar",
            "salmon",
            "khaki",
            "linen",
            "crimson",
            "navy",
            "black",
            "white",
            "grey",
        };
    }
}
