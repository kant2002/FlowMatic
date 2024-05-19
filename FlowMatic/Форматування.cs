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
        текст = текст.ReplaceLineEndings("");
        var chunkSize = 12;
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

        if (текст.Length % chunkSize != 0)
        {
            var i = 1 + (текст.Length / chunkSize);
            var chunk = текст.Substring((текст.Length / chunkSize) * chunkSize);
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
