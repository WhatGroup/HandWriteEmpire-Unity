## 手写帝国
一款手写汉字打怪类游戏，使用Unity结合Android Studio开发(此为Unity部分)
*演示视频地址:https://github.com/WhatGroup/HandWriteEmpire-Unity/blob/master/20180613_093407.mp4* 

*备注：目前后台已经没有部署，如果测试，需要自行搭建后台及修改代码内的后台地址，后台工程地址请[点这里](https://github.com/WhatGroup/HandWriteEmpire-Backstage)*
## IDE信息
Unity 2017.3.0f3(64bit)   
Android Studio 3.1
## 功能
1.	手写输入功能：利用原生Android实现手写功能并记录手写轨迹数据，在通过灵云提供的sdk分析获取的轨迹数据，从而实现手写字体识别的功能。
2.	自动提交功能：通过Android与Unity相互通信原理，在Unity实现计时器功能，如果手指脱离屏幕时间超过一定时间间隔则自动提交轨迹数据，并进行分析。
3.	场景栈：在Unity中没有返回栈的概念，即不能通过返回键返回上一个场景。该功能主要利用一个静态的全局类记录所有场景的跳转信息并监听返回按钮，实现了返回栈功能。
4.	封装网络请求：将应用所需的网络请求封装到一个静态的工具类，在获取网络数据时只需调用相应的静态方法并实现回调方法即可。
5.	封装平台调用类：由于应用主要运行在Android平台，为方便调用Android系统提供的API，实现一个专门用于调用Android原生方法的工具类。
6.	计分系统：主要利用攻击次数，错误次数，角色死亡数等数据生成最后的评分数据
7.	扣血系统：利用角色和敌人的攻击力和防御值等数据来实现主角和敌人的扣血机制
8.	敌人AI：实现计时器功能，在一定的时间间隔内发动攻击
9.	动画控制系统：利用攻击、防御、治疗以及暂停继续等按钮并结合是否被攻击等数据控制各个角色以及敌人的动画
10.	登录注销逻辑：用户登录后保存应用的登录状态，后续无需登录，切换账号可通过注销功能实现 
#### 效果图
![个人中心](https://github.com/WhatGroup/HandWriteEmpire-Unity/blob/master/EffectPicures/main.png)

![设置](https://github.com/WhatGroup/HandWriteEmpire-Unity/blob/master/EffectPicures/person_center.png)

![错字本](https://github.com/WhatGroup/HandWriteEmpire-Unity/blob/master/EffectPicures/error_book.png)

![战斗模式](https://github.com/WhatGroup/HandWriteEmpire-Unity/blob/master/EffectPicures/adventure.png)

![角色选择](https://github.com/WhatGroup/HandWriteEmpire-Unity/blob/master/EffectPicures/role_select.png)
