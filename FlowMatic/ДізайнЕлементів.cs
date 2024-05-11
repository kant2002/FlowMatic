using System.Diagnostics;

namespace FlowMatic;

public class ДізайнЕлементів
{
    public ushort РозмірЕлемента;

    public string[] Ключи;

    public override string ToString()
    {
        var ключи = string.Join("", Ключи.Select((значение, індекс) => $@"KEY {індекс,1}       
{значение,-12}
".ReplaceLineEndings()));
        var результат = ($@"ITEM DESIGN 
            
ITEM SIZE   
         {РозмірЕлемента:D3}
NO OF KEYS  
           {Ключи.Length,1}
" + ключи).ReplaceLineEndings();
        var довжинаРядка = 12 + Environment.NewLine.Length;
        Debug.Assert(результат.Length % довжинаРядка == 0);
        return результат;
    }
}
