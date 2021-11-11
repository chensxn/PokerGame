using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame
{
    public class Game : IGame
    {
        //各属性含义参加接口说明
        public bool GameRunning { get; set; } = false;
        public List<int> Situation { get; set; }
        public IRule GameRule { get; set; }
        public IPlayer[] Players { get; set; }
        public List<int> CurrentSituation { get; } = new List<int>();
        public IPlayer CurrentPlayer { get; set; }
        public IUI UI { get; set; }
        GameSetting InitSetting;

        public Game(
            GameSetting setting,    //可设置不同的开局局面
            IPlayer[] players,          //参加的玩家, 可支持若干个玩家及不同玩家类型
            IRule rule,         //注入判断胜负的规则, 可定义不同的规则玩法
            IUI ui      //注入的UI层
            )
        {
            InitSetting = setting;
            GameRule = rule;
            Players = players;
            UI = ui;
            //为玩家对象搭建好双向链表
            for (int i = 0; i < players.Length; i++)
            {
                if (i < players.Length - 1)
                {
                    players[i].Next = players[i + 1];
                    players[i + 1].Prior = players[i];
                }
                else
                {
                    players[i].Next = players[0];
                    players[0].Prior = players[i];
                }
            }
        }

        //开始一局游戏
        public void Begin()
        {
            //根据设定初始化局面
            CurrentSituation.Clear();
            CurrentSituation.AddRange(InitSetting.InitSituation);
            GameRunning = true;
            CurrentPlayer = Players[0];
            StringBuilder sb = new StringBuilder();
            int total = InitSetting.InitSituation.Sum();
            sb.AppendFormat("将{0}张牌, 分成{1}行, 安排{1}个玩家，每人可以在一轮内，在任意行拿任意张牌，但不能跨行, 拿最后一张牌的人即为输家/n请输入两个数字, 代表要从第几行取多少个牌, 中间用空格分隔, 例如输入: 2 3 表示从第2行取3个",
                total, InitSetting.LineCount);
            UI.ShowGameDescription(sb.ToString());
            UI.ShowSituation(this);
        }
        //结束一局游戏
        public void Finish()
        {
            GameRunning = false;
        }
        //得到一个步法
        public void OnGetOneMove(Move move)
        {
            if (Validate(move))
            {
                UI.ShowMove(CurrentPlayer, move);
                CurrentSituation[move.GetFromLine - 1] -= move.GetPokerCount;
                var winPlayer = CheckWinner();
                if (winPlayer == null)
                {
                    CurrentPlayer = CurrentPlayer.Next;
                    UI.ShowSituation(this);
                }
                else
                {
                    UI.ShowGameResult(winPlayer);
                    Finish();
                }
            }
        }
        //校验步法的逻辑合法性
        bool Validate(Move move)
        {
            bool result = false;
            if (move.GetFromLine > CurrentSituation.Count || move.GetFromLine < 1)
            {
                UI.ShowErrorMessage($"行数不正确, 应在1到{CurrentSituation.Count}之间");
            }
            else if (move.GetPokerCount < 1)
            {
                UI.ShowErrorMessage("最少要取一张牌");
            }
            else if (move.GetPokerCount > CurrentSituation[move.GetFromLine -1])
            {
                UI.ShowErrorMessage($"第{move.GetFromLine}行已经只有{CurrentSituation[move.GetFromLine - 1]}个牌了, 不能取多于这行的剩余的牌数");
            }
            else result = true;
            return result;
        }
        //判断获胜者, 如果为null则未决出胜负
        public IPlayer CheckWinner()
        {
            //调用规则
            var winState = GameRule.CheckWinner(CurrentSituation);
            switch (winState)
            {
                case WinResult.None:
                    return null;
                case WinResult.CurrentPlayer:
                    return CurrentPlayer;
                case WinResult.PriorPlayer:
                    return CurrentPlayer.Prior;
                case WinResult.NextPlayer:
                    return CurrentPlayer.Next;
                default:
                    return null;
            }
        }
    }
}
