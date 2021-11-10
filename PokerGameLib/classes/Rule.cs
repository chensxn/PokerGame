using System.Collections.Generic;
using System.Linq;

namespace PokerGame
{
    //题目要求实现的胜负规则, 取最后一张牌的输(可根据需要定义另外的规则注入到Game的初始化, 实现多种玩法)
    public class RuleLastLost : IRule
    {
        public WinResult CheckWinner(List<int> situation)
        {
            int m = situation.Max();
            if (m == 0)     //牌都取完了, 赢家是上一轮的玩家
                return WinResult.PriorPlayer;
            else    //还有牌, 未决出赢家
                return WinResult.None;
        }
    }
}
