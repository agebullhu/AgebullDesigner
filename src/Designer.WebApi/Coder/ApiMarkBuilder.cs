using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.RobotCoder;

namespace Agebull.EntityModel.Designer.WebApi
{


    public sealed class ApiMarkBuilder : EntityCoderBase
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
        protected override void CreateBaCode(string path)
        {
            string file = Path.Combine(path, "project_api.md");
            StringBuilder code = new StringBuilder();
            code.Append($@"## Best practice
## �汾��Ϣ��
   V 1.0
   
## ������Ա��

| ����   |    Email   |    �ֻ�    |      ˵��      |
|--------|:----------:|:----------:|:---------------|");
            code.Append($@"

# {Project.Caption} ��Ŀ API �б�");
            foreach (var item in Project.ApiItems)
            {
                code.Append($@"
            *{item.Method}[{item.RoutePath}){item.Name}({item.Argument?.Name}) - {item.Caption}");
            }

            foreach (var item in Project.ApiItems)
            {
                ItemReadMe(code, item);
            }


            SaveCode(file, code.ToString());
        }

        /// <summary>
        ///     ������չ����
        /// </summary>
        protected override void CreateExCode(string path)
        {
            StringBuilder code = new StringBuilder();
            code.Append($@"## Best practice
## �汾��Ϣ��
   V 1.0
   
## ������Ա��

| ����   |    Email   |    �ֻ�    |      ˵��      |
|--------|:----------:|:----------:|:---------------|");
            code.Append($@"

# {Project.Caption} ʵ����� API �б�");
            foreach (var entity in Project.Entities)
            {
                code.Append($@"
    # {entity.Caption}
            *POST[entity/{entity.Name}/AddNew](#{entity.Name}) - ����{entity.Caption}
            *POST[entity/{entity.Name}/Update](#{entity.Name}) - ����{entity.Caption}
            *POST[entity/{entity.Name}/Delete](#Argument<int>) - ɾ��{entity.Caption}
            *POST[entity/{entity.Name}/Query](#PageArgument) - ��ҳ{entity.Caption}");
            }

            foreach (var entity in Project.Entities)
            {
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
                        Properties = new ConfigCollection<PropertyConfig>
                        {
                            new PropertyConfig
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
                        Properties = new ConfigCollection<PropertyConfig>
                        {
                            new PropertyConfig
                            {
                                Name = "Page",
                                Caption="ҳ��",
                                Description= "��0��ʼ��ҳ��",
                                CsType = "int"
                            },
                            new PropertyConfig
                            {
                                Name = "PageSize",
                                Caption="ÿҳ����",
                                CsType = "int"
                            },
                            new PropertyConfig
                            {
                                Name = "Order",
                                Caption="�����ֶ�",
                                CsType = "string",
                                CanEmpty = true
                            },
                            new PropertyConfig
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
            SaveCode(file, code.ToString());
        }


        private static void ItemReadMe(StringBuilder code, ApiItem item)
        {
            code.Append($@"
<a id=""{item.Name}""></a>
## {item.Name}

#### ����
{item.Caption}
{item.Description}
### url
**{item.RoutePath}**

### ���󷽷�
**{item.Method}**

#### �����֤
**HeaderParams**
*[Authorization: Bearer #AccessToken/*DeviceId]*

### �������");
            if (item.Argument == null)
            {
                code.Append(@"��");
            }
            else
            {
                code.Append($@"
    �������ͣ�{item.Name}({item.Argument.Caption})
    �����ṹ��
        | ����  | ���� |  ���� |  ˵�� |");
                foreach (var property in item.Argument.Properties.Where(p => p.CanUserInput))
                {
                    code.Append($@"
        {property.Name} | {property.LastCsType} | {(property.CanEmpty ? "��" : "��")} | {property.Caption}:{
                            property.Description
                        }");
                }
            }
            code.Append(@"
### ����ֵ");
            if (item.Argument == null)
            {
                code.Append(@"��(��׼����)");
            }
            else
            {
                code.Append($@"
    �������ͣ�{item.Name}({item.Argument.Caption})
    ���ݽṹ��
        | ����  | ���� |  ���� |  ˵�� |");
                foreach (var property in item.Argument.Properties.Where(p => p.CanUserInput))
                {
                    code.Append($@"
        {property.Name} | {property.LastCsType} | {(property.CanEmpty ? "��" : "��")} | {property.Caption}:{
                            property.Description
                        }");
                }
            }

            code.Append($@"
}}
```

### ����ʾ��
    *URL��http://����ַ/{item.RoutePath}");
            if (item.Argument == null)
            {
                code.Append(@"
    *Request>(�޲���)");
            }
            else
            {
                code.Append(@"
>Request
{");
                bool isFirst = true;
                foreach (var property in item.Argument.Properties.Where(p => p.CanUserInput))
                {
                    if (isFirst)
                        isFirst = false;
                    else
                        code.Append(",");
                    if (property.CsType == "string")
                    {
                        code.Append($@"
    ""{property.Name}"":""�������""");
                    }
                    else
                    {
                        code.Append($@"
    ""{property.Name}"":*��Ĳ���");
                    }
                }
                code.Append(@"
}");
            }

            code.Append(@"    
>Success
{
    ""Result"": true,
    ""Message"": ""�����ɹ�""");
            if (item.Result != null)
            {
                code.Append(@",
    ""ResultData"",{ ");
                bool isFirst = true;
                foreach (var property in item.Result.Properties.Where(p => p.CanUserInput))
                {
                    if (isFirst)
                        isFirst = false;
                    else
                        code.Append(",");
                    if (property.CsType == "string")
                    {
                        code.Append($@"
        ""{property.Name}"":""�������""");
                    }
                    else
                    {
                        code.Append($@"
        ""{property.Name}"":*�������");
                    }
                }
                code.Append(@" 
    }");
            }
            code.Append(@" 
}
```
>Failed
{
    ""Result"": false,
    ""Message"": ""������Ϣ"",
    ""ErrorCode"": �������
}
```");
        }

        #endregion


    }

}

