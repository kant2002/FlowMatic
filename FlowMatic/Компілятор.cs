using FlowMatic.Операції;

namespace FlowMatic
{
    public class Компілятор
    {
        public Програма Скомпілювати(string ісходнийКод)
        {
            var лінії = ісходнийКод.Split(Environment.NewLine);
            return new Програма(лінії.Select(РозібратиОперацію).ToArray());
        }

        private ІОперація РозібратиОперацію(string код, int номерРядка)
        {
            var префікс = $"({номерРядка}) ";
            if (!код.StartsWith(префікс))
            {
                throw new InvalidOperationException("Invalid line number.");
            }

            код = код[префікс.Length..];
            if (код[код.Length - 1] != '.')
            {
                throw new InvalidOperationException("Last symbol in line should be dot.");
            }

            код = код[..(код.Length - 1)];
            var частиКода = код.Split([' '], 2);
            var команда = частиКода[0];
            switch (команда)
            {
                case "INPUT":
                    return РозібратиInput(частиКода[1]);
                default:
                    throw new InvalidOperationException("Неподдерживаемая команда");
            }
        }

        private ІОперація РозібратиInput(string код)
        {
            var частини = код.Split(" ; ");
            var вхідні = РозібратиОписФайла(частини[0]);
            var вихідні = РозібратиОписФайла(частини[1][("OUTPUT ".Length)..]);
            var високошвидкісні = Array.Empty<char>();
            if (частини.Length >= 2 && частини[2].StartsWith("HSP "))
            {
                високошвидкісні = частини[2]["HSP ".Length..].Split(",").Select(_ => _[0]).ToArray();
            }
            return new Input()
            {
                ВхідніФайли = вхідні,
                ВихідніФайли = вихідні,
                ВисокошвидкосніПрінтери = високошвидкісні,
            };

            Input.ОписФайла[] РозібратиОписФайла(string текст)
            {
                var частини = текст.Split(" ");
                List<Input.ОписФайла> результат = new();
                for (int i = 0; i < частини.Length; i++)
                {
                    int д = 1;
                    var назва = частини[i];
                    var код = частини[i + 1]["FILE-".Length];
                    var привіди = Array.Empty<char>();
                    if (i + 2 < частини.Length)
                    {
                        if (частини[i + 2] == "SERVO" || частини[i + 2] == "SERVOS")
                        {
                            // Чи повинен я перевірити що можливо лише 2 серво-привіда мати.
                            привіди = частини[i + 3].Split(",").Select(_ => _[0]).ToArray();
                            д = 3;
                        }
                    }

                    результат.Add(new(назва, код, привіди));
                    i += д;
                }

                return результат.ToArray();
            }
        }
    }
}
