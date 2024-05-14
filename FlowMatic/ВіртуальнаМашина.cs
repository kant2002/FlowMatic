using FlowMatic.Операції;
using FlowMatic.Юнівак;

namespace FlowMatic;

public class ВіртуальнаМашина
{
    private СервоПривід[] СервоПривіди = new СервоПривід[16];

    private Dictionary<char, int> СервоПривідиСловник = new Dictionary<char, int>();
    private Dictionary<char, string> ФайловіБуфери = new Dictionary<char, string>();

    public СервоПривід ВзятиСервоПривід(char файл)
    {
        return СервоПривіди[СервоПривідиСловник[файл]];
    }

    private int позиція;
    public void ВиконатиПрограму(ЛентаВводу лентаВводу)
    {
        позиція = 0;
        var компілятор = new Компілятор();
        var програма = компілятор.Скомпілювати(лентаВводу.ІсходнийКод);
        while (true)
        {
            var поточнаКоманда = програма.Операції[позиція];
        }
    }
}
