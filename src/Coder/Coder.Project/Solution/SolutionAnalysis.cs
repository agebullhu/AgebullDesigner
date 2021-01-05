using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Agebull.Common;
using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;

namespace Agebull.EntityModel.RobotCoder
{
    public class SolutionAnalysis
    {
        string FileName => Path.Combine(SolutionConfig.Current.RootPath, GlobalConfig.LocalSolution.SrcFolder, GlobalConfig.LocalSolution.Name + ".sln");

        /// <summary>
        /// 生成文件
        /// </summary>
        public void SyncSolutionFile()
        {
            using (WorkModelScope.CreateScope(WorkModel.Coder))
            {
                IOHelper.CheckPath(SolutionConfig.Current.RootPath, SolutionConfig.Current.SrcFolder);
                IOHelper.CheckPath(SolutionConfig.Current.RootPath, SolutionConfig.Current.DocFolder);
                IOHelper.CheckPath(SolutionConfig.Current.RootPath, "sql");
                if (!File.Exists(FileName))
                    File.WriteAllText(FileName, Create());
                CheckPrjectFile();
                CreateDomain();
            }
        }

        #region 全新生成文件

        public string Create()
        {
            var code = new StringBuilder();

            code.Append(fileHead);
            //项目声明
            code.Append($@"
Project(""{projectDefaultKey}"") = ""{GlobalConfig.LocalSolution.Name}.Domain"", ""Domain\{GlobalConfig.LocalSolution.Name}.Domain.csproj"", ""{{{GlobalConfig.LocalSolution.Key.ToUpper()}}}""
EndProject");
            foreach (var prj in GlobalConfig.LocalSolution.Projects)
            {
                code.Append($@"
Project(""{folderDefaultKey}"") = ""{prj.Name}"", ""{prj.Name}"", ""{{{prj.Key.ToUpper()}}}""
EndProject
Project(""{projectDefaultKey}"") = ""{prj.Name}.Model"", ""{prj.Name}\Model\{prj.Name}.Model.csproj"", ""{{{prj.ModelKey.ToString().ToUpper()}}}""
EndProject
Project(""{projectDefaultKey}"") = ""{prj.Name}.Api"", ""{prj.Name}\Api\{prj.Name}.Api.csproj"", ""{{{prj.ApiKey.ToString().ToUpper()}}}""
EndProject");
            }
            //项目平台配置
            code.Append(globalHead);
            foreach (var prj in GlobalConfig.LocalSolution.Projects)
            {
                code.Append($@"
		{{{prj.ModelKey.ToString().ToUpper()}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{{prj.ModelKey.ToString().ToUpper()}}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{{prj.ModelKey.ToString().ToUpper()}}}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{{{prj.ModelKey.ToString().ToUpper()}}}.Release|Any CPU.Build.0 = Release|Any CPU
		{{{prj.ApiKey.ToString().ToUpper()}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{{prj.ApiKey.ToString().ToUpper()}}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{{prj.ApiKey.ToString().ToUpper()}}}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{{{prj.ApiKey.ToString().ToUpper()}}}.Release|Any CPU.Build.0 = Release|Any CPU");
            }
            code.Append(endGlobalSection);
            //文件夹包含
            code.AppendFormat(preGlobalSection, "0");
            foreach (var prj in GlobalConfig.LocalSolution.Projects)
            {
                code.Append($@"
		{{{prj.ModelKey.ToString().ToUpper()}}} = {{{prj.Key.ToUpper()}}}
		{{{prj.ApiKey.ToString().ToUpper()}}} = {{{prj.Key.ToUpper()}}}");
            }
            code.Append(endGlobalSection);
            code.AppendFormat(preGlobalSection, "NestedProjects");
            foreach (var prj in GlobalConfig.LocalSolution.Projects)
            {
                code.Append($@"
		{{{prj.ModelKey.ToString().ToUpper()}}} = {{{prj.Key.ToUpper()}}}
		{{{prj.ApiKey.ToString().ToUpper()}}} = {{{prj.Key.ToUpper()}}}");
            }
            code.Append(endGlobalSection);
            code.Append(endGlobal);
            return code.ToString();
        }


        #region const
        const string fileHead = @"Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 16
VisualStudioVersion = 16.0.30011.22
MinimumVisualStudioVersion = 10.0.40219.1";

        const string globalHead = @"
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution";
        static string preGlobalSection = @"
    GlobalSection({0}) = preSolution";

        const string endGlobal = @"
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {EB44A344-9DB9-4820-B192-FA744EF956ED}
	EndGlobalSection
EndGlobal";
        const string endGlobalSection = @"
    EndGlobalSection";

        const string projectDefaultKey = "{9A19103F-16F7-4668-BE54-9A1E7A4F7556}";
        const string folderDefaultKey = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}";
        #endregion

        #endregion

        #region 文件解析

        public bool Read()
        {
            if (!File.Exists(FileName))
                return false;
            var lineWords = ReadContent();
            for (int i = 0; i < lineWords.Count; i++)
            {
                var line = lineWords[i];
                switch (line[0].ToLower())
                {
                    case "project":
                        i = ReadProject(lineWords, i);
                        break;
                    case "global":
                        i = ReadGlobal(lineWords, i);
                        break;
                }
            }
            return true;
        }

        int ReadProject(List<SolutionLine> lineWords, int idx)
        {
            var line = lineWords[idx];
            switch (line[1])
            {
                case projectDefaultKey:
                    break;
                default:
                    return ToProjectEnd(lineWords, idx);
            }
            var guid = Guid.Parse(line[5]);
            //var prj = GlobalConfig.LocalSolution.Projects.FirstOrDefault(p=>p.);
            return ToProjectEnd(lineWords, idx);
        }
        int ToProjectEnd(List<SolutionLine> lineWords, int i)
        {
            for (i++; i < lineWords.Count; i++)
            {
                var line = lineWords[i];
                switch (line[0].ToLower())
                {
                    case "endproject":
                        return i;
                }
            }
            return i;
        }
        int ReadGlobal(List<SolutionLine> lineWords, int idx)
        {


            return idx;
        }

        List<SolutionLine> ReadContent()
        {
            var lines = File.ReadAllLines(FileName);

            var lineWrods = new List<SolutionLine>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                lineWrods.Add(SplitWords(line));
            }
            return lineWrods;
        }

        #endregion

        #region 单词解析

        /// <summary>
        /// 拆分到单词
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static SolutionLine SplitWords(string str)
        {
            var line = new SolutionLine
            {
                Line = str,
                NewLine = str,
                Words = new List<SolutionWord>()
            };
            var wordBuilder = new StringBuilder();
            void AddWord(string type = "word", bool check = true)
            {
                if (!check || wordBuilder.Length > 0)
                {
                    var word = new SolutionWord
                    {
                        Type = type,
                        Word = wordBuilder.ToString()
                    };
                    if (type == "word" && word.Word.PadRight(7).Equals(".csproj", StringComparison.OrdinalIgnoreCase))
                    {
                        word.Type = "prj";
                    }
                    line.Words.Add(word);
                }
                wordBuilder.Clear();
            }
            bool inQuote = false;
            bool inGuid = false;
            foreach (var ch in str)
            {
                if (inQuote)
                {
                    if (ch == '\"')
                    {
                        AddWord("name", false);
                        inQuote = false;
                        break;
                    }
                    wordBuilder.Append(ch);
                    break;
                }
                if (inGuid)
                {
                    wordBuilder.Append(ch);
                    if (ch == '}')
                    {
                        AddWord("guid", false);
                        inGuid = false;
                    }
                    break;
                }
                if (ch == '\"')
                {
                    AddWord();
                    inQuote = true;
                    break;
                }
                if (ch == '{')
                {
                    AddWord();
                    inGuid = true;
                    break;
                }
                if (ch == '=')
                {
                    AddWord();
                    line.Words.Add(new SolutionWord
                    {
                        Type = "=",
                        Word = "="
                    });
                    break;
                }
                if (char.IsWhiteSpace(ch))
                {
                    AddWord();
                    break;
                }
                if (char.IsLetter(ch))
                {
                    AddWord();
                    break;
                }
                wordBuilder.Append(ch);
            }
            AddWord();
            return line;
        }

        #endregion

        #region 项目文件

        void CheckPrjectFile()
        {
            foreach (var prj in SolutionConfig.Current.Projects)
            {
                var model = Path.Combine(prj.ModelPath, prj.Name + ".Model.csproj");
                if (!File.Exists(model))
                {
                    File.WriteAllText(model, ModelProjectContent(prj.Name));
                }
                var api = Path.Combine(prj.ApiPath, prj.Name + ".Api.csproj");
                if (!File.Exists(api))
                {
                    File.WriteAllText(api, ApiProjectContent(prj.ModelFolder, prj.Name));
                }
            }
        }

        void CreateDomain()
        {
            var path = IOHelper.CheckPath(SolutionConfig.Current.RootPath, SolutionConfig.Current.SrcFolder, "Domain");
            Path.Combine(GlobalConfig.RootPath, "Templates", "Domain", "OperatorInjection.cs").FileCopyTo( Path.Combine(path, "OperatorInjection.cs"));
            var prjFile = Path.Combine(path, SolutionConfig.Current.Name + ".Domain.csproj");
            if (!File.Exists(prjFile))
            {
                var projectReference = new StringBuilder();
                foreach (var prj in SolutionConfig.Current.Projects)
                {
                    projectReference.Append($@"
    <ProjectReference Include=""..\{prj.Name}\Api\{prj.Name}.Api.csproj""/>");
                }
                var tmpFile = Path.Combine(GlobalConfig.RootPath, "Templates", "Domain", "Domain.csproj");
                var text = File.ReadAllText(tmpFile);
                File.WriteAllText(prjFile, text.Replace("@ProjectReference", projectReference.ToString()));
            }
            var proFile = Path.Combine(path, "Program.cs");
            if (!File.Exists(proFile))
            {
                var tmpFile = Path.Combine(GlobalConfig.RootPath, "Templates", "Domain", "Program.cs");
                var text = File.ReadAllText(tmpFile);
                File.WriteAllText(proFile, text.Replace("@namespace", SolutionConfig.Current.NameSpace));
            }
            
           var startFile = Path.Combine(path, "Startup.cs");
            if (!File.Exists(startFile))
            {
                var tmpFile = Path.Combine(GlobalConfig.RootPath, "Templates", "Domain", "Startup.cs");
                var text = new StringBuilder();
                text.Append(File.ReadAllText(tmpFile));
                var reg = new StringBuilder();

                foreach (var prj in SolutionConfig.Current.Projects)
                {
                    reg.Append($@"
    services.AddScoped<{prj.DataBaseObjectName}>();");

                }
                text.Replace("@namespace", SolutionConfig.Current.NameSpace);
                text.Replace("@dbRegist",reg.ToString());
                File.WriteAllText(startFile, text.ToString());
            }
            var cfgFile = Path.Combine(path, "appsettings.json");
            if (!File.Exists(cfgFile))
            {
                var code = new StringBuilder();
                var tmpFile = Path.Combine(GlobalConfig.RootPath, "Templates", "Domain", "appsettings.json");
                code.Append(File.ReadAllText(tmpFile));

                var connections = new StringBuilder();
                bool first = true;
                foreach (var prj in SolutionConfig.Current.Projects)
                {
                    if (first)
                    {
                        first = false;
                        code.Replace("@ApiServiceName", prj.ApiName);
                        code.Replace("@DbNameSpace", $"{prj.NameSpace}.DataAccess.{prj.DataBaseObjectName}");
                    }
                    else connections.Append(',');

                    connections.Append($@"
    ""{prj.DataBaseObjectName}"": ""Database={prj.DbSoruce}; Data Source={prj.DbHost}; SslMode=none; User Id={prj.DbUser}; Password={prj.DbPassWord}; CharSet=utf8mb4; port=3306; Compress=false; Pooling=true; Min Pool Size=0; Max Pool Size=500; Connection Lifetime=0;""");

                }
                code.Replace("@ConnectionStrings", connections.ToString());

                File.WriteAllText(cfgFile, code.ToString());
            }
        }

        string ModelProjectContent(string name) => $@"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <OutputType>Library</OutputType>
    <TargetFramework>netcoreapp3.1</TargetFramework>
  </PropertyGroup>

  <PropertyGroup Condition = ""'$(Configuration)|$(Platform)'=='Release|AnyCPU'"" >
    <DocumentationFile> {name}.Model.xml </DocumentationFile>
    <DefineConstants> TRACE; monitor,StandardPropertyChanged</DefineConstants>
  </PropertyGroup>

  <PropertyGroup Condition = ""'$(Configuration)|$(Platform)'=='Debug|AnyCPU'"" >
    <DocumentationFile> {name}.Model.xml </DocumentationFile>
    <DefineConstants> TRACE; monitor,StandardPropertyChanged</DefineConstants>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include = ""CSRedisCore"" Version=""3.6.5.2"" />
    <PackageReference Include = ""ZeroTeam.MessageMVC.Abstractions"" Version=""1.0.1.110"" />
    <PackageReference Include = ""Agebull.EntityModel.MySql"" Version=""5.0.0.104"" />
    <PackageReference Include = ""Agebull.EntityModel.BusinessLogic"" Version=""5.0.0.104"" />
    <PackageReference Include=""Agebull.EntityModel.Injection"" Version=""5.0.0.104"" />
  </ItemGroup>
</Project>";
        string ApiProjectContent(string prjModel, string name) => $@"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <OutputType>Library</OutputType>
    <TargetFramework>netcoreapp3.1</TargetFramework>
  </PropertyGroup>

  <PropertyGroup Condition = ""'$(Configuration)|$(Platform)'=='Release|AnyCPU'"" >
    <DocumentationFile> {name}.Api.xml </DocumentationFile>
    <DefineConstants> TRACE; monitor,StandardPropertyChanged</DefineConstants>
  </PropertyGroup>

  <PropertyGroup Condition = ""'$(Configuration)|$(Platform)'=='Debug|AnyCPU'"" >
    <DocumentationFile> {name}.Api.xml </DocumentationFile>
    <DefineConstants> TRACE; monitor,StandardPropertyChanged</DefineConstants>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include = ""ZeroTeam.MessageMVC.ModelApi"" Version=""5.0.0.104"" />
    <PackageReference Include = ""ZeroTeam.MessageMVC.Http"" Version=""1.0.1.110"" />
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include = ""..\{prjModel}\Model\{name}.Model.csproj"" />
  </ItemGroup >
</Project>";
        #endregion
    }
}