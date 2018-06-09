汇总
===
ps:res/userLevelInfos/userLevelInfos.json文件不要删除  
ps:api/config.php文件不要删除

### 其他  
路径                    |   描述
:---:                   |   :---:  
api/config.php          |   连接数据库  
sql/201806041940.sql    |   数据库导出文件  
res/userLevelInfos/userLevelInfos.json  |   生成用户的userLevelInfosPath的初始数据  

### API接口
接口                    |   描述
:---:                   |   :---:
api/auth/login.php          |   登录
api/auth/register.php       |   注册
api/get/user_info.php       |   获取用户数据
api/get/find_word.php       |   查字
api/post/user_info.php      |   提交用户数据
api/post/user\_level\_infos.php    |   提交关卡数据
api/post/error\_word\_infos.php      |   提交错字本的数据

### 资源目录
路径                    |   描述
:---:                   |   :---:
res/images/portrait/    |   存放用户的头像
res/images/rolePortrait/|   存放角色的头像
res/images/roleLiHui/   |   存放角色立绘图片
res/images/enemyPortrait/ |   存放敌人的头像
res/images/enemyLiHui/    |   存放敌人立绘图片
res/wordInfo/           |   存放关卡汉字的内容
res/enemyInfo/          |   存放对应关卡的敌人数据
res/userLevelInfos/     |   存放用户关卡的数据
res/userErrorWordInfos/ |   存放用户错字本的数据

### 状态码
状态码  | 描述
:--:    | :--:
200     | 请求成功
401     | 用户名或密码错误
403     | 用户授权错误，服务器拒绝访问(token错误)
409     | 冲突 服务器在完成请求时发生冲突。(用户名重复)
410     | 资源不存在

各个接口
===

### 注册账号
请求地址: api/auth/register.php  
传参: 
account ==> 账号; paw ==> 密码  
请求示例: api/auth/register.php?account=3115008370&paw=123456    
返回值:  
> 如果注册成功，则attach的内容为token  
> 如果注册失败，则attach的内容为空
    
返回示例:  
1. 注册成功(状态码:200)  
```json
{
    "type" : "success",
    "message" : "register success",
    "attach" : "5816a47899b6df8004786e20ff55854c"
}
```
2. 用户名重复(状态码:409)  
```json
{
    "type" : "error",
    "message" : "account repeat",
    "atttch" : ""
}
```

### 登录账号
请求地址: api/auth/login.php    
传参: account ==> 账号; paw ==> 密码   
请求示例: api/auth/login.php?account=3115008370&paw=123456  
返回值: 

> 如果登录成功，则attach的内容为token  
> 如果登录失败，则attach的内容为空

返回示例:  
1. 用户名和密码正确(状态码:200)：  
```json
{
   "type":"success",
   "message":"login success!",
   "attach":"5816a47899b6df8004786e20ff55854c"
}
```
2. 用户名或密码错误(状态码:401):
```json
{
   "type":"error",
   "message":"username or password invalid!",
   "attach":""
}
```

### 获取用户数据
请求地址:api/get/user_info.php  
传参: token  
请求示例:api/get/user_info.php?token=5816a47899b6df8004786e20ff55854c  

返回参数说明:  

参数                 | 描述                     | 类型
:--:                 | :--:                     | :--:
account              | 账号                     | string
userName             | 用户名                   | string
portraitPath         | 用户头像路径             | string 
defenseValue         | 防御点                   | string
attackValue          | 攻击点                   | string
cureValue            | 治疗点                   | string
roleInfos            | 角色信息                 | json
userLevelInfosPath   | 用户关卡数据保存的路径   | string 
userErrorWordInfosPath | 用户错字本数据保存的路径 | string

roleInfos参数

参数             |  描述                  | 类型
:---:            |  :---:                 | :---:
state            |  是否是选中状态(0/1/2) | string
roleName         |  选取的角色名称        | string
rolePortraitPath |  角色头像地址          | string
roleLiHuiPath    |  角色立绘地址          | stirng   
roleType         |  角色的类型            | string
roleIntro        |  角色介绍              | string 
roleSkillDesc    |  角色技能描述          | string
unlockValue      |  解锁所需消耗的点      | string
roleHp           |  角色hp                | string
roleSkillValue   |  角色发动技能造成伤害  | string

