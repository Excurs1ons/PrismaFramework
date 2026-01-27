using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using ZLogger;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace PrismaFramework.Test.Editor.Benchmark.ZLogger
{
    public class ZLoggerBenchmarkTests
    {
        
    }
    public class BenchmarkResults
    {
        public string TestName { get; set; }
        public int Iterations { get; set; }
        public long DebugLogTime { get; set; }
        public long ZLoggerTime { get; set; }
        public double TimeDifference => ZLoggerTime - DebugLogTime;
        public double PercentageDifference => DebugLogTime > 0 ? (TimeDifference / (double)DebugLogTime * 100) : 0;
        public bool IsZLoggerFaster => ZLoggerTime < DebugLogTime;
        public double DebugLogOpsPerSec => DebugLogTime > 0 ? (Iterations / (DebugLogTime / 1000.0)) : 0;
        public double ZLoggerOpsPerSec => ZLoggerTime > 0 ? (Iterations / (ZLoggerTime / 1000.0)) : 0;

        public void CalculateMetrics()
        {
            // 计算平均值等统计信息可以在这里添加
        }
    }
}