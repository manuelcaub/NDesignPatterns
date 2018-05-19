using System;
using System.Collections.Generic;
using System.Linq;

namespace NDesignPatterns.Behavioral
{
    public static class MementoExample
    {
        public static void Execute()
        {
            var originator = new Originator();
            var caretaker = new Caretaker(originator);
            originator.SetState("State 1");
            caretaker.Save();
            originator.SetState("State 2");
            caretaker.Save();
            originator.SetState("Bad State 1");
            caretaker.Save();
            originator.SetState("Bad State 2");

            caretaker.Restore(x => !x.State.Contains("Bad"));
            Console.WriteLine(originator.ToString());
        }
    }

    internal sealed class Memento
    {
        public string State { get; set; }

        public Memento(string state)
        {
            State = state;
        }
    }

    internal sealed class Originator
    {
        private string _state;

        public Memento CreateMemento()
        {
            return new Memento(_state);
        }

        public void SetMemento(Memento memento)
        {
            _state = memento.State;
        }

        public void SetState(string state)
        {
            _state = state;
        }

        public override string ToString()
        {
            return _state;
        }
    }

    internal sealed class Caretaker
    {
        private readonly List<Memento> _mementoes = new List<Memento>();
        private readonly Originator _originator;

        public Caretaker(Originator originator)
        {
            _originator = originator;
        }

        public Memento Save()
        {
            var memento = _originator.CreateMemento();
            _mementoes.Add(memento);
            return memento;
        }

        public void Restore(int index)
        {
            var match = _mementoes[index];
            _originator.SetMemento(match);
        }

        public void Restore(Func<Memento, bool> where)
        {
            var match = _mementoes.Where(where).First();
            _originator.SetMemento(match);
        }
    }
}