> 备注:   
> state有三种状态: 0表示未解锁，1表示解锁未选中，2表示解锁并选中

返回示例:
1. token错误返回示例(状态码:403):
```json
{
   "type":"error",
   "message":"token invalid!",
   "attach":""
}  
```
2. token正确返回示例(状态码:200):
```json
{
    "account":"3115008370",
    "userName": "wgb",
    "portraitPath": "res/images/portrait/img_20180524111830.png",
    "attackValue": 123,
    "defenseValue": 456,
    "cureValue": 789,
    "roleInfos": [
        {
            "id":1,
            "state": 2,
            "roleName": "钢笔",
            "rolePortraitPath":"res/images/rolePortrait/role_20180526221836.jpg",
            "roleLiHuiPath":"res/images/roleLiHui/role_20180526221836.jpg",
            "roleType":"attack",
            "roleIntro":"攻击型",
            "roleSkillDesc":"攻击",
            "unlockValue":50,
            "roleHp":100,
            "roleSkillValue":20
        },
        {
            "id":2,
            "state": 2,
            "roleName": "数位板",
            "rolePortraitPath":"res/images/rolePortrait/role_20180526221850.jpg",
            "roleLiHuiPath":"res/images/roleLiHui/role_20180526221850.jpg",
            "roleType":"defense",
            "roleIntro":"防御型",
            "roleSkillDesc":"防御",
            "unlockValue":100,
            "roleHp":100,
            "roleSkillValue":20
        },
        {
            "id":3,
            "state": 2,
            "roleName": "墨水",
            "rolePortraitPath":"res/images/rolePortrait/role_20180526221931.jpg",
            "roleLiHuiPath":"res/images/roleLiHui/role_20180526221931.jpg",
            "roleType":"cure",
            "roleIntro":"治愈型",
            "roleSkillDesc":"治愈",
            "unlockValue":100,
            "roleHp":100,
            "roleSkillValue":50
        },
        {
            "id":4,
            "state": 0,
            "roleName": "4号选手",
            "rolePortraitPath":"res/images/rolePortrait/role_19700101000000.jpg",
            "roleLiHuiPath":"res/images/roleLiHui/role_19700101000000.jpg",
            "roleType":"cure",
            "roleIntro":"eeeeeeeeeeeeeee",
            "roleSkillDesc":"fffffffffffff",
            "unlockValue":100,
            "roleHp":100,
            "roleSkillValue":50
        },
        {
            "id":5,
            "state": 1,
            "roleName": "5号选手",
            "rolePortraitPath":"res/images/rolePortrait/role_19700101000000.jpg",
            "roleLiHuiPath":"res/images/roleLiHui/role_19700101000000.jpg",
            "roleType":"attack",
            "roleIntro":"gggggggggggggggggg",
            "roleSkillDesc":"hhhhhhhhhhhhhhhh",
            "unlockValue":100,
            "roleHp":100,
            "roleSkillValue":50
        },
        {
            "id":6,
            "state": 1,
            "roleName": "6号选手",
            "rolePortraitPath":"res/images/rolePortrait/role_19700101000000.jpg",
            "roleLiHuiPath":"res/images/roleLiHui/role_19700101000000.jpg",
            "roleType":"attack",
            "roleIntro":"gggggggggggggggggg",
            "roleSkillDesc":"hhhhhhhhhhhhhhhh",
            "unlockValue":100,
            "roleHp":100,
            "roleSkillValue":50
        }
    ],
    "userLevelInfosPath":"res/userLevelInfos/userlevelInfos_20180526222610.json",
    "userErrorWordInfosPath":"res/userErrorWordInfos/userErrorWordInfos_20180526222610.json"
}
```
### 寻字
请求地址:api/get/find_word.php  
传参: token,word ==>  需要查询的字  
请求示例:  
查找"你"字 ==> api/get/find_word.php?token=5816a47899b6df8004786e20ff55854c&word  = 你    
返回参数说明:

