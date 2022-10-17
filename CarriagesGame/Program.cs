using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var debug = args.Length > 0 && args[0] == "debug";
            var random = new Random();
            var carriagesNumber = random.Next(2, 21);
            var carriages = new bool[carriagesNumber];
            for (var i = 0; i < carriagesNumber; i++)
            {
                carriages[i] = random.Next(0, 2) == 1;
            }
            var hasSolution = false;
            var quit = false;
            int currentCarriageIndex = random.Next(0, carriagesNumber - 1);
            var lastAction = "";
            var lastActionMoveCounter = 0;

            Console.WriteLine("Welcome to the carriages game. It based on a brainteaser from the internet.");
            Console.WriteLine("It sounds this way: \"You woke up inside a train that is connected on it's beginning and the end and has random amount of carriages. The only thing you can do is to move inside that train and switch lighting on or off inside carriages. From the beggining, light may be (randomly) on or off. Your mission here is to count carriages count the most efficient but accurate way.\"");
            Console.WriteLine("Well, let's try to get a solution here?\n");

            while (!hasSolution && !quit)
            {
                if (debug)
                {
                    Console.WriteLine(String.Format("\nDEBUG: currentCarriage: {0} of {1}\n", currentCarriageIndex, carriages.Length - 1));
                    for (var i = 0; i < carriages.Length; i++)
                    {
                        Console.WriteLine(String.Format("DEBUG: {0} carriage light is {1}", i, carriages[i] ? "ON" : "OFF"));
                    }
                    Console.WriteLine("\n");
                }
                var isLightUp = carriages[currentCarriageIndex];
                var lightUpStr = string.Format(
                        "This carriage is{0}lit up. What will you do? *forward* to go forward; *backward* to go backward; *switch* to switch light; *solution* to enter solution; *quit* to leave",
                        !isLightUp ? " not " : " "
                    );
                Console.WriteLine(lightUpStr);
                var userInput = Console.ReadLine();
                var parsed = userInput == null ? new string[] { "" } : userInput.Split(" ");
                var userAction = parsed[0];

                var userActionMultiplierStr = parsed.Length > 1 ? parsed[1] : "1";
                int userActionMultiplier;

                try
                {
                    userActionMultiplier = Convert.ToInt32(userActionMultiplierStr);
                } catch
                {
                    userActionMultiplier = 1;
                }


                if (lastAction != userAction)
                {
                    lastActionMoveCounter = userActionMultiplier;
                    lastAction = userAction;
                }
                else
                {
                    lastActionMoveCounter += userActionMultiplier;
                }


                if (userAction == "forward")
                {
                    currentCarriageIndex += userActionMultiplier % carriages.Length;
                    Console.WriteLine(
                        String.Format(
                            "Moving forward for {0} carriages. You already moved {1} in this direction in a row.",
                            userActionMultiplier,
                            lastActionMoveCounter
                        )
                    );
                    if (currentCarriageIndex > carriages.Length - 1)
                    {
                        currentCarriageIndex = currentCarriageIndex - carriages.Length;
                    }
                    continue;
                }
                if (userAction == "backward")
                {
                    currentCarriageIndex -= userActionMultiplier % carriages.Length;
                    Console.WriteLine(
                        String.Format(
                            "Moving backward for {0} carriages. You already moved {1} in this direction in a row.",
                            userActionMultiplier,
                            lastActionMoveCounter
                        )
                    );
                    if (currentCarriageIndex < 0)
                    {
                        currentCarriageIndex = carriages.Length + currentCarriageIndex;
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
                Console.WriteLine("Bad command.");
            }
            Console.WriteLine("Job is done. Or isn't. Whatever.");
            Console.WriteLine("Press any key to quit...");
            Console.ReadKey();
        }
    }
}