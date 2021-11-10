using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace PokerGame
{
    //人类玩家
    public class PlayerHuman : IPlayer
    {
        public string Name { get; set; }
        public IPlayer Prior { get ; set; }
        public IPlayer Next { get; set; }

        //总是返回null, 由其他途径输入
        public Move PlayMove(List<int> situation, IRule rule) => null;
    }
}
