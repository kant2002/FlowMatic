namespace FlowMatic.Операції;

public class Input : ІОперація
{
    public ОписФайла[] ВхідніФайли;
    public ОписФайла[] ВихідніФайли;
    public bool ПопереднійВибір;
    public char[] ВисокошвидкосніПрінтери;
    public char[] КонвертериЛенти;
    public char? Перезапуск;

    public record ОписФайла(string НазваФайла, char КодФайла, char[] Сервоприводи);
}
