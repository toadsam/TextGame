using System.Numerics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace TextGame
{
    internal class Program
    {
        private static Character player;
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
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    DisplayMyInfo();
                    break;

                case 2:
                    // 작업해보기
                    break;
            }

        }

       static void DisplayMyInfo()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보르 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level}");
            Console.WriteLine($"{player.Name}({player.Job})");
            Console.WriteLine($"공격력 :{player.Atk}");
            Console.WriteLine($"방어력 : {player.Def}");
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
    
    public class Character
    {
        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
        }
    }
}