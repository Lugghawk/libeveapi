using System;
using System.Collections.Generic;
using System.Text;

using libeveapi;

namespace ConsoleTests
{
    public class CharacterListExamples
    {
        public static void PrintCharacterList()
        {
            CharacterList characterList = EveApi.GetAccountCharacters(2278819, "fTR02LZVWRTPTpC5pB5GAzPkWcPUAdVMr3qMjY7Ewia5QzYTGrao1pS6efvuIS1s");
            foreach (CharacterList.CharacterListItem cli in characterList.CharacterListItems)
            {
                Console.WriteLine("Name: {0} Corporation: {1}", cli.Name, cli.CorporationName);
            }
        }
    }
}
