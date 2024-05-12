using System.Diagnostics;

namespace FlowMatic;

public class Діректорія
{
    public ushort МаксімальнийАдрес;

    public override string ToString()
    {
        var результат = $@"DIRECTORY   
            
00W00000W{МаксімальнийАдрес:D3}
W-STORAGE   
END DIRECTRY
".ReplaceLineEndings();
        var довжинаРядка = 12 + Environment.NewLine.Length;
        Debug.Assert(результат.Length % довжинаРядка == 0);
        var кількістьСлів = (результат.Length / довжинаРядка) % 60;
        if (кількістьСлів == 0)
        {
            кількістьСлів = 59;
        }
        else
        {
            кількістьСлів = 59 - кількістьСлів;
        }

        результат += string.Join("", Enumerable.Range(0, кількістьСлів).Select(_ => "000000000000" + Environment.NewLine));
        результат += "END DIRECTRY" + Environment.NewLine;
        Debug.Assert(результат.Length % довжинаРядка == 0);
        Debug.Assert((результат.Length / довжинаРядка) % 60 == 0);
        return результат;
    }
}
