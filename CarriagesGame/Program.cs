using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();
            var carriagesNumber = random.Next(2, 21);
            var carriages = new bool[carriagesNumber];
            for (var i = 0; i < carriagesNumber; i++)
            {
                carriages[i] = random.Next(0, 2) == 1;
            }
            bool hasSolution = false;
            bool quit = false;
            int currentCarriageIndex = random.Next(0, carriagesNumber - 1);
            while (!hasSolution && !quit)
            {
                var isLightUp = carriages[currentCarriageIndex];
                var lightUpStr = string.Format(
                        "This carriage is{0}lit up. What will you do? *forward* to go forward; *backward* to go backward; *switch* to switch light; *solution* to enter solution; *quit* to leave",
                        !isLightUp ? " not " : " "
                    );
                Console.WriteLine(lightUpStr);
                var userAction = Console.ReadLine();

                if (userAction == "forward")
                {
                    currentCarriageIndex += 1;
                    if (currentCarriageIndex > carriages.Length - 1)
                    {
                        currentCarriageIndex = 0;
                    }
                    continue;
                }
                if (userAction == "backward")
                {
                    currentCarriageIndex -= 1;
                    if (currentCarriageIndex < 0)
                    {
                        currentCarriageIndex = carriages.Length - 1;
                    }
                    continue;
                }
                if (userAction == "switch")
                {
                    carriages[currentCarriageIndex] = !carriages[currentCarriageIndex];
                    continue;
                }
                if (userAction == "quit")
                {
                    quit = true;
                    continue;
                }
                if (userAction == "solution")
                {
                    Console.WriteLine("Enter it.");
                    var solution = Console.ReadLine();
                    int solInt = 0;
                    try
                    {
                        solInt = Convert.ToInt32(solution);
                    }
                    catch
                    {
                        solInt = -1;
                    }
                    hasSolution = solInt == carriages.Length;
                    if (!hasSolution)
                    {
                        Console.WriteLine("Bad answer either wrong or incorrect.");
                        var hasAnswer = false;
                        while (!hasAnswer)
                        {
                            Console.WriteLine("Wanna continue to play? (Y/n)");
                            var answer = Console.ReadKey();
                            Console.WriteLine("\n");
                            if (answer.Key == ConsoleKey.Y || answer.Key == ConsoleKey.N)
                            {
                                hasAnswer = true;
                                quit = answer.Key == ConsoleKey.N;
                            }
                        }
                        continue;
                    }
                    if (hasSolution)
                    {
                        Console.WriteLine(String.Format("Congratz! Your answer is correct. It is {0}", carriages.Length));
                    }
                }
            }
            Console.WriteLine("Job is done. Or isn't. Whatever.");
            Console.WriteLine("Press any key to quit...");
            Console.ReadKey();
        }
    }
}