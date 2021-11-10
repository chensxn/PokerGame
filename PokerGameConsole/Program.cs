using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace PokerGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //以下游戏由两个人类玩家Alice和Bob对战
            Console.WriteLine("以下游戏由两个人类玩家Alice和Bob对战");
            IGame game = new Game(
                new GameSetting(3, 5, 7),
                new IPlayer[] { new PlayerHuman() { Name = "Alice" }, new PlayerHuman() { Name = "Bob" } },
                new RuleLastLost(), new ConsoleUI()
                );
            RunGame(game);

            //以下游戏由人类玩家Alice和一个简单策略的电脑玩家对战
            Console.WriteLine("以下游戏由Alice和一个简单策略的电脑玩家对战");
            game = new Game(
                new GameSetting(3, 5, 7),
                new IPlayer[] { new PlayerHuman() { Name = "Alice" }, new PlayerRobot("IdolRobot", new Strategy_Allways_GetOne()) },
                new RuleLastLost(), new ConsoleUI()
                );
            RunGame(game);

            //以下游戏由人类玩家Alice和一个有经验策略的电脑玩家对战
            Console.WriteLine("以下游戏由Alice和一个有经验策略的电脑玩家对战");
            game = new Game(
                new GameSetting(3, 5, 7),
                new IPlayer[] { new PlayerHuman() { Name = "Alice" }, new PlayerRobot("SmartRobot", new Strategy_Smarter()) },
                new RuleLastLost(), new ConsoleUI()
                );
            RunGame(game);
        }

        static void RunGame(IGame game)
        {
            Regex regx = new Regex(@"^\s*(\d+)\s+(\d+)");
            while (true)
            {
                game.Begin();
                while (game.GameRunning)
                {
                    //电脑玩家会运算返回一个步法, 人类玩家对象总是返回null
                    Move nextMove = game.CurrentPlayer.PlayMove(game.CurrentSituation, game.GameRule);
                    if (nextMove == null)
                    {
                        //通过控制台接受步法输入
                        var input = Console.ReadLine();
                        //使用正则表达式进行格式校验, 并匹配步法的两个参数
                        var match = regx.Match(input);
                        if (!match.Success)
                        {
                            game.UI.ShowErrorMessage("请输入两个数字, 代表要从第几行取多少个牌, 中间用空格分隔, 例如输入: 2 3 表示从第2行取3个");
                            continue;
                        }
                        int line = int.Parse(match.Groups[1].Value);
                        int lineCount = int.Parse(match.Groups[2].Value);
                        nextMove = new Move { GetFromLine = line, GetPokerCount = lineCount };
                    }
                    //把步法交给Game对象处理, 对于步法的合法性也由Game对象校验
                    game.OnGetOneMove(nextMove);
                }
                Console.WriteLine("重新开局请按Y");
                var select = Console.ReadLine();
                if (!select.Trim().Equals("Y", StringComparison.OrdinalIgnoreCase))
                    break;
            }
        }
    }
}
