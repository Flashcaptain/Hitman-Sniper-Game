using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum RuleResult
{
    Done,
    Working
}

namespace Casanova.Prelude
{
    public class Tuple<T, E>
    {
        public T Item1 { get; set; }
        public E Item2 { get; set; }

        public Tuple(T item1, E item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
    }

    public static class MyExtensions
    {
        //public T this[List<T> list]
        //{
        //  get { return list.ElementAt(0); }
        //}

        public static bool CompareStruct<T>(this List<T> list1, List<T> list2)
        {
            if (list1.Count != list2.Count) return false;
            for (int i = 0; i < list1.Count; i++)
            {
                if (!list1[i].Equals(list2[i])) return false;
            }
            return true;
        }

        public static T Head<T>(this List<T> list)
        {
            return list.ElementAt(0);
        }

        public static T Head<T>(this IEnumerable<T> list)
        {
            return list.ElementAt(0);
        }

        public static List<T> Tail<T>(this List<T> list)
        {
            return list.Skip(1).ToList();
        }

        public static IEnumerable<T> Tail<T>(this IEnumerable<T> list)
        {
            return list.Skip(1);
        }

        public static int Length<T>(this List<T> list)
        {
            return list.Count;
        }

        public static int Length<T>(this IEnumerable<T> list)
        {
            return list.ToList().Count;
        }
    }

    public class Cons<T> : IEnumerable<T>
    {
        public class Enumerator : IEnumerator<T>
        {
            public Enumerator(Cons<T> parent)
            {
                firstEnumerated = 0;
                this.parent = parent;
                tailEnumerator = parent.tail.GetEnumerator();
            }

            byte firstEnumerated;
            Cons<T> parent;
            IEnumerator<T> tailEnumerator;

            public T Current
            {
                get
                {
                    if (firstEnumerated == 0) return default(T);
                    if (firstEnumerated == 1) return parent.head;
                    return tailEnumerator.Current;
                }
            }

            object IEnumerator.Current
            {
                get { throw new Exception("Empty sequence has no elements"); }
            }

            public bool MoveNext()
            {
                if (firstEnumerated == 0)
                {
                    if (parent.head == null) return false;
                    firstEnumerated++;
                    return true;
                }
                if (firstEnumerated == 1) firstEnumerated++;
                return tailEnumerator.MoveNext();
            }

            public void Reset()
            {
                firstEnumerated = 0;
                tailEnumerator.Reset();
            }

            public void Dispose()
            {
            }
        }

        T head;
        IEnumerable<T> tail;

        public Cons(T head, IEnumerable<T> tail)
        {
            this.head = head;
            this.tail = tail;
        }


        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }
    }

    public class Empty<T> : IEnumerable<T>
    {
        public class Enumerator : IEnumerator<T>
        {
            public T Current
            {
                get { throw new Exception("Empty sequence has no elements"); }
            }

            object IEnumerator.Current
            {
                get { throw new Exception("Empty sequence has no elements"); }
            }

            public bool MoveNext()
            {
                return false;
            }

            public void Reset()
            {
            }

            public void Dispose()
            {
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator();
        }
    }

    public abstract class Option<T> : IComparable, IEquatable<Option<T>>
    {
        public bool IsSome;

        public bool IsNone
        {
            get { return !IsSome; }
        }

        protected abstract Just<T> Some { get; }

        public U Match<U>(Func<T, U> f, Func<U> g)
        {
            if (IsSome)
                return f(Some.Value);
            return g();
        }

        public bool Equals(Option<T> b)
        {
            return Match(
                x => b.Match(
                    y => x.Equals(y),
                    () => false),
                () => b.Match(
                    y => false,
                    () => true));
        }

        public override bool Equals(Object other)
        {
            if (other == null) return false;
            if (other is Option<T>)
            {
                var other1 = other as Option<T>;
                return Equals(other1);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return GetHashCode();
        }


        public static bool operator ==(Option<T> a, Option<T> b)
        {
            if ((Object) a == null || (Object) b == null) return Equals(a, b);
            return a.Equals(b);
        }

        public static bool operator !=(Option<T> a, Option<T> b)
        {
            if ((Object) a == null || (Object) b == null) return Equals(a, b);
            return !(a.Equals(b));
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            if (obj is Option<T>)
            {
                var obj1 = obj as Option<T>;
                if (Equals(obj1)) return 0;
            }
            return -1;
        }

        abstract public T Value { get; }

        public override string ToString()
        {
            return "Option<" + typeof(T) + ">";
        }
    }

    public class Just<T> : Option<T>
    {
        T elem;

        public Just(T elem)
        {
            this.elem = elem;
            IsSome = true;
        }

        protected override Just<T> Some
        {
            get { return this; }
        }

        public override T Value
        {
            get { return elem; }
        }
    }

    public class Nothing<T> : Option<T>
    {
        public Nothing()
        {
            IsSome = false;
        }

        protected override Just<T> Some
        {
            get { return null; }
        }

        public override T Value
        {
            get { throw new Exception("Cant get a value from a None object"); }
        }
    }

    public class Utils
    {
        public static T IfThenElse<T>(Func<bool> c, Func<T> t, Func<T> e)
        {
            if (c())
                return t();
            return e();
        }
    }
}

public class FastStack
{
    public int[] Elements;
    public int Top;

    public FastStack(int elems)
    {
        Top = 0;
        Elements = new int[elems];
    }

    public void Clear()
    {
        Top = 0;
    }

    public void Push(int x)
    {
        Elements[Top++] = x;
    }
}

public class RuleTable
{
    public RuleTable(int elems)
    {
        ActiveIndices = new FastStack(elems);
        SupportStack = new FastStack(elems);
        ActiveSlots = new bool[elems];
        SupportSlots = new bool[elems];
    }

    public FastStack ActiveIndices;
    public FastStack SupportStack;
    public bool[] ActiveSlots;
    public bool[] SupportSlots;

    public void Clear()
    {
        for (int i = 0; i < ActiveSlots.Length; i++)
        {
            ActiveSlots[i] = false;
        }
    }

    public void Add(int i)
    {
        if (!ActiveSlots[i])
        {
            ActiveSlots[i] = true;
            ActiveIndices.Push(i);
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           