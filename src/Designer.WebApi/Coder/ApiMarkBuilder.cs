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

        #region �������
        /// <summary>
        /// �Ƿ��д
        /// </summary>
        protected override bool CanWrite => true;

        /// <summary>
        /// ����
        /// </summary>
        protected override string FileSaveConfigName => "File_API_Description_md";
        /// <summary>
        /// �Ƿ�ͻ��˴���
        /// </summary>
        protected override bool IsClient => true;

        /// <summary>
        ///     ����ʵ�����
        /// </summary>
        protected override void CreateCustomCode(string path)
        {
            StringBuilder code = new StringBuilder();
            code.Append($@"# {Project.Caption} �ӿ��ĵ�
## �汾��Ϣ��
   V 1.0
   
## ����

|����|Email|����|�ֻ�|˵��|
|-|-|-|-|-|
|AgebullDesigner|agebull@qq.com|{DateTime.Now:yyyy-MM-dd HH:mm:ss}||��������|

# API �б�

|����|ApiName|
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
        ///     ������չ����
        /// </summary>
        protected override void CreateDesignerCode(string path)
        {
            StringBuilder code = new StringBuilder();
            code.Append($@"# {Project.Caption} �ӿ��ĵ�
## �汾��Ϣ��
   V 1.0
   
## ������Ա��

| ����   |    Email   |    �ֻ�    |      ˵��      |
|--------|:----------:|:----------:|:---------------|

##  ʵ����� API �б�");
            foreach (var entity in Project.Entities)
            {
                if (entity.ExtendConfigListBool["NoApi"])
                    continue;
                code.Append($@"
### {entity.Caption}
    *POST[entity/{entity.Name}/AddNew](#{entity.Name}) - ����{entity.Caption}
    *POST[entity/{entity.Name}/Update](#{entity.Name}) - ����{entity.Caption}
    *POST[entity/{entity.Name}/Delete](#Argument<int>) - ɾ��{entity.Caption}
    *POST[entity/{entity.Name}/Query](#PageArgument) - ��ҳ{entity.Caption}");
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
                    Caption = $"����{entity.Caption}"
                });
                ItemReadMe(code, new ApiItem
                {
                    RoutePath = $"entity/{entity.Name}/Update",
                    CallArg = entity.Name,
                    ResultArg = entity.Name,
                    Name = "Update",
                    Caption = $"����{entity.Caption}"
                });
                ItemReadMe(code, new ApiItem
                {
                    RoutePath = $"entity/{entity.Name}/Delete",
                    Argument = new EntityConfig
                    {
                        Name = "Argument<int>",
                        Caption = "ID����",
                        Properties = new ConfigCollection<FieldConfig>
                        {
                            new FieldConfig
                            {
                                Name = "Value",
                                Caption="ֵ",
                                CsType = "int"
                            }
                        }
                    },
                    Name = "Delete",
                    Caption = $"ɾ��{entity.Caption}"
                });
                ItemReadMe(code, new ApiItem
                {
                    RoutePath = $"entity/{entity.Name}/Delete",
                    Argument = new EntityConfig
                    {
                        Name = "PageArgument",
                        Caption = "��ҳ����",
                        Properties = new ConfigCollection<FieldConfig>
                        {
                            new FieldConfig
                            {
                                Name = "Page",
                                Caption="ҳ��",
                                Description= "��0��ʼ��ҳ��",
                                CsType = "int"
                            },
                            new FieldConfig
                            {
                                Name = "PageSize",
                                Caption="ÿҳ����",
                                CsType = "int"
                            },
                            new FieldConfig
                            {
                                Name = "Order",
                                Caption="�����ֶ�",
                                CsType = "string",
                                CanEmpty = true
                            },
                            new FieldConfig
                            {
                                Name = "Desc",
                                Caption="�Ƿ�������",
                                CsType = "bool",
                                CanEmpty = true
                            },
                        }
                    },
                    Name = "Delete",
                    Caption = $"��ҳ{entity.Caption}"
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

### ˵��

- {item.Description.Replace("\n", "\n- ")}

### ������

{item.Code}

### ApiName

{item.RoutePath}

### ���󷽷�

{item.Method}

### �����֤

AccessToken

### �������
");
            if (item.Argument == null)
            {
                code.Append(@"��");
            }
            else
            {
                code.Append(@"

|����|����|����|����|����|˵��|
|-|-|-|-|-|-|");
                foreach (var property in item.Argument.LastProperties.Where(p => !p.NoneApiArgument))
                {
                    code.Append($@"
|{property.ApiArgumentName}|{property.Caption}|{property.DataType}|{(property.Datalen > 0 ? property.Datalen.ToString() : "-")}|{(property.CanEmpty ? "��" : "��")}|{property.Description}|");
                }
            }

            code.Append(@"

### ����ֵ
");
            if (item.Result == null)
            {
                code.Append(@"
��׼����");
            }
            else
            {
                code.Append(@"

**data**

|����|����|����|����|˵��|
|-|-|-|-|-|");
                foreach (var property in item.Result.LastProperties.Where(p => !p.NoneApiArgument))
                {
                    code.Append($@"
|{property.ApiArgumentName}|{property.Caption}|{property.DataType}|{(property.Datalen > 0 ? property.Datalen.ToString() : "-")}|{property.Description}|");
                }
            }

            code.Append($@"

### ʾ��

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
    ""msg"": ""�����ɹ�""");
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
    ""msg"": ""������Ϣ""
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

