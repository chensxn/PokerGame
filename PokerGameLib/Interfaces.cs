using System.Collections.Generic;

namespace PokerGame
{
    //该枚举表达胜负结果
    public enum WinResult
    {
        None, CurrentPlayer, NextPlayer, PriorPlayer
    }
    //该类型代表一步
    public class Move
    {
        public int GetFromLine;     //从哪一行取牌
        public int GetPokerCount;   //取多少张牌
    }
    //玩家接口, 实现了双向链表, 指向前一个玩家和后一个玩家, 为了可扩展为多人游戏
    public interface IPlayer
    {
        string Name { get; set; }
        IPlayer Prior { get; set; }
        IPlayer Next { get; set; }
        //获得玩家的步法
        Move PlayMove(List<int> situation, IRule rule);
    }
    //胜负规则接口
    public interface IRule
    {
        //判断当前局面下是否已经有胜负结果
        WinResult CheckWinner(List<int> situation);
    }
    //游戏接口
    public interface IGame
    {
        //表示游戏是否进行中
        bool GameRunning { get; set; }
        //当前游戏的规则
        IRule GameRule { get; set; }
        //当前游戏的玩家
        IPlayer[] Players { get; set; }
        //UI层对象, 负责交互
        IUI UI { get; set; }

        //当前步数的玩家
        IPlayer CurrentPlayer { get; set; }
        //当前局面
        List<int> CurrentSituation { get;  }
        //开始新的一局
        void Begin();
        //接受到一个步法
        void OnGetOneMove(Move move);
        //判断是否已产生出赢家
        IPlayer CheckWinner();
        //结束本局游戏
        void Finish();
    }

    //UI层接口
    public interface IUI
    {
        //显示游戏说明
        void ShowGameDescription(string message);
        //显示错误提示
        void ShowErrorMessage(string message);
        //显示局面
        void ShowSituation(IGame game);
        //显示步法
        void ShowMove(IPlayer player, Move move);
        //显示游戏结果
        void ShowGameResult(IPlayer winner);
    }
}
