<img width='720' src='https://raw.githubusercontent.com/ke-Grandet/Unity2DAdventureGame/master/images/0.png' />

## 说明
* 是一个2D横版动作游戏，玩家有二段跳和三段攻击
* 设计了行为不同的四种敌人：跳跃的青蛙，绕行浮岛的负鼠，俯冲攻击的鹰和巡逻的骷髅；骷髅通过状态模式实现，其它三个通过动画状态机实现；巡逻点由空物体设置
* 提取敌人受击和伤害玩家的行为作为公共组件，并创建敌人父类，以此与玩家交互
* 有分数奖励和生命值奖励两种物品，调用单例管理器更新数值
* UGUI显示玩家血量、奖励分数和游戏菜单

## 未来更新
* 加入音效
* 加入技能
* 丰富地形
* 安卓适配

## 0.1更新
### 0.1.0
* √ 初始版本

## 截图
<img width='720' src='https://raw.githubusercontent.com/ke-Grandet/Unity2DAdventureGame/master/images/1.png' />
<img width='720' src='https://raw.githubusercontent.com/ke-Grandet/Unity2DAdventureGame/master/images/2.png' />
<img width='720' src='https://raw.githubusercontent.com/ke-Grandet/Unity2DAdventureGame/master/images/3.png' />
<img width='720' src='https://raw.githubusercontent.com/ke-Grandet/Unity2DAdventureGame/master/images/4.png' />
<img width='720' src='https://raw.githubusercontent.com/ke-Grandet/Unity2DAdventureGame/master/images/5.png' />
<img width='720' src='https://raw.githubusercontent.com/ke-Grandet/Unity2DAdventureGame/master/images/6.png' />