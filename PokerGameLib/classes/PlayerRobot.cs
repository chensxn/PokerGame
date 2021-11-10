using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame
{
    public class PlayerRobot : IPlayer
    {
        public string Name { get ; set ; }
        public IPlayer Prior { get; set; }
        public IPlayer Next { get; set; }
        IStrategy strategy;     //注入的策略
        public PlayerRobot(string name, IStrategy strategy)
        {
            this.Name = name;
            this.strategy = strategy;
        }

        public Move PlayMove(List<int> situation, IRule rule)
        {
            return strategy.CreateOneMove(situation, rule);
        }
    }
}
