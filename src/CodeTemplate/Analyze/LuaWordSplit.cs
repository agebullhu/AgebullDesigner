// /*****************************************************
// (c)2008-2017 Copy right www.Gboxt.com
// 作者:agebull
// 工程:CodeRefactor-Agebull.CodeRefactor.CodeAnalyze
// 建立:2017年3月22日
// 修改:2017年4月27日
// *****************************************************/

/*
 仿Razor语法糖
 ***@语法糖的前导出现,必须是@与规定字符,不允许出现空格
 ***配对(成对匹配),使用先进先出的方式匹配,语法块或变量中的C#注释与字符串中的内容不参与匹配
 * 例:
 @{
    if(true)
    {
        var txt="这里的}不参与匹配";//这个注释的{{也不参与匹配
    }
 }
 <input type='text'/>
 *此时@{代码块的的结束在第六行的}处,解析结果代码类似如下
    if(true)
    {
        var txt="这里的}不参与匹配";//这个注释的{{也不参与匹配
    }
    appendCode(" <input type='text'/>");
 [语法块]
 1 @-...:单行代码,@-必须在行的头部出现(之前空白被许可)才有效,此代码到行尾结束
 2 @{...}:大括号配对内部的内容的解析为一般代码,@为扩展的内部输出方法,等同于appendCode方法
 *例:
 @{
    appendCode("123");
 }
 <input type='text'/>
 *结果代码(lua)
    appendCode("123");
    appendCode(" <input type='text'/>");
    *例:
 @{
    @123
 }
 <input type='text'/>
 *结果代码(lua)
    appendCode("123");
    appendCode(" <input type='text'/>");

 [变量]
 1 属性或字段输出
 @名称.名称:
    名称是字母数字下划线组成的包括各种文字(单词以ASCII128内的非字母数字下划线的字符结束),后方可以连接.字符(允许.前后间隔空白)以访问属性或字段
    后方的()的方法调用不支持,需要方法返回值,请用@()模式
    *例:
    <input type='@name.Length'/>
    *结果代码(lua)
     appendCode(@"    <input type='");
     appendCode(name.Length);
     appendCode(@"'/>",name.Length);
    特别说明:@end:输出end关键字
 2 内容输出
   @():小括号配对中的内容直接解析为变量输出(所有一个变量输出\方法调用\强制转换均可用),如无返回值的方法调用会导致编译错误
 *例:
    <input type='@(name.ToString())'/>
    *结果代码(lua)
     appendCode(@"    <input type='");
     appendCode(name.ToString());
     appendCode(@"'/>",name.Length);
 3 @#...#@为文档内容配置格式如下:
    [name] : [value] 
    name是配置的名称,value隔一个半角冒号,如果value之前有一个引号,则到另一个引号(之前没有\转义)结束,否则到行尾结束,
    有特别说明内容时,不区分大小写
    name与value说明如下:
    3.1 名称:指生成的lua代码的function的名称
    [name]为name\名称,[value]符合lua的function要求的名称
    3.2 标题:显示在模板列表的标题
    [name]caption\标题,[value]任意
    3.3 说明:对模板的一个内容说明
    [name]desc\description\说明\备注\模板说明,[value]任意
    3.4 分类:在模板列表中的分类    
    [name]classify\类别\分类,[value]任意
    3.5 目标语言:生成的代码的编程语言
    [name]language\语言\目标语言,[value]你要生成的语言,如C\C++\JAVA\JS\PHP\C#等
    3.6 参数类型:适用于当前模板的配置对象的数据类型,如果设置,当前选择必须是指定类型
    [name]type\model\modeltype\参数\参数类型\数据模型\模型\模型类型,
    [value]当前只能是如下内容
        字段配置: 字段\field\property\propertyconfig
        实体配置: 实体\entity\entityconfig
        枚举配置: 枚举\enum\enumconfig
        类型配置: 类型\类型定义\type\typedef\typedefitem\typeconfig\typedefconfig
        命令配置: 命令\用户命令\按钮\btn\button\command\cmd
        项目配置: 项目\project\按钮\projectconfig
        解决方案: 解决方案\solution\按钮\solutionconfig
    3.7 执行条件:此模板能否执行的lua代码,为空表示无其它条件(如果参数类型有的话,会首先校验参数类型)
                 如果执行出错,将不执行代码生成
    [name]condition\执行条件,[value]正确的lua代码
    3.8 代码保存路径:如果代码生成后要保存到一个文件中,可以设置
    [name]savesath\codesavesath\代码保存路径\代码保存\保存路径,
    [value]为有效的路径,
        %root%:solution配置的路径,
        %project%:project配置的根路径
        %bl%:project配置中的业务逻辑路径
        %model%:project配置中的数据模型路径
        %ui%:project配置中的用户界面路径
 5 @*...*@ : 模板注释

 普通输出
 1两个@,输出@字符
 2 @之后不符合以上原则的,@作为普通输出,即使@+空白+{也是如此
 3 其它不符合以上语法糖的内容
 4 代码块之后的换行不输出到代码


 防错建议
 1 方法调用返回值的输出,必须使用@(...)方式,如@(tostring(var1))
 2 方法在控制块中,如果有[换行+@+}+换行]或[换行+@+}+换行]的普通输出时,请用@@{
 3 使用属性或字段输出时,如果后方连接成单词的容易造成歧义,请使用@()方式,如:
    例子A 
    有变量 name且值为123,想要输出的结果为 str123456,写法为string str@(name)456,
    如写成str@name456,将可能使用已定义的变量name456输出或运行时因为没有这个变量出错
    例子B
    有变量 name且值为id,想要输出的结果为 str_id_name,写法为string str_@(name)_name,
    如写成str_@name_name,将可能使用已定义的变量name_name输出或运行时因为没有这个变量出错
*/
#region 引用

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Agebull.Common.LUA;
using Agebull.EntityModel.Config;

#endregion

namespace Agebull.EntityModel.RobotCoder.CodeTemplate.LuaTemplate
{
    /// <summary>
    ///     基本单词分析
    /// </summary>
    /// <remarks>
    /// 算法说明:
    /// 第一步:字符识别
    ///     1 确定字符环境
    ///     1.1 默认为内容环境(0)
    ///     1.2 @单词,为值嵌入(2),仅连续的[单词][.:][单词][.:]形式可用,连接符无空格
    ///     1.3 @{},为语句块(1),}出栈后恢复内容环境(0)
    ///     1.4 @(),为值嵌入(3),)出栈后恢复内容环境(0)
    ///     1.5 @**@,为注释,从开始直到结束情况出现后,为一个单词
    ///     1.6 @##@,为模板配置信息,从开始直到结束情况出现后,为一个单词,此过程中会识别模板配置信息(独立方式)
    ///     2 如果是内容环境(0)
    ///     2.1 连续字符组合成单词
    ///     2.2 标点单独成为单词
    ///     2.3 连续空白组合成为单词
    ///     3 否则是LUA嵌入(1,2,3)
    ///     2.1 连续字符组合成单词
    ///     2.2 标点按LUA操作符组成单词(同时完成单词类型判定)
    ///     2.3 连续空白组合成为单词
    ///     2.4 引号开始到结束(忽略转义符下的引号),为一个文本类型单词.引号是对称的(即单引号对单引号,双引号对双引号)
    ///     2.5 [[]]开始到结束(包括[=[]=]风格),为一个文本类型单词.到对应内容(]])出现时结束
    ///     2.5 --LUA注释,
    ///         如果--紧接[[,无视换行直到]]出栈结束(内部可以有多次[[与]]的对)为一个注释单词
    ///         否则到行尾结束为一个注释单词
    /// 第二步:单词识别
    ///     1 前置条件,当加入新单词对象时,处理上一个单词对象,或全部字符解析结束时处理最后一个单词对象
    ///     2 LUA嵌入的单词做类型判定
    ///     3 单词加入到当前区块中
    /// 第三步:加入到语法树
    ///     1 前置条件,单词识别结束后触发
    ///     2 按当前单词类型(内容,文档注释,文档配置,LUA嵌入),装入紧接相同类型块中(如果不紧接则在当前树位置新增一块)
    ///     3 非LUA嵌入,处理到此结束
    ///     4 LUA嵌入的空白与注释被忽略
    /// 第四步:区域组合
    ///     4 如果单词有区域开始特性,则将当前块入栈并在其的尾部压入一个新区域块,之后加入的普通单词以这个区域为根
    ///     5 如果单词有区域结束特性,则将出栈并恢复到入栈前的环境
    /// 第五步:区块建立
    ///     6 如果单词有后方应为区块的特性,则将在当前块后面加入一个区块特性的块,之后加入的普通单词以这个区块为根
    /// 第六步:操作符块建立
    ///     7 如果单词有操作符特性,将根据操作符级别组合,即低级别的作为高级别的子级存在(双向组合的对前一个低级别的也同样处理)
    /// 第七步:普通单词加入
    ///     8 如果树尾部是未完成的操作符块,加入它并标识其为完成
    ///     9 否则加入当前根(注意第四步与第五步会改变当前根)
    /// 第八步:小括号区域加入的特别处理
    ///     10 如果前一个单词是字母且不是关键字(until例外),则应该与前一个单词组合成为方法调用块(无视当前根)
    /// 第九步:中括号区域加入的特别处理
    ///     10 为满足中括号与.连接符的相同特性,强制在中括号加入前增加一个虚拟的.字符
    /// </remarks>
    public sealed class LuaWordSpliter
    {
        #region 公开对象

