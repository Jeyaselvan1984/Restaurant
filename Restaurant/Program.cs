using System;
using System.Collections.Generic;

namespace Restaurant
{
    // delegate for the event's signature
    public delegate void TableEventHandler(object sender, TableEventArgs e);
    public delegate void MealChangeHandler(object sender, MealChangeArgs e);

    public enum Meals
    {
        none,
        appetizer,
        main,
        desert,
        done
    };


    public class MealChangeArgs : EventArgs
    {
        public customer c;
        public MealChangeArgs(customer cVal)
        {
            this.c = cVal;
        }


    }

    public class customer
    {

        public event MealChangeHandler mealEvent;
        public string firstName { get; set; }
        public string lastName { get; set; }
        public Meals Meal { get; set; }
        public void HandleTableReady(object sender, TableEventArgs e)
        {
            Console.WriteLine("{0} {1} got a table.", this.firstName, this.lastName);
        }

        public void raiseMealEvent(Meals e)
        {

            switch (e)
            {
                case Restaurant.Meals.none:
                    {
                        this.Meal = Meals.none;
                        if (mealEvent != null)
                        {
                            mealEvent(this, new MealChangeArgs(this));
                        }
                        break;
                    }
                case Restaurant.Meals.appetizer:
                    {
                        this.Meal = Meals.appetizer;
                        if (mealEvent != null)
                        {
                            mealEvent(this, new MealChangeArgs(this));
                        }
                        break;
                    }
                case Restaurant.Meals.main:
                    {
                        this.Meal = Meals.main;
                        if (mealEvent != null)
                        {
                            mealEvent(this, new MealChangeArgs(this));
                        }
                        break;
                    }
                case Restaurant.Meals.desert:
                    {
                        this.Meal = Meals.desert;
                        if (mealEvent != null)
                        {
                            mealEvent(this, new MealChangeArgs(this));
                        }
                        break;
                    }
                case Restaurant.Meals.done:
                    {
                        this.Meal = Meals.done;
                        if (mealEvent != null)
                        {
                            mealEvent(this, new MealChangeArgs(this));
                        }
                        break;
                    }
            }

        }


    }

    
    public class Table
    {

        public event TableEventHandler tableEvent;

        public void NotifyTableReady()
        {
            TableEventHandler tableReady = tableEvent;
            if (tableReady != null)
            {
                tableReady(this, new TableEventArgs());
            }
        }
    }



    public class TableEventArgs : EventArgs
    {
        
        public TableEventArgs()
        {
            Console.WriteLine("Table is open");
        }
    }



    public class Program
    {
      
        public static void HandleChangeofMeal(object sender, MealChangeArgs e)
        {

            Console.WriteLine("{0} {1} is having {2}.", e.c.firstName, e.c.lastName, e.c.Meal);
        }

        public static void AllProcessingComplete()
        {
            Console.WriteLine("Everyone is full!");
        }
        static void Main(string[] args)
        {
            Table tble = new Table();
           
            Queue<customer> customerQueue = new Queue<customer>(5);

            customer[] customers = new customer[] { new customer{ firstName = "J1", lastName = "L1"}, 
                                                    new customer { firstName = "J2", lastName = "L2" }, 
                                                    new customer { firstName = "J3", lastName = "L3" }, 
                                                    new customer { firstName = "J4", lastName = "L4" }, 
                                                    new customer { firstName = "J5", lastName = "L5" } };

            for (int i=0;i<customers.Length;i++)
            {
                customers[i].mealEvent += Program.HandleChangeofMeal;
                customerQueue.Enqueue(customers[i]);
            }

           
            while (customerQueue.Count != 0)
            {
                
                customer c = customerQueue.Dequeue();
                tble.tableEvent += c.HandleTableReady;
                tble.NotifyTableReady();
                c.raiseMealEvent(Meals.appetizer);
                c.raiseMealEvent(Meals.main);
                c.raiseMealEvent(Meals.desert);
                c.raiseMealEvent(Meals.done);
                tble.tableEvent -= c.HandleTableReady;
            }
            if(customerQueue.Count == 0)
                Program.AllProcessingComplete();


        }
    }
}
