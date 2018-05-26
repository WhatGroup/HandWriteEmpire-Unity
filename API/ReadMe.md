汇总
===
### API接口
接口                    |   描述
:---:                   |   :---:
api/auth/login          |   登录
api/auth/register       |   注册
api/get/user_data       |   获取用户数据
api/post/user_data      |   提交本地用户数据
api/post/level_infos    |   提交关卡数据

### 资源目录
路径                    |   描述
:---:                   |   :---:
res/images/portrait/    |   存放用户的头像
res/images/rolePortrait/|   存放角色的头像
res/level_infos/        |   存放用户关卡的数据
res/level_info/         |   存放具体关卡的内容

各个接口
===

### 注册账号
请求地址: api/auth/resgister  
传参: username ==> 用户名; password ==> 密码  
请求示例: api/resgister?username=wgb&password=123445  
返回值:  
返回示例:
1. 注册成功(状态码:200)
```json
{
    "type":"success",
    "message":"register success"
}
```
2. 注册失败(状态码:400)
```json
{
    "type":"error",
    "message":"register error"
}
```

### 登录账号
请求地址: api/auth/login  
传参: username ==> 用户名; password ==> 密码  
返回值: token ==> 用户唯一标识  
返回示例: 
1. 用户名和密码正确(状态码:200)：  
token=5816a47899b6df8004786e20ff55854c
2. 用户名或密码错误(状态码:401):
```json
{
   "type":"error",
   "message":"username or password invalid!"
}
```

### 获取用户数据
请求地址:api/get/user_data  
传参: token  
请求示例:api/get/user_data?token=abcdef0123456  
返回参数说明:  
参数             | 描述                     |类型
:---:            | :---:                    | :---:
portrait         | 用户头像路径             | string 
defenseProperty  | 防御值                   | int
attackProperty   | 攻击值                   | int
cureProperty     | 治疗值                   | int
roleInfos        | 当前已获得角色信息       | json
levelInfosUrl    | 用户关卡数据保存的路径   | string 

roleInfos参数
参数         |  描述                |   类型
:---:        |  :---:               |   :---:
state        |  是否是选中状态(1/0) |   int
name         |  选取的角色名称      |   string
type         |  角色的类型          |   string
value        |  属性值              |   int
rolePortrait |  角色的头像          |   string

返回示例:
1. token错误返回示例(状态码:403):
```json
{
   "type":"error",
   "message":"token invalid!"
}  
```
2. token正确返回示例(状态码:200):
```json
{
    "portrait": "res/images/portrait/img_20180524111830.png",
    "attackProperty": 100,
    "defenseProperty": 200,
    "cureProperty": 300,
    "roleInfos": [
        {
            "rolePortrait":"res/images/rolePortrait/role_20180526221836.jpg",
            "state": 1,
            "name": "XXX",
            "type": "attack",
            "value": 100,
        },
        {
            "rolePortrait":"res/images/rolePortrait/role_20180526221850.jpg",
            "state": 1,
            "name": "YYY",
            "type": "defense",
            "value": 200
        },
        {
            "rolePortrait":"res/images/rolePortrait/role_20180526221915.jpg",
            "state": 1,
            "name": "ZZZ",
            "type": "cure",
            "value": 300
        },
        {
            "rolePortrait":"res/images/rolePortrait/role_20180526221931.jpg",
            "state": 0,
            "name": "AAA",
            "type": "cure",
            "value": 300
        }
    ],
    "levelInfosUri":"res/LevelInfos/levelInfos_20180526222610.json"
}
```
###  全部关卡数据格式
参数    | 描述                                  | 类型
:---:   | :---:                                 | :---
state   | 该关卡的状态:ok/current/lock          | string
flag    | 当前关卡获得的旗子数                  | int
dataUri | 本关卡数据保存的路径                  | string
level   | 当前的关卡数                          | int
```json
[
    {
        "state": "ok",
        "flag": 1,
        "dataUri": "res/level_info/1.json",
        "level": 1
    },
    {
        "state": "current",
        "flag": 0,
        "dataUri": "res/level_info/2.json",
        "level": 2
    },
    {
        "state": "lock",
        "flag": 0,
        "dataUri": "res/level_info/3.json",
        "level": 3
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