using System;
using System.Collections.Generic;
using System.Linq;
using Executor.Contracts;
using Executor.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BashSoftTesting
{
    [TestClass]
    public class OrderedDataStructureTester
    {
        private ISimpleOrderedBag<string> names;

        [TestInitialize]
        public void SetUp()
        {
            this.names = new SimpleSortedList<string>();
        }

        [TestMethod]
        public void TestEmptyCtor()
        {
            this.names = new SimpleSortedList<string>();
            Assert.AreEqual(this.names.Capacity, 16, "Default capacity should be {0} but actual capacity is {1}.", 16, this.names.Capacity);
            Assert.AreEqual(this.names.Size, 0);
        }

        [TestMethod]
        public void TestCtorWithInitialCapacity()
        {
            this.names = new SimpleSortedList<string>(20);
            Assert.AreEqual(this.names.Capacity, 20, "Initial capacity should be {0} but actual capacity is {1}.", 20, this.names.Capacity);
            Assert.AreEqual(this.names.Size, 0);
        }

        [TestMethod]
        public void TestCtorWithAllParams()
        {
            this.names = new SimpleSortedList<string>(30, StringComparer.OrdinalIgnoreCase);
            Assert.AreEqual(this.names.Capacity, 30, "Initial capacity should be {0} but actual capacity is {1}.", 30, this.names.Capacity);
            Assert.AreEqual(this.names.Size, 0);
        }

        [TestMethod]
        public void TestCtorWithInitialComparer()
        {
            this.names = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase);
            Assert.AreEqual(this.names.Capacity, 16, "Default capacity should be {0} but actual capacity is {1}.", 16, this.names.Capacity);
            Assert.AreEqual(this.names.Size, 0);
        }

        [TestMethod]
        public void TestAddIncreasesSize()
        {
            this.names.Add("Nasko");
            Assert.AreEqual(1, this.names.Size);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddNullShouldThrowException()
        {
            this.names.Add(null);
        }

        [TestMethod]
        public void TestAddUnsortedDataIsHeldSorted()
        {
            List<string> expectedOrder = new List<string>() {"Balkan", "Georgi", "Rosen"};
            this.names.Add("Rosen");
            this.names.Add("Georgi");
            this.names.Add("Balkan");

            int count = 0;
            foreach (var name in this.names)
            {
                Assert.AreEqual(name, expectedOrder[count]);
                count++;
            }
        }

        [TestMethod]
        public void TestAddingMoreThanInitialCapacity()
        {
            string[] elementsToAdd = "0 1 2 3 4 5 6 7 8 9 a b c d e f g".Split();
            for (int i = 0; i < elementsToAdd.Length; i++)
            {
                this.names.Add(elementsToAdd[i]);
            }
            Assert.AreEqual(17, this.names.Size);
            Assert.AreNotEqual(16, this.names.Size);
        }

        [TestMethod]
        public void TestAddAllFromCollectionIncreasesSize()
        {
            List<string> expected = new List<string>() {"Georgi", "Dimitar"};
            this.names.AddAll(expected);
            Assert.AreEqual(2, this.names.Size);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddingAllFromNullThrowsException()
        {
            List<string> collection = null;
            this.names.AddAll(collection);
        }

        [TestMethod]
        public void TestAddAllKeepsSorted()
        {
            SortedSet<string> set = new SortedSet<string>() { "Panayot", "Roicho", "Boyan", "Asen" };
            this.names.AddAll(set);

            List<string> getValuesFromCustomCollection = new List<string>();
            foreach (var name in this.names)
            {
                getValuesFromCustomCollection.Add(name);
            }

            CollectionAssert.AreEqual(getValuesFromCustomCollection, set);
        }

        [TestMethod]
        public void TestRemoveValidElementDecreasesSize()
        {
            this.names.Add("Pesho");
            this.names.Remove("Pesho");

            Assert.AreEqual(0, this.names.Size);
        }

        [TestMethod]
        public void TestRemoveValidElementRemoveSelectedOne()
        {
            this.names.Add("Ivan");
            this.names.Add("Nasko");
            this.names.Remove("Ivan");
            Assert.AreNotSame("Ivan", this.names.InnerCollection[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRemovingNullThrowsException()
        {
            this.names.Remove(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestJoinWithNull()
        {
            this.names.Add("Ivan");
            this.names.Add("Atanas");
            string result = this.names.JoinWith(null);
        }

        [TestMethod]
        public void TestJoinWorksFine()
        {
            string expected = "Atanas Ivan";
            this.names.Add("Ivan");
            this.names.Add("Atanas");
            string actual = this.names.JoinWith(" ");
            Assert.AreEqual(expected, actual);
        }
    }
}
