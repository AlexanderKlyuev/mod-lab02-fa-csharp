using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    internal class Program
    {
        public class State
        {
            public string Name;
            public Dictionary<char, State> Transitions;
            public bool IsAcceptState;
        }

        public class FA
        {
            protected bool? Run(State InitialState, IEnumerable<char> s)
            {
                State current = InitialState;
                foreach (var c in s)
                {
                    if (!current.Transitions.ContainsKey(c))
                        return false;
                    current = current.Transitions[c];
                }
                return current.IsAcceptState;
            }
        }

        public class FA3 : FA
        {
            public static State a = new State()
            {
                Name = "a",
                IsAcceptState = false,
                Transitions = new Dictionary<char, State>()
            };
            public State b = new State()
            {
                Name = "b",
                IsAcceptState = false,
                Transitions = new Dictionary<char, State>()
            };
            public State c = new State()
            {
                Name = "c",
                IsAcceptState = true,
                Transitions = new Dictionary<char, State>()
            };

            State InitialState = a;

            public FA3()
            {
                a.Transitions['0'] = a;
                a.Transitions['1'] = b;
                b.Transitions['0'] = a;
                b.Transitions['1'] = c;
                c.Transitions['0'] = c;
                c.Transitions['1'] = c;
            }

            public bool? Run(IEnumerable<char> s) => base.Run(InitialState, s);
        }

        static void Main(string[] args)
        {
            String s = "00110011";
            FA3 fa3 = new FA3();
            bool? result1 = fa3.Run(s);
            Console.WriteLine(result1);
        }
    }
}
