using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame
{
    //定义游戏开局的行数和每行牌数
    public class GameSetting
    {
        public int LineCount { get; set; }
        public List<int> InitSituation { get; } = new List<int>();
        public GameSetting(params int[] pokerCount)
        {
            LineCount = pokerCount.Length;
            foreach (int lc in pokerCount)
                InitSituation.Add(lc);
        }
    }
}
