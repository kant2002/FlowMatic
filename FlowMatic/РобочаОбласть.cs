using System.Diagnostics;

namespace FlowMatic;

public class РобочаОбласть
{

    public ДізайнЕлементів ДізайнЕлементів;

    public ДізайнПолей? ДізайнПолей;

    public override string ToString()
    {
        var результат = $@"NAME OF FILE
W-STQRAGE   ".ReplaceLineEndings();
        if (ДізайнЕлементів is not null)
        {
            результат += Environment.NewLine + ДізайнЕлементів.ToString();
        }

        if (ДізайнПолей is not null)
        {
            результат += Environment.NewLine + ДізайнПолей.ToString();
        }

        var довжинаРядка = 12 + Environment.NewLine.Length;
        Debug.Assert(результат.Length % довжинаРядка == 0);
        var кількістьСлів = (результат.Length / довжинаРядка) % 60;
        if (кількістьСлів == 0)
        {
            кількістьСлів = 60;
        }
        else
        {
            кількістьСлів = 59 - кількістьСлів;
        }

        результат += string.Join("", Enumerable.Range(0, кількістьСлів).Select(_ => "000000000000" + Environment.NewLine));
        результат += "END FILE DES" + Environment.NewLine;
        Debug.Assert(результат.Length % довжинаРядка == 0);
        Debug.Assert((результат.Length / довжинаРядка) % 60 == 0);
        return результат;
    }
}
