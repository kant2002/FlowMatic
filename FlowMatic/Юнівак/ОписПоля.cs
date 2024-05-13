using System.Diagnostics;

namespace FlowMatic.Юнівак;

public class ОписПоля
{
    public required string Назва;
    public required ushort Місце;
    public required ТипПоля Тип;
    public required string ПозіціяТочки;
    public required byte? ПозіціяЗнака;
    public required byte ПозіціяНайлівішогоСимвола;
    public required byte КількістьСимволів;
    public string Витягач = "000000000000";

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
00000{типПоля}{ПозіціяТочки,-2}{ДоБази12(ПозіціяЗнака)}{ДоБази12(ПозіціяНайлівішогоСимвола)}{ДоБази12(КількістьСимволів)}0
{Витягач,-12}
".ReplaceLineEndings();
        var довжинаРядка = 12 + Environment.NewLine.Length;
        Debug.Assert(результат.Length % довжинаРядка == 0);
        return результат;
    }

    static char ДоБази12(byte? значення)
    {
        if (!значення.HasValue) return ' ';

        return значення.Value switch
        {
            > 0 and < 9 => (char)('0' + значення),
            10 => 'A',
            11 => 'B',
            12 => 'C',
        };
    }
}
