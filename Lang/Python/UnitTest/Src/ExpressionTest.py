import traceback
import string
import os.path
import sys
from FDO import *
import unittest

class ExpressionTest(unittest.TestCase):
    def _parseExpression(self, exprText, exprResult = None, expectedType = None):
        expr = FdoExpression.Parse(exprText)
        self.assertIsNotNone(expr)
        exprStr = expr.ToString()
        self.assertIsNotNone(exprStr)
        if not exprResult is None:
            self.assertEqual(exprResult, exprStr)
        else:
            self.assertEqual(exprText, exprStr)
        if not expectedType is None:
            self.assertEqual(expectedType, expr.ExpressionType)

    def testExpressionParse(self):
        self._parseExpression("SELECT(CLS,P1,'P1 >= 45') AS MyName", "( SELECT(CLS,P1,'P1 >= 45') ) AS MyName", FdoExpressionItemType_ComputedIdentifier)
        self._parseExpression("4", None, FdoExpressionItemType_DataValue)
        self._parseExpression("4.5", None, FdoExpressionItemType_DataValue)
        self._parseExpression("TRUE", None, FdoExpressionItemType_DataValue)
        self._parseExpression("true", "TRUE", FdoExpressionItemType_DataValue)
        self._parseExpression("True", "TRUE", FdoExpressionItemType_DataValue)
        self._parseExpression("4+4.5", None, FdoExpressionItemType_BinaryExpression)
        self._parseExpression("4.5+5.4*6.6-3.2/12", None, FdoExpressionItemType_BinaryExpression)
        self._parseExpression("-3.4", None, FdoExpressionItemType_DataValue)
        self._parseExpression("-3.4-5.6", None, FdoExpressionItemType_BinaryExpression)
        self._parseExpression("-(3.4*5.6)", None, FdoExpressionItemType_UnaryExpression)
        self._parseExpression("-'abc'","-('abc')", FdoExpressionItemType_UnaryExpression)
        self._parseExpression("-'abc'-'abc'","-('abc')-'abc'", FdoExpressionItemType_BinaryExpression)
        self._parseExpression("(2*6)-(3*5)", "2*6-3*5", FdoExpressionItemType_BinaryExpression)
        self._parseExpression("-TRUE", "-(TRUE)", FdoExpressionItemType_UnaryExpression)
        self._parseExpression("'abc'", None, FdoExpressionItemType_DataValue)
        self._parseExpression("'abc''def'", None, FdoExpressionItemType_DataValue)
        self._parseExpression("a+b*34/12", None, FdoExpressionItemType_BinaryExpression)
        
        self._parseExpression("12345678901234567", None, FdoExpressionItemType_DataValue)
    #if MONO
        #self._parseExpression("123456789012345678901", "1.23456789012346e+20", FdoExpressionItemType_DataValue)
    #else
        #self._parseExpression("123456789012345678901", "1.23456789012346e+020", FdoExpressionItemType_DataValue)
    #endif
        self._parseExpression("1.2e13", "12000000000000", FdoExpressionItemType_DataValue)
        self._parseExpression("-2 --2 +2 ++2", "-2--2+2+2", FdoExpressionItemType_BinaryExpression)

        # dates 
        self._parseExpression("DATE '1951-12-24'", None, FdoExpressionItemType_DataValue)
        self._parseExpression("DATE '1971-12-24'", None, FdoExpressionItemType_DataValue)

        # time is meaningless since it would be stored since Jan 1, 1970
        self._parseExpression("TIME '12:01:30'", None, FdoExpressionItemType_DataValue)
        self._parseExpression("TIMESTAMP '2003-10-23 11:00:02'", None, FdoExpressionItemType_DataValue)

        # compound expressions (some from Oracle manual)
        self._parseExpression("SQRT(144)", None, FdoExpressionItemType_Function)
        self._parseExpression("SQRT('abc')", None, FdoExpressionItemType_Function)
        self._parseExpression("SQRT(SQRT(2)+3)*2", None, FdoExpressionItemType_BinaryExpression)
        self._parseExpression("my_fun(TO_CHAR('abc','DD-MMM-YY'))", "my_fun(TO_CHAR('abc', 'DD-MMM-YY'))", FdoExpressionItemType_Function)
        self._parseExpression(":EnterName", None, FdoExpressionItemType_Parameter)
        self._parseExpression(":'Enter Name'", None, FdoExpressionItemType_Parameter)
        self._parseExpression("MIN(a,b,2*3+45.67,d,e)", "MIN(a, b, 2*3+45.67, d, e)", FdoExpressionItemType_Function)
        self._parseExpression("'CLARK' || 'SMITH'", "'CLARK'+'SMITH'", FdoExpressionItemType_BinaryExpression)
        self._parseExpression("'Lewis'+'Clark'", None, FdoExpressionItemType_BinaryExpression)
        self._parseExpression("LENGTH('MOOSE') * 57", "LENGTH('MOOSE')*57", FdoExpressionItemType_BinaryExpression)
        self._parseExpression("Pipes.state", None, FdoExpressionItemType_Identifier)
        self._parseExpression("\"Pipes . state\"", None, FdoExpressionItemType_Identifier)
        self._parseExpression("\"%one two thre#EE\"", None, FdoExpressionItemType_Identifier)
        self._parseExpression("'%one two thre#EE'", None, FdoExpressionItemType_DataValue)
    #if MONO
        #self._parseExpression("2.3e400", "inf", FdoExpressionItemType_DataValue)
    #else
        #self._parseExpression("2.3e400", "1.#INF", FdoExpressionItemType_DataValue)
    #endif
        self._parseExpression("(2+3)*12", None, FdoExpressionItemType_BinaryExpression)
        self._parseExpression("sqrt(144)+(12-32/12)", "sqrt(144)+12-32/12", FdoExpressionItemType_BinaryExpression)
        self._parseExpression("( Width*Height ) AS Area", None, FdoExpressionItemType_ComputedIdentifier)

    def testDataValue(self):
        bool1 = FdoDataValue.Create(True)
        self.assertEqual(bool1.DataType, FdoDataType_Boolean)
        bool2 = FdoDataValue.Create(False)
        self.assertEqual(bool2.DataType, FdoDataType_Boolean)
        short1 = FdoDataValue.Create(float(3.14))
        self.assertEqual(short1.DataType, FdoDataType_Single)
        str1 = FdoDataValue.Create("abcd")
        self.assertEqual(str1.DataType, FdoDataType_String)