using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Summary description for MultiSetTest
    /// </summary>
[TestClass]
public class MultiSetTest
{
    public MultiSetTest()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private TestContext testContextInstance;

    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext
    {
        get
        {
            return testContextInstance;
        }
        set
        {
            testContextInstance = value;
        }
    }

    #region Additional test attributes
    //
    // You can use the following additional attributes as you write your tests:
    //
    // Use ClassInitialize to run code before running the first test in the class
    // [ClassInitialize()]
    // public static void MyClassInitialize(TestContext testContext) { }
    //
    // Use ClassCleanup to run code after all tests in a class have run
    // [ClassCleanup()]
    // public static void MyClassCleanup() { }
    //
    // Use TestInitialize to run code before running each test 
    // [TestInitialize()]
    // public void MyTestInitialize() { }
    //
    // Use TestCleanup to run code after each test has run
    // [TestCleanup()]
    // public void MyTestCleanup() { }
    //
    #endregion

    [TestMethod]
    public void TestCount()
    {
        MultiSet<string> empty = new MultiSet<string>();
        Assert.AreEqual<int>(0, empty.Count);

        empty.Add("foo");
        empty.Add("bar");
        Assert.AreEqual<int>(2, empty.Count);

        empty.Add("foo");
        empty.Add("foo");
        Assert.AreEqual<int>(4, empty.Count);
    }

    [TestMethod]
    public void TestEmptyEquals()
    {
        MultiSet<string> a = new MultiSet<string>();
        MultiSet<string> b = new MultiSet<string>();

        Assert.AreEqual<IEquatable<MultiSet<string>>>(a, b);
    }

    [TestMethod]
    public void TestEquals()
    {
        MultiSet<String> requirements = new MultiSet<string>();
        requirements.Add("DAIRY");
        requirements.Add("PLANT");
        requirements.Add("PLANT");

        MultiSet<String> available = new MultiSet<string>();
        available.Add("PLANT");
        available.Add("PLANT");
        available.Add("DAIRY");

        Assert.AreEqual(requirements, available);
        Assert.AreEqual<int>(requirements.GetHashCode(), available.GetHashCode());
    }

    [TestMethod]
    public void TestEmptyExcept()
    {
        MultiSet<string> a = new MultiSet<string>();
        MultiSet<string> b = new MultiSet<string>();

        MultiSet<string> diff = a.Except(b);
        Assert.AreEqual<int>(0, diff.Count);
    }

    [TestMethod]
    public void TestExcept()
    {
        MultiSet<String> requirements = new MultiSet<string>();
        requirements.Add("DAIRY");
        requirements.Add("PLANT");
        requirements.Add("PLANT");

        MultiSet<String> available = new MultiSet<string>();
        available.Add("PLANT");
        available.Add("PLANT");
        available.Add("DAIRY");

        Assert.AreEqual(requirements.Except(available), new MultiSet<string>());
        Assert.AreEqual(available.Except(requirements), new MultiSet<String>());
    }
}

