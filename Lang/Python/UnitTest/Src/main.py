#
# Copyright (C) 2004-2007  Autodesk, Inc.
# 
# This library is free software; you can redistribute it and/or
# modify it under the terms of version 2.1 of the GNU Lesser
# General Public License as published by the Free Software Foundation.
# 
# This library is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
# Lesser General Public License for more details.
# 
# You should have received a copy of the GNU Lesser General Public
# License along with this library; if not, write to the Free Software
# Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
#

# Import Python libs
import sys
sys.path.append("../../Bin/x64/Release")
import traceback
import unittest

# Import unit tests
from ClientServicesTest import ClientServicesTest
from TypeMapTest import TypeMapTest
from CapabilitiesTest import CapabilitiesTest
from ConnectTest import ConnectTest
from ExpressionTest import ExpressionTest
from GeometryTest import GeometryTest
from SpatialContextTest import SpatialContextTest
from SchemaTest import SchemaTest
from SelectAggregateTest import SelectAggregateTest
from SelectTest import SelectTest
from InsertTest import InsertTest
from UpdateTest import UpdateTest
from DeleteTest import DeleteTest
from FilterTest import FilterTest

if __name__ == "__main__":
	suite = unittest.TestSuite()
	loader = unittest.TestLoader()
	suite.addTests(loader.loadTestsFromModule(ClientServicesTest))
	suite.addTests(loader.loadTestsFromModule(TypeMapTest))
	suite.addTests(loader.loadTestsFromModule(CapabilitiesTest))
	suite.addTests(loader.loadTestsFromModule(ConnectTest))
	suite.addTests(loader.loadTestsFromModule(ExpressionTest))
	suite.addTests(loader.loadTestsFromModule(GeometryTest))
	suite.addTests(loader.loadTestsFromModule(SpatialContextTest))
	suite.addTests(loader.loadTestsFromModule(SchemaTest))
	suite.addTests(loader.loadTestsFromModule(SelectAggregateTest))
	suite.addTests(loader.loadTestsFromModule(SelectTest))
	suite.addTests(loader.loadTestsFromModule(InsertTest))
	suite.addTests(loader.loadTestsFromModule(UpdateTest))
	suite.addTests(loader.loadTestsFromModule(DeleteTest))
	suite.addTests(loader.loadTestsFromModule(FilterTest))

	unittest.main()

