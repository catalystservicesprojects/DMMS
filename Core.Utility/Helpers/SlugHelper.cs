using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace DMMS.Utility.Helpers
{
    public static class SlugHelper
    {
        public static string GenerateSlug(string phrase)
        {
            if (string.IsNullOrWhiteSpace(phrase))
                return "";

            string str = phrase.ToLowerInvariant();

            // Normalize unicode
            str = str.Normalize(NormalizationForm.FormD);

            var sb = new StringBuilder();

            foreach (char c in str)
            {
                var category = Char.GetUnicodeCategory(c);
                if (category != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            str = sb.ToString().Normalize(NormalizationForm.FormC);

            // remove comma in numbers (2,500 → 2500)
            str = Regex.Replace(str, @"(\d)[,](\d)", "$1$2");

            // remove apostrophes
            str = str.Replace("'", "").Replace("’", "");

            // remove invalid chars but keep unicode
            str = Regex.Replace(str, @"[^\p{L}\p{Nd}\s-]", "");

            // spaces to hyphen
            str = Regex.Replace(str, @"\s+", "-");

            // remove duplicate hyphens
            str = Regex.Replace(str, @"-+", "-");

            return str.Trim('-');
        }
    }
}