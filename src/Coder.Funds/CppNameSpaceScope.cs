using System.Text;
using Agebull.Common.Base;

namespace Agebull.EntityModel.RobotCoder
{
    public class CppNameSpaceScope : ScopeBase
    {
        private StringBuilder _coder;
        private string[] _namespace;
        public static CppNameSpaceScope CreateScope(StringBuilder code, string nameSpace)
        {
            var scope = new CppNameSpaceScope
            {
                _coder = code,
                _namespace = nameSpace?.Split('.')
            };
            scope.Begin();
            return scope;
        }

        private int sapce;
        private string sapceCode="";
        private int len;

        private void Begin()
        {
            if (_namespace == null)
                return;
            len = _namespace.Length;
            foreach (var name in _namespace)
            {
                _coder.AppendLine();
                _coder.Append(' ', sapce);
                _coder.Append($@"namespace {name}");
                _coder.AppendLine();
                _coder.Append(' ', sapce);
                _coder.Append('{');
                sapce += 4;
            }
            _coder.AppendLine();
            StringBuilder sb = new StringBuilder();
            sb.Append(' ', sapce);
            sapceCode = sb.ToString();
        }

        public void Append(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return;
            _coder.AppendLine(_namespace == null ? code : code.Replace("\n", "\n" + sapceCode));
        }

        private void End()
        {
            if (_namespace == null)
                return;
            for (int index = 0; index < len; index++)
            {
                sapce -= 4;
                _coder.AppendLine();
                _coder.Append(' ', sapce);
                _coder.Append('}');
            }
        }
        protected override void OnDispose()
        {
            End();
        }
    }
}
