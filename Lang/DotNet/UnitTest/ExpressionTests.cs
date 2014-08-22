using NUnit.Framework;
using OSGeo.FDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestFixture]
    public class ExpressionTests
    {
        private static void ParseExpression(string exprText, string exprResult = null, FdoExpressionItemType? expectedType = null)
        {
            FdoExpression expr = FdoExpression.Parse(exprText);
            Assert.NotNull(expr, "null FdoExpression parsing: ", exprText);

            string exprStr = expr.ToString();
            Assert.NotNull(exprStr);

            if (exprResult != null)
            {
                Assert.AreEqual(exprResult, exprStr);
            }
            else
            {
                Assert.AreEqual(exprText, exprStr);
            }

            if (expectedType.HasValue)
            {
                Assert.AreEqual(expectedType.Value, expr.ExpressionType);
                switch (expectedType.Value)
                {
                    case FdoExpressionItemType.FdoExpressionItemType_BinaryExpression:
                        Assert.IsInstanceOf<FdoBinaryExpression>(expr);
                        break;
                    case FdoExpressionItemType.FdoExpressionItemType_ComputedIdentifier:
                        Assert.IsInstanceOf<FdoComputedIdentifier>(expr);
                        break;
                    case FdoExpressionItemType.FdoExpressionItemType_DataValue:
                        Assert.IsInstanceOf<FdoDataValue>(expr);
                        break;
                    case FdoExpressionItemType.FdoExpressionItemType_Function:
                        Assert.IsInstanceOf<FdoFunction>(expr);
                        break;
                    case FdoExpressionItemType.FdoExpressionItemType_GeometryValue:
                        Assert.IsInstanceOf<FdoGeometryValue>(expr);
                        break;
                    case FdoExpressionItemType.FdoExpressionItemType_Identifier:
                        Assert.IsInstanceOf<FdoIdentifier>(expr);
                        break;
                    case FdoExpressionItemType.FdoExpressionItemType_Parameter:
                        Assert.IsInstanceOf<FdoParameter>(expr);
                        break;
                    case FdoExpressionItemType.FdoExpressionItemType_SubSelectExpression:
                        Assert.IsInstanceOf<FdoSubSelectExpression>(expr);
                        break;
                    case FdoExpressionItemType.FdoExpressionItemType_UnaryExpression:
                        Assert.IsInstanceOf<FdoUnaryExpression>(expr);
                        break;
                }
            }
        }

        [Test]
        public void TestExpressionParse()
        {
            ParseExpression("SELECT(CLS,P1,'P1 >= 45') AS MyName", "( SELECT(CLS,P1,'P1 >= 45') ) AS MyName", FdoExpressionItemType.FdoExpressionItemType_ComputedIdentifier);
	        ParseExpression("4", null, FdoExpressionItemType.FdoExpressionItemType_DataValue);
	        ParseExpression("4.5", null, FdoExpressionItemType.FdoExpressionItemType_DataValue);
	        ParseExpression("TRUE", null, FdoExpressionItemType.FdoExpressionItemType_DataValue);
	        ParseExpression("true", "TRUE", FdoExpressionItemType.FdoExpressionItemType_DataValue);
	        ParseExpression("True", "TRUE", FdoExpressionItemType.FdoExpressionItemType_DataValue);
	        ParseExpression("4+4.5", null, FdoExpressionItemType.FdoExpressionItemType_BinaryExpression);
	        ParseExpression("4.5+5.4*6.6-3.2/12", null, FdoExpressionItemType.FdoExpressionItemType_BinaryExpression);
	        ParseExpression("-3.4", null, FdoExpressionItemType.FdoExpressionItemType_DataValue);
	        ParseExpression("-3.4-5.6", null, FdoExpressionItemType.FdoExpressionItemType_BinaryExpression);
	        ParseExpression("-(3.4*5.6)", null, FdoExpressionItemType.FdoExpressionItemType_UnaryExpression);
	        ParseExpression("-'abc'","-('abc')", FdoExpressionItemType.FdoExpressionItemType_UnaryExpression);
	        ParseExpression("-'abc'-'abc'","-('abc')-'abc'", FdoExpressionItemType.FdoExpressionItemType_BinaryExpression);
	        ParseExpression("(2*6)-(3*5)", "2*6-3*5", FdoExpressionItemType.FdoExpressionItemType_BinaryExpression);
	        ParseExpression("-TRUE", "-(TRUE)", FdoExpressionItemType.FdoExpressionItemType_UnaryExpression);
	        ParseExpression("'abc'", null, FdoExpressionItemType.FdoExpressionItemType_DataValue);
	        ParseExpression("'abc''def'", null, FdoExpressionItemType.FdoExpressionItemType_DataValue);
	        ParseExpression("a+b*34/12", null, FdoExpressionItemType.FdoExpressionItemType_BinaryExpression);
	        //ParseExpression("“a", "a");
	        //ParseExpression("‘a", "'a'");
	        ParseExpression("12345678901234567", null, FdoExpressionItemType.FdoExpressionItemType_DataValue);
        #if MONO
	        ParseExpression("123456789012345678901", "1.23456789012346e+20", FdoExpressionItemType.FdoExpressionItemType_DataValue);
        #else
            ParseExpression("123456789012345678901", "1.23456789012346e+020", FdoExpressionItemType.FdoExpressionItemType_DataValue);
	        
        #endif
	        ParseExpression("1.2e13", "12000000000000", FdoExpressionItemType.FdoExpressionItemType_DataValue);
	        ParseExpression("-2 --2 +2 ++2", "-2--2+2+2", FdoExpressionItemType.FdoExpressionItemType_BinaryExpression);

	        // dates 
	        ParseExpression("DATE '1951-12-24'", null, FdoExpressionItemType.FdoExpressionItemType_DataValue);
	        ParseExpression("DATE '1971-12-24'", null, FdoExpressionItemType.FdoExpressionItemType_DataValue);

	        // time is meaningless since it would be stored since Jan 1, 1970
	        ParseExpression("TIME '12:01:30'", null, FdoExpressionItemType.FdoExpressionItemType_DataValue);
	        ParseExpression("TIMESTAMP '2003-10-23 11:00:02'", null, FdoExpressionItemType.FdoExpressionItemType_DataValue);

	        // compound expressions (some from Oracle manual)
	        ParseExpression("SQRT(144)", null, FdoExpressionItemType.FdoExpressionItemType_Function);
	        ParseExpression("SQRT('abc')", null, FdoExpressionItemType.FdoExpressionItemType_Function);
	        ParseExpression("SQRT(SQRT(2)+3)*2", null, FdoExpressionItemType.FdoExpressionItemType_BinaryExpression);
	        ParseExpression("my_fun(TO_CHAR('abc','DD-MMM-YY'))", "my_fun(TO_CHAR('abc', 'DD-MMM-YY'))", FdoExpressionItemType.FdoExpressionItemType_Function);
	        ParseExpression(":EnterName", null, FdoExpressionItemType.FdoExpressionItemType_Parameter);
	        ParseExpression(":'Enter Name'", null, FdoExpressionItemType.FdoExpressionItemType_Parameter);
	        ParseExpression("MIN(a,b,2*3+45.67,d,e)", "MIN(a, b, 2*3+45.67, d, e)", FdoExpressionItemType.FdoExpressionItemType_Function);
	        ParseExpression("'CLARK' || 'SMITH'", "'CLARK'+'SMITH'", FdoExpressionItemType.FdoExpressionItemType_BinaryExpression);
	        ParseExpression("'Lewis'+'Clark'", null, FdoExpressionItemType.FdoExpressionItemType_BinaryExpression);
	        ParseExpression("LENGTH('MOOSE') * 57", "LENGTH('MOOSE')*57", FdoExpressionItemType.FdoExpressionItemType_BinaryExpression);
	        ParseExpression("Pipes.state", null, FdoExpressionItemType.FdoExpressionItemType_Identifier);
	        ParseExpression("\"Pipes . state\"", null, FdoExpressionItemType.FdoExpressionItemType_Identifier);
	        ParseExpression("\"%one two thre#EE\"", null, FdoExpressionItemType.FdoExpressionItemType_Identifier);
	        ParseExpression("'%one two thre#EE'", null, FdoExpressionItemType.FdoExpressionItemType_DataValue);
        #if MONO
            ParseExpression("2.3e400", "inf", FdoExpressionItemType.FdoExpressionItemType_DataValue);
        #else
            ParseExpression("2.3e400", "1.#INF", FdoExpressionItemType.FdoExpressionItemType_DataValue);
        #endif
	        ParseExpression("(2+3)*12", null, FdoExpressionItemType.FdoExpressionItemType_BinaryExpression);
	        ParseExpression("sqrt(144)+(12-32/12)", "sqrt(144)+12-32/12", FdoExpressionItemType.FdoExpressionItemType_BinaryExpression);
            ParseExpression("( Width*Height ) AS Area", null, FdoExpressionItemType.FdoExpressionItemType_ComputedIdentifier);
        }

        [Test]
        public void TestDataValue()
        {
            try
            {
                var bool1 = FdoDataValue.Create(true);
                Assert.IsInstanceOf<FdoBooleanValue>(bool1);
                var bool2 = FdoDataValue.Create(false);
                Assert.IsInstanceOf<FdoBooleanValue>(bool2);
                var byte1 = FdoDataValue.Create(byte.MinValue);
                Assert.IsInstanceOf<FdoByteValue>(byte1);
                var byte2 = FdoDataValue.Create(byte.MaxValue);
                Assert.IsInstanceOf<FdoByteValue>(byte2);
                var short1 = FdoDataValue.Create(short.MinValue);
                Assert.IsInstanceOf<FdoInt16Value>(short1);
                var short2 = FdoDataValue.Create(short.MaxValue);
                Assert.IsInstanceOf<FdoInt16Value>(short2);
                var int1 = FdoDataValue.Create(int.MinValue);
                Assert.IsInstanceOf<FdoInt32Value>(int1);
                var int2 = FdoDataValue.Create(int.MaxValue);
                Assert.IsInstanceOf<FdoInt32Value>(int2);
                var long1 = FdoDataValue.Create(long.MinValue);
                Assert.IsInstanceOf<FdoInt64Value>(long1);
                var long2 = FdoDataValue.Create(long.MaxValue);
                Assert.IsInstanceOf<FdoInt64Value>(long2);
                var float1 = FdoDataValue.Create(float.MinValue);
                Assert.IsInstanceOf<FdoSingleValue>(float1);
                var float2 = FdoDataValue.Create(float.MaxValue);
                Assert.IsInstanceOf<FdoSingleValue>(float2);
                var double1 = FdoDataValue.Create(double.MinValue, FdoDataType.FdoDataType_Double);
                Assert.IsInstanceOf<FdoDoubleValue>(double1);
                var double2 = FdoDataValue.Create(double.MaxValue, FdoDataType.FdoDataType_Double);
                Assert.IsInstanceOf<FdoDoubleValue>(double2);
                var dec1 = FdoDataValue.Create(double.MinValue, FdoDataType.FdoDataType_Decimal);
                Assert.IsInstanceOf<FdoDecimalValue>(dec1);
                var dec2 = FdoDataValue.Create(double.MaxValue, FdoDataType.FdoDataType_Decimal);
                Assert.IsInstanceOf<FdoDecimalValue>(dec2);
            }
            catch (ManagedFdoException ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
    }
}
