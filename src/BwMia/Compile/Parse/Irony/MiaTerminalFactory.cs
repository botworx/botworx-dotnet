using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Irony.Parsing;

using System.Globalization;

namespace Botworx.Mia.Compile.Parse.Irony
{
    public static class MiaTerminalFactory
    {
        public static IdentifierTerminal CreateId(string name)
        {
            IdentifierTerminal id = new IdentifierTerminal(name, IdOptions.AllowsEscapes | IdOptions.CanStartWithEscape);
            //id.AddPrefix(":", IdOptions.IsNotKeyword | IdOptions.NameIncludesPrefix);
            //From spec:
            //Start char is "_" or letter-character, which is a Unicode character of classes Lu, Ll, Lt, Lm, Lo, or Nl 
            id.StartCharCategories.AddRange(new UnicodeCategory[] {
         UnicodeCategory.UppercaseLetter, //Ul
         UnicodeCategory.LowercaseLetter, //Ll
         UnicodeCategory.TitlecaseLetter, //Lt
         UnicodeCategory.ModifierLetter,  //Lm
         UnicodeCategory.OtherLetter,     //Lo
         UnicodeCategory.LetterNumber     //Nl
      });
            //Internal chars
            /* From spec:
            identifier-part-character: letter-character | decimal-digit-character | connecting-character |  combining-character |
                formatting-character
      */
            id.CharCategories.AddRange(id.StartCharCategories); //letter-character categories
            id.CharCategories.AddRange(new UnicodeCategory[] {
        UnicodeCategory.DecimalDigitNumber, //Nd
        UnicodeCategory.ConnectorPunctuation, //Pc
        UnicodeCategory.SpacingCombiningMark, //Mc
        UnicodeCategory.NonSpacingMark,       //Mn
        UnicodeCategory.Format                //Cf
      });
            //Chars to remove from final identifier
            id.CharsToRemoveCategories.Add(UnicodeCategory.Format);
            return id;
        }
        /*public static IdentifierTerminal CreatePropertyId(string name)
        {
            IdentifierTerminal id = CreateId(name);
            //id.AddPrefix(":", IdOptions.IsNotKeyword | IdOptions.NameIncludesPrefix);
            id.AllFirstChars = ":";
            return id;
        }*/
        public static IdentifierTerminal CreateVariableId(string name)
        {
            IdentifierTerminal id = CreateId(name);
            //id.AddPrefix("$", IdOptions.IsNotKeyword | IdOptions.NameIncludesPrefix);
            //id.AddPrefix("$", IdOptions.None);
            id.AllFirstChars = "$";
            return id;
        }
        public static IdentifierTerminal CreateNounId(string name)
        {
            IdentifierTerminal id = new IdentifierTerminal(name, IdOptions.AllowsEscapes | IdOptions.CanStartWithEscape);
            id.CaseRestriction = CaseRestriction.FirstUpper;
            //id.AddPrefix(":", IdOptions.IsNotKeyword | IdOptions.NameIncludesPrefix);
            //From spec:
            //Start char is "_" or letter-character, which is a Unicode character of classes Lu, Ll, Lt, Lm, Lo, or Nl 
            id.StartCharCategories.AddRange(new UnicodeCategory[] {
         UnicodeCategory.UppercaseLetter, //Ul
         //UnicodeCategory.LowercaseLetter, //Ll
         UnicodeCategory.TitlecaseLetter, //Lt
         UnicodeCategory.ModifierLetter,  //Lm
         UnicodeCategory.OtherLetter,     //Lo
         UnicodeCategory.LetterNumber     //Nl
      });
            //Internal chars
            /* From spec:
            identifier-part-character: letter-character | decimal-digit-character | connecting-character |  combining-character |
                formatting-character
      */
            id.CharCategories.AddRange(id.StartCharCategories); //letter-character categories
            id.CharCategories.AddRange(new UnicodeCategory[] {
        UnicodeCategory.LowercaseLetter, //Ll
        UnicodeCategory.DecimalDigitNumber, //Nd
        UnicodeCategory.ConnectorPunctuation, //Pc
        UnicodeCategory.SpacingCombiningMark, //Mc
        UnicodeCategory.NonSpacingMark,       //Mn
        UnicodeCategory.Format                //Cf
      });
            //Chars to remove from final identifier
            id.CharsToRemoveCategories.Add(UnicodeCategory.Format);
            return id;
        }
        public static IdentifierTerminal CreateVerbId(string name)
        {
            IdentifierTerminal id = new IdentifierTerminal(name, IdOptions.AllowsEscapes | IdOptions.CanStartWithEscape);
            id.CaseRestriction = CaseRestriction.FirstLower;
            //id.AddPrefix(":", IdOptions.IsNotKeyword | IdOptions.NameIncludesPrefix);
            //From spec:
            //Start char is "_" or letter-character, which is a Unicode character of classes Lu, Ll, Lt, Lm, Lo, or Nl 
            id.StartCharCategories.AddRange(new UnicodeCategory[] {
         //UnicodeCategory.UppercaseLetter, //Ul
         UnicodeCategory.LowercaseLetter, //Ll
         UnicodeCategory.TitlecaseLetter, //Lt
         UnicodeCategory.ModifierLetter,  //Lm
         UnicodeCategory.OtherLetter,     //Lo
         UnicodeCategory.LetterNumber     //Nl
      });
            //Internal chars
            /* From spec:
            identifier-part-character: letter-character | decimal-digit-character | connecting-character |  combining-character |
                formatting-character
      */
            id.CharCategories.AddRange(id.StartCharCategories); //letter-character categories
            id.CharCategories.AddRange(new UnicodeCategory[] {
        UnicodeCategory.UppercaseLetter, //Ul
        UnicodeCategory.DecimalDigitNumber, //Nd
        UnicodeCategory.ConnectorPunctuation, //Pc
        UnicodeCategory.SpacingCombiningMark, //Mc
        UnicodeCategory.NonSpacingMark,       //Mn
        UnicodeCategory.Format                //Cf
      });
            //Chars to remove from final identifier
            id.CharsToRemoveCategories.Add(UnicodeCategory.Format);
            return id;
        }

    }
}
