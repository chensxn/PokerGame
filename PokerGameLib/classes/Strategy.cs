using System.Collections.Generic;

namespace PokerGame
{
    //策略接口, 用于注入到电脑玩家对象, 自动生成步法
    public interface IStrategy
    {
        //根据局面返回一步, rule参数暂时没用上
        Move CreateOneMove(List<int> situation, IRule rule);
    }

    //这个策略只会总是从最多数量的牌堆里面取一张
    public class Strategy_Allways_GetOne : IStrategy
    {
        public virtual Move CreateOneMove(List<int> situation, IRule rule)
        {
            if (situation == null || situation.Count == 0) return null;
            //找到最多的一行牌
            int mostLine = 0;
            int most = situation[0];
            for (int i = 1; i < situation.Count; i++)
                if (situation[i] > most)
                {
                    most = situation[i];
                    mostLine = i;
                }
            if (most <= 0) return null;
            return new Move { GetFromLine = mostLine + 1, GetPokerCount = 1 };
        }
    }

    //这个策略聪明一些, 它记住了一些必赢的局面, 会尽量达到这些局面
    public class Strategy_Smarter : Strategy_Allways_GetOne
    {
        //取牌后达到以下局面会赢
        List<int[]> GoodSituation = new List<int[]> {
            new int[]{0,0,1}, 
            new int[]{1,1,1},
            new int[]{0,2,2},
            new int[]{0,3,3},
            new int[]{0,4,4},
            new int[]{0,5,5},
            new int[]{1,2,3},
            new int[]{1,4,5},
            new int[]{2,4,6}
        };
        /// <summary>
        /// 比较两个局面是否相同
        /// </summary>
        /// <param name="situation">一个List, 本方法内部不影响这个List</param>
        /// <param name="compairTo">一个已排序的数组</param>
        /// <returns></returns>
        bool SituationEqual(List<int> situation, int[] compairTo)
        {
            if (situation.Count != compairTo.Length) return false;
            var target = new List<int>(situation);
            target.Sort();
            for (int i = 0; i < target.Count; i++)
                if (target[i] != compairTo[i])
                    return false;
            return true;
        }
        public override Move CreateOneMove(List<int> situation, IRule rule)
        {
            //遍历所有的步数可能性
            for (int i = 0; i < situation.Count; i++)
            {
                List<int> target = new List<int>(situation);
                //从第i行逐张取牌, 看是否能达到好局面
                while (target[i] > 0)
                {
                    target[i]--;
                    for (int loop = 0; loop < GoodSituation.Count; loop++)
                    {
                        if (SituationEqual(target, GoodSituation[loop]))
                            return new Move { GetFromLine = i + 1, GetPokerCount = situation[i] - target[i] };  //找到达到好局面的走法
                    }
                }
            }
            return base.CreateOneMove(situation, rule);     //找不到好的走法, 交给基类处理
        }
    }
}
