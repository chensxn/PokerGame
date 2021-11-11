# PokerGame
根据测试题目所作。实现一个游戏，将15张牌分成3，5，7三行，两人轮流从中取牌，每次只能从其中一行中取，取到最后一张牌的人输。

（本答卷作了通用性扩展，可支持设定不限于两个参与, 设定开局局面为若干行，每行若干张牌，并尝试实现了电脑玩家对战，电脑玩家可设定不同的策略）。

文件说明：本解决方案分为两个项目，PokerGameConsole是一个控制台客户端，PokerGameLib是核心类库，将客户端程序和核心逻辑层作了适当分离，由于相对简单，没有再将接口定义细分成单独项目。

思路概述：
1. 对核心概念进行分解，分别定义了对应的接口。包括：
  IGame：游戏管理接口
  IPlayer:玩家接口
  IRule：胜负规则接口
  IUI:界面交互显示接口
  IStrategy:电脑玩家策略接口
  
  应用中的各类对象通过接口连接，一定程度上体现了依赖注入的思想，提高了程序的可扩展性和可维护性，也方便了单元测试（时间关系，没有制作测试项目）。
2. 针对IPlayer接口，实现了人类玩家类型和电脑玩家的类型，电脑的策略模拟了两种AI，一种是无脑的每次只取最多数量牌堆里的1张，另一种是有根据必赢局面经验，而进行步法搜索的逻辑，有兴趣可以实际运行下对战看看效果:)
3. 实现了类似MVC的架构,将显示层与控制层分离,如果需要扩展成图形界面就很容易实现.