参数        | 描述      | 类型
:-:         | :-:       | :-:
pinyin      | 拼音      | string
content     | 查找的字  | string
detail      | 字的解释  | string

返回示例:  
1.token错误(状态码:403)
```json
{
    "type":"error",
    "message":"token invaild!",
    "attach":""
}
```
2.查找的字不存在(状态码410)
```json
{
    "type":"fail",
    "message":"not find resource",
    "attach":""
    
}
```
3.查找成功(状态码200)
```json
{
    "pinyin": "kē",
    "content": "科",
    "detail": "社会上习惯于把科学和技术连在一起，统称为“科技”。实际二者既有密切联系，又有重要区别。科学解决理论问题，技术解决实际问题"
}
```
###  全部关卡数据格式
参数    | 描述                                  | 类型
:---:   | :---:                                 | :---
state   | 该关卡的状态:ok/current/lock          | string
flag    | 当前关卡获得的旗子数                  | string
wordInfoPath | 本关卡汉字数据保存的路径         | string
enemyInfoPath| 本关卡敌人数据保存的路径         | string
level   | 当前的关卡数                          | string

> 备注:关卡的有三种状态，current之前的关卡均为ok，之后的关卡均为lock

```json
{
    "userLevelInfos":
    [
        {
        "state": "current",
        "flag": 1,
        "wordInfoPath": "res/wordInfo/1.json",
        "enemyInfoPath": "res/enemyInfo/enemy1.json",
        "level": 1
      },
      {
        "state": "lock",
        "flag": 0,
        "wordInfoPath": "res/wordInfo/2.json",
        "enemyInfoPath": "res/enemyInfo/enemy2.json",
        "level": 2
      },
      {
        "state": "lock",
        "flag": 0,
        "wordInfoPath": "res/wordInfo/3.json",
        "enemyInfoPath": "res/enemyInfo/enemy3.json",
        "level": 3
      },
      {
        "state": "lock",
        "flag": 0,
        "wordInfoPath": "res/wordInfo/4.json",
        "enemyInfoPath": "res/enemyInfo/enemy4.json",
        "level": 4
      },
      {
        "state": "lock",
        "flag": 0,
        "wordInfoPath": "res/wordInfo/5.json",
        "enemyInfoPath": "res/enemyInfo/enemy5.json",
        "level": 5
      },
      {
        "state": "lock",
        "flag": 0,
        "wordInfoPath": "res/wordInfo/6.json",
        "enemyInfoPath": "res/enemyInfo/enemy6.json",
        "level": 6
      },
      {
        "state": "lock",
        "flag": 0,
        "wordInfoPath": "res/wordInfo/7.json",
        "enemyInfoPath": "res/enemyInfo/enemy7.json",
        "level": 7
      },
      {
        "state": "lock",
        "flag": 0,
        "wordInfoPath": "res/wordInfo/8.json",
        "enemyInfoPath": "res/enemyInfo/enemy8.json",
        "level": 8
      },
      {
        "state": "lock",
        "flag": 0,
        "wordInfoPath": "res/wordInfo/9.json",
        "enemyInfoPath": "res/enemyInfo/enemy9.json",
        "level": 9
      },
      {
        "state": "lock",
        "flag": 0,
        "wordInfoPath": "res/wordInfo/10.json",
        "enemyInfoPath": "res/enemyInfo/enemy10.json",
        "level": 10
      }
    ]
}
```

### 错字本数据的格式

参数        | 描述      | 类型
:-:         | :-:       | :-:
pinyin      | 拼音      | string
content     | 查找的字  | string
detail      | 字的解释  | string


```json
{
    "userErrorWordInfos":
    [
        {
            "pinyin": "xiàn",
            "content": "现",
            "detail": "现世,今生;眼前一刹那"
        },
        {
            "pinyin": "wèi",
            "content": "未",
            "detail": "从现在往后的时间"
        }
    ]
}

```

