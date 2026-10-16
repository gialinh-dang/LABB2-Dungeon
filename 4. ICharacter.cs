using System;
using System.Collections.Generic;
using System.Text;

namespace LABB2._Dungeon
{
    public interface ICharacter
    {
        string Name { get; }
        int HP { get; set;  }
        Dice AttackDice { get; }
        Dice DefenceDice { get; }
    }
}
