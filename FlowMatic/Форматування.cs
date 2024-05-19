using FlowMatic.Юнівак;

namespace FlowMatic;

public class Форматування
{
    public static string Переформатувати(ДізайнФайлу файл) => Переформатувати(файл.ToString());
    public static string Переформатувати(РобочаОбласть робочаОбласть) => Переформатувати(робочаОбласть.ToString());
    public static string Переформатувати(Діректорія діректорія) => Переформатувати(діректорія.ToString());

    public static string Переформатувати(string текст)
    {
        System.Text.StringBuilder результат = new();
        var chunkSize = 12 + Environment.NewLine.Length;
        for (var i = 0; i < текст.Length / chunkSize; i++)
        {
            var chunk = текст.Substring(i * chunkSize, 12);
            результат.Append(chunk);
            if (i % 10 == 9)
            {
                результат.Append(Environment.NewLine);
            }
            else
            {
                результат.Append(' ');
            }
        }

        return результат.ToString();
    }
}
