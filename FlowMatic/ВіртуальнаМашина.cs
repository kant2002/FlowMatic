using FlowMatic.Операції;
using FlowMatic.Юнівак;
using Система.ВВ;
using static FlowMatic.Операції.Input;

namespace FlowMatic;

public class ВіртуальнаМашина
{
    public СервоПривід[] СервоПривіди { get; private set; } = new СервоПривід[16];
    public int КількістьПриводів { get; private set; }
    public int КількістьФайлів => СервоПривідиСловник.Count;
    public IEnumerable<РезерваціяПривіда> РезерваціяПривідів => СервоПривідиСловник.Values;
    private Dictionary<char, РезерваціяПривіда> СервоПривідиСловник = new ();
    private Dictionary<char, string> ФайловіБуфери = new ();

    public СервоПривід ВзятиСервоПривід(char файл)
    {
        return СервоПривідиСловник[файл].ПоточнийПривід;
    }

    public bool ФлагОстанова {  get; private set; }
    public string Ошибка { get; private set; }
    private Програма програма;
    public ЛентаВводу ЛентаВводу { get; private set; }
    public int Позиція { get; private set; }

    public void СкомпілюватиПрограму(ЛентаВводу лентаВводу)
    {
        var компілятор = new Компілятор();
        програма = компілятор.Скомпілювати(лентаВводу.ІсходнийКод);
        this.ЛентаВводу = лентаВводу;
        ПідготуватиВхід((Input)програма.Операції[0]);
        Позиція = 0;

    }
    public void ВиконатиПрограму()
    {
        Ошибка = "";
        Позиція = 0;
        while (true)
        {
            ВиконатиРядокПрограми();
            if (ФлагОстанова)
                break;
        }
    }
    public void ВиконатиРядокПрограми()
    {
        Ошибка = "";
        ФлагОстанова = false;
        var поточнаКоманда = програма.Операції[Позиція];
        switch (поточнаКоманда)
        {
            case Input input:
                ВиконатиInput(input);
                break;
            case Compare compare:
                ВиконатиCompare(compare);
                break;
            case Jump jump:
                ВиконатиJump(jump);
                break;
            case Move move:
                ВиконатиMove(move);
                break;
            case ReadItem readItem:
                ВиконатиReadItem(readItem);
                break;
            case SetOperation setOperation:
                ВиконатиSetOperation(setOperation);
                break;
            case Stop:
                ФлагОстанова = true;
                return;
            case Test jump:
                ВиконатиTest(jump);
                break;
            case Transfer transfer:
                ВиконатиTransfer(transfer);
                break;
            case WriteItem writeItem:
                ВиконатиWriteItem(writeItem);
                break;
            default:
                throw new NotSupportedException($"Not supported command {поточнаКоманда} with index {Позиція}");
        }
    }

    private void ВиконатиInput(Input input)
    {
        foreach (var описФайла in input.ВхідніФайли)
        {
            var ф = ВзятиФайл(описФайла);
            var серво = ВзятиСервоПривід(описФайла.КодФайла);
            if (!серво.ЛентаВставлена)
            {
                Ошибка = "Лента для приводу із кодом " + описФайла.КодФайла + " не визначена";
                ФлагОстанова = true;
                break;
            }

            var елемент = серво.Прочитати(ф.ДізайнЕлементів.РозмірЕлемента * 12);
            ФайловіБуфери[описФайла.КодФайла] = елемент;
        }

        if (!ФлагОстанова)
        {
            Позиція++;
        }
    }

    private ДізайнФайлу ВзятиФайл(ОписФайла описФайла)
    {
        return ЛентаВводу.Файли.FirstOrDefault(_ => _.НазваФайлу == описФайла.НазваФайла) ?? throw new InvalidOperationException($"File {описФайла.НазваФайла} does not exists. Probably compilation was wrong."); ;
    }

