﻿using System.Linq;
using Util.Datas.Ef.Logs;
using Xunit;

namespace Util.Datas.Tests.Logs {
    /// <summary>
    /// Ef日志测试
    /// </summary>
    public class EfLogTest {
        /// <summary>
        /// 获取Sql参数字典
        /// </summary>
        [Fact]
        public void TestGetSqlParameters_1() {
            var sqlParams = "@p0='c4a42939-3569-4915-bb53-14edad2323cf'";
            var list = EfLog.GetSqlParameters( sqlParams );
            Assert.Equal( 1, list.Count );
            Assert.Equal( "@p0", list.Keys.FirstOrDefault() );
            Assert.Equal( "'c4a42939-3569-4915-bb53-14edad2323cf'", list.Values.FirstOrDefault() );
        }

        /// <summary>
        /// 获取Sql参数字典
        /// </summary>
        [Fact]
        public void TestGetSqlParameters_2() {
            Assert.Empty( EfLog.GetSqlParameters( null ) );
            Assert.Empty( EfLog.GetSqlParameters( "" ) );
            Assert.Empty( EfLog.GetSqlParameters( " " ) );
        }

        /// <summary>
        /// 获取Sql参数字典
        /// </summary>
        [Fact]
        public void TestGetSqlParameters_3() {
            var sqlParams = "@p0='70eac351-1eb0-45d1-b431-431646df7ab1' (Nullable = false) (Size = 450), @p1='18.52'";
            var list = EfLog.GetSqlParameters( sqlParams );
            Assert.Equal( 2, list.Count );
            Assert.Equal( "@p0", list.Keys.FirstOrDefault() );
            Assert.Equal( "'70eac351-1eb0-45d1-b431-431646df7ab1'", list.Values.FirstOrDefault() );
            Assert.Equal( "@p1", list.Keys.LastOrDefault() );
            Assert.Equal( "'18.52'", list.Values.LastOrDefault() );
        }

        /// <summary>
        /// 获取Sql参数字典
        /// </summary>
        [Fact]
        public void TestGetSqlParameters_4() {
            var sqlParams = "@p0='' (Nullable = false) (Size = 450)";
            var list = EfLog.GetSqlParameters( sqlParams );
            Assert.Equal( 1, list.Count );
            Assert.Equal( "@p0", list.Keys.FirstOrDefault() );
            Assert.Equal( "''", list.Values.FirstOrDefault() );
        }

        /// <summary>
        /// 获取Sql参数字典
        /// </summary>
        [Fact]
        public void TestGetSqlParameters_5() {
            var sqlParams = "@p0='70eac351-1eb0-45d1-b431-431646df7ab1' (Nullable = false) (Size = 450), @p1='18.52', @p2='11/15/2017 00:00:41' (Nullable = true), @p3='' (DbType = Guid), @p4='' (Size = 100) (DbType = String), @p5='' (DbType = Int32), @p6='11/15/2017 00:00:41' (Nullable = true), @p7='' (DbType = Guid), @p8='' (Size = 20) (DbType = String), @p9='5a0b1329ab89e01250' (Nullable = false) (Size = 20), @p10='' (Size = 30) (DbType = String), @p11='' (Size = 20) (DbType = String), @p12='c4f1ba98-dab6-451f-8257-3c35ab06a304', @p13='code' (Nullable = false) (Size = 30), @p14='a' (Nullable = false) (Size = 200)";
            var list = EfLog.GetSqlParameters( sqlParams );
            Assert.Equal( 15, list.Count );
            Assert.Equal( "@p0", list.Keys.FirstOrDefault() );
            Assert.Equal( "'70eac351-1eb0-45d1-b431-431646df7ab1'", list.Values.FirstOrDefault() );
            Assert.Equal( "@p14", list.Keys.LastOrDefault() );
            Assert.Equal( "'a'", list.Values.LastOrDefault() );
        }

        /// <summary>
        /// 获取Sql参数字典
        /// </summary>
        [Fact]
        public void TestGetSqlParameters_6() {
            var sqlParams = "@p0='' (DbType = Guid)";
            var list = EfLog.GetSqlParameters( sqlParams );
            Assert.Equal( 1, list.Count );
            Assert.Equal( "@p0", list.Keys.FirstOrDefault() );
            Assert.Equal( "null", list.Values.FirstOrDefault() );
        }

        /// <summary>
        /// 获取Sql
        /// </summary>
        [Fact]
        public void TestGetSql_1() {
            var sql = "VALUES (@p0)";
            var sqlParams = "@p0='c4a42939-3569-4915-bb53-14edad2323cf'";
            var result = EfLog.GetSql( sql, sqlParams );
            Assert.Equal( "VALUES ('c4a42939-3569-4915-bb53-14edad2323cf')", result );
        }

        /// <summary>
        /// 获取Sql
        /// </summary>
        [Fact]
        public void TestGetSql_2() {
            var sql = "VALUES (@p0, @p1)";
            var sqlParams = "@p0='c4a42939-3569-4915-bb53-14edad2323cf', @p1='18.52'";
            var result = EfLog.GetSql( sql, sqlParams );
            Assert.Equal( "VALUES ('c4a42939-3569-4915-bb53-14edad2323cf', '18.52')", result );
        }

        /// <summary>
        /// 获取Sql
        /// </summary>
        [Fact]
        public void TestGetSql_3() {
            var sql = "VALUES (1)";
            var result = EfLog.GetSql( sql, null );
            Assert.Equal( sql, result );
            result = EfLog.GetSql( sql, "" );
            Assert.Equal( sql, result );
        }

        /// <summary>
        /// 获取Sql
        /// </summary>
        [Fact]
        public void TestGetSql_4() {
            var sql = "VALUES (@p0,@p1,@p12)";
            var sqlParams = "@p0='a', @p1='1.5',@p12='b'";
            var result = EfLog.GetSql( sql, sqlParams );
            Assert.Equal( "VALUES ('a','1.5','b')", result );
        }

        /// <summary>
        /// 获取Sql
        /// </summary>
        [Fact]
        public void TestGetSql_5() {
            var sql = "VALUES (@p1111,@p111,@p1,@p11,@p1)";
            var sqlParams = "@p111='c',@p11='b',@p1='a' ";
            var result = EfLog.GetSql( sql, sqlParams );
            Assert.Equal( "VALUES (@p1111,'c','a','b','a')", result );
        }

        /// <summary>
        /// 获取Sql
        /// </summary>
        [Fact]
        public void TestGetSql_6() {
            var sql = "VALUES (@p1)";
            var sqlParams = "@p1='' (DbType = Guid)";
            var result = EfLog.GetSql( sql, sqlParams );
            Assert.Equal( "VALUES (null)", result );
        }
    }
}
