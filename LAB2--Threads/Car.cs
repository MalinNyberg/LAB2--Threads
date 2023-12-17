using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Threading;
using System.Buffers.Text;


namespace LAB2__Threads
{
    internal class Car
    {
        public string Name { get; }
        public int Speed { get; private set; }
        public double Distance { get; private set; }
        public bool IsDriving { get; private set; }

        public Car(string name, int speed)
        {
            Name = name;
            Speed = speed;
            Distance = 0;
            IsDriving = true;
        }

        public void Drive()
        {
            Console.WriteLine($"{Name} is starting the race!");
            while (IsDriving && Distance < 10.0)
            {
                Distance += Speed / 3600.0; // Convert speed from km/h to km/s

                // random issue every 30 seconds
                if (DateTime.Now.Second % 30 == 0)
                    CarIssue();

                Thread.Sleep(1000);
            }

            if (IsDriving) //when not true = displays the cw
            {
                Console.WriteLine($"{Name} has finished the race!");
            }
        }

        //method that displays the cars status
        public void DisplayStatus()
        {
            Console.WriteLine();
            Console.WriteLine($"{Name} Status - Distance: {Distance} km, Speed: {Speed} km/h");
        }

        //method that randomly calls an event/issue that happens every 30 sec to one randomly selected car. 
        private void CarIssue() 
        {
            var random = new Random();
            var issues = new (string, double, Action)[]
            {
            ("Out of petrol", 1.0 / 50, () =>
            {
                Console.WriteLine($"{Name} has run out of petrol. {Name} has to stop and refuel for 30 sec");
                Thread.Sleep(30000);
            }),
            ("Flat tire", 2.0 / 50, () =>
            {
                Console.WriteLine($"{Name} has got a flat tire. {Name} has to stop and change the tire for 20 sec");
                Thread.Sleep(20000);
            }),
            ("Bird on windshield", 5.0 / 50, () =>
            {
                Console.WriteLine($"{Name} crashed with a bird. {Name} has to stop and clean the windshield for 10 sec");
                Thread.Sleep(10000);
            }),
            ("Engine failure", 10.0 / 50, () =>
            {
                Speed -= 1; //Reduce speed by 1 km/h in case of engine failure
                Console.WriteLine($"{Name} has engine failure. Speed has dropped to {Speed} km/h.");
            }),
            };

            //Randomizes an event based on probability
            foreach (var (issue, probability, action) in issues)
            {
                if (random.NextDouble() < probability)
                {
                    Console.WriteLine();
                    Console.WriteLine($"{Name} has a problem: {issue}");
                    action.Invoke();
                    break;
                }
            }
        }
    }
}

