import traceback
import string
import os
import sys
import shutil
from FDO import *
import unittest

class FilterTest(unittest.TestCase):
    def _parseFilterToNull(self, pwzFilter):
        pFilter = None
        try:
            pFilter = FdoFilter.Parse(pwzFilter)
        except:
            return None
        self.assertIsNone(pFilter)

    def _parseFilter(self, pwzFilter, pwzResult = None):
        pFilter = FdoFilter.Parse(pwzFilter)
        self.assertIsNotNone(pFilter)
        pwzOut = pFilter.ToString()
        self.assertIsNotNone(pwzOut)
        if pwzResult is None:
            self.assertEqual(pwzOut, pwzFilter)
        else:
            self.assertEqual(pwzOut, pwzResult)

    def testParse(self):
        self._parseFilter("colourIndex > -a", "colourIndex > -(a)")
        self._parseFilter("colourIndex > -(5)")
        self._parseFilter("colourIndex > -5")
        self._parseFilter("colourIndex < -5")
        self._parseFilter("colourIndex >= -5")
        self._parseFilter("colourIndex <= -5")
        self._parseFilter("\"colour$Index\" > -a", "\"colour$Index\" > -(a)")
        self._parseFilter("(Id = 1 and Name = 'Fred') or (Id = 2 and Name = 'John')", "Id = 1 AND Name = 'Fred' OR Id = 2 AND Name = 'John'")

        # identifiers with special characters
        self._parseFilter("\"A\"\"B\" = '123'")
        self._parseFilter("\"@#$%OR\" = '123'")  # DID 550139
        self._parseFilter("\"OR\" = '123'")
        self._parseFilter("\"or\" = '123' OR \"Or\" = 'abc'")

        # grammatical nightmares
        self._parseFilterToNull("1 12345678901234567 1.234 1.2e13 -2 --2 +2 ++2")
        self._parseFilterToNull("+ - * / < <= = == >= > : || @")
        self._parseFilterToNull(" AND BEYOND BOUNDINGBOX COMPARE CONTAINS COVEREDBY CROSSES DATE DAY DISJOINT DISTANCE ENVELOPEINTERSECTS EQUALS FALSE HOUR IN INSIDE INTERSECTS LIKE MINUTE MONTH NOT NULL OR OVERLAPS SECOND SPATIAL TIME TIMESTAMP TOUCHES TRUE WITHIN YEAR")
        self._parseFilterToNull(" And beyond BoundingBox Compare conTAINS coveredby")
        self._parseFilterToNull(" \"missing quote")
        #? self._parseFilterToNull("tooooolonnnnngggggidentifierakdadjsaljdasljlasjdsljdslj")
        self._parseFilterToNull("\"tooooooolonnngggstringliteralsasad;sklad;sls                                                                                                                                                                                                              ")
        self._parseFilterToNull("+=4=")

        self._parseFilter("a<4", "a < 4")
        self._parseFilter("a<4 or col= 2", "a < 4 OR col = 2")
        self._parseFilter("a = b OR c = d")
        self._parseFilter("a = b AND c = d")
        self._parseFilter("a = b OR c = d or a = b AND c = d", "a = b OR c = d OR a = b AND c = d")
        self._parseFilter("a = b AND c = d or a = b AND c = d", "a = b AND c = d OR a = b AND c = d")
        self._parseFilter("a = b")
        self._parseFilter("a <> b")
        self._parseFilter("a != b", "a <> b")
        self._parseFilter("a < b")
        self._parseFilter("a <= b")
        self._parseFilter("a > b")
        self._parseFilter("a >= b")
        self._parseFilter("a LIKE 'abc*'")
        self._parseFilter("\"Feature Number\" = 4642")

        # Sascha Nikolayev sample
        self._parseFilter("col1 > 10 and col2 in (1,2,3,4)", "col1 > 10 AND col2 IN (1, 2, 3, 4)")

        # Example from Vinay.  Identifier were only returning their name, not 
        # the fully qualified text.
        self._parseFilter("NOT (Entity.Color NULL)")
        self._parseFilter("Entity.Color IN ('Red', 'Blue', 'Green')")

        # Examples from FDO Expressions and FiltersRpt.doc
        # Hence a filter to select roads with four or more lanes might look like:
        self._parseFilter("Lanes >= 4")

        # Similarly in the Civil model a filter to select all PipeNetworks that
        # have at least one Pipe in the proposed state might look like:
        self._parseFilter("Pipes.state = 'proposed'")

        # Also using the Civil model a filter to select all existing Parcels
        # whose owner contains the text "Smith" might look something like:
        self._parseFilter("state = 'existing' and owner like '%Smith%'", "state = 'existing' AND owner LIKE '%Smith%'")

        # Likewise a filter to select all Parcels that are either affected
        # or encroached upon by some change might look like:
        self._parseFilter("state in ('affected', 'encroached')", "state IN ('affected', 'encroached')")

        self._parseFilter("state in (:'Enter State', 'encroached', TRUE, 123.333, 14)", "state IN (:'Enter State', 'encroached', TRUE, 123.333, 14)")

        # Distance Conditions
        self._parseFilter("feature beyond GeomFromText('POINT(0.0 0.0)') 12.0", "feature BEYOND GeomFromText('POINT (0 0)') 12")
        self._parseFilter("feature withindistance GeomFromText('POINT(0.0 0.0)') 12", "feature WITHINDISTANCE GeomFromText('POINT (0 0)') 12")
        self._parseFilter("feature withindistance GeomFromText('POINT(0.0 0.0)') 12.0", "feature WITHINDISTANCE GeomFromText('POINT (0 0)') 12")

        # Spatial Conditions
        self._parseFilter("feature contains GeomFromText('POINT(0.0 0.0)')", "feature CONTAINS GeomFromText('POINT (0 0)')")
        self._parseFilter("feature crosses GeomFromText('POINT(0.0 0.0)')", "feature CROSSES GeomFromText('POINT (0 0)')")
        self._parseFilter("feature disjoint GeomFromText('POINT(0.0 0.0)')", "feature DISJOINT GeomFromText('POINT (0 0)')")
        self._parseFilter("feature equals GeomFromText('POINT(0.0 0.0)')", "feature EQUALS GeomFromText('POINT (0 0)')")
        self._parseFilter("feature intersects GeomFromText('POINT(0.0 0.0)')", "feature INTERSECTS GeomFromText('POINT (0 0)')")
        self._parseFilter("feature overlaps GeomFromText('POINT(0.0 0.0)')", "feature OVERLAPS GeomFromText('POINT (0 0)')")
        self._parseFilter("feature touches GeomFromText('POINT(0.0 0.0)')", "feature TOUCHES GeomFromText('POINT (0 0)')")
        self._parseFilter("feature within GeomFromText('POINT(0.0 0.0)')", "feature WITHIN GeomFromText('POINT (0 0)')")
        self._parseFilter("feature coveredby GeomFromText('POINT(0.0 0.0)')", "feature COVEREDBY GeomFromText('POINT (0 0)')")
        self._parseFilter("feature inside GeomFromText('POINT(0.0 0.0)')", "feature INSIDE GeomFromText('POINT (0 0)')")
        self._parseFilter("feature envelopeintersects GeomFromText('POINT(0.0 0.0)')", "feature ENVELOPEINTERSECTS GeomFromText('POINT (0 0)')")

        self._parseFilter("feature touches GeomFromText('POINT(0.0 0.0)') AND feature beyond GeomFromText('POINT(0.0 0.0)') 12.0 AND state = 'troubled'", "feature TOUCHES GeomFromText('POINT (0 0)') AND feature BEYOND GeomFromText('POINT (0 0)') 12 AND state = 'troubled'")

        self._parseFilter("( NumberOfLanes*Speed ) AS Capacity, Capacity > 200")

        self._parseFilter("( Width*Length ) AS Area, Area > 100 AND Area < 1000")
        self._parseFilter("( (Width+Length)*2 ) AS perimeter, perimeter > 100 AND perimeter < 1000")
        self._parseFilter("( (Width+Length)*2 ) AS perimeter, ( Width*Length ) AS Area, Area > 1000 OR perimeter < 100")
        self._parseFilter("( (Width+Length)*2 ) AS perimeter, ( Width*Length ) AS Area, Area > perimeter")

        self._parseFilter("FeatId IN ( SELECT (CLS, (PROP*30) AS P1, P1 >= 45, JOIN(CLS2 AS CC, JOININNER, CLS.P1=CC.P2), JOIN(CLS2 AS CC2, JOINRIGHTOUTER, CLS.P1=CC2.P2)) )", "FeatId IN (SELECT(CLS,( PROP*30 ) AS P1,P1 >= 45,JOIN(CLS2 AS CC,JOININNER,CLS.P1 = CC.P2),JOIN(CLS2 AS CC2,JOINRIGHTOUTER,CLS.P1 = CC2.P2)))")
        self._parseFilter("FeatId IN ( SELECT (CLS, (PROP*30) AS P1, P1=45))", "FeatId IN (SELECT(CLS,( PROP*30 ) AS P1,P1 = 45))")
        self._parseFilter("FeatId IN ( SELECT (CLS, (CLS.PROP*30 + \"CLS\".P0+ CLS.\"P2\") AS P1, \"CLS\".\"P1\"=45))", "FeatId IN (SELECT(CLS,( CLS.PROP*30+CLS.P0+CLS.P2 ) AS P1,CLS.P1 = 45))")