using System.Numerics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace TextGame
{
    internal class Program
    {
        private static Character player;
        private static Inventory ironarmor;
        private static Inventory oldsword;
        private static Inventory trainingarmor;
        private static Inventory spartanarmor;
        private static Inventory bronzeaxe;
        private static Inventory spartaspear;
        static List<Inventory> Item = new List<Inventory>(); 
        static List<Inventory> StoreItem = new List<Inventory>();
        static void Main(string[] args)
        {
            GameDataSetting();
            DisplayGameIntro();
            Console.WriteLine("Hello, World!");
        }

         static void GameDataSetting()
        {
            // 캐릭터 정보 세팅
             player = new Character("Chad", "전사", 1, 10, 5, 100, 1500);
            ironarmor = new Inventory("무쇠갑옷", "방어력 + ", 5, "무쇠로 만들어져 튼튼한 갑옷입니다.",false);
            trainingarmor = new Inventory("수련자 갑옷", "방어력 + ", 5, "수련에 도움을 주는 갑옷입니다.", false,1000);
            spartanarmor = new Inventory("무쇠갑옷", "방어력 + ", 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", false,3500);
            oldsword = new Inventory("낡은 검", "공격력 + ", 2, "쉽게 볼 수 있는 낡은 검 입니다.", false);
            bronzeaxe = new Inventory("청동 도끼", "공격력 + ", 5, "어디선가 사용됐던거 같은 도끼입니다.", false,1500);
            spartaspear = new Inventory("스파르타의 창", "공격력 + ", 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", false,2000);
            Item.Add(ironarmor);
            Item.Add(oldsword);
            StoreItem.Add(trainingarmor);
            StoreItem.Add(spartanarmor);
            StoreItem.Add(bronzeaxe);
            StoreItem.Add(spartaspear);

            // 아이템 정보 세팅
        }
        
        static void DisplayGameIntro()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 전전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 3);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;

                case 2:
                    DIsplayInventory();
                    break;
                case 3:
                    DisplayStore();
                    break;
                    
            }

        }

        static void DisplayStore()
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine("");

            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < StoreItem.Count; i++)
            {
                Console.WriteLine($"-{i + 1} {StoreItem[i].ItemName}    |  {StoreItem[i].WhatAbility}{StoreItem[i].AbilityNunber} | {StoreItem[i].Explanation}   |  {StoreItem[i].Gold}G");
            }
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;

                case 1:

                    break;
            }

        }

        static void DIsplayStoreSsll()
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine("");

            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < StoreItem.Count; i++)
            {
                Console.WriteLine($"-{i + 1} {StoreItem[i].ItemName}    |  {StoreItem[i].WhatAbility}{StoreItem[i].AbilityNunber} | {StoreItem[i].Explanation}   |  {StoreItem[i].Gold}G");
            }
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("0. 나가기");
        }
        static void ItemListPrint()
        {
            for (int i = 0; i < Item.Count; i++)
            {
               
                if (Item[i].IsWearing == true)
                {
                    Console.WriteLine($"- [E]{Item[i].ItemName}    |  {Item[i].WhatAbility}{Item[i].AbilityNunber} | {Item[i].Explanation}");
                }
                else
                {
                    Console.WriteLine($"-{Item[i].ItemName}    |  {Item[i].WhatAbility}{Item[i].AbilityNunber} | {Item[i].Explanation}");
                }
            } 
        }
        static void ItemManagementPrint(List<Inventory>item)
        {
            for (int i = 0; i < Item.Count; i++)
            {

                if (Item[i].IsWearing == true)
                {
                    Console.WriteLine($"- {i + 1} [E]{Item[i].ItemName}    |  {Item[i].WhatAbility}{Item[i].AbilityNunber} | {Item[i].Explanation}");                  
                }
                else
                {
                    Console.WriteLine($"-{i + 1} {Item[i].ItemName}    |  {Item[i].WhatAbility}{Item[i].AbilityNunber} | {Item[i].Explanation}");                
                }
            }
        }
       
        static void DisplayItemManagement()
        {
            Console.Clear();

            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            ItemManagementPrint(Item);

            Console.WriteLine();
            Console.WriteLine("0. 나가기");


            while (true)
            {
                //SelectItem();
               
                if(SelectItem() == false)
                {
                    break;
                }
                Console.Clear();           
                Console.WriteLine("인벤토리 - 장착 관리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                ItemManagementPrint(Item);

                Console.WriteLine();
                Console.WriteLine("0. 나가기");
            }
            DisplayGameIntro();
        }

        static bool SelectItem()
        {

            
            int i = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("i :" + i);
            if (i-1>=0 && i-1 < Item.Count)
            {
                if (Item[i-1].IsWearing == true)
                {                          
                    Item[i - 1].IsWearing = false;
                }
                else
                {                   
                    Item[i - 1].IsWearing = true;
                }
                return true;
            }
            else if (i == 0)
            {
                
                return false;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                return true;
            }
           
        }
        static void AddAbility(string AbilityName)
        {
            for (int i = 0; i < Item.Count; i++)
            {
                if (Item[i].IsWearing == true)
                {
                    if (Item[i].WhatAbility == AbilityName)
                    {
                        Console.Write($"(+{Item[i].AbilityNunber})");
                    }
                }
            }
            Console.WriteLine();
        }
        static void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보르 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.WriteLine($"Lv.{player.Level}");
            
                
                    
            Console.Write($"공격력 :{player.Atk}");
            AddAbility("공격력 + ");
            Console.Write($"방어력 : {player.Def}");
            AddAbility("방어력 + ");


            Console.WriteLine($"체력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;

                case 1:
                   
                    break;
            }
        }
        
        static void DIsplayInventory() 
        {
            Console.Clear();

            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            ItemListPrint();

            Console.WriteLine();
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("0. 나가기");

            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;
                case 1:
                    DisplayItemManagement();
                    break;


            }

          
        }

        static int CheckValidInput(int min, int max)  //체크하는 함수
        {
            while (true) //실행
            {
                string input = Console.ReadLine();  //키를 입력받는다.

                bool parseSuccess = int.TryParse(input, out var ret);  //input을 int숫자형 ret로 변경 맞다면 true를 가져옴
                if (parseSuccess)  //성공했으면
                {
                    if (ret >= min && ret <= max)  //ret가 최소값과 최대값 사이에 있으면
                        return ret;//  반환시킴  -> whlie문 빠져나옴
                }

                Console.WriteLine("잘못된 입력입니다."); //실행시키고 다시 while문
            }
        }
    }
    
   

    
}