        /// <summary>
        /// 语法树根
        /// </summary>
        public AnalyzeBlock Root => _root;

        /// <summary>
        /// 当前模板对应的配置对象
        /// </summary>
        public TemplateConfig Config { get; set; }


        /// <summary>
        ///     基本单词
        /// </summary>
        public List<WordUnit> Words { get; private set; }

        /// <summary>
        ///     代码文本
        /// </summary>
        public string Code { get; set; }

        #endregion

        #region 分析文本

        /// <summary>
        ///     分析文本
        /// </summary>
        public static List<WordUnit> Do(string code)
        {
            var analyzer = new LuaWordSpliter();
            analyzer.SplitTemplateWords(code ?? "");
            return analyzer.Words;
        }

        /// <summary>
        ///     拆分文本
        /// </summary>
        public void SplitTemplateWords(string code)
        {
            Code = code;
            Reset(false);
            while (StepNextChar())
                IdentifyChar();
            OnWordEnd();
            ReleaseTree(Root);
        }

        /// <summary>
        ///     拆分文本
        /// </summary>
        public void SplitLuaWords(string code)
        {
            Code = code;
            Reset(true);
            _luaType = TemplateUnitType.Code;
            while (StepNextChar())
                IdentifyChar();
            OnWordEnd();
            ReleaseTree(Root);
        }

        /// <summary>
        /// 重置对象
        /// </summary>
        private void Reset(bool isLuaCode)
        {
            if (Config == null)
            {
                Config = new TemplateConfig
                {
                    Template = Code
                };
            }
            ResetChar(isLuaCode);
            ResetWord();
            ResetTree();
        }

        #endregion

        #region 字符处理

        #region 字符环境

        /// <summary>
        /// 当前迭代的字符下标
        /// </summary>
        private int _iterIndex;

        /// <summary>
        /// 当前字符的下标
        /// </summary>
        private int _charIndex;

        /// <summary>
        /// 当前字符所在的列
        /// </summary>
        private int _curLine;

        /// <summary>
        /// 当前字符所在的列
        /// </summary>
        private int _curColumn;

        /// <summary>
        /// 处理的当前字符
        /// </summary>
        private char _curChar;

        /// <summary>
        /// 处理的前一个字符
        /// </summary>
        private char _preChar;

        /// <summary>
        /// 当前字符是否处于行的开始
        /// </summary>
        private bool _isLineStart;

        /// <summary>
        /// 跳过立即遇到的空行
        /// </summary>
        private bool _skipLine;

        /// <summary>
        /// 重置字符环境
        /// </summary>
        private void ResetChar(bool isLuaCode)
        {
            _luaType = isLuaCode ? TemplateUnitType.Code : TemplateUnitType.Content;
            _curWord = null;
            _skipLine = true;
            _curLine = 0;
            _curColumn = -1;
            _charIndex = -1;
            _iterIndex = -1;
            _curChar = _preChar = '\0';
            _isLineStart = true;
        }

        /// <summary>
        ///     取下一个字符
        /// </summary>
        private bool StepNextChar()
        {
            do
            {
                if (++_iterIndex >= Code.Length)
                    return false;
            } while (Code[_iterIndex] == '\r');

            _charIndex++;
            _preChar = _curChar;
            _curChar = Code[_iterIndex];
            switch (_preChar)
            {
                case '\r':
                case '\t':
                case ' ':
                case '\u2028':
                case '\u2029':
                case '\u000B':
                case '\u000C':
                    break;
                case '\n':
                    _isLineStart = true;
                    break;
                default:
                    _isLineStart = false;
                    break;
            }
            switch (_curChar)
            {
                case '\n':
                    _curLine++;
                    _curColumn = 0;
                    if (_skipLine)
                    {
                        _curChar = _preChar;//防止之前被认到这个空行
                        _skipLine = false;
                        return StepNextChar();
                    }
                    break;
                case '\r':
                case '\t':
                case ' ':
                case '\u2028':
                case '\u2029':
                case '\u000B':
                case '\u000C':
                    if (_skipLine)
                    {
                        return StepNextChar();
                    }
                    break;
                default:
                    _skipLine = false;
                    _curColumn++;
                    break;
            }

            return true;
        }

        /// <summary>
        /// 此字符是否为空白
        /// </summary>
        /// <param name="ch">字符</param>
        /// <returns>是否为空白</returns>
        private static bool IsSpace(char ch)
        {
            switch (ch)
            {
                case '\n':
                case '\r':
                case '\t':
                case ' ':
                case '\u2028':
                case '\u2029':
                case '\u000B':
                case '\u000C':
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 此字符是否为普通字母
        /// </summary>
        /// <param name="ch">字符</param>
        /// <returns>是否为普通字母</returns>
        private static bool IsLetterChar(char ch)
        {
            return ch >= 128
                   || ch == '_'
                   || ch >= '0' && ch <= '9' //数字
                   || ch >= 'A' && ch <= 'Z' //字母
                   || ch >= 'a' && ch <= 'z';
        }

        #endregion

        #region 字符识别

        /// <summary>
        /// 字符识别
        /// </summary>
        private void IdentifyChar()
        {
            if (IsSpace(_curChar))
            {
                if (_luaType == TemplateUnitType.SimpleValue)
                {
                    _luaType = TemplateUnitType.Content;
                    NewWord(true);
                }
                else if (_curWord != null && IsSpace(_preChar))
                {
                    PushCharToCurWord();
                }
                else
                {
                    NewWord(true);
                }
            }
            else if (IsLetterChar(_curChar))
            {
                IdentifyWordChar();
            }
            else
            {
                IdentifyPunctuateChar();
            }
        }

        /// <summary>
        /// 单词字符识别
        /// </summary>
        private void IdentifyWordChar()
        {
            if (_curWord == null || _curWord.IsPunctuate ||
                _curWord.ItemFamily == CodeItemFamily.Space || _curWord.ItemFamily == CodeItemFamily.Sharp)
            {
                NewWord(false);
            }
            PushCharToCurWord();
            if (char.IsNumber(_curChar) && _curWord.NumberType == 0)
            {
                _curWord.NumberType = 1;
                if (_luaType > 0)
                {
                    _curWord.SetRace(CodeItemRace.Value, CodeItemFamily.Constant, CodeItemType.Number);
                }
            }
        }

        /// <summary>
        /// 标点字符识别
        /// </summary>
        private void IdentifyPunctuateChar()
        {
            if (_curWord != null && _curWord.NumberType == 1 && _curChar == '.')
            {
                PushCharToCurWord();
                _curWord.NumberType = 2;
                return;
            }
            if (_luaType > TemplateUnitType.SimpleValue)
            {
                MarkLuaPunctuate();
                return;
            }
            if (_luaType == TemplateUnitType.SimpleValue)
            {
                if (_curChar == ':' || _curChar == '.')
                {
                    MarkLuaPunctuate();
                    return;
                }
                _luaType = TemplateUnitType.Content;
            }
            if (_curChar == '@')
            {
                IdentifySyntacticSugar();
            }
            else
            {
                NewWord(true);
            }
        }

        #endregion

        #region 嵌入代码识别

        /// <summary>
        ///     嵌入代码识别
        /// </summary>
        private void IdentifySyntacticSugar()
        {
            NewWord(true);
            bool preIsLine = _isLineStart;
            if (!StepNextChar())
                return;
            if (char.IsNumber(_curChar))
            {
                IdentifyWordChar();
                return;
            }
            if (IsLetterChar(_curChar))
            {
                SetCurWordIsSharp(false);
                _luaType = TemplateUnitType.SimpleValue;
                IdentifyWordChar();
            }
            else
            {
                IdentifySyntacticSugar(preIsLine);
            }
        }

        /// <summary>
        ///     嵌入代码识别
        /// </summary>
        private void IdentifySyntacticSugar(bool preIsLine)
        {
            switch (_curChar)
            {
                case '@':
                    SetCurWordIsSharp(false);
                    NewWord(true);
                    SetCurWordIsContent();
                    return;
                default:
                    //此时@为普通字符
                    SetCurWordIsContent();
                    IdentifyChar(); //处理它
                    return;
                case '*': //@*...*@为注释
                    _curWord.SetRace(CodeItemRace.Assist, CodeItemFamily.Rem, CodeItemType.TemplateRem);
                    _curWord.UnitType = TemplateUnitType.Rem;
                    PushCharToCurWord();
                    MergeEndByChar('*', '@');
                    _luaType = TemplateUnitType.Content;
                    _skipLine = true;
                    return;
                case '#': //@#...#@为文档配置
                    PushCharToCurWord();
                    ReadTemplateConfig();
                    _luaType = TemplateUnitType.Content;
                    _skipLine = true;
                    return;
                case '-': //单行@-表示单行代码
                    if (preIsLine)
                    {
                        PushCharToCurWord();
                        SetCurWordIsSharp(true);
                        _luaType = TemplateUnitType.Code;
                        MergeToLineEnd(true);//放弃空行
                        _luaType = TemplateUnitType.Content;
                        return;
                    }
                    //其它标点回退,此时@为普通字符
                    SetCurWordIsContent();
                    IdentifyChar(); //处理它
                    return;
                case '(': //@()小括号配对变量块
                    PushCharToCurWord();
                    SetCurWordIsSharp(false);
                    _luaType = TemplateUnitType.Value;
                    FindCouple('(', ')');
                    NewWord(true);
                    SetCurWordIsSharp(false);
                    _luaType = TemplateUnitType.Content;
                    return;
                case '{': //@{}多行代码块
                    PushCharToCurWord();
                    SetCurWordIsSharp(true);
                    _luaType = TemplateUnitType.Code;
                    FindCouple('{', '}');
                    NewWord(true);
                    SetCurWordIsSharp(true);
                    _luaType = TemplateUnitType.Content;
                    _skipLine = true;
                    return;
            }
        }
        #endregion

        #region 特殊语法环境

        /// <summary>
        ///     合并字符
        /// </summary>
        private void MergeString()
        {
            _curWord.EmptyChar = 1;
            var sign = _curChar;
            var preShift = false;
            _curWord.SetRace(CodeItemRace.Value, CodeItemFamily.Constant, CodeItemType.String);
            _curWord.ValueType = LuaDataTypeItem.StringValue;
            while (StepNextChar())
            {
                PushCharToCurWord();
                if (preShift)
                {
                    preShift = false; //前方为转义字符不处理
                    continue;
                }
                if (_curChar == '\\')
                {
                    preShift = true;
                    continue;
                }
                if (_curChar == sign)
                    return; //成对出现,结束字符串识别
            }
        }

        /// <summary>
        ///     合并到行尾结束的对象
        /// </summary>
        private void MergeToLineEnd(bool doCheck = false)
        {
            while (StepNextChar())
            {
                switch (_curChar)
                {
                    case '\n':
                        if (!doCheck)
                            PushCharToCurWord();
                        return;
                    default:
                        if (doCheck)
                            IdentifyChar();
                        else
                            PushCharToCurWord();
                        break;
                }
            }
        }

        /// <summary>
        ///     合并注释
        /// </summary>
        private void MergeLuaRem()
        {
            _curWord.UnitType = TemplateUnitType.Rem;
            if (!StepNextChar())
                return;
            var a = _curChar;
            PushCharToCurWord();
            if (StepNextChar())
                return;
            PushCharToCurWord();
            if (a != '[' || _curChar != '[')
            {
                MergeToLineEnd();
                return;
            }
            var preType = 0;
            var stack = 0; //嵌套处理
            while (StepNextChar())
            {
                PushCharToCurWord();
                switch (_curChar)
                {
                    case ']':
                        if (preType == 1)
                            if (--stack == 0)
                                return;
                        preType = 1;
                        break;
                    case '[':
                        if (preType == 2)
                            stack++;
                        preType = 2;
                        break;
                    default:
                        preType = 0;
                        break;
                }
            }
        }


        /// <summary>
        ///     合并到指定字符依次出现
        /// </summary>
        private void MergeEndByChar(char a, char b)
        {
            var shfit = false;
            while (StepNextChar())
            {
                PushCharToCurWord();
                if (_curChar == a)
                    shfit = true;
                else if (_curChar == b && shfit)
                    return;
                else shfit = false;
            }
        }

        /// <summary>
        ///     查找成对的符号(如果内部有,按栈方式处理到栈为空)
        /// </summary>
        /// <param name="start">开始字符</param>
        /// <param name="end">结束</param>
        /// <returns></returns>
        private void FindCouple(char start, char end)
        {
            var block = 1;
            while (StepNextChar())
            {
                if (_curChar == start)
                    block++;
                else if (_curChar == end)
                    --block;
                if (block != 0)
                {
                    IdentifyChar();
                    continue;
                }
                break;
            }
        }

        /// <summary>
        ///     试图合并多行文本
        /// </summary>
        private bool TryMergeMulitString()
        {
            var startRange = 1; //几个符号开头的
            var start = _iterIndex;
            var index = _charIndex;
            var chars = new List<char>
            {
                _curChar
            };

            while (StepNextChar())
            {
                if (_curChar == '=') //等于号计数
                {
                    startRange++;
                    chars.Add(_curChar);
                }
                else if (_curChar == '[') //成了多行注释
                {
                    startRange++;
                    NewWord(false);
                    _curWord.Start = index;
                    _curWord.End = _charIndex;
                    _curWord.Chars.AddRange(chars);
                    PushCharToCurWord();
                    break;
                }
                else
                {
                    //不是多行文本,恢复环境
                    _iterIndex = start - 1;
                    _charIndex = index - 1;
                    StepNextChar();
                    return false;
                }
            }
            _curWord.EmptyChar = startRange;
            _curWord.SetRace(CodeItemRace.Value, CodeItemFamily.Constant, CodeItemType.String);
            _curWord.ValueType = LuaDataTypeItem.StringValue;
            var endRange = 0; //相同数量的结束
            while (StepNextChar())
            {
                PushCharToCurWord();
                switch (_curChar)
                {
                    case ']':
                        if (endRange + 1 == startRange)
                            return true;
                        endRange = 1;
                        break;
                    case '=':
                        if (endRange > 0)
                            endRange++;
                        break;
                    default:
                        endRange = 0;
                        break;
                }
            }
            _curWord.IsError = true; //不正确
            return false;
        }

        #endregion

        #region 杂注

        /// <summary>
        ///     读取文档配置@#...#@
        /// </summary>
        private void ReadTemplateConfig()
        {
            _curWord.SetRace(CodeItemRace.Assist, CodeItemFamily.Rem, CodeItemType.TemplateConfig);
            _curWord.UnitType = TemplateUnitType.Config;
            _curWord.Name = "Config";
            //已识别的名称
            string name = null;
            //内容装入对象
            StringBuilder builder = new StringBuilder();
            //当前识别的步骤
            int step = 0;
            //闭合需要的字符
            char close = '\0';
            //上一个字符(此语法环境中的)
            char pre = '\0';
            //是否可以转义(结束的前提)
            bool shfit = false;
            while (StepNextChar())
            {
                PushCharToCurWord();
                if (shfit)
                {
                    if (_curChar == '@')
                    {
                        break;
                    }
                    //转义失败,#作为普通字符处理
                    CheckTemplateConfigChar(builder, '#', pre, ref step, ref name, ref close);
                    shfit = false;
                }
                //开始转义,#暂不处理
                if (_curChar == '#')
                {
                    shfit = true;
                    continue;
                }
                CheckTemplateConfigChar(builder, _curChar, pre, ref step, ref name, ref close);
                pre = _curChar;
            }
            WriteToTemplateConfig(name, builder.ToString());
        }

        /// <summary>
        /// 处理配置中的单词
        /// </summary>
        /// <param name="builder">内容装入对象</param>
        /// <param name="ch">当前字符</param>
        /// <param name="pre">上一个字符(此语法环境中的)</param>
        /// <param name="step">当前识别的步骤</param>
        /// <param name="name">已识别的名称</param>
        /// <param name="close">闭合需要的字符</param>
        private void CheckTemplateConfigChar(StringBuilder builder, char ch, char pre, ref int step, ref string name,
            ref char close)
        {
            switch (step)
            {
                case 0: //取得名称,并在出现第一个冒时时进入第二步
                    if (IsSpace(ch))
                    {
                        return;
                    }
                    if (IsLetterChar(ch))
                    {
                        builder.Append(ch);
                    }
                    else
                    {
                        if (ch == ':')
                        {
                            step = 1;
                            name = builder.ToString();
                        }
                        builder.Clear();
                    }
                    return;
                case 1: //检测值是以哪种方式存在
                    if (ch == '\n') //空说明,跳过
                    {
                        step = 0;
                        builder.Clear();
                        return;
                    }
                    if (IsSpace(ch))
                    {
                        return;
                    }
                    switch (ch)
                    {
                        case '\'':
                        case '\"':
                            step = 3; //有引号的
                            close = ch;
                            break;
                        default:
                            builder.Append(ch);
                            step = 2; //没引号的
                            break;
                    }
                    break;
                case 2: //没引号的,到行结束
                    if (ch == '\n')
                    {
                        WriteToTemplateConfig(name, builder.ToString());
                        builder.Clear();
                        step = 0;
                        name = null;
                        break;
                    }
                    builder.Append(ch);
                    break;
                case 3: //有引号的,到引号结束,使用转义
                    if (pre == '\\')
                    {
                        builder.Remove(builder.Length - 1, 1);
                    }
                    else if (ch == close)
                    {
                        WriteToTemplateConfig(name, builder.ToString());
                        step = 0;
                        name = null;
                        builder.Clear();
                        break;
                    }
                    builder.Append(ch);
                    break;
            }
        }

        /// <summary>
        /// 将值写入模板配置中
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">内容</param>
        private void WriteToTemplateConfig(string name, string value)
        {
            if (name == null)
                return;
            switch (name.ToLower())
            {
                case "name":
                case "名称":
                    Config.Name = value.FromLuaChar();
                    break;
                case "caption":
                case "标题":
                    Config.Caption = value.FromLuaChar();
                    break;
                case "desc":
                case "description":
                case "说明":
                case "备注":
                case "模板说明":
                    Config.Description = value.FromLuaChar();
                    break;
                case "classify":
                case "类别":
                case "分类":
                    Config.Classify = value.FromLuaChar();
                    break;
                case "condition":
                case "执行条件":
                    Config.Condition = value.FromLuaChar();
                    break;
                case "language":
                case "语言":
                case "目标语言":
                    Config.Language = value.FromLuaChar();
                    break;
                case "savesath":
                case "codesavesath":
                case "代码保存路径":
                case "代码保存":
                case "保存路径":
                    Config.CodeSavePath = value.FromLuaChar();
                    break;
                case "type":
                case "model":
                case "modeltype":
                case "参数":
                case "参数类型":
                case "数据模型":
                case "模型":
                case "模型类型":
                    var type = value.FromLuaChar();
                    switch (type.ToLower())
                    {
                        case "字段":
                        case "field":
                        case "property":
                        case "propertyconfig":
                            Config.ModelType = typeof(PropertyConfig).Name;
                            break;
                        case "实体":
                        case "entity":
                        case "entityconfig":
                            Config.ModelType = typeof(EntityConfig).Name;
                            break;
                        case "枚举":
                        case "enum":
                        case "enumconfig":
                            Config.ModelType = typeof(EnumConfig).Name;
                            break;
                        case "类型":
                        case "类型定义":
                        case "type":
                        case "typedef":
                        case "typedefitem":
                        case "typeconfig":
                        case "typedefconfig":
                            Config.ModelType = typeof(TypedefItem).Name;
                            break;
                        case "命令":
                        case "用户命令":
                        case "按钮":
                        case "btn":
                        case "button":
                        case "cmd":
                        case "command":
                            Config.ModelType = typeof(UserCommandConfig).Name;
                            break;
                        case "项目":
                        case "project":
                        case "projectconfig":
                            Config.ModelType = typeof(ProjectConfig).Name;
                            break;
                        case "解决方案":
                        case "solution":
                        case "solutionconfig":
                            Config.ModelType = typeof(SolutionConfig).Name;
                            break;
                        default:
                            Config.ModelType = type;
                            break;
                    }
                    break;
            }
        }

        #endregion

        #endregion

        #region 单词处理

        /// <summary>
        /// 重置单词环境
        /// </summary>
        private void ResetWord()
        {
            Words = new List<WordUnit>();
            _curWord = null;
            _preNoSapceWord = null;
        }

        /// <summary>
        /// 当前单词
        /// </summary>
        private WordUnit _curWord;

        /// <summary>
        /// 前一个非空单词
        /// </summary>
        private WordUnit _preNoSapceWord;

        /// <summary>
        /// 前一个单词
        /// </summary>
        private WordUnit _preWord;

        /// <summary>
        /// 将当前字符写入当前单词
        /// </summary>
        private void PushCharToCurWord()
        {
            _curWord.Append(_charIndex, _curChar);
        }

        /// <summary>
        /// 生成新的单词对象
        /// </summary>
        /// <param name="pushCur">是否直接加入当前字符</param>
        private void NewWord(bool pushCur)
        {
            if (_curWord != null)
            {
                OnWordEnd();
                _preWord = _curWord;
                if (!_curWord.IsSpace)
                    _preNoSapceWord = _curWord;
            }
            _curWord = new WordUnit
            {
                Line = _curLine,
                Column = _curColumn,
                UnitType = _luaType == TemplateUnitType.SimpleValue ? TemplateUnitType.Value : _luaType
            };
            Words.Add(_curWord);
            if (pushCur)
                PushCharToCurWord();
            if (_luaType == TemplateUnitType.Content)
            {
                SetCurWordIsContent();
            }
        }

        /// <summary>
        /// 将当前单词设为转义字符
        /// </summary>
        private void SetCurWordIsSharp(bool isCode)
        {
            if (_preWord != null && _preWord.UnitType == TemplateUnitType.Content && _preWord.IsLine)
            {
                _preWord.Clear();
            }
            _curWord.UnitType = TemplateUnitType.Sharp;
            _curWord.SetRace(CodeItemRace.Assist, CodeItemFamily.Sharp, isCode ? CodeItemType.BlockSharp : CodeItemType.ValueSharp);
        }
        /// <summary>
        /// 将当前单词设为内容
        /// </summary>
        private void SetCurWordIsContent()
        {
            _curWord.SetRace(CodeItemRace.Value, CodeItemFamily.Constant, CodeItemType.Content);
            _curWord.UnitType = TemplateUnitType.Content;
            _curWord.ValueType = LuaDataTypeItem.StringValue;
            _curWord.IsContent = true;
        }
        /// <summary>
        /// 单词结束处理(单词识别并加入语法树)
        /// </summary>
        private void OnWordEnd()
        {
            if (_curWord == null || _curWord.IsEmpty)
                return;
            if (_curWord.UnitType >= TemplateUnitType.SimpleValue)
            {
                MarkLuaWord(_curWord);
                if (_curWord.ItemType == CodeItemType.Key_End && _curWord.UnitType != TemplateUnitType.Code)
                {
                    _curWord.UnitType = TemplateUnitType.Code;
                    _luaType = TemplateUnitType.Content;
                }
            }
            AppendCurWordTree();
        }

        #region 标点

        /// <summary>
        /// 标识LUA的标点相关的关键字
        /// </summary>
        private void MarkLuaPunctuate()
        {
            switch (_curChar)
            {
                #region 其它

                case '#':
                    NewWord(true);
                    _curWord.IsKeyWord = true;
                    _curWord.JoinLevel = 2;
                    _curWord.ValueType = LuaDataTypeItem.NumberValue;
                    _curWord.JoinFeature = JoinFeature.Front;
                    _curWord.SetRace(CodeItemRace.Sentence, CodeItemFamily.Operator, CodeItemType.StringLen);

                    break;
                case ';':
                    NewWord(true);
                    _curWord.IsKeyWord = true;
                    _curWord.SetRace(CodeItemRace.Control, CodeItemFamily.Control, CodeItemType.Separator_Semicolon);

                    break;
                case '\'':
                case '\"':
                    NewWord(true);
                    MergeString();
                    break;
                //case '\n':
                //case '\r':
                //case '\t':
                //case ' ':
                //case '\u2028':
                //case '\u2029':
                //case '\u000B':
                //case '\u000C':
                //    if (!_curWord.IsSpace)
                //        NewWord(true);
                //    else
                //        PushCharToCurWord();
                //    break;
                default:
                    NewWord(true);
                    _curWord.IsError = true;
                    break;

                #endregion

                #region 扩号区域

                case '@':
                    NewWord(true);
                    _curWord.IsKeyWord = true;
                    _curWord.PrimaryLevel = 1;
                    _curWord.JoinLevel = 2;
                    _curWord.JoinFeature = JoinFeature.Front;
                    _curWord.ValueType = LuaDataTypeItem.FunctionConfirm;
                    _curWord.SetRace(CodeItemRace.Sentence, CodeItemFamily.Operator, CodeItemType.Print);
                    break;

                case '{':
                    NewWord(true);
                    _curWord.IsKeyWord = true;
                    _curWord.PrimaryLevel = 1;
                    _curWord.JoinFeature = JoinFeature.BracketOpen;
                    _curWord.ValueType = LuaDataTypeItem.TableDefinition;
                    _curWord.SetRace(CodeItemRace.Range, CodeItemFamily.BracketRange, CodeItemType.Brackets41);
                    break;
                case '}':
                    NewWord(true);
                    _curWord.IsKeyWord = true;
                    _curWord.PrimaryLevel = 1;
                    _curWord.JoinFeature = JoinFeature.BracketClose;
                    _curWord.SetRace(CodeItemRace.Range, CodeItemFamily.BracketRange, CodeItemType.Brackets42);
                    break;
                case '(':
                    NewWord(true);
                    _curWord.IsKeyWord = true;
                    _curWord.PrimaryLevel = 1;
                    _curWord.JoinFeature = JoinFeature.BracketOpen;
                    _curWord.SetRace(CodeItemRace.Range, CodeItemFamily.BracketRange, CodeItemType.Brackets21);
                    break;
                case ')':
                    NewWord(true);
                    _curWord.IsKeyWord = true;
                    _curWord.JoinFeature = JoinFeature.BracketClose;
                    _curWord.SetRace(CodeItemRace.Range, CodeItemFamily.BracketRange, CodeItemType.Brackets22);

                    break;
                case '[':
                    if (!TryMergeMulitString())
                    {
                        NewWord(true);
                        _curWord.IsKeyWord = true;
                        _curWord.PrimaryLevel = 1;
                        _curWord.JoinFeature = JoinFeature.BracketOpen;
                        _curWord.SetRace(CodeItemRace.Range, CodeItemFamily.BracketRange, CodeItemType.Brackets31);
                    }
                    break;
                case ']':
                    NewWord(true);
                    _curWord.IsKeyWord = true;
                    _curWord.JoinFeature = JoinFeature.BracketClose;
                    _curWord.SetRace(CodeItemRace.Range, CodeItemFamily.BracketRange, CodeItemType.Brackets32);

                    break;

                #endregion

                #region 算术计算

                case '+':
                    NewWord(true);
                    _curWord.IsKeyWord = true;
                    _curWord.PrimaryLevel = 1;
                    _curWord.JoinLevel = 5;
                    _curWord.ValueType = LuaDataTypeItem.NumberValue;
                    _curWord.JoinFeature = JoinFeature.TowWay;
                    _curWord.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compute, CodeItemType.Compute_Add);

                    break;
                case '-':
                    if (_preChar == '-') //注释
                    {
                        PushCharToCurWord();
                        MergeLuaRem();
                    }
                    else
                    {
                        NewWord(true);
                        _curWord.IsKeyWord = true;
                        _curWord.JoinLevel = 5;
                        _curWord.ValueType = LuaDataTypeItem.NumberValue;
                        _curWord.JoinFeature = JoinFeature.TowWay;
                        _curWord.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compute, CodeItemType.Compute_Sub);
                    }
                    break;
                case '*':
                    NewWord(true);
                    _curWord.IsKeyWord = true;
                    _curWord.PrimaryLevel = 1;
                    _curWord.JoinLevel = 4;
                    _curWord.ValueType = LuaDataTypeItem.NumberValue;
                    _curWord.JoinFeature = JoinFeature.TowWay;
                    _curWord.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compute, CodeItemType.Compute_Mulit);

                    break;
                case '/':
                    NewWord(true);
                    _curWord.IsKeyWord = true;
                    _curWord.PrimaryLevel = 1;
                    _curWord.JoinLevel = 4;
                    _curWord.ValueType = LuaDataTypeItem.NumberValue;
                    _curWord.JoinFeature = JoinFeature.TowWay;
                    _curWord.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compute, CodeItemType.Compute_Div);

