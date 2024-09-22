using FlowMatic.Операції;

namespace FlowMatic
{
    public class Компілятор
    {
        public Програма Скомпілювати(string ісходнийКод)
        {
            var лінії = ісходнийКод.ReplaceLineEndings().Split(Environment.NewLine);
            return new Програма(лінії.Select(РозібратиОперацію).ToArray());
        }

        private ІОперація РозібратиОперацію(string код, int номерРядка)
        {
            var префікс = $"({номерРядка}) ";
            if (!код.StartsWith(префікс))
            {
                throw new InvalidOperationException($"Invalid line number. Have code {код}, expected line number {номерРядка}");
            }

            код = код[префікс.Length..];
            if (код[код.Length - 1] != '.' && !код.StartsWith("STOP."))
            {
                throw new InvalidOperationException($"Last symbol in line should be dot. Instead {код[код.Length - 1]}");
            }

            код = код[..(код.Length - 1)];
            var частиКода = код.Split([' '], 2);
            var команда = частиКода[0];
            switch (команда)
            {
                case "INPUT":
                    return РозібратиInput(частиКода[1]);
                case "COMPARE":
                    return РозібратиCompare(частиКода[1]);
                case "TRANSFER":
                    return РозібратиTransfer(частиКода[1]);
                case "WRITE-ITEM":
                    return РозібратиWriteItem(частиКода[1]);
                case "JUMP":
                    return РозібратиJump(частиКода[1]);
                case "READ-ITEM":
                    return РозібратиReadItem(частиКода[1]);
                case "SET":
                    return РозібратиSetOperation(частиКода[1]);
                case "TEST":
                    return РозібратиTest(частиКода[1]);
                case "REWIND":
                    return РозібратиRewind(частиКода[1]);
                case "CLOSE-OUT":
                    return РозібратиCloseOut(частиКода[1]);
                case "STOP.":
                    return new Stop();
                case "MOVE":
                    return РозібратиMove(частиКода[1]);
                default:
                    throw new InvalidOperationException($"Неподдерживаемая команда {команда}");
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

        private ІОперація РозібратиCompare(string код)
        {
            var частини = код.Split(" ; ");
            var поля = частини[0].Split(" WITH ");
            var i = 1;
            ushort? рівно = null;
            ushort? більше = null;
            for (i = 1; i < частини.Length - 1; i++)
            {
                if (частини[i].StartsWith("IF EQUAL GO TO OPERATION "))
                {
                    рівно = ushort.Parse(частини[i]["IF EQUAL GO TO OPERATION ".Length..]);
                }
                if (частини[i].StartsWith("IF GREATER GO TO OPERATION "))
                {
                    більше = ushort.Parse(частини[i]["IF GREATER GO TO OPERATION ".Length..]);
                }
            }

            return new Compare()
            {
                Перший = РозібратиПосиланняНаПоле(поля[0]),
                Другий = РозібратиПосиланняНаПоле(поля[1]),
                Рівно = рівно,
                Більше = більше,
                Інакше = ushort.Parse(частини[частини.Length - 1]["OTHERWISE GO TO OPERATION ".Length..]),
            };
        }

        private ІОперація РозібратиTransfer(string код)
        {
            var частини = код.Split(" TO ");
            return new Transfer(частини[0][0], частини[1][0]);
        }

        private ІОперація РозібратиWriteItem(string код)
        {
            return new WriteItem(код[0]);
        }

        private ІОперація РозібратиJump(string код)
        {
            return new Jump(ushort.Parse(код["TO OPERATION ".Length..]));
        }

        private static ПосиланняНаПоле РозібратиПосиланняНаПоле(string код)
        {
            return new(код[..(код.Length - 4)], код[код.Length - 2]);
        }

        private ІОперація РозібратиReadItem(string код)
        {
            var частини = код.Split(" ; ");
            if (частини.Length == 1)
            {
                return new ReadItem(код[0], null);
            }
            else
            {
                return new ReadItem(код[0], ushort.Parse(частини[1]["IF END OF DATA GO TO OPERATION ".Length..]));
            }
        }

        private ІОперація РозібратиSetOperation(string код)
        {
            var частини = код.Split(" TO GO TO ");
            return new SetOperation(ushort.Parse(частини[0]["OPERATION ".Length..]), ushort.Parse(частини[1]["OPERATION ".Length..]));
        }

        private ІОперація РозібратиTest(string код)
        {
            var частини = код.Split(" ; ");
            var ч = частини[0].Split(" AGAINST ");
            var п = РозібратиПосиланняНаПоле(ч[0]);
            ushort? рівно = null;
            ushort? більше = null;
            ushort? менше = null;
            ushort? нерівне = null;
            for (int i = 1; i < частини.Length - 1; i++)
            {
                if (частини[i].StartsWith("IF EQUAL GO TO OPERATION "))
                {
                    рівно = ushort.Parse(частини[i]["IF EQUAL GO TO OPERATION ".Length..]);
                }
                if (частини[i].StartsWith("IF GREATER GO TO OPERATION "))
                {
                    більше = ushort.Parse(частини[i]["IF GREATER GO TO OPERATION ".Length..]);
                }
                if (частини[i].StartsWith("IF LESS GO TO OPERATION "))
                {
                    менше = ushort.Parse(частини[i]["IF LESS GO TO OPERATION ".Length..]);
                }
                if (частини[i].StartsWith("IF UNEQUAL GO TO OPERATION "))
                {
                    нерівне = ushort.Parse(частини[i]["IF UNEQUAL GO TO OPERATION ".Length..]);
                }
            }

            return new Test(п, [new(ч[1], більше, рівно, менше, нерівне, ushort.Parse(частини[частини.Length - 1]["OTHERWISE GO TO OPERATION ".Length..]))]);
        }

        private ІОперація РозібратиRewind(string код)
        {
            return new Rewind(код.Split(", ").Select(_ => _[0]).ToArray());
        }

        private ІОперація РозібратиCloseOut(string код)
        {
            var частини = код.Split(" ", 2);
            return new CloseOut(частини[1].Split(", ").Select(_ => _[0]).ToArray());
        }

        private ІОперація РозібратиMove(string код)
        {
            var частини = код.Split(" ; ");
            return new Move(частини.Select<string, (ПосиланняНаПоле ІсходнеПоле, ПосиланняНаПоле[] ЦільовіПоля)>(x =>
            {
                var y = x.Split(" TO ");

                return (РозібратиПосиланняНаПоле(y[0]), [РозібратиПосиланняНаПоле(y[1].TrimEnd())]);
            }).ToArray());
        }
    }
}
