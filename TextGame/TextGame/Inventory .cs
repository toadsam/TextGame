using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame
{
    public class Inventory
    {
        public string ItemName { get; }
        public string WhatAbility { get; }

        public int AbilityNunber { get; }

        public string Explanation { get; }

        public bool IsWearing { get; set; }

        public int Gold { get; set; }

        public bool IsSell { get; set; }
        public Inventory(string itemname, string whatabilit, int abilitynumber, string explantion, bool iswearing)
        {
            ItemName = itemname;
            WhatAbility = whatabilit;
            AbilityNunber = abilitynumber;
            Explanation = explantion;
            IsWearing = iswearing;


        }

        public Inventory(string itemname, string whatabilit, int abilitynumber, string explantion, bool iswearing,int gold , bool issell)
        {
            ItemName = itemname;
            WhatAbility = whatabilit;
            AbilityNunber = abilitynumber;
            Explanation = explantion;
            IsWearing = iswearing;
            Gold = gold;
            IsSell = issell; 
        }
    }
}
