using NUnit.Framework;
using OSGeo.FDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [SetUpFixture]
    public class TestBootstrap
    {
        [SetUp]
        public void Setup()
        {
            FdoMemCheck.EnableMemoryLeakChecking();
        }

        [TearDown]
        public void Teardown()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            //long deallocations = FdoAllocationTracker.Deallocations;
            //System.Diagnostics.Debug.WriteLine("{0} objects cleaned up during this test run", deallocations);
            FdoMemCheck.DumpMemoryLeakResults();
        }
    }

    public class TestDataStore
    {
        public const string SDF     = "../../../../../../TestData/SDF/World_Countries.sdf";
        public const string SHP     = "../../../../../../TestData/SHP/World_Countries.shp";
        public const string SHP_DIR = "../../../../../../TestData/SHP/";
        public const string SQLITE  = "../../../../../../TestData/SQLite/World_Countries.sqlite";
    }
}
