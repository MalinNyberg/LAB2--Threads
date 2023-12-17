using System;

namespace LAB2__Threads
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Creates cars and giving the car its speed
            Car car1 = new Car("Ferrari", 120);
            Car car2 = new Car("Porsche", 120);

            // Creates threads for the cars 
            Thread thread1 = new Thread(car1.Drive);
            Thread thread2 = new Thread(car2.Drive);

            // Start the race/threads
            thread1.Start();
            thread2.Start();

            Console.WriteLine("Press <ENTER> to show status on the cars");

            //Displays status when the user presses enter. 
            while (car1.IsDriving || car2.IsDriving)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    car1.DisplayStatus();
                    car2.DisplayStatus();
                }
            }

            // Waiting for the race to finnish 
            thread1.Join();
            thread2.Join();

            // Displays the results
            Console.WriteLine($"{car1.Name} Distance: {car1.Distance} km");
            Console.WriteLine($"{car2.Name} Distance: {car2.Distance} km");

            // Displays the winner
            if (car1.Distance > car2.Distance)
                Console.WriteLine($"{car1.Name} Wins!");
            else if (car2.Distance > car1.Distance)
                Console.WriteLine($"{car2.Name} Wins!");
            else
                Console.WriteLine("The race ended in a tie!");
        }
    }
}