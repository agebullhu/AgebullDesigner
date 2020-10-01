using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer.WebApi
{


    public sealed class ApiMarkBuilder : ModelCoderBase
    {

        #region 主体代码
        /// <summary>
        /// 是否可写
        /// </summary>
        protected override bool CanWrite => true;

        /// <summary>
        /// 名称
        /// </summary>
        protected override string FileSaveConfigName => "File_API_Description_md";
        /// <summary>
        /// 是否客户端代码
        /// </summary>
        protected override bool IsClient => true;

        /// <summary>
        ///     生成实体代码
        /// </summary>
        protected override void CreateCustomCode(string path)
        {
            StringBuilder code = new StringBuilder();
            code.Append($@"# {Project.Caption} 接口文档
## 版本信息：
   V 1.0
   
## 作者

|姓名|Email|日期|手机|说明|
|-|-|-|-|-|
|AgebullDesigner|agebull@qq.com|{DateTime.Now:yyyy-MM-dd HH:mm:ss}||代码生成|

# API 列表

|名称|ApiName|
|-|-|");
            foreach (var item in Project.ApiItems)
            {
                code.Append($@"
|{item.Caption}|{item.RoutePath}|");
            }

            using (CodeGeneratorScope.CreateScope(Project))
            {
                foreach (var item in Project.ApiItems)
                {
                    ItemReadMe(code, item);
                }
            }

            string file = Path.Combine(path, Project.ApiName + ".md");

            WriteFile(file, code.ToString());
        }

        /// <summary>
        ///     生成扩展代码
        /// </summary>
        protected override void CreateDesignerCode(string path)
        {
            StringBuilder code = new StringBuilder();
            code.Append($@"# {Project.Caption} 接口文档
## 版本信息：
   V 1.0
   
## 开发人员：

| 名称   |    Email   |    手机    |      说明      |
|--------|:----------:|:----------:|:---------------|

##  实体操作 API 列表：");
            foreach (var entity in Project.Entities)
            {
                if (entity.ExtendConfigListBool["NoApi"])
                    continue;
                code.Append($@"
### {entity.Caption}
    *POST[entity/{entity.Name}/AddNew](#{entity.Name}) - 创建{entity.Caption}
    *POST[entity/{entity.Name}/Update](#{entity.Name}) - 更新{entity.Caption}
    *POST[entity/{entity.Name}/Delete](#Argument<int>) - 删除{entity.Caption}
    *POST[entity/{entity.Name}/Query](#PageArgument) - 分页{entity.Caption}");
            }

            foreach (var entity in Project.Entities)
            {
                if (entity.ExtendConfigListBool["NoApi"])
                    continue;
                ItemReadMe(code, new ApiItem
                {
                    RoutePath = $"entity/{entity.Name}/AddNew",
                    CallArg = entity.Name,
                    ResultArg = entity.Name,
                    Name = "AddNew",
                    Caption = $"创建{entity.Caption}"
                });
                ItemReadMe(code, new ApiItem
                {
                    RoutePath = $"entity/{entity.Name}/Update",
                    CallArg = entity.Name,
                    ResultArg = entity.Name,
                    Name = "Update",
                    Caption = $"更新{entity.Caption}"
                });
                ItemReadMe(code, new ApiItem
                {
                    RoutePath = $"entity/{entity.Name}/Delete",
                    Argument = new EntityConfig
                    {
                        Name = "Argument<int>",
                        Caption = "ID参数",
                        Properties = new ConfigCollection<FieldConfig>
                        {
                            new FieldConfig
                            {
                                Name = "Value",
                                Caption="值",
                                CsType = "int"
                            }
                        }
                    },
                    Name = "Delete",
                    Caption = $"删除{entity.Caption}"
                });
                ItemReadMe(code, new ApiItem
                {
                    RoutePath = $"entity/{entity.Name}/Delete",
                    Argument = new EntityConfig
                    {
                        Name = "PageArgument",
                        Caption = "分页参数",
                        Properties = new ConfigCollection<FieldConfig>
                        {
                            new FieldConfig
                            {
                                Name = "Page",
                                Caption="页号",
                                Description= "从0开始的页号",
                                CsType = "int"
                            },
                            new FieldConfig
                            {
                                Name = "PageSize",
                                Caption="每页行数",
                                CsType = "int"
                            },
                            new FieldConfig
                            {
                                Name = "Order",
                                Caption="排序字段",
                                CsType = "string",
                                CanEmpty = true
                            },
                            new FieldConfig
                            {
                                Name = "Desc",
                                Caption="是否反序排列",
                                CsType = "bool",
                                CanEmpty = true
                            },
                        }
                    },
                    Name = "Delete",
                    Caption = $"分页{entity.Caption}"
                });
            }

            string file = Path.Combine(path, "entity_api.md");
            WriteFile(file, code.ToString());
        }


        private void ItemReadMe(StringBuilder code, ApiItem item)
        {
            code.Append($@"

----------------

## {item.Caption}

### 说明

- {item.Description.Replace("\n", "\n- ")}

### 操作码

{item.Code}

### ApiName

{item.RoutePath}

### 请求方法

{item.Method}

### 身份验证

AccessToken

### 请求参数
");
            if (item.Argument == null)
            {
                code.Append(@"无");
            }
            else
            {
                code.Append(@"

|名称|标题|类型|长度|必填|说明|
|-|-|-|-|-|-|");
                foreach (var property in item.Argument.LastProperties.Where(p => !p.NoneApiArgument))
                {
                    code.Append($@"
|{property.ApiArgumentName}|{property.Caption}|{property.DataType}|{(property.Datalen > 0 ? property.Datalen.ToString() : "-")}|{(property.CanEmpty ? "是" : "否")}|{property.Description}|");
                }
            }

            code.Append(@"

### 返回值
");
            if (item.Result == null)
            {
                code.Append(@"
标准返回");
            }
            else
            {
                code.Append(@"

**data**

|名称|标题|类型|长度|说明|
|-|-|-|-|-|");
                foreach (var property in item.Result.LastProperties.Where(p => !p.NoneApiArgument))
                {
                    code.Append($@"
|{property.ApiArgumentName}|{property.Caption}|{property.DataType}|{(property.Datalen > 0 ? property.Datalen.ToString() : "-")}|{property.Description}|");
                }
            }

            code.Append($@"

### 示例

#### Request

```javascript
{{
    ""header "": {{
        ""organizationId"": ""000000001"",
        ""appId"": ""888888888"",
        ""token"": ""ASDFGHJKLOIUYTREWZXCVBNMKJHGF"",
        ""tradeCode"": ""{item.Code}"",
        ""requestId"": ""784EA1A695277A0177C5"",
        ""requestTime"": ""2018-06-21T09:10:11"",
        ""sign"": ""54B952784EA1A695277A0177C53A49DD"",
        ""extend"": ""action=doit&scope=public""
    }}");
            if (item.Argument != null)
            {
                code.Append($@",
    ""data "":");
                EntityJsonCode(code, item.Argument, 1);
            }
            code.Append(@"
}
```

#### Success

```javascript
{
    ""tradeCode"": ""30001"",
    ""requestId"": ""requestId"",
    ""extend"": ""action=doit&scope=public"",
    ""code"": 0,
    ""msg"": ""操作成功""");
            if (item.Result != null)
            {
                code.Append(@",
    ""data"":");
                EntityJsonCode(code, item.Result, 1);
            }

            code.Append(@"
}
```

#### Failed

```javascript
{
    ""tradeCode"": ""30001"",
    ""requestId"": ""requestId"",
    ""extend"": ""action=doit&scope=public"",
    ""code"": -1,
    ""msg"": ""错误消息""
}
```
");
        }

        private static void EntityJsonCode(StringBuilder code, EntityConfig item, int level)
        {
            int space = level * 4 + 4;
            code.Append("{");
            bool isFirst = true;
            foreach (var property in item.LastProperties.Where(p => !p.NoneApiArgument))
            {
                if (isFirst)
                    isFirst = false;
                else
                    code.Append(",");
                code.AppendLine();
                code.Append(' ', space);

                code.Append($@"""{property.ApiArgumentName}"" : ");
                if (property.IsArray)
                    code.AppendLine("[");
                var value = property.HelloCode;
                if (property.IsReference)
                {
                    var en = GlobalConfig.GetEntity(property.CustomType);
                    if (en != null)
                        EntityJsonCode(code, en, level + 1);
                    else
                        code.Append("{}");
                }
                else if (property.CsType == "string")
                {
                    code.Append(value == null ? $"\"{property.Caption}\"" : $"\"{value}\" ");
                }
                else if (property.CsType == "DateTime")
                {
                    code.Append(value == null ? DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") : $"\"{value}\"");
                }
                else
                {
                    code.Append(value ?? "0");
                }
                if (property.IsArray)
                {
                    code.AppendLine();
                    code.Append(' ', space);
                    code.Append("]");
                }
            }
            code.AppendLine();
            code.Append(' ', level * 4);
            code.Append("}");
        }

        #endregion


    }

}

