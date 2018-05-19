using System;
using System.Collections.Generic;
using System.Linq;

namespace NDesignPatterns.Behavioral
{
    public static class MementoExample2
    {
        public static void Execute()
        {
            var originator = new Originator2();
            originator.SetState("State 1");
            originator.Save();
            originator.SetState("State 2");
            originator.Save();
            originator.SetState("Bad State 1");
            originator.Save();
            originator.SetState("Bad State 2");
            originator.RestoreLastGoodState();
            Console.WriteLine(originator.ToString());
        }
    }

    internal sealed class Memento2
    {
        public string State { get; }

        public Memento2(string state)
        {
            State = state;
        }
    }

    internal sealed class Originator2
    {
        private string _state;
        private readonly Caretaker _caretaker = new Caretaker();

        public void RestoreLastGoodState()
        {
            this._state = _caretaker.Restore(x => !x.State.Contains("Bad")).State;
        }

        public void Save()
        {
            _caretaker.Save(new Memento(_state));
        }

        public void SetState(string state)
        {
            _state = state;
        }

        public override string ToString()
        {
            return _state;
        }

        private sealed class Caretaker
        {
            private readonly List<Memento> _mementoes = new List<Memento>();

            public void Save(Memento memento)
            {
                _mementoes.Add(memento);
            }

            public Memento Restore(Func<Memento, bool> where)
            {
                return _mementoes.Where(where).Last();
            }
        }
    }
}