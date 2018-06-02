汇总
===
### API接口
接口                    |   描述
:---:                   |   :---:
api/auth/login          |   登录
api/auth/register       |   注册
api/get/user_info       |   获取用户数据
api/get/find_word       |   查字
api/post/user_info      |   提交用户数据
api/post/user\_level\_infos    |   提交关卡数据
api/post/error_word_infos      |   提交错字本的数据

### 资源目录
路径                    |   描述
:---:                   |   :---:
res/images/portrait/    |   存放用户的头像
res/images/rolePortrait/|   存放角色的头像
res/images/roleLiHui/   |   存放角色立绘图片
res/levelInfo/          |   存放具体关卡的内容
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
请求地址: api/auth/resgister  
传参: 
account ==> 账号; pwd ==> 密码  
请求示例: api/resgister?account=3115008370&pwd=123456    
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
请求地址: api/auth/login    
传参: account ==> 账号; pwd ==> 密码   
请求示例: api/register?account=3115008370&pwd=123456  
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
请求地址:api/get/user_info  
传参: token  
请求示例:api/get/user_info?token=5816a47899b6df8004786e20ff55854c  

返回参数说明:  

参数                 | 描述                     | 类型
:--:                 | :--:                     | :--:
account              | 账号                     | string
userName             | 用户名                   | string
portraitPath         | 用户头像路径             | string 
defenseValue         | 防御点                   | int
attackValue          | 攻击点                   | int
cureValue            | 治疗点                   | int
roleInfos            | 角色信息                 | json
userLevelInfosPath   | 用户关卡数据保存的路径   | string 
userErrorWordInfosPath | 用户错字本数据保存的路径 | string

roleInfos参数

参数             |  描述                  | 类型
:---:            |  :---:                 | :---:
state            |  是否是选中状态(0/1/2) | int
roleName         |  选取的角色名称        | string
rolePortraitPath |  角色头像地址          | string
roleLiHuiPath    |  角色立绘地址          | stirng   
roleType         |  角色的类型            | string
roleIntro        |  角色介绍              | string 
roleSkillDesc    |  角色技能描述          | string
unlockValue      |  解锁所需消耗的点      | int
roleHp           |  角色hp                | int
roleSkillValue   |  角色发动技能造成伤害  | int

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
            "state": 2,
            "roleName": "XXX",
            "rolePortraitPath":"res/images/rolePortrait/role_20180526221836.jpg",
            "roleLiHuiPath":"res/images/roleLiHui/role_20180526221836.jpg",
            "roleType":"attack",
            "roleIntro":"xxxxxxxxxxxxxxxx",
            "roleSkillDesc":"yyyyyyyyyyyyyyyyyy",
            "unlockValue":50,
            "roleHp":100,
            "roleSkillValue":20
        },
        {
            "state": 2,
            "roleName": "YYY",
            "rolePortraitPath":"res/images/rolePortrait/role_20180526221850.jpg",
            "roleLiHuiPath":"res/images/rolePortrait/role_20180526221850.jpg",
            "roleType":"defense",
            "roleIntro":"aaaaaaaa",
            "roleSkillDesc":"bbbbbbbb",
            "unlockValue":100,
            "roleHp":100,
            "roleSkillValue":20
        },
        {
            "state": 2,
            "roleName": "ZZZ",
            "rolePortraitPath":"res/images/rolePortrait/role_20180526221931.jpg",
            "roleLiHuiPath":"res/images/rolePortrait/role_20180526221931.jpg",
            "roleType":"cure",
            "roleIntro":"ccccccccccccccc",
            "roleSkillDesc":"dddddddddddddddd",
            "unlockValue":100,
            "roleHp":100,
            "roleSkillValue":50
        },
        {
            "state": 0,
            "roleName": "JJJJJ",
            "rolePortraitPath":"res/images/rolePortrait/role_20180526221915.jpg",
            "roleLiHuiPath":"res/images/rolePortrait/role_20180526221915.jpg",
            "roleType":"cure",
            "roleIntro":"eeeeeeeeeeeeeee",
            "roleSkillDesc":"fffffffffffff",
            "unlockValue":100,
            "roleHp":100,
            "roleSkillValue":50
        },
        {
            "state": 1,
            "roleName": "KKKK",
            "rolePortraitPath":"res/images/rolePortrait/role_20180526221915.jpg",
            "roleLiHuiPath":"res/images/rolePortrait/role_20180526221915.jpg",
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
请求地址:api/get/find_word 
传参: token,word ==>  需要查询的字  
请求示例:  
比如查找"你"字 ==> api/get/find_word?token=5816a47899b6df8004786e20ff55854c&word  = 你    
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
flag    | 当前关卡获得的旗子数                  | int
infoPath | 本关卡数据保存的路径                  | string
level   | 当前的关卡数                          | int

> 备注:关卡的有三种状态，current之前的关卡均为ok，之后的关卡均为lock

```json
[
    {
        "state": "ok",
        "flag": 1,
        "infoPath": "res/levelInfo/1.json",
        "level": 1
    },
    {
        "state": "current",
        "flag": 0,
        "infoPath": "res/levelInfo/2.json",
        "level": 2
    },
    {
        "state": "lock",
        "flag": 0,
        "infoPath": "res/levelInfo/3.json",
        "level": 3
    }
]
```

### 错字本数据的格式

参数        | 描述      | 类型
:-:         | :-:       | :-:
pinyin      | 拼音      | string
content     | 查找的字  | string
detail      | 字的解释  | string


```json
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
