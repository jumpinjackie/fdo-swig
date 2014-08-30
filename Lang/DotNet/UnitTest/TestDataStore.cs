using OSGeo.FDO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class TestBootstrap : IDisposable
    {
        public TestBootstrap()
        {
            FdoMemCheck.EnableMemoryLeakChecking();
        }

        public void Dispose()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            //long deallocations = FdoAllocationTracker.Deallocations;
            //System.Diagnostics.Debug.WriteLine("{0} objects cleaned up during this test run", deallocations);
            FdoMemCheck.DumpMemoryLeakResults();
        }
    }

    public class TestDataStore : Xunit.IUseFixture<TestBootstrap>
    {
        public const string SDF     = "../../../../../../TestData/SDF/World_Countries.sdf";
        public const string SHP     = "../../../../../../TestData/SHP/World_Countries.shp";
        public const string SHP_DIR = "../../../../../../TestData/SHP/";
        public const string SQLITE  = "../../../../../../TestData/SQLite/World_Countries.sqlite";

        public static void CopySDF(string targetPath)
        {
            File.Copy(SDF, targetPath, true);
        }

        public static void CopySHP(string targetDir, string targetName = "World_Countries")
        {
            string[] files = Directory.GetFiles(SHP_DIR);
            foreach (string f in files)
            {
                string ext = Path.GetExtension(f);
                string dstFile = Path.Combine(targetDir, targetName + ext);
                File.Copy(f, dstFile, true);
            }
        }

        public static void CopySQLite(string targetPath)
        {
            File.Copy(SQLITE, targetPath, true);
        }

        public TestBootstrap TestData { get; private set; }

        public void SetFixture(TestBootstrap data)
        {
            this.TestData = data;
        }
    }
}
