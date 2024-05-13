using System.Diagnostics;

namespace FlowMatic.Юнівак;

public partial class ДізайнПолей
{
    public ОписПоля[] Поля;

    public override string ToString()
    {
        var результат = $@"FIELD DESIGN
            
{string.Join("", Поля.Select(_ => _.ToString()))}END FILE DES
000000000000
".ReplaceLineEndings();
        var довжинаРядка = 12 + Environment.NewLine.Length;
        Debug.Assert(результат.Length % довжинаРядка == 0);
        return результат;
    }
}
