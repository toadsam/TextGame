using System.Numerics;
using System.Reflection;
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
        private static Inventory meat;
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
             player = new Character("재훈재훈", "전사", 1, 10, 5, 100, 1500);
            ironarmor = new Inventory("무쇠갑옷", "방어력 + ", 5, "무쇠로 만들어져 튼튼한 갑옷입니다.",false);
            trainingarmor = new Inventory("수련자 갑옷", "방어력 + ", 5, "수련에 도움을 주는 갑옷입니다.", false,1000,false);
            spartanarmor = new Inventory("무쇠갑옷", "방어력 + ", 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", false,3500,false);
            oldsword = new Inventory("낡은 검", "공격력 + ", 2, "쉽게 볼 수 있는 낡은 검 입니다.", false);
            bronzeaxe = new Inventory("청동 도끼", "공격력 + ", 5, "어디선가 사용됐던거 같은 도끼입니다.", false,1500,false);
            spartaspear = new Inventory("스파르타의 창", "공격력 + ", 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", false,2000,false);
            meat = new Inventory("고기꼬기고기꼬기", "체력 + ", 2000, "고기다. 두고두고 먹을 수 있다. 엄청나다", false, 2, false);
            Item.Add(ironarmor);
            Item.Add(oldsword);
            StoreItem.Add(trainingarmor);
            StoreItem.Add(spartanarmor);
            StoreItem.Add(bronzeaxe);
            StoreItem.Add(spartaspear);
            StoreItem.Add(meat);
            // 아이템 정보 세팅
        }
        
        static void DisplayGameIntro()       //게임초기화면 표시
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

        static void DisplayStore()    //상점화면 표시
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
                Console.WriteLine($"-{StoreItem[i].ItemName}    |  {StoreItem[i].WhatAbility}{StoreItem[i].AbilityNunber} | {StoreItem[i].Explanation}   |  {StoreItem[i].Gold}G");
            }
            Console.WriteLine("");
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
                    break;

                case 1:
                    DIsplayStoreSsll();
                    break;
            }

        }

        static void DIsplayStoreSsll()  //상점구매화면 표시
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G");
            Console.WriteLine("");

            Console.WriteLine("[아이템 목록]");
            StoreItemListPrint();
            Console.WriteLine("");
          
            Console.WriteLine("0. 나가기");
            while (true)
            {
                if(SelectSell() == false)
                {
                    break;
                }
                Console.Clear();
                Console.WriteLine("상점 - 아이템 구매");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine("");

                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.Gold} G");
                Console.WriteLine("");

                Console.WriteLine("[아이템 목록]");
                StoreItemListPrint();
                Console.WriteLine("");

                Console.WriteLine("0. 나가기");
            }
            DisplayMyInfo();
        }
        static void StoreItemListPrint()     //상점구매리스트출력 메서드
        {
            for (int i = 0; i < StoreItem.Count; i++)   //상점리스트만큼 증가
            {
                if (StoreItem[i].IsSell == true)  // 상점이 플레이어에게 물건을 팔았다면
                {
                    Console.WriteLine($"-{i + 1} {StoreItem[i].ItemName}    |  {StoreItem[i].WhatAbility}{StoreItem[i].AbilityNunber} | {StoreItem[i].Explanation}   |  구매완료");  //구매완료 출력
                }
                else
                {
                    Console.WriteLine($"-{i + 1} {StoreItem[i].ItemName}    |  {StoreItem[i].WhatAbility}{StoreItem[i].AbilityNunber} | {StoreItem[i].Explanation}   |  {StoreItem[i].Gold}G");  //아이템의 골드 출룍
                }
            }
        }

        static bool SelectSell()  //어떤걸 사고 팔지 선택하는 메서드
        {
            int i = Convert.ToInt32(Console.ReadLine());  //사고 팔 아이템번호 입력받기        
            if (i - 1 >= 0 && i - 1 < StoreItem.Count)    //입력받은 번호가 아이템리스트 인덱스안에 있다면
            {
                if (StoreItem[i - 1].IsSell == true) //이미 플레이어가 가지고 있다면
                {
                    StoreItem[i - 1].IsSell = false; //상점에 팔기
                    player.Gold += (int)(StoreItem[i - 1].Gold * 0.85);  //돈 받기
                    Item.Remove(StoreItem[i - 1]);   //플레이어가 가지고있는 인벤토리리스트에서 제거
                    Console.WriteLine($"{player.Gold}G");
                    Console.WriteLine("판매 완료!");
                    Thread.Sleep(1000);
                } 
                else  //플레이어가 물건을 살 때
                {              
                  
                    if (player.Gold >= StoreItem[i - 1].Gold)  //플레이어의 돈이 살 물건의 돈보다 같거나 많다면
                    {
                        StoreItem[i - 1].IsSell = true;   //물건사기
                        player.Gold -= StoreItem[i - 1].Gold; //플레이어의 돈에서 물건 가격빼기
                        Console.WriteLine($"{player.Gold}G");
                        Item.Add(StoreItem[i - 1]);  //플레이어 아이템리스트에 추가
                        Console.WriteLine("구매 완료!");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.WriteLine("금액이 부족합니다.");  
                        Thread.Sleep(1000);
                    }
                }
                return true;
            }
            else if (i == 0)  //0을 누르면
            {

                return false;  //false 반환 -> while문을 빠져나가기 위해서
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                return true;
            }
        }
        static void ItemListPrint()  //플레이어 아이템출력 리스트
        {
            for (int i = 0; i < Item.Count; i++)
            {
               
                if (Item[i].IsWearing == true)  //플레이어가 장착중인 아이템이라면
                {
                    Console.WriteLine($"- [E]{Item[i].ItemName}    |  {Item[i].WhatAbility}{Item[i].AbilityNunber} | {Item[i].Explanation}");
                }
                else
                {
                    Console.WriteLine($"-{Item[i].ItemName}    |  {Item[i].WhatAbility}{Item[i].AbilityNunber} | {Item[i].Explanation}");
                }
            } 
        }
        static void ItemManagementPrint()  //창착관리 출력 메서드 -> ItemListPrint()에 번호만 붙임
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
       
        static void DisplayItemManagement()  //아이템관리 화면
        {
            Console.Clear();

            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            ItemManagementPrint();

            Console.WriteLine();
            Console.WriteLine("0. 나가기");


            while (true)
            {          
               
                if(SelectItem() == false)
                {
                    break;
                }
                Console.Clear();           
                Console.WriteLine("인벤토리 - 장착 관리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                ItemManagementPrint();

                Console.WriteLine();
                Console.WriteLine("0. 나가기");
            }
            DisplayGameIntro();
        }

        static bool SelectItem()  //장착선택 메서드
        {

            
            int i = Convert.ToInt32(Console.ReadLine());  //입력받기
            Console.WriteLine("i :" + i);
            if (i-1>=0 && i-1 < Item.Count)  //입력받은 수-1이 아이템인덱스에 있다면
            {
                if (Item[i-1].IsWearing == true)  //이미 장착중이라면
                {                          
                    Item[i - 1].IsWearing = false; //장착 해제
                }
                else //장착x였다면
                {                   
                    Item[i - 1].IsWearing = true;  //장착
                }
                return true;
            }
            else if (i == 0)  //0이면
            {
                
                return false;  //false반환 -> while문을 빠져나가기 위한 것
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                return true;
            }
           
        }
        static void AddAbility(string AbilityName)  //상태창에 장착한 아이템의 능력을 추가하는 메서드
        {
            for (int i = 0; i < Item.Count; i++)
            {
                if (Item[i].IsWearing == true)  //장착한 옷이라면
                {
                    if (Item[i].WhatAbility == AbilityName)  //아이템의 능력과 입력받은 능력의 이름이 같으면
                    {
                        Console.Write($"(+{Item[i].AbilityNunber})"); //출력
                    }
                }
            }
            Console.WriteLine();
        }
        static void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.WriteLine($"Lv.{player.Level}");
            
                
                    
            Console.Write($"공격력 :{player.Atk}");
            AddAbility("공격력 + ");
            Console.Write($"방어력 : {player.Def}");
            AddAbility("방어력 + ");


            Console.Write($"체력 : {player.Hp}");
            AddAbility("체력 + ");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    DisplayGameIntro();
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