    private void ПідготуватиВхід(Input input)
    {
        СервоПривідиСловник.Clear();
        int кодПривіда = 0;
        foreach (var файл in input.ВхідніФайли)
        {
            var дізайнФайлу = ВзятиФайл(файл);
            var резерваціяПривіда = new РезерваціяПривіда()
            {
                Привід = new(),
                Привід2 = дізайнФайлу.БагатоБобінний ? new СервоПривід() : null,
                ДізайнФайлу = дізайнФайлу,
                АлокованіПриводи = дізайнФайлу.БагатоБобінний ? [кодПривіда + 1, кодПривіда + 2] : [кодПривіда],
            };
            СервоПривіди[кодПривіда] = резерваціяПривіда.Привід;
            if (резерваціяПривіда.Привід2 is not null)
            {
                СервоПривіди[кодПривіда + 1] = резерваціяПривіда.Привід2;
            }
            кодПривіда += дізайнФайлу.БагатоБобінний ? 2 : 1;
            СервоПривідиСловник[файл.КодФайла] = резерваціяПривіда;
        }

        foreach (var файл in input.ВихідніФайли)
        {
            var дізайнФайлу = ВзятиФайл(файл);
            var резерваціяПривіда = new РезерваціяПривіда()
            {
                Привід = new(),
                Привід2 = дізайнФайлу.БагатоБобінний ? new СервоПривід() : null,
                ДізайнФайлу = дізайнФайлу,
                АлокованіПриводи = дізайнФайлу.БагатоБобінний ? [кодПривіда, кодПривіда + 1] : [кодПривіда],
            };
            СервоПривіди[кодПривіда] = резерваціяПривіда.Привід;
            if (резерваціяПривіда.Привід2 is not null)
            {
                СервоПривіди[кодПривіда + 1] = резерваціяПривіда.Привід2;
            }
            кодПривіда += дізайнФайлу.БагатоБобінний ? 2 : 1;
            СервоПривідиСловник[файл.КодФайла] = резерваціяПривіда;
        }

        КількістьПриводів = кодПривіда - 1;
    }

    private void ВиконатиCompare(Compare compare)
    {
        var першийБуфер = ФайловіБуфери[compare.Перший.Файл];
        var другийБуфер = ФайловіБуфери[compare.Другий.Файл];
        var першийДізайн = СервоПривідиСловник[compare.Перший.Файл].ДізайнФайлу;
        var другийДізайн = СервоПривідиСловник[compare.Другий.Файл].ДізайнФайлу;
        var першеПоле = ВзятиПоле(першийДізайн, compare.Перший.Поле, першийБуфер);
        var другеПоле = ВзятиПоле(другийДізайн, compare.Другий.Поле, другийБуфер);
        var порівняти = першеПоле.CompareTo(другеПоле);
        if (порівняти == 0 && compare.Рівно is not null)
        {
            Позиція = compare.Рівно.Value;
            return;
        }

        if (порівняти > 0 && compare.Більше is not null)
        {
            Позиція = compare.Більше.Value;
            return;
        }

        Позиція = compare.Інакше;
    }

    private void ВиконатиReadItem(ReadItem readItem)
    {
        var резервація = СервоПривідиСловник[readItem.Файл];
        var серво = резервація.Привід;
        if (серво.КінецьДаних)
        {
            if (readItem.Кінець == null)
            {
                Ошибка = $"Reel is empty for file {readItem.Файл}.";
                ФлагОстанова = true;
                return;
            }

            Позиція = readItem.Кінець.Value;
            return;
        }

        var елемент = серво.Прочитати(резервація.ДізайнФайлу.ДізайнЕлементів.РозмірЕлемента * 12);
        ФайловіБуфери[readItem.Файл] = елемент;
        if (елемент.Substring(0, резервація.ДізайнФайлу.МаркерКінцяФайла.Length) == резервація.ДізайнФайлу.МаркерКінцяФайла)
        {
            серво.КінецьДаних = true;
        }

        Позиція++;
    }

    private void ВиконатиSetOperation(SetOperation setOperation)
    {
        програма.Операції[setOperation.Операція] = new Jump(setOperation.НоваОперація);
        Позиція++;
    }

    private void ВиконатиJump(Jump jump)
    {
        Позиція = jump.Операція;
    }

