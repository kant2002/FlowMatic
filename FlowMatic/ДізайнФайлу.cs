using System.Diagnostics;

namespace FlowMatic;

public class ДізайнФайлу
{
    public required string НазваФайлу;
    public string Мітка = "";
    public ushort МісцеМітки;
    public bool БагатоБобінний;
    public bool ІндікаторКількостіБлоків;
    public ushort МісцеКількостіБлоків;
    public string МаркерКінцяБобіни = "ZZZZZZZZY";
    public string МаркерКінцяФайла = "ZZZZZZZZZ";
    public ushort МісцеПершого;
    public ushort МісцеОстаннього;

    public ДізайнЕлементів ДізайнЕлементів;

    public ДізайнПолей ДізайнПолей;

    public override string ToString()
    {
        var результат = $@"NAME OF FILE
{НазваФайлу,-12}
FILE DESIGN 
            
LABEL       
{Мітка,12}
LOC OF LABEL
         {МісцеМітки:D3}
MULTI REEL  
           {(БагатоБобінний ? "1" : "0")}
BLK CT IND  
           {(ІндікаторКількостіБлоків ? "1" : "0")}
BLK CT LOC  
         {МісцеКількостіБлоків:D3}
END REEL SEN
{МаркерКінцяБобіни,-12}
END FILE SEN
{МаркерКінцяФайла,-12}
LOC IN FIRST
         {МісцеПершого:D3}
LOC IN LAST 
         {МісцеОстаннього:D3}
{ДізайнЕлементів}{ДізайнПолей}".ReplaceLineEndings();
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
