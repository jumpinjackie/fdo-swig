using OSGeo.FDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace UnitTest
{
    public class TypemapTests
    {
        [Fact]
        public void TestValueExpression()
        {
            var boolVal = FdoBooleanValue.Create(true);
            var pBool = FdoPropertyValue.Create("BOOL", boolVal);
            var boolVal2 = pBool.GetValue();
            Assert.IsAssignableFrom<FdoBooleanValue>(boolVal2);
            Assert.Equal(boolVal.Boolean, ((FdoBooleanValue)boolVal2).Boolean);

            var byteVal = FdoByteValue.Create(1);
            var pByte = FdoPropertyValue.Create("BYTE", byteVal);
            var byteVal2 = pByte.GetValue();
            Assert.IsAssignableFrom<FdoByteValue>(byteVal2);
            Assert.Equal(byteVal.Byte, ((FdoByteValue)byteVal2).Byte);

            var dt = new FdoDateTime();
            var dtVal = FdoDateTimeValue.Create(dt);
            var pDateTime = FdoPropertyValue.Create("DATETIME", dtVal);
            var dtVal2 = pDateTime.GetValue();
            Assert.IsAssignableFrom<FdoDateTimeValue>(dtVal2);
            var dt1 = dtVal.GetDateTime();
            var dt2 = ((FdoDateTimeValue)dtVal2).GetDateTime();
            Assert.Equal(dt1.year, dt2.year);
            Assert.Equal(dt1.month, dt2.month);
            Assert.Equal(dt1.day, dt2.day);
            Assert.Equal(dt1.hour, dt2.hour);
            Assert.Equal(dt1.minute, dt2.minute);
            Assert.Equal(dt1.seconds, dt2.seconds);

            var dblVal = FdoDoubleValue.Create(1.0);
            var pDouble = FdoPropertyValue.Create("DOUBLE", dblVal);
            var dblVal2 = pDouble.GetValue();
            Assert.IsAssignableFrom<FdoDoubleValue>(dblVal2);
            Assert.Equal(dblVal.Double, ((FdoDoubleValue)dblVal2).Double);

            var i16Val = FdoInt16Value.Create(1);
            var pInt16 = FdoPropertyValue.Create("INT16", i16Val);
            var i16Val2 = pInt16.GetValue();
            Assert.IsAssignableFrom<FdoInt16Value>(i16Val2);
            Assert.Equal(i16Val.Int16, ((FdoInt16Value)i16Val2).Int16);

            var i32Val = FdoInt32Value.Create(1);
            var pInt32 = FdoPropertyValue.Create("INT32", i32Val);
            var i32Val2 = pInt32.GetValue();
            Assert.IsAssignableFrom<FdoInt32Value>(i32Val2);
            Assert.Equal(i32Val.Int32, ((FdoInt32Value)i32Val2).Int32);

            var i64Val = FdoInt64Value.Create(1);
            var pInt64 = FdoPropertyValue.Create("INT64", i64Val);
            var i64Val2 = pInt64.GetValue();
            Assert.IsAssignableFrom<FdoInt64Value>(i64Val2);
            Assert.Equal(i64Val.Int64, ((FdoInt64Value)i64Val2).Int64);

            var singVal = FdoSingleValue.Create(1.0f);
            var pSingle = FdoPropertyValue.Create("SINGLE", singVal);
            var singVal2 = pSingle.GetValue();
            Assert.IsAssignableFrom<FdoSingleValue>(singVal2);
            Assert.Equal(singVal.Single, ((FdoSingleValue)singVal2).Single);

            var strVal = FdoStringValue.Create("Hello World");
            var pString = FdoPropertyValue.Create("STRING", strVal);
            var strVal2 = pString.GetValue();
            Assert.IsAssignableFrom<FdoStringValue>(strVal2);
            Assert.Equal(strVal.String, ((FdoStringValue)strVal2).String);

            var geomFact = FdoFgfGeometryFactory.GetInstance();
            var geom = geomFact.CreateGeometry("POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10))");
            var fgf = geomFact.GetFgfBytes(geom);
            var geomVal = FdoGeometryValue.Create(fgf);
            var pGeom = FdoPropertyValue.Create("GEOMETRY", geomVal);
            var geomVal2 = pGeom.GetValue();
            Assert.IsAssignableFrom<FdoGeometryValue>(geomVal2);
        }

        [Fact]
        public void TestLiteralValue()
        {
            var boolVal = FdoBooleanValue.Create(true);
            var pBool = FdoParameterValue.Create("BOOL", boolVal);
            var boolVal2 = pBool.GetValue();
            Assert.IsAssignableFrom<FdoBooleanValue>(boolVal2);
            Assert.Equal(boolVal.Boolean, ((FdoBooleanValue)boolVal2).Boolean);

            var byteVal = FdoByteValue.Create(1);
            var pByte = FdoParameterValue.Create("BYTE", byteVal);
            var byteVal2 = pByte.GetValue();
            Assert.IsAssignableFrom<FdoByteValue>(byteVal2);
            Assert.Equal(byteVal.Byte, ((FdoByteValue)byteVal2).Byte);

            var dt = new FdoDateTime();
            var dtVal = FdoDateTimeValue.Create(dt);
            var pDateTime = FdoParameterValue.Create("DATETIME", dtVal);
            var dtVal2 = pDateTime.GetValue();
            Assert.IsAssignableFrom<FdoDateTimeValue>(dtVal2);
            var dt1 = dtVal.GetDateTime();
            var dt2 = ((FdoDateTimeValue)dtVal2).GetDateTime();
            Assert.Equal(dt1.year, dt2.year);
            Assert.Equal(dt1.month, dt2.month);
            Assert.Equal(dt1.day, dt2.day);
            Assert.Equal(dt1.hour, dt2.hour);
            Assert.Equal(dt1.minute, dt2.minute);
            Assert.Equal(dt1.seconds, dt2.seconds);

            var dblVal = FdoDoubleValue.Create(1.0);
            var pDouble = FdoParameterValue.Create("DOUBLE", dblVal);
            var dblVal2 = pDouble.GetValue();
            Assert.IsAssignableFrom<FdoDoubleValue>(dblVal2);
            Assert.Equal(dblVal.Double, ((FdoDoubleValue)dblVal2).Double);

            var i16Val = FdoInt16Value.Create(1);
            var pInt16 = FdoParameterValue.Create("INT16", i16Val);
            var i16Val2 = pInt16.GetValue();
            Assert.IsAssignableFrom<FdoInt16Value>(i16Val2);
            Assert.Equal(i16Val.Int16, ((FdoInt16Value)i16Val2).Int16);

            var i32Val = FdoInt32Value.Create(1);
            var pInt32 = FdoParameterValue.Create("INT32", i32Val);
            var i32Val2 = pInt32.GetValue();
            Assert.IsAssignableFrom<FdoInt32Value>(i32Val2);
            Assert.Equal(i32Val.Int32, ((FdoInt32Value)i32Val2).Int32);

            var i64Val = FdoInt64Value.Create(1);
            var pInt64 = FdoParameterValue.Create("INT64", i64Val);
            var i64Val2 = pInt64.GetValue();
            Assert.IsAssignableFrom<FdoInt64Value>(i64Val2);
            Assert.Equal(i64Val.Int64, ((FdoInt64Value)i64Val2).Int64);

            var singVal = FdoSingleValue.Create(1.0f);
            var pSingle = FdoParameterValue.Create("SINGLE", singVal);
            var singVal2 = pSingle.GetValue();
            Assert.IsAssignableFrom<FdoSingleValue>(singVal2);
            Assert.Equal(singVal.Single, ((FdoSingleValue)singVal2).Single);

            var strVal = FdoStringValue.Create("Hello World");
            var pString = FdoParameterValue.Create("STRING", strVal);
            var strVal2 = pString.GetValue();
            Assert.IsAssignableFrom<FdoStringValue>(strVal2);
            Assert.Equal(strVal.String, ((FdoStringValue)strVal2).String);

            var geomFact = FdoFgfGeometryFactory.GetInstance();
            var geom = geomFact.CreateGeometry("POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10))");
            var fgf = geomFact.GetFgfBytes(geom);
            var geomVal = FdoGeometryValue.Create(fgf);
            var pGeom = FdoParameterValue.Create("GEOMETRY", geomVal);
            var geomVal2 = pGeom.GetValue();
            Assert.IsAssignableFrom<FdoGeometryValue>(geomVal2);
        }
    }
}