    private void ВиконатиTest(Test test)
    {
        var першийБуфер = ФайловіБуфери[test.Поле.Файл];
        var першийДізайн = СервоПривідиСловник[test.Поле.Файл].ДізайнФайлу;
        var першеПоле = ВзятиПоле(першийДізайн, test.Поле.Поле, першийБуфер);
        foreach(var (значення, більше, рівно, менше, неРівне, інакше) in test.Порівняння)
        {
            var порівняти = першеПоле.CompareTo(значення);
            if (порівняти == 0 && рівно is not null)
            {
                Позиція = рівно.Value;
                return;
            }

            if (порівняти > 0 && більше is not null)
            {
                Позиція = більше.Value;
                return;
            }

            if (порівняти < 0 && менше is not null)
            {
                Позиція = менше.Value;
                return;
            }

            if (порівняти != 0 && неРівне is not null)
            {
                Позиція = неРівне.Value;
                return;
            }

            Позиція = інакше;
        }
    }

    private void ВиконатиTransfer(Transfer transfer)
    {
        ФайловіБуфери[transfer.ЦільовийФайл] = ФайловіБуфери[transfer.ІсходнийФайл];
        Позиція++;
    }

    private void ВиконатиMove(Move move)
    {
        foreach (var (ІсходнеПоле, ЦільовіПоля) in move.ОписПереміщення)
        {
            var значення = ВзятиПоле(ІсходнеПоле.Поле, ІсходнеПоле.Файл);
            foreach (var ціль in ЦільовіПоля)
            {
                ВстановитиПоле(ІсходнеПоле.Поле, ІсходнеПоле.Файл, значення);
            }
        }

        Позиція++;
    }

    private void ВиконатиWriteItem(WriteItem writeItem)
    {
        var буфер = ФайловіБуфери[writeItem.Файл];
        var резервація = СервоПривідиСловник[writeItem.Файл];
        var серво = резервація.Привід;
        серво.Записати(буфер);
        Позиція++;
    }

    private void ВстановитиПоле(string назваПоля, char файл, string значення)
    {
        var буфер = ФайловіБуфери[файл];
        var дізайн = СервоПривідиСловник[файл].ДізайнФайлу;
        var дізайнПолей = дізайн.ДізайнПолей ?? throw new InvalidDataException($"The reel does not have field design specified");
        var поле = дізайнПолей.Поля.FirstOrDefault(_ => _.Назва == назваПоля) ?? throw new InvalidDataException($"The reel does not have field {назваПоля}");
        string новеЗначення = буфер.Substring(0, поле.Місце * 12 + поле.ПозіціяНайлівішогоСимвола) + значення + буфер.Substring(поле.Місце * 12 + поле.ПозіціяНайлівішогоСимвола + поле.КількістьСимволів);
        ФайловіБуфери[файл] = новеЗначення;
    }

    private string ВзятиПоле(string назваПоля, char файл)
    {
        var буфер = ФайловіБуфери[файл];
        var дізайн = СервоПривідиСловник[файл].ДізайнФайлу;
        var дізайнПолей = дізайн.ДізайнПолей ?? throw new InvalidDataException($"The reel does not have field design specified");
        var поле = дізайнПолей.Поля.FirstOrDefault(_ => _.Назва == назваПоля) ?? throw new InvalidDataException($"The reel does not have field {назваПоля}");
        return буфер.Substring(поле.Місце * 12 + поле.ПозіціяНайлівішогоСимвола, поле.КількістьСимволів);
    }

    private string ВзятиПоле(ДізайнФайлу дізайн, string назваПоля, string буфер)
    {
        var дізайнПолей = дізайн.ДізайнПолей ?? throw new InvalidDataException($"The reel does not have field design specified");
        var поле = дізайнПолей.Поля.FirstOrDefault(_ => _.Назва == назваПоля) ?? throw new InvalidDataException($"The reel does not have field {назваПоля}");
        return буфер.Substring(поле.Місце * 12 + поле.ПозіціяНайлівішогоСимвола, поле.КількістьСимволів);
    }

    public class РезерваціяПривіда
    {
        public required СервоПривід Привід { get; set; }
        public СервоПривід? Привід2 { get; set; }

        public required ДізайнФайлу ДізайнФайлу { get; set; }

        public required int[] АлокованіПриводи { get; set; }

        public СервоПривід ПоточнийПривід
        {
            get
            {
                return Привід;
            }
        }
    }
}