### 关卡的内容格式
参数    |       描述        |类型
:--:    |       :--:        |:--
pinyin  |   词语对应的拼音  |   string
content |   具体的词语      |   string
pinyin  |   词语的解释      |   string
```json
[
    {
        "pinyin": "xiàn zài",
        "content": "现 在",
        "detail": "现世,今生;眼前一刹那"
    },
    {
        "pinyin": "wèi lái",
        "content": "未 来",
        "detail": "从现在往后的时间"
    }
]
```

### 敌人数据的格式

参数              |  描述                  | 类型
:---:             |  :---:                 | :---:
enemyName         |  敌人名称              | string
enemyPortraitPath |  敌人头像地址          | string
enemyLiHuiPath    |  敌人立绘地址          | stirng   
enemyIntro        |  敌人介绍              | string 
enemySkillDesc    |  敌人技能描述          | string
enemyHp           |  敌人hp                | string
enemySkillValue   |  敌人发动技能造成伤害  | string

```json
{
    "enemyName": "diren",
    "enemyPortraitPath": "res/images/enemyPortrait/enemy_20180526221931.jpg",
    "enemyLiHuiPath": "res/images/enemyLiHui/enemy_20180526221931.jpg",
    "enemyIntro":"qwertyuiop",
    "enemySkillDesc":"asdfghjkl",
    "enemyHp":100,
    "enemySkillValue":20
}
```


### 数据库设计

#### user表（用户）  

名称              |  描述                  | 类型        
:---:             |  :---:                 | :---:      
id                |  主键                  | int(10)      
account           |  登录账号              | varchar(45)      
password          |  密码                  | varchar(100)           
token             |  用户登录令牌          | varchar(100)        

 
#### user_info表（用户信息） 

名称                          |  描述                       | 类型      
:---:                         |  :---:                      | :---:      
id                            |  主键                       | int(10)      
userName                      |  用户名                     | varchar(45)      
portraitPath                  |  用户头像路径               | text         
defenseValue                  |  防御点                     | int(10)          
attackValue                   |  攻击点                     | int(10)        
cureValue                     |  治疗点                     | int(10)            
userLevelInfosPath            |  用户关卡数据保存的路径     | int(10)        
userErrorWordInfosPath        |  用户错字本数据保存的路径   | int(10)        
userId                        |  外键 user的id              | int(10)        



#### role_info表（角色信息）    
名称                          |  描述                       | 类型        
:---:                         |  :---:                      | :---:      
id                            |  主键                       | int(10)        
roleName                      |  角色名称                   | varchar(45)      
rolePortraitPath              |  角色头像地址               | text        
roleLiHuiPath                 |  角色立绘地址               | text       
roleType                      |  角色的类型                 | varchar(45)        
roleIntro                     |  角色介绍                   | text        
roleSkillDesc                 |  角色技能描述               | text      
unlockValue                   |  解锁所需消耗的点           | int(10)        
roleHp                        |  角色hp                     | int(10)      
roleSkillValue                |  角色发动技能造成伤害       | int(10)      


#### user_role表 （用户与角色的匹配信息）

名称                          |  描述                       | 类型    
:---:                         |  :---:                      | :---:      
id                            |  主键                       | int(10)      
userId                        |  外键 user的id              | int(10)      
role1                         |  用户是否选中角色1          | int(10)        
role2                         |  用户是否选中角色2          | int(10)       
role3                         |  用户是否选中角色3          | int(10)        
role4                         |  用户是否选中角色4          | int(10)        
role5                         |  用户是否选中角色5          | int(10)      
role6                         |  用户是否选中角色6          | int(10)        

role1,role2,...对应角色id      


#### word表 （单词表）

名称                          |  描述                       | 类型      
:---:                         |  :---:                      | :---:      
id                            |  主键                       | int(10)      
pinyin                        |  拼音                       | varchar(45)      
content                       |  字                         | varchar(45)       
detail                        |  字的解释                   | text      