                    break;
                case '%':
                    NewWord(true);
                    _curWord.IsKeyWord = true;
                    _curWord.PrimaryLevel = 1;
                    _curWord.JoinLevel = 4;
                    _curWord.ValueType = LuaDataTypeItem.NumberValue;
                    _curWord.JoinFeature = JoinFeature.TowWay;
                    _curWord.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compute, CodeItemType.Compute_Mod);

                    break;
                case '^':
                    NewWord(true);
                    _curWord.IsKeyWord = true;
                    _curWord.PrimaryLevel = 1;
                    _curWord.JoinLevel = 3;
                    _curWord.ValueType = LuaDataTypeItem.NumberValue;
                    _curWord.JoinFeature = JoinFeature.TowWay;
                    _curWord.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compute, CodeItemType.Compute_Exp);

                    break;

                #endregion

                #region 连接符

                case ',':
                    NewWord(true);
                    _curWord.IsKeyWord = true;
                    _curWord.PrimaryLevel = 1;
                    _curWord.JoinLevel = 10;
                    _curWord.JoinFeature = JoinFeature.TowWay;
                    _curWord.SetRace(CodeItemRace.Sentence, CodeItemFamily.Separator, CodeItemType.Separator_Comma);

                    break;

                case ':':
                    NewWord(true);
                    _curWord.IsKeyWord = true;
                    _curWord.PrimaryLevel = 1;
                    _curWord.JoinLevel = 1;
                    _curWord.JoinFeature = JoinFeature.TowWay;
                    _curWord.SetRace(CodeItemRace.Sentence, CodeItemFamily.Separator, CodeItemType.Separator_Colon);

                    break;
                case '.':
                    if (_preChar != '.')
                    {
                        if (_curWord.NumberType == 1)
                        {
                            PushCharToCurWord();
                            _curWord.NumberType = 2;
                        }
                        else
                        {
                            NewWord(true);
                            _curWord.IsKeyWord = true;
                            _curWord.PrimaryLevel = 1;
                            _curWord.JoinLevel = 1;
                            _curWord.JoinFeature = JoinFeature.TowWay;
                            _curWord.SetRace(CodeItemRace.Sentence, CodeItemFamily.Separator, CodeItemType.Separator_Dot);
                        }
                    }
                    else if (_curWord.Chars.Count == 1)
                    {
                        PushCharToCurWord();
                        _curWord.JoinLevel = 9;
                        _curWord.ValueType = LuaDataTypeItem.StringValue;
                        _curWord.SetRace(CodeItemRace.Sentence, CodeItemFamily.Separator,
                            CodeItemType.Separator_StringJoin);
                    }
                    else if (_curWord.Chars.Count == 2 && _curWord.NumberType == 0)
                    {
                        PushCharToCurWord();
                        _curWord.IsKeyWord = false;
                        _curWord.JoinLevel = -1;
                        _curWord.ValueType = LuaDataTypeItem.TableDefinition;
                        _curWord.JoinFeature = JoinFeature.None;
                        _curWord.SetRace(CodeItemRace.Variable, CodeItemFamily.Variable, CodeItemType.Variable_Arg);
                    }
                    else
                    {
                        PushCharToCurWord();
                        _curWord.IsError = false;
                    }
                    break;

                #endregion

                #region 逻辑比较

                case '>':
                    NewWord(true);
                    _curWord.IsKeyWord = true;
                    _curWord.ValueType = LuaDataTypeItem.BoolValue;
                    _curWord.JoinLevel = 6;
                    _curWord.JoinFeature = JoinFeature.TowWay;
                    _curWord.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compare, CodeItemType.Compare_Greater);

                    break;
                case '<':
                    NewWord(true);
                    _curWord.IsKeyWord = true;
                    _curWord.ValueType = LuaDataTypeItem.BoolValue;
                    _curWord.JoinLevel = 6;
                    _curWord.JoinFeature = JoinFeature.TowWay;
                    _curWord.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compare, CodeItemType.Compare_Less);

                    break;
                case '=':
                    var canLink = false;
                    switch (_preChar)
                    {
                        case '>':
                            canLink = true;
                            _curWord.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compare,
                                CodeItemType.Compare_GreaterEqual);
                            break;
                        case '<':
                            canLink = true;
                            _curWord.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compare,
                                CodeItemType.Compare_LessEqual);
                            break;
                        case '~':
                            canLink = true;
                            _curWord.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compare,
                                CodeItemType.Compare_NotEqual);
                            break;
                        case '=':
                            canLink = true;
                            _curWord.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compare, CodeItemType.Compare_Equal);
                            break;
                    }
                    if (canLink)
                    {
                        PushCharToCurWord();
                        _curWord.PrimaryLevel = 1;
                        _curWord.ValueType = LuaDataTypeItem.BoolValue;
                        _curWord.JoinLevel = 6;
                        _curWord.JoinFeature = JoinFeature.TowWay;
                    }
                    else
                    {
                        NewWord(true);
                        _curWord.PrimaryLevel = 1;
                        _curWord.IsKeyWord = true;
                        _curWord.JoinLevel = 13;
                        _curWord.JoinFeature = JoinFeature.TowWay;
                        _curWord.SetRace(CodeItemRace.Sentence, CodeItemFamily.SetValue, CodeItemType.Separator_Equal);
                    }
                    break;

                    #endregion
            }
        }

        #endregion

        #region 关键字

        private void MarkLuaWord(WordUnit word)
        {
            if (word.IsPunctuate || word.ItemRace == CodeItemRace.Assist || word.ItemRace == CodeItemRace.Value)
                return;
            if (!_keyWords.Contains(word.Word))
            {
                word.Name = word.Word;
                word.SetRace(CodeItemRace.Variable, CodeItemFamily.Variable, CodeItemType.Variable_Global);
                return;
            }
            word.IsKeyWord = true;
            switch (word.Word)
            {
                default:
                    word.SetRace(CodeItemRace.Sentence, CodeItemFamily.KeyWord);
                    break;
                case "nil":
                    word.ValueType = LuaDataTypeItem.Nil;
                    word.SetRace(CodeItemRace.Value, CodeItemFamily.Constant, CodeItemType.Value_Null);
                    break;
                case "true":
                    word.ValueType = LuaDataTypeItem.BoolValue;
                    word.SetRace(CodeItemRace.Value, CodeItemFamily.Constant, CodeItemType.Value_True);
                    break;
                case "false":
                    word.ValueType = LuaDataTypeItem.BoolValue;
                    word.SetRace(CodeItemRace.Value, CodeItemFamily.Constant, CodeItemType.Value_False);
                    break;
                case "and":
                    word.JoinLevel = 7;
                    word.JoinFeature = JoinFeature.TowWay;
                    word.SetRace(CodeItemRace.Sentence, CodeItemFamily.Logical, CodeItemType.Key_And);
                    break;
                case "or":
                    word.JoinLevel = 9;
                    word.JoinFeature = JoinFeature.TowWay;
                    word.SetRace(CodeItemRace.Sentence, CodeItemFamily.Logical, CodeItemType.Key_Or);
                    break;
                case "not":
                    word.JoinLevel = 2;
                    word.PrimaryLevel = 1;
                    word.JoinFeature = JoinFeature.Front;
                    word.SetRace(CodeItemRace.Sentence, CodeItemFamily.Compare, CodeItemType.Key_Not);
                    break;
                case "local":
                    word.JoinLevel = 12;
                    word.JoinFeature = JoinFeature.Front;
                    word.PrimaryLevel = 33;
                    word.SetRace(CodeItemRace.Control, CodeItemFamily.Scope, CodeItemType.Key_Local);
                    break;
                case "break":
                    word.SetRace(CodeItemRace.Control, CodeItemFamily.Control, CodeItemType.Key_Break);
                    break;
                case "return":
                    word.JoinLevel = 11;
                    word.JoinFeature = JoinFeature.Front;
                    word.SetRace(CodeItemRace.Control, CodeItemFamily.Control, CodeItemType.Key_Return);
                    break;
                case "for":
                    word.NeedSentence = true;
                    word.JoinFeature = JoinFeature.RangeOpen;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Iterator, CodeItemType.Key_For);
                    break;
                case "in":
                    word.NeedSentence = true;
                    word.JoinFeature = JoinFeature.TowWay;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Control, CodeItemType.Key_In);
                    break;
                case "if":
                    word.NeedSentence = true;
                    word.JoinFeature = JoinFeature.RangeOpen;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Condition, CodeItemType.Key_If);
                    break;
                case "while":
                    word.NeedSentence = true;
                    word.JoinFeature = JoinFeature.RangeOpen;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Condition, CodeItemType.Key_While);
                    break;
                case "else":
                    word.NeedSentence = true;
                    word.JoinFeature = JoinFeature.RangeShift;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Condition, CodeItemType.Key_Else);
                    break;
                case "elseif":
                    word.NeedSentence = true;
                    word.JoinFeature = JoinFeature.RangeShift;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Condition, CodeItemType.Key_Elseif);
                    break;
                case "then":
                    word.NeedSentence = true;
                    word.JoinFeature = JoinFeature.RangeShift;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Control, CodeItemType.Key_Then);
                    break;
                case "go":
                    word.NeedSentence = true;
                    word.JoinFeature = JoinFeature.RangeShift;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Control, CodeItemType.Key_Go);
                    break;
                case "repeat":
                    word.NeedSentence = true;
                    word.JoinFeature = JoinFeature.RangeOpen;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Condition, CodeItemType.Key_Repeat);
                    break;
                case "until":
                    word.JoinFeature = JoinFeature.RangeClose;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Condition, CodeItemType.Key_Until);
                    break;
                case "function":
                    word.NeedSentence = true;
                    word.JoinFeature = JoinFeature.RangeOpen;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Range, CodeItemType.Key_Function);
                    break;
                case "do":
                    word.NeedSentence = true;
                    word.JoinFeature = JoinFeature.RangeOpen;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Range, CodeItemType.Key_Do);
                    break;
                case "end":
                    word.JoinFeature = JoinFeature.RangeClose;
                    word.SetRace(CodeItemRace.Range, CodeItemFamily.Range, CodeItemType.Key_End);
                    break;
            }
        }

        private readonly List<string> _keyWords = new List<string>
        {
            "nil",
            "true",
            "false",
            "local",
            "return",
            "function",
            "and",
            "not",
            "or",
            "if",
            "else",
            "elseif",
            "repeat",
            "until",
            "while",
            "for",
            "in",
            "then",
            "go",
            "do",
            "end",
            "break"
        };

        #endregion

        #endregion

        #region 语法树

        /// <summary>
        /// 区域的环境保存
        /// </summary>
        internal class BlockScreen
        {
            /// <summary>
            /// 入栈时的当前块
            /// </summary>
            public AnalyzeBlock PreBlock { get; set; }

            /// <summary>
            /// 入栈后的当前块
            /// </summary>
            public AnalyzeBlock Current { get; set; }

            /// <summary>
            /// 区块结束类型要求
            /// </summary>
            public CodeItemType EndItemType { get; set; }
        }

        /// <summary>
        /// 当前的区域
        /// </summary>
        private AnalyzeBlock _curRange;

        /// <summary>
        /// 前一次加入区域的对象类型(0内容,1LUA语句,2LUA内容,3)
        /// </summary>
        private TemplateUnitType _preType;

        /// <summary>
        /// 区域栈
        /// </summary>
        private Stack<BlockScreen> _rangeStack;

        /// <summary>
        /// 当前字符所在的LUA语言级别
        /// </summary>
        private TemplateUnitType _luaType;
        /// <summary>
        /// 语法树根
        /// </summary>
        private AnalyzeBlock _root;

        /// <summary>
        /// 初始化语法树
        /// </summary>
        private void ResetTree()
        {
            _rangeStack = new Stack<BlockScreen>();
            _root = new AnalyzeBlock
            {
                Name = "root"
            };
            _root.SetRace(CodeItemRace.Range);
            var code = new AnalyzeBlock
            {
                Name = _luaType.ToString(),
                UnitType = _luaType
            };
            _preType = _luaType;
            if (_luaType == TemplateUnitType.Code)
                code.SetRace(CodeItemRace.Range, CodeItemFamily.Block);
            else
                code.SetRace(CodeItemRace.Value, CodeItemFamily.Constant, CodeItemType.Content);
            _root.Append(code);
            _curRange = code;
        }


        /// <summary>
        /// 终结发布区块
        /// </summary>
        /// <param name="parent"></param>
        private void ReleaseTree(AnalyzeBlock parent)
        {
            foreach (var block in parent.Elements.OfType<AnalyzeBlock>())
                ReleaseTree(block);
            parent.Release();
        }

        /// <summary>
        ///     加入到语法树
        /// </summary>
        /// <returns></returns>
        private void AppendCurWordTree()
        {
            var type = _curWord.UnitType;
            string name = _curWord.UnitType.ToString();
            if (_curWord.UnitType >= TemplateUnitType.SimpleValue)
            {
                switch (_curWord.ItemFamily)
                {
                    case CodeItemFamily.Rem:
                    case CodeItemFamily.Space:
                        return;
                }
                switch (_curWord.ItemType)
                {
                    case CodeItemType.Key_If:
                    case CodeItemType.Key_While:
                    case CodeItemType.Key_For:
                    case CodeItemType.Key_Foreach:
                    case CodeItemType.Key_Repeat:
                    case CodeItemType.Key_Until:
                    case CodeItemType.Key_Do:
                    case CodeItemType.Key_End:
                        _preType = type;
                        MergeRange();
                        return;
                    case CodeItemType.Key_Function:
                        _preType = type;
                        MergeRange();
                        return;
                    case CodeItemType.Brackets31:
                    case CodeItemType.Brackets21:
                    case CodeItemType.Brackets41:
                    case CodeItemType.Brackets22:
                    case CodeItemType.Brackets32:
                    case CodeItemType.Brackets42:
                        _preType = type;
                        MergeBracket();
                        return;
                }
            }
            //else if (_curWord.UnitType == TemplateUnitType.Content)
            //{
            //    if (_preType != type && _curWord.Lenght == 1 && _curWord.IsLine)
            //    {
            //        _curWord.Clear();
            //        _curWord.IsReplenish = true;
            //    }
            //}
            if (_preType != type)
            {
                var parent = _preType == TemplateUnitType.Code ? _curRange : _curRange.Parent;
                _preType = type;
                _curRange.Release();
                var block = new AnalyzeBlock
                {
                    Name = name,
                    UnitType = _curWord.UnitType,
                    IsUnit = _curWord.UnitType < TemplateUnitType.SimpleValue,
                    IsContent = _curWord.ItemType == CodeItemType.Content,
                    IsLock = _curWord.UnitType < TemplateUnitType.SimpleValue,
                    IsWord = _curWord.ItemType == CodeItemType.Content
                };
                if (_curWord.UnitType < TemplateUnitType.SimpleValue)
                    block.SetRace(_curWord.ItemRace, _curWord.ItemFamily, _curWord.ItemType);
                else
                    block.SetRace(CodeItemRace.Range);
                AppendToBlock(parent, block);
                _curRange = block;
            }
            if (_curWord.UnitType >= TemplateUnitType.SimpleValue)
                AppendToBlock(_curRange, _curWord);
            else
                _curRange.Append(_curWord);
        }

        /// <summary>
        /// 向块加入单词
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="unit"></param>
        private void AppendToBlock(AnalyzeBlock parent, AnalyzeUnitBase unit)
        {
            if (parent.ItemRace > CodeItemRace.Value && unit.ItemFamily == CodeItemFamily.Space)
            {
                return;
            }
            AnalyzeBlock block = parent;
            if (parent.IsBlock && block.Elements.Count > 0)
            {
                var analyzeBlock = block.Elements[block.Elements.Count - 1] as AnalyzeBlock;
                if (analyzeBlock != null && analyzeBlock.IsBlock)
                {
                    block = analyzeBlock;
                }
            }
            var word = unit as WordUnit;
            if (word != null)
            {
                //if (word.ItemType == CodeItemType.Separator_Semicolon && _preWord != null)
                //{
                //    _preWord.Parent.Append(word);
                //    _preWord.Parent.NeedNext = false;//语句结束
                //    return;
                //}
                if (word.NeedSentence)
                {
                    AppendSentence(block, word);
                    return;
                }
                if (word.JoinFeature == JoinFeature.TowWay || word.JoinFeature == JoinFeature.Front)
                {
                    AppendLinkWord(block, word);
                    return;
                }
            }
            AppendGeneral(block, unit);
        }

        #endregion

        #region 语句块组合

        #region 区块处理

        /// <summary>
        ///     括号块组合
        /// </summary>
        /// <returns></returns>
        private void MergeBracket()
        {
            var parent = GetParent();
            if (_curWord.ItemType == CodeItemType.Brackets31)
            {
                AppendToBlock(parent, new WordUnit('.')
                {
                    Start = _curWord.Start,
                    End = _curWord.End,
                    IsReplenish = true,
                    IsKeyWord = true,
                    PrimaryLevel = 1,
                    JoinLevel = 1,
                    JoinFeature = JoinFeature.TowWay,
                    ItemRace = CodeItemRace.Sentence,
                    ItemFamily = CodeItemFamily.Separator,
                    ItemType = CodeItemType.Separator_Dot
                });
            }
            switch (_curWord.ItemType)
            {
                case CodeItemType.Brackets21:
                    var block = PushRange(parent, _curWord.ItemType + 1, false);
                    CheckBrackets21Parent(block);
                    block.IsWord = true;
                    break;
                case CodeItemType.Brackets31:
                    block = PushRange(parent, _curWord.ItemType + 1);
                    block.SetRace(CodeItemRace.Sentence, CodeItemFamily.ValueSentence, CodeItemType.Table_Child);
                    block.IsWord = true;
                    break;
                case CodeItemType.Brackets41:
                    block = PushRange(parent, _curWord.ItemType + 1);
                    block.SetRace(CodeItemRace.Type, CodeItemFamily.TableDefault, CodeItemType.Table);
                    block.IsWord = true;
                    break;
                case CodeItemType.Brackets22:
                case CodeItemType.Brackets32:
                case CodeItemType.Brackets42:
                    PopRange();
                    break;
            }
        }

        private AnalyzeBlock GetParent()
        {
            switch (_curRange.UnitType)
            {
                case TemplateUnitType.Code:
                case TemplateUnitType.SimpleValue:
                case TemplateUnitType.Value:
                    return _curRange;
            }
            if (_curRange.Parent != _root)
                return _curRange.Parent;
            _root.Append(_curRange = new AnalyzeBlock
            {
                Name = _luaType.ToString(),
                UnitType = TemplateUnitType.Code,
                ItemRace = CodeItemRace.Range,
                ItemFamily = CodeItemFamily.Block
            });
            return _curRange;
        }

        /// <summary>
        /// 小括号组合修正
        /// </summary>
        /// <param name="block"></param>
        private void CheckBrackets21Parent(AnalyzeBlock block)
        {
            if (_preNoSapceWord == null)
            {
                AppendToBlock(_rangeStack.Peek().PreBlock, block);
                return;
            }
            switch (_preNoSapceWord.ItemType)
            {
                case CodeItemType.Variable_Global:
                case CodeItemType.Variable_Local:
                    block.ItemType = CodeItemType.CallArgument;
                    var sc = new AnalyzeBlock
                    {
                        Name = _curWord.ItemType.ToString(),
                        UnitType = _curWord.UnitType,
                        Primary = _preNoSapceWord,
                        ItemRace = CodeItemRace.Sentence,
                        ItemFamily = CodeItemFamily.Call,
                        ItemType = CodeItemType.Call
                    };
                    var par = _preNoSapceWord.Parent;
                    par.Elements.Remove(_preNoSapceWord);
                    par.Append(sc);
                    sc.Append(_preNoSapceWord);
                    sc.Append(block);
                    break;
                case CodeItemType.Call:
                    _preNoSapceWord.Parent.Append(block);
                    block.ItemType = CodeItemType.CallArgument;
                    break;
                case CodeItemType.Key_Until:
                    _preNoSapceWord.Parent.Append(block);
                    block.ItemType = CodeItemType.Condition;
                    break;
                case CodeItemType.Key_Function:
                    _preNoSapceWord.Parent.Append(block);
                    block.ItemType = CodeItemType.Argument;
                    break;
                default:
                    AppendToBlock(_rangeStack.Peek().PreBlock, block);
                    break;
            }
        }

        /// <summary>
        ///     语句块组合
        /// </summary>
        /// <returns></returns>
        private void MergeRange()
        {
            CloseErrorBrackets();

            var parent = GetParent();
            AnalyzeBlock block = null;
            switch (_curWord.ItemType)
            {
                case CodeItemType.Key_Repeat:
                    block = PushRange(parent, CodeItemType.Key_Until);
                    break;
                case CodeItemType.Key_Function:
                case CodeItemType.Key_If:
                case CodeItemType.Key_While:
                case CodeItemType.Key_For:
                case CodeItemType.Key_Foreach:
                    block = PushRange(parent, CodeItemType.Key_End);
                    break;
                case CodeItemType.Key_Do:
                    //do与for冲突,此处不做块处理,而作为语句需要处理
                    if (_curRange.ItemType == CodeItemType.Key_For || _curRange.ItemType == CodeItemType.Key_Foreach)
                    {
                        AppendToBlock(_curRange, _curWord);
                        return;
                    }
                    block = PushRange(parent, CodeItemType.Key_End);
                    break;
                case CodeItemType.Key_Until:
                case CodeItemType.Key_End:
                    PopRange();
                    return;
            }
            block?.SetRace(CodeItemRace.Range, CodeItemFamily.Range, _curWord.ItemType);
        }

        /// <summary>
        /// 生成一个新区域并入栈
        /// </summary>
        /// <param name="parent">入栈时的上级</param>
        /// <param name="end">结束类型</param>
        /// <param name="addRangeToCurRange">是否将新区域加入到当前区域</param>
        /// <returns>新区域</returns>
        private AnalyzeBlock PushRange(AnalyzeBlock parent, CodeItemType end, bool addRangeToCurRange = true)
        {
            var block = new AnalyzeBlock
            {
                Name = _curWord.ItemType.ToString(),
                UnitType = _curWord.UnitType,
                Primary = _curWord
            };
            AppendToBlock(block, _curWord);
            if (addRangeToCurRange)
                AppendToBlock(parent, block);


            _rangeStack.Push(new BlockScreen
            {
                PreBlock = GetParent(),
                Current = block,
                EndItemType = end
            });
            _curRange = block;
            return block;
        }

        /// <summary>
        /// 当前区域出栈
        /// </summary>
        private void PopRange()
        {
            if (_rangeStack.Count == 0)
            {
                _curRange.Append(_curWord); ; //无法终结,普通调用
                return;
            }
            if (_rangeStack.Peek().EndItemType != _curWord.ItemType)
            {
                if (_rangeStack.All(p => p.EndItemType != _curWord.ItemType))
                {
                    _curRange.Append(_curWord); ; //无法终结,普通调用
                    return;
                }
                //强制闭合
                do
                {
                    _rangeStack.Peek().Current.Release();
                    _rangeStack.Peek().Current.IsError = true;
                    _rangeStack.Pop();
                } while (_rangeStack.Peek().EndItemType != _curWord.ItemType);
            }
            var screen = _rangeStack.Pop();
            screen.Current.Append(_curWord);
            _curRange = screen.PreBlock;
        }

        /// <summary>
        /// 强制关闭未闭合的括号区域
        /// </summary>
        private void CloseErrorBrackets()
        {
            while (_rangeStack.Count > 0)
            {
                switch (_rangeStack.Peek().EndItemType)
                {
                    case CodeItemType.Brackets22:
                    case CodeItemType.Brackets32:
                    case CodeItemType.Brackets42:
                        _rangeStack.Peek().Current.Release();
                        _rangeStack.Peek().Current.IsError = true;
                        _rangeStack.Pop();
                        _curRange = _rangeStack.Peek().Current;
                        continue;
                }
                break;
            }
        }

        #endregion

        #region 普通字符

        /// <summary>
        /// 加入普通字符
        /// </summary>
        /// <param name="block"></param>
        /// <param name="unit"></param>
        private void AppendGeneral(AnalyzeBlock block, AnalyzeUnitBase unit)
        {
            var app = GetHungry(block);
            if (app == null)
            {
                block.Append(unit);
            }
            else
            {
                app.Append(unit);
                app.NeedNext = false;
            }
        }

        /// <summary>
        /// 查找处于饥饿状态的块
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        private static AnalyzeBlock GetHungry(AnalyzeBlock parent)
        {
            if (parent.NeedNext)
                return parent;
            if (parent.Elements.Count == 0)
                return null;
            var analyzeBlock = parent.Elements[parent.Elements.Count - 1] as AnalyzeBlock;
            if (analyzeBlock == null)
                return null;
            return GetHungry(analyzeBlock);
        }

        #endregion

        #region 语句块

        /// <summary>
        /// 加入有明显子级语句的关键字
        /// </summary>
        /// <param name="block"></param>
        /// <param name="word"></param>
        /// <remarks>
        /// 需要下级语句块的
        /// for in do then go while if elseif else repeat function 
        /// </remarks>
        private void AppendSentence(AnalyzeBlock block, WordUnit word)
        {
            //要强制关闭未闭合的括号区域
            CloseErrorBrackets();
            string name = "Block";
            switch (_curWord.ItemType)
            {
                case CodeItemType.Key_In:
                    if (_rangeStack.Count > 0 && _rangeStack.Peek().Current.ItemType == CodeItemType.Key_For)
                    {
                        _rangeStack.Peek().Current.ItemType = CodeItemType.Key_Foreach;
                        block = (AnalyzeBlock)_curRange.Elements[1];
                        block.Append(word);
                        block.SetRace(_curWord.ItemRace, _curWord.ItemFamily, _curWord.ItemType);
                        block.JoinLevel = 32;
                        block.NeedNext = true;
                    }
                    return;
                case CodeItemType.Key_If:
                case CodeItemType.Key_While:
                case CodeItemType.Key_Elseif:
                    name = "Condition";
                    break;
                case CodeItemType.Key_For:
                    name = "Iterator";
                    break;
                case CodeItemType.Key_Foreach:
                    name = "IteratorChild";
                    break;
                case CodeItemType.Key_Function:
                    name = "Function";
                    break;
            }
            switch (_curWord.ItemType)
            {
                case CodeItemType.Key_If:
                case CodeItemType.Key_While:
                    block.IsBlock = true;
                    block.Append(word);
                    block.Append(new AnalyzeBlock
                    {
                        IsBlock = true,
                        Name = name,
                        UnitType = _curWord.UnitType,
                        ItemRace = CodeItemRace.Range,
                        ItemFamily = CodeItemFamily.ConditionRange
                    });
                    return;
                case CodeItemType.Key_Elseif:
                    //要回退到当前栈顶
                    if (_rangeStack.Count > 0)
                        block = _rangeStack.Peek().Current;
                    block.Append(word);
                    block.Append(new AnalyzeBlock
                    {
                        IsBlock = true,
                        Name = name,
                        UnitType = _curWord.UnitType,
                        ItemRace = CodeItemRace.Range,
                        ItemFamily = CodeItemFamily.ConditionRange
                    });
                    return;
                case CodeItemType.Key_Repeat:
                    block.IsBlock = true;
                    break;
                case CodeItemType.Key_For:
                case CodeItemType.Key_Foreach:
                    block.IsBlock = true;
                    block.Append(word);
                    block.Append(new AnalyzeBlock
                    {
                        IsBlock = true,
                        Name = name,
                        UnitType = _curWord.UnitType,
                        ItemRace = CodeItemRace.Range,
                        ItemFamily = CodeItemFamily.IteratorRange
                    });
                    return;
                case CodeItemType.Key_Function:
                    block.IsBlock = true;
                    block.Append(word);
                    block.Append(new AnalyzeBlock
                    {
                        IsBlock = true,
                        Name = name,
                        UnitType = _curWord.UnitType,
                        ItemRace = CodeItemRace.Range,
                        ItemFamily = CodeItemFamily.FunctionRange
                    });
                    return;
                case CodeItemType.Key_Do:
                    if (_rangeStack.Count > 0 && (
                            _rangeStack.Peek().Current.ItemType == CodeItemType.Key_For ||
                            _rangeStack.Peek().Current.ItemType == CodeItemType.Key_Foreach))
                        break;
                    block.Append(word);
                    block.Append(new AnalyzeBlock
                    {
                        IsBlock = true,
                        Name = name,
                        UnitType = _curWord.UnitType,
                        ItemRace = CodeItemRace.Range,
                        ItemFamily = CodeItemFamily.Block
                    });
                    return;
            }
            //要回退到当前栈顶
            if (_rangeStack.Count > 0)
                block = _rangeStack.Peek().Current;
            block.Append(word);
            block.Append(new AnalyzeBlock
            {
                IsBlock = true,
                Name = name,
                UnitType = _curWord.UnitType,
                ItemRace = CodeItemRace.Range,
                ItemFamily = CodeItemFamily.Block
            });
        }

        #endregion

        #region 连接关键字

        /// <summary>
        /// 连接关键字的连接处理
        /// </summary>
        /// <param name="block"></param>
        /// <param name="word"></param>
        /// <remarks>
        /// 连接符等级:
        /// 1 . :
        /// 2 @ # not 
        /// 3 ^
        /// 4 * /  %
        /// 5 + -
        /// 6 〈 〉 〈= 〉= == ~=
        /// 7 and 
        /// 8 or
        /// 9 ..
        /// 10 ,(在大括号区域可变)
        /// 11 local
        /// 12 =
        /// </remarks>
        private void AppendLinkWord(AnalyzeBlock block, WordUnit word)
        {
            //在表定义中,逗号的级别最高
            if (word.ItemType == CodeItemType.Separator_Comma &&
                _curRange.Primary != null && _curRange.Primary.ItemType == CodeItemType.Brackets41)
            {
                word.JoinLevel = 32;
            }
            AppendLevelLinkWord(block, word);
        }

        /// <summary>
        /// 加入分级连接的关键字
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="word"></param>
        private void AppendLevelLinkWord(AnalyzeBlock parent, WordUnit word)
        {
            if (parent.Elements.Count == 0)
            {
                //第一次构造
                var block = new AnalyzeBlock
                {
                    Primary = word,
                    UnitType = word.UnitType,
                    NeedNext = true,
                    JoinLevel = word.JoinLevel
                };
                block.SetRace(word.ItemRace, word.ItemFamily, word.ItemType);
                block.Append(word);
                parent.Elements.Add(block);
                parent.NeedNext = false;
                return;
            }
            FindAndAppendLowLevel(parent, parent.Elements[parent.Elements.Count - 1], word);
        }

        /// <summary>
        /// 查找最小等级并加入
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="current"></param>
        /// <param name="word"></param>
        private void FindAndAppendLowLevel(AnalyzeBlock parent, AnalyzeUnitBase current, WordUnit word)
        {
            if (current.IsWord)
            {
                //第一个级别比自己低的做子级(单词级别最低)
                var newBlock = new AnalyzeBlock
                {
                    Primary = word,
                    UnitType = word.UnitType,
                    NeedNext = true,
                    JoinLevel = word.JoinLevel
                };
                newBlock.SetRace(word.ItemRace, word.ItemFamily, word.ItemType);
                if (word.JoinFeature == JoinFeature.TowWay && current.JoinLevel < 0)
                    newBlock.Append(current);
                newBlock.Append(word);
                if (word.JoinFeature == JoinFeature.TowWay && current.JoinLevel < 0)
                {
                    parent.Elements[parent.Elements.Count - 1] = newBlock;
                }
                else
                {
                    parent.Append(newBlock);
                    parent.NeedNext = false;
                }
                return;
            }
            var block = (AnalyzeBlock)current;
            if (block.JoinLevel == word.JoinLevel)
            {
                //同级别的加入并设置饥饿状态
                block.Append(word);
                block.NeedNext = true;
                return;
            }
            if (block.JoinLevel < word.JoinLevel)
            {
                //第一个级别比自己低的做子级
                var newBlock = new AnalyzeBlock
                {
                    Primary = word,
                    NeedNext = true,
                    UnitType = word.UnitType,
                    JoinLevel = word.JoinLevel
                };
                newBlock.SetRace(word.ItemRace, word.ItemFamily, word.ItemType);
                if (word.JoinFeature == JoinFeature.TowWay)
                {
                    newBlock.Append(block);
                    newBlock.Append(word);
                    parent.Elements[parent.Elements.Count - 1] = newBlock;
                }
                else
                {
                    newBlock.Append(word);
                    parent.Append(newBlock);
                }
            }
            else
            {
                Debug.Assert(block.Elements.Count > 0);
                FindAndAppendLowLevel(block, block.Elements[block.Elements.Count - 1], word);
            }
        }

        #endregion

        #endregion
    }
}