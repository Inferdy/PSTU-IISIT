using System.Collections;

namespace ProductionSystem
{
    public class ExclusiveList<T> : IEnumerable<T>
    {
        private class Node
        {
            public T Value;
            public Node? Next;

            public Node(T value, Node? next)
            {
                Value = value;
                Next = next;
            }
        }

        private Node? head = null;
        private Node? tail = null;

        public void AddLast(T value)
        {
            if (head == null)
            {
                head = new Node(value, null);
                tail = head;
            }
            else
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                tail.Next = new Node(value, null);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                tail = tail.Next;
            }
        }

        public void Clear()
        {
            head = null;
            tail = null;
        }

        public void Merge(IEnumerable<T>? pool)
        {
            if (pool == null)
                return;

            if (head == null)
            {
                foreach (T item in pool)
                    AddLast(item);
            }
            else
            {
                Node? last = tail;

                foreach (T item in pool)
                {
                    for (Node current = head; ;)
                    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        if (current.Value.Equals(item))
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                            goto skip;

                        if (current == last)
                            break;
                        else
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                            current = current.Next;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                    }

                    AddLast(item);

                skip:;
                }
            }
        }

        private class Enumerator : IEnumerator<T>
        {
            private Node? head;
            private Node? current = null;
            private bool ready = false;

            public Enumerator(Node? head)
            {
                this.head = head;
            }

            public T Current
            {
                get
                {
                    try
                    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        return current.Value;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    }
                    catch (NullReferenceException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

#pragma warning disable CS8603 // Possible null reference return.
            object IEnumerator.Current => Current;
#pragma warning restore CS8603 // Possible null reference return.

            public void Dispose()
            {
                //Empty
            }

            public bool MoveNext()
            {
                if (ready)
                {
                    try
                    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        current = current.Next;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    }
                    catch (NullReferenceException)
                    {
                        throw new InvalidOperationException();
                    }
                }
                else
                {
                    current = head;
                    ready = true;
                }

                return current != null;
            }

            public void Reset()
            {
                current = null;
                ready = false;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}