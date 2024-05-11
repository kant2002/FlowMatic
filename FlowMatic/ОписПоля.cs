using System.Diagnostics;

namespace FlowMatic;

public class ОписПоля
{
    public required string Назва;
    public ushort Місце;
    public ТипПоля Тип;

    public override string ToString()
    {
        var типПоля = Тип switch
        {
            ТипПоля.Літери => '1',
            ТипПоля.ЧислаТаЛітери => '2',
            ТипПоля.Числовий => '3',
        };
        var результат = $@"{Назва,-12}
000{Місце:D3}000000
00000{типПоля}xxxxx0
            
".ReplaceLineEndings();
        var довжинаРядка = 12 + Environment.NewLine.Length;
        Debug.Assert(результат.Length % довжинаРядка == 0);
        return результат;
    }
}
