using OSGeo.FDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class FilterTests
    {
        // parse an FDO Filter that we except to return NULL because of error
        void ParseFilterToNull(string pwzFilter)
        {
            /*
            Assert.Throws<ManagedFdoException>(() =>
            {
                FdoFilter filter = FdoFilter.Parse(pwzFilter);
            });
             */
            // get root node of expression parse tree
            FdoFilter  pFilter = null;
            try
            {
                pFilter = FdoFilter.Parse(pwzFilter);
            }
            catch (ManagedFdoException)
            {
                return;
            }
            Assert.Null(pFilter); //, "FdoFilter::Parse() should have returned NULL!");
        }

        // parse an FDO Filter
        void ParseFilter<T>(string pwzFilter, string pwzResult = null, Type expectedType = null) where T : FdoFilter
        {
	        // get root node of expression parse tree
	        FdoFilter pFilter = FdoFilter.Parse(pwzFilter);
            Assert.NotNull(pFilter);
            
	        // output back to string if successful
	        string pwzOut = pFilter.ToString();
            Assert.NotNull(pwzOut);

            if (pwzResult == null)
            {
                Assert.Equal(pwzOut, pwzFilter); //, "Parse/ToString do not match!\n\t<{0}> should be <{1}>\n", pwzOut, pwzFilter);
            }
            else
            {
                Assert.Equal(pwzOut, pwzResult); //, "Parse/ToString do not match!\n\t<{0}> should be <{1}>\n", pwzOut, pwzResult);
            }

            Assert.IsAssignableFrom<T>(pFilter);
        }

        [Fact]
        public void TestParse()
        {
            ParseFilter<FdoComparisonCondition>("colourIndex > -a", "colourIndex > -(a)");
            ParseFilter<FdoComparisonCondition>("colourIndex > -(5)");
            ParseFilter<FdoComparisonCondition>("colourIndex > -5");
            ParseFilter<FdoComparisonCondition>("colourIndex < -5");
            ParseFilter<FdoComparisonCondition>("colourIndex >= -5");
            ParseFilter<FdoComparisonCondition>("colourIndex <= -5");
            ParseFilter<FdoComparisonCondition>("\"colour$Index\" > -a", "\"colour$Index\" > -(a)");
            ParseFilter<FdoBinaryLogicalOperator>("(Id = 1 and Name = 'Fred') or (Id = 2 and Name = 'John')",
		        "Id = 1 AND Name = 'Fred' OR Id = 2 AND Name = 'John'");

	        // identifiers with special characters
            ParseFilter<FdoComparisonCondition>("\"A\"\"B\" = '123'");
            ParseFilter<FdoComparisonCondition>("\"@#$%OR\" = '123'");	// DID 550139
            ParseFilter<FdoComparisonCondition>("\"OR\" = '123'");
            ParseFilter<FdoBinaryLogicalOperator>("\"or\" = '123' OR \"Or\" = 'abc'");

	        // grammatical nightmares
	        ParseFilterToNull("1 12345678901234567 1.234 1.2e13 -2 --2 +2 ++2");
	        ParseFilterToNull("+ - * / < <= = == >= > : || @");
	        ParseFilterToNull(" AND BEYOND BOUNDINGBOX COMPARE CONTAINS COVEREDBY CROSSES DATE DAY DISJOINT DISTANCE ENVELOPEINTERSECTS EQUALS FALSE HOUR IN INSIDE INTERSECTS LIKE MINUTE MONTH NOT NULL OR OVERLAPS SECOND SPATIAL TIME TIMESTAMP TOUCHES TRUE WITHIN YEAR");
	        ParseFilterToNull(" And beyond BoundingBox Compare conTAINS coveredby");
	        ParseFilterToNull(" \"missing quote");
        //?	ParseFilterToNull("tooooolonnnnngggggidentifierakdadjsaljdasljlasjdsljdslj");
	        ParseFilterToNull("\"tooooooolonnngggstringliteralsasad;sklad;sls                                                                                                                                                                                                              ");
	        ParseFilterToNull("+=4=");

            ParseFilter<FdoComparisonCondition>("a<4", "a < 4");
            ParseFilter<FdoBinaryLogicalOperator>("a<4 or col= 2",
                "a < 4 OR col = 2");
            ParseFilter<FdoBinaryLogicalOperator>("a = b OR c = d");
            ParseFilter<FdoBinaryLogicalOperator>("a = b AND c = d");
            ParseFilter<FdoBinaryLogicalOperator>("a = b OR c = d or a = b AND c = d",
                "a = b OR c = d OR a = b AND c = d");
            ParseFilter<FdoBinaryLogicalOperator>("a = b AND c = d or a = b AND c = d",
                "a = b AND c = d OR a = b AND c = d");
            ParseFilter<FdoComparisonCondition>("a = b");
            ParseFilter<FdoComparisonCondition>("a <> b");
            ParseFilter<FdoComparisonCondition>("a != b",
		        "a <> b");
            ParseFilter<FdoComparisonCondition>("a < b");
            ParseFilter<FdoComparisonCondition>("a <= b");
            ParseFilter<FdoComparisonCondition>("a > b");
            ParseFilter<FdoComparisonCondition>("a >= b");
            ParseFilter<FdoComparisonCondition>("a LIKE 'abc*'");
            ParseFilter<FdoComparisonCondition>("\"Feature Number\" = 4642");

	        // Sascha Nikolayev sample
            ParseFilter<FdoBinaryLogicalOperator>("col1 > 10 and col2 in (1,2,3,4)",
                "col1 > 10 AND col2 IN (1, 2, 3, 4)");

            // Example from Vinay.  Identifier were only returning their name, not 
            // the fully qualified text.
            ParseFilter<FdoUnaryLogicalOperator>("NOT (Entity.Color NULL)");
            ParseFilter<FdoInCondition>("Entity.Color IN ('Red', 'Blue', 'Green')");

	        // Examples from FDO Expressions and FiltersRpt.doc
	        // Hence a filter to select roads with four or more lanes might look like:
            ParseFilter<FdoComparisonCondition>("Lanes >= 4");

	        // Similarly in the Civil model a filter to select all PipeNetworks that
	        // have at least one Pipe in the proposed state might look like:
            ParseFilter<FdoComparisonCondition>("Pipes.state = 'proposed'");

	        // Also using the Civil model a filter to select all existing Parcels
	        // whose owner contains the text "Smith" might look something like:
            ParseFilter<FdoBinaryLogicalOperator>("state = 'existing' and owner like '%Smith%'",
                "state = 'existing' AND owner LIKE '%Smith%'");

	        // Likewise a filter to select all Parcels that are either affected
	        // or encroached upon by some change might look like:
            ParseFilter<FdoInCondition>("state in ('affected', 'encroached')",
                "state IN ('affected', 'encroached')");

            ParseFilter<FdoInCondition>("state in (:'Enter State', 'encroached', TRUE, 123.333, 14)",
                "state IN (:'Enter State', 'encroached', TRUE, 123.333, 14)");

	        // Distance Conditions
            ParseFilter<FdoGeometricCondition>("feature beyond GeomFromText('POINT(0.0 0.0)') 12.0",
                "feature BEYOND GeomFromText('POINT (0 0)') 12");
            ParseFilter<FdoGeometricCondition>("feature withindistance GeomFromText('POINT(0.0 0.0)') 12",
                "feature WITHINDISTANCE GeomFromText('POINT (0 0)') 12");
            ParseFilter<FdoGeometricCondition>("feature withindistance GeomFromText('POINT(0.0 0.0)') 12.0",
                "feature WITHINDISTANCE GeomFromText('POINT (0 0)') 12");

	        // Spatial Conditions
            ParseFilter<FdoGeometricCondition>("feature contains GeomFromText('POINT(0.0 0.0)')",
                "feature CONTAINS GeomFromText('POINT (0 0)')");
            ParseFilter<FdoGeometricCondition>("feature crosses GeomFromText('POINT(0.0 0.0)')",
                "feature CROSSES GeomFromText('POINT (0 0)')");
            ParseFilter<FdoGeometricCondition>("feature disjoint GeomFromText('POINT(0.0 0.0)')",
                "feature DISJOINT GeomFromText('POINT (0 0)')");
            ParseFilter<FdoGeometricCondition>("feature equals GeomFromText('POINT(0.0 0.0)')",
                "feature EQUALS GeomFromText('POINT (0 0)')");
            ParseFilter<FdoGeometricCondition>("feature intersects GeomFromText('POINT(0.0 0.0)')",
                "feature INTERSECTS GeomFromText('POINT (0 0)')");
            ParseFilter<FdoGeometricCondition>("feature overlaps GeomFromText('POINT(0.0 0.0)')",
                "feature OVERLAPS GeomFromText('POINT (0 0)')");
            ParseFilter<FdoGeometricCondition>("feature touches GeomFromText('POINT(0.0 0.0)')",
                "feature TOUCHES GeomFromText('POINT (0 0)')");
            ParseFilter<FdoGeometricCondition>("feature within GeomFromText('POINT(0.0 0.0)')",
                "feature WITHIN GeomFromText('POINT (0 0)')");
            ParseFilter<FdoGeometricCondition>("feature coveredby GeomFromText('POINT(0.0 0.0)')",
                "feature COVEREDBY GeomFromText('POINT (0 0)')");
            ParseFilter<FdoGeometricCondition>("feature inside GeomFromText('POINT(0.0 0.0)')",
                "feature INSIDE GeomFromText('POINT (0 0)')");
            ParseFilter<FdoGeometricCondition>("feature envelopeintersects GeomFromText('POINT(0.0 0.0)')",
                "feature ENVELOPEINTERSECTS GeomFromText('POINT (0 0)')");

            ParseFilter<FdoBinaryLogicalOperator>("feature touches GeomFromText('POINT(0.0 0.0)') AND feature beyond GeomFromText('POINT(0.0 0.0)') 12.0 AND state = 'troubled'",
                "feature TOUCHES GeomFromText('POINT (0 0)') AND feature BEYOND GeomFromText('POINT (0 0)') 12 AND state = 'troubled'");

            ParseFilter<FdoComparisonCondition>("( NumberOfLanes*Speed ) AS Capacity, Capacity > 200");

            ParseFilter<FdoBinaryLogicalOperator>("( Width*Length ) AS Area, Area > 100 AND Area < 1000");
            ParseFilter<FdoBinaryLogicalOperator>("( (Width+Length)*2 ) AS perimeter, perimeter > 100 AND perimeter < 1000");
            ParseFilter<FdoBinaryLogicalOperator>("( (Width+Length)*2 ) AS perimeter, ( Width*Length ) AS Area, Area > 1000 OR perimeter < 100");
            ParseFilter<FdoComparisonCondition>("( (Width+Length)*2 ) AS perimeter, ( Width*Length ) AS Area, Area > perimeter");

            ParseFilter<FdoInCondition>("FeatId IN ( SELECT (CLS, (PROP*30) AS P1, P1 >= 45, JOIN(CLS2 AS CC, JOININNER, CLS.P1=CC.P2), JOIN(CLS2 AS CC2, JOINRIGHTOUTER, CLS.P1=CC2.P2)) )",
                "FeatId IN (SELECT(CLS,( PROP*30 ) AS P1,P1 >= 45,JOIN(CLS2 AS CC,JOININNER,CLS.P1 = CC.P2),JOIN(CLS2 AS CC2,JOINRIGHTOUTER,CLS.P1 = CC2.P2)))");
            ParseFilter<FdoInCondition>("FeatId IN ( SELECT (CLS, (PROP*30) AS P1, P1=45))",
                "FeatId IN (SELECT(CLS,( PROP*30 ) AS P1,P1 = 45))");
            ParseFilter<FdoInCondition>("FeatId IN ( SELECT (CLS, (CLS.PROP*30 + \"CLS\".P0+ CLS.\"P2\") AS P1, \"CLS\".\"P1\"=45))",
                "FeatId IN (SELECT(CLS,( CLS.PROP*30+CLS.P0+CLS.P2 ) AS P1,CLS.P1 = 45))");
        }

        [Fact]
        public void TestSqlInjectionProtection()
        {
            string suspectQuery = "RNAME = '{0}'";

            var tests = new[]
            {
                new { sql = "a'; DROP TABLE Parcels; SELECT * FROM Parcels WHERE '1' = '1", testTokens = new string[] { "DROP", ";" } },
                new { sql = "b'; DELETE FROM Parcels; SELECT * FROM Parcels WHERE '1' = '1", testTokens = new string[] { "DELETE", ";" } },
                new { sql = "c'; UPDATE Parcels SET RNAME = ''; SELECT * FROM Parcels WHERE '1' = '1", testTokens = new string[] { "UPDATE", "SET", ";" } }
            };

            int testNo = 1;
            foreach (var t in tests)
            {
                try
                {
                    string origFilter = string.Format(suspectQuery, t.sql);
                    FdoFilter filter = FdoFilter.Parse(origFilter);
                    string filterStr = filter.ToString();
                    Console.WriteLine("Test {0}: Drop table attempt\n   Original: {1}\n   Parsed: {2}", testNo, origFilter, filterStr);
                    Assert.False(t.testTokens.Any(tok => filterStr.ToUpper().Contains(tok.ToUpper())));
                }
                catch (ManagedFdoException ex)
                {
                    Console.WriteLine("Test {0}: Drop table attempt - {1}", testNo, ex.Message);
                }
                testNo++;
            }
        }
    }
}
