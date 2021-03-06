﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Executor.Contracts;

namespace Executor.DataStructures
{
    public class SimpleSortedList<T> : ISimpleOrderedBag<T> where T : IComparable<T>
    {
        private const int DefaultSize = 16;

        private T[] innerCollection;
        private int size;
        private IComparer<T> comparison;

        public SimpleSortedList(int capacity, IComparer<T> comparison)
        {
            InitializeInnerCollection(capacity);
            this.comparison = comparison;
        }

        public SimpleSortedList(int capacity)
            : this(capacity, Comparer<T>.Create((x, y) => x.CompareTo(y)))
        {
        }

        public SimpleSortedList(IComparer<T> comparison)
            : this(DefaultSize, comparison)
        {
        }

        public SimpleSortedList()
            : this(DefaultSize, Comparer<T>.Create((x, y) => x.CompareTo(y)))
        {
        }

        public void InitializeInnerCollection(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException("Capacity cannot be nagative!");
            }

            this.innerCollection = new T[capacity];
        }

        public T[] InnerCollection
        {
            get { return this.innerCollection; }
        }

        public int Size
        {
            get { return this.size; }
            private set { this.size = value; }
        }

        public bool Remove(T element)
        {
            if (element == null)
            {
                throw new ArgumentNullException();
            }

            bool hasBeenRemoved = false;
            int indexOfRemovedElement = 0;
            for (int i = 0; i < this.Size; i++)
            {
                if (this.innerCollection[i].Equals(element))
                {
                    indexOfRemovedElement = i;
                    this.innerCollection[i] = default(T);
                    hasBeenRemoved = true;
                    break;
                }
            }

            if (hasBeenRemoved)
            {
                for (int i = indexOfRemovedElement; i < this.Size - 1; i++)
                {
                    this.innerCollection[i] = this.innerCollection[i + 1];
                }

                this.innerCollection[this.Size - 1] = default(T);
                this.Size--;
            }

            return hasBeenRemoved;
        }

        public int Capacity { get { return this.innerCollection.Length; } }

        public void Add(T element)
        {
            if (element == null)
            {
                throw new ArgumentNullException();
            }

            if (this.innerCollection.Length == this.Size)
            {
                Resize();
            }

            this.innerCollection[size] = element;
            this.size++;
            Array.Sort(this.innerCollection, 0, size, comparison);
        }

        private void Resize()
        {
            T[] newCollection = new T[this.Size * 2];
            Array.Copy(innerCollection, newCollection, Size);
            innerCollection = newCollection;
        }

        public void AddAll(ICollection<T> elements)
        {
            if (elements == null)
            {
                throw new ArgumentNullException();
            }

            if (this.Size + elements.Count >= this.innerCollection.Length)
            {
                this.MultiResize(elements);
            }

            foreach (var element in elements)
            {
                this.innerCollection[Size] = element;
                this.size++;
            }

            Array.Sort(this.innerCollection, 0, size, comparison);
        }

        private void MultiResize(ICollection<T> elements)
        {
            int newSize = this.innerCollection.Length*2;
            while (this.Size + elements.Count >= newSize)
            {
                newSize *= 2;
            }

            T[] newCollection = new T[newSize];
            Array.Copy(this.innerCollection, newCollection, size);
            this.innerCollection = newCollection;
        }

        public string JoinWith(string joiner)
        {
            if (joiner == null)
            {
                throw new ArgumentNullException();
            }

            StringBuilder builder = new StringBuilder();
            foreach (var element in this.innerCollection.Where(c => c != null))
            {
                builder.Append(element);
                builder.Append(joiner);
            }

            builder.Remove(builder.Length - joiner.Length, joiner.Length);
            return builder.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Size; i++)
            {
                yield return this.innerCollection[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
