# Lean.Cus API 文档

## 目录

- [接口规范](#接口规范)
- [认证方式](#认证方式)
- [错误码](#错误码)
- [通用响应格式](#通用响应格式)
- [接口列表](#接口列表)

## 接口规范

### 基础规范

1. 基础路径
```
https://api.lean365.com/api/v1
```

2. 请求方法
- GET：查询资源
- POST：创建资源
- PUT：更新资源
- DELETE：删除资源

3. 请求头
```
Content-Type: application/json
Authorization: Bearer {token}
```

### 命名规范

1. 接口路径
- 使用小写字母
- 使用连字符(-)分隔单词
- 使用复数形式表示资源集合
```
/api/v1/users                # 用户列表
/api/v1/users/{id}          # 单个用户
/api/v1/users/{id}/roles    # 用户角色
```

2. 查询参数
- 使用驼峰命名
- 分页参数：pageSize, pageIndex
- 排序参数：sortField, sortOrder
```
/api/v1/users?pageSize=10&pageIndex=1&sortField=createTime&sortOrder=desc
```

## 认证方式

### JWT认证

1. 获取令牌
```http
POST /api/v1/auth/login
Content-Type: application/json

{
    "username": "admin",
    "password": "123456"
}

Response:
{
    "code": 200,
    "data": {
        "token": "eyJhbGciOiJIUzI1NiIs...",
        "expires": 7200
    }
}
```

2. 使用令牌
```http
GET /api/v1/users
Authorization: Bearer eyJhbGciOiJIUzI1NiIs...
```

### 刷新令牌
```http
POST /api/v1/auth/refresh
Authorization: Bearer {refresh_token}

Response:
{
    "code": 200,
    "data": {
        "token": "eyJhbGciOiJIUzI1NiIs...",
        "expires": 7200
    }
}
```

## 错误码

### HTTP状态码
- 200 OK：请求成功
- 201 Created：创建成功
- 204 No Content：删除成功
- 400 Bad Request：请求参数错误
- 401 Unauthorized：未授权
- 403 Forbidden：禁止访问
- 404 Not Found：资源不存在
- 500 Internal Server Error：服务器错误

### 业务状态码
```json
{
    "200": "操作成功",
    "401": "未授权",
    "403": "禁止访问",
    "404": "资源不存在",
    "500": "系统错误",
    "1001": "用户名或密码错误",
    "1002": "账号已被锁定",
    "1003": "验证码错误",
    "1004": "旧密码错误",
    "2001": "参数验证失败",
    "2002": "数据不存在",
    "2003": "数据已存在",
    "3001": "上传文件失败",
    "3002": "文件类型不支持",
    "3003": "文件大小超限"
}
```

## 通用响应格式

### 成功响应
```json
{
    "code": 200,
    "message": "操作成功",
    "data": {
        // 响应数据
    }
}
```

### 分页响应
```json
{
    "code": 200,
    "message": "操作成功",
    "data": {
        "total": 100,
        "pageSize": 10,
        "pageIndex": 1,
        "items": [
            // 数据列表
        ]
    }
}
```

### 错误响应
```json
{
    "code": 500,
    "message": "系统错误",
    "data": null
}
```

## 接口列表

### 认证接口

#### 登录
```http
POST /api/v1/auth/login

Request:
{
    "username": "admin",
    "password": "123456",
    "code": "1234",
    "uuid": "abcd"
}

Response:
{
    "code": 200,
    "message": "登录成功",
    "data": {
        "token": "eyJhbGciOiJIUzI1NiIs...",
        "expires": 7200
    }
}
```

#### 登出
```http
POST /api/v1/auth/logout

Response:
{
    "code": 200,
    "message": "登出成功"
}
```

### 用户接口

#### 获取用户列表
```http
GET /api/v1/users?pageSize=10&pageIndex=1

Response:
{
    "code": 200,
    "data": {
        "total": 100,
        "pageSize": 10,
        "pageIndex": 1,
        "items": [
            {
                "id": 1,
                "username": "admin",
                "nickname": "管理员",
                "email": "admin@lean365.com",
                "status": 1,
                "createTime": "2024-01-01 12:00:00"
            }
        ]
    }
}
```

#### 获取用户详情
```http
GET /api/v1/users/{id}

Response:
{
    "code": 200,
    "data": {
        "id": 1,
        "username": "admin",
        "nickname": "管理员",
        "email": "admin@lean365.com",
        "status": 1,
        "roles": ["admin"],
        "permissions": ["*"],
        "createTime": "2024-01-01 12:00:00"
    }
}
```

#### 创建用户
```http
POST /api/v1/users

Request:
{
    "username": "test",
    "password": "123456",
    "nickname": "测试用户",
    "email": "test@lean365.com",
    "roleIds": [1, 2]
}

Response:
{
    "code": 200,
    "message": "创建成功",
    "data": {
        "id": 2
    }
}
```

#### 更新用户
```http
PUT /api/v1/users/{id}

Request:
{
    "nickname": "测试用户2",
    "email": "test2@lean365.com",
    "roleIds": [1, 2, 3]
}

Response:
{
    "code": 200,
    "message": "更新成功"
}
```

#### 删除用户
```http
DELETE /api/v1/users/{id}

Response:
{
    "code": 200,
    "message": "删除成功"
}
```

### 角色接口

#### 获取角色列表
```http
GET /api/v1/roles?pageSize=10&pageIndex=1

Response:
{
    "code": 200,
    "data": {
        "total": 10,
        "pageSize": 10,
        "pageIndex": 1,
        "items": [
            {
                "id": 1,
                "name": "admin",
                "code": "admin",
                "sort": 1,
                "status": 1,
                "createTime": "2024-01-01 12:00:00"
            }
        ]
    }
}
```

### 菜单接口

#### 获取菜单树
```http
GET /api/v1/menus/tree

Response:
{
    "code": 200,
    "data": [
        {
            "id": 1,
            "name": "系统管理",
            "code": "system",
            "path": "/system",
            "icon": "setting",
            "sort": 1,
            "children": [
                {
                    "id": 2,
                    "name": "用户管理",
                    "code": "user",
                    "path": "/system/user",
                    "icon": "user",
                    "sort": 1
                }
            ]
        }
    ]
}
```

### 字典接口

#### 获取字典数据
```http
GET /api/v1/dict/data/{code}

Response:
{
    "code": 200,
    "data": [
        {
            "label": "正常",
            "value": "1",
            "sort": 1
        },
        {
            "label": "停用",
            "value": "0",
            "sort": 2
        }
    ]
}
```

### 文件接口

#### 上传文件
```http
POST /api/v1/files/upload
Content-Type: multipart/form-data

Response:
{
    "code": 200,
    "message": "上传成功",
    "data": {
        "url": "https://files.lean365.com/2024/01/01/file.jpg",
        "name": "file.jpg",
        "size": 1024
    }
}
```

#### 下载文件
```http
GET /api/v1/files/download/{id}

Response:
文件流
``` 