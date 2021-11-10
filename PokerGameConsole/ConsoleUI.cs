using System;
using System.Text;

namespace PokerGame
{
    //控制台程序下实现的UI交互
    class ConsoleUI : IUI
    {
        public void ShowErrorMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowGameDescription(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowGameResult(IPlayer winer)
        {
            Console.WriteLine($"游戏结束, {winer.Name}获得了胜利!!");
        }

        public void ShowMove(IPlayer player, Move move)
        {
            Console.WriteLine($"{player.Name} 从第{move.GetFromLine}行取走了{move.GetPokerCount}张牌");
        }

        public void ShowSituation(IGame game)
        {
            Console.WriteLine("=================================");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("轮到{0}走, 当前局面为====>", game.CurrentPlayer.Name);
            for (int i = 0; i < game.CurrentSituation.Count; i++)
                sb.AppendFormat(" 第{0}行: [{1}张牌] ", i + 1, game.CurrentSituation[i]);
            Console.WriteLine(sb.ToString());
        }
    }
}
