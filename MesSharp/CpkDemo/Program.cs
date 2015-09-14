﻿// -----------------------------------------------------------------------
//  <copyright file="Program.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2015 OSharp. All rights reserved.
//  </copyright>
//  <last-editor>最后修改人</last-editor>
//  <last-date>2015-08-26 10:15</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

using Autofac;

using CpkDemo.Menjin;

using FileClizz;

using menjin;

using Mes.Core;
using Mes.Core.Caching;
using Mes.Core.Data;
using Mes.Demo.Models.Hr;
using Mes.Demo.Models.TestLog;
using Mes.Utility.Develop;
using Mes.Utility.Extensions;

using Quartz;
using Quartz.Impl;


namespace CpkDemo
{

    internal class Program : IDependency
    {
        private static Program _program;
        // ReSharper disable once NotAccessedField.Local
        private static ICache _cache;
        //   public LogResolver LogResolver { get; set; }
        public IRepository<SwipeCard, int> SwipeCardRepository { get; set; }

        public IRepository<TemporaryCard, int> TemporaryCardRepository { get; set; }

        public IRepository<IgnoreCard, int> IgnoreCardRepository { get; set; }

        public static EntranceSolution EnSolution { get; set; }

        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
          
                  Startup.Start();
                  _program = Startup.Container.Resolve<Program>();
                  _cache = CacheManager.GetCacher(typeof(Program));
                  EnSolution = Startup.Container.Resolve<EntranceSolution>();
                 EnSolution.EntranceTest();
            //  EntranceTest();
            //   Jobinitialization();

            //test();
            //    EntranceSolution();
            //Console.ReadLine();

        }

        private static void Jobinitialization()
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<HrJob>()
                .WithIdentity("myJob", "group1")
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            //ITrigger trigger = TriggerBuilder.Create()
            //  .WithIdentity("myTrigger", "group1")
            //  .StartNow()
            //  .WithSimpleSchedule(x => x
            //      .WithIntervalInHours(1)
            //      .RepeatForever())
            //  .Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .WithCronSchedule("0/5 * * * * ?")
                .ForJob("myJob", "group1")
                .Build();
            sched.ScheduleJob(job, trigger);
        }

        //private static void LogExcute()
        //{
        //    _program.LogResolver.CpkExcute(_program.LogResolver.CpkLogFromPath,
        //        _program.LogResolver.CpkLogToPath,
        //        _program.LogResolver.CpkTransfaseDataTable);
        //    _program.LogResolver.TestLogExcute(_program.LogResolver.TestLogFromPath,
        //        _program.LogResolver.TestLogToPath,
        //        _program.LogResolver.TestLogTransfaseDataTable);
        //}

        public static void test()
        {
            var ping = new Ping();
            var pingReply = ping.Send("172.16.202.202");
            // if (pingReply.Status == IPStatus.Success)
            Entrance entrance = new Entrance("172.16.202.202", 37526);
            //var cards = new long[] { 12844864, 16317849, 16257097, 15150896 };
            var cards = new long[] { 16257097, 15150896 };

            var doors = new Int64[] { 1, 2 };
            // entrance.PrivalegesClear();
            var a = entrance.PrivilegeCount();
            entrance.PrivalegeEndRemoveRange(cards, doors);
            // entrance.PrivalegeEndAddRange(cards, doors);
            var b = entrance.PrivilegeCount();
            entrance.RecordClear();
        }

        public static void Wrtest()
        {
            using (var stream=new System.IO.StreamWriter("E:\\log.txt",true))
            {
                stream.WriteLine(DateTime.Now.ToString("hh:mm:ss"));
            }
        }
        public static void EntranceTest()
        {
            Entrance entrance = new Entrance("172.16.202.202", 37526);
            //19565344
            // var cards = new long[] { 12844864, 16317849, 16257097, 15150896 };
            DateTime dt = DateTime.Now;
            entrance.SetTime(dt);
            List<RecordModel> temporaryCardModel = _program.TemporaryCardRepository.Entities.ToList()
                .Select(a => new RecordModel
                {
                    Card = a.Card.PadLeft(10, '0'),
                    EmpNo = a.EmpNo,
                    EmpName = a.EmpName
                }).ToList();
            List<RecordModel> recordModels = HrUserRespority.Cards().ToList();
            recordModels = recordModels.Concat(temporaryCardModel).ToList();

            var models = entrance.Records().ToList();
            foreach (var model in models)
            {
                model.DoorIo = DoorInResoler(model.BarCode.Substring(2, 4) + model.BarCode.Substring(17, 1));
            }
            var result = from m in models
                         join r in recordModels
                         on m.Card equals r.Card into os
                         from r2 in os.DefaultIfEmpty(
                             new RecordModel { Card = null, EmpName = null, EmpNo = null }
                             )
                         select new SwipeCard
                         {
                             Card = m.Card,
                             EmpNo = r2.EmpNo,
                             EmpName = r2.EmpName,
                             BarCode = m.BarCode,
                             DoorIo = m.DoorIo,
                             SwipdeDate = m.SwipdeDate,
                             SwipeTime = m.SwipeTime
                         };
            var swipeCards = result.DistinctBy(b => new { b.Card, b.EmpNo, b.EmpName, b.BarCode, b.DoorIo, b.SwipdeDate, b.SwipeTime })
                .OrderBy(m => m.SwipdeDate).ToList();

            List<string> ignoreEmpNos = _program.IgnoreCardRepository.Entities.Select(m => m.EmpNo).ToList();
            List<SwipeCard> ignoreCards = new List<SwipeCard>();
            foreach (var swipeCard in swipeCards)
            {
                if (!ignoreEmpNos.Contains(swipeCard.EmpNo))
                    ignoreCards.Add(swipeCard);
            }
            // _program.SwipeCardRepository.BulkInsertAll(ignoreCards);

            long[] cards = recordModels.Select(r => r.Card.CastTo<long>()).ToArray();
            var doors = new Int64[] { 1, 2 };
            entrance.PrivalegeAddRange(cards, doors);
            entrance.RecordClear();
        }

        public static void EntranceExcute()
        {
            Entrance entrance37477 = new Entrance("172.16.192.241", 37477);
            Entrance entrance37647 = new Entrance("172.16.192.242", 37647);


            List<RecordModel> temporaryCardModel = _program.TemporaryCardRepository.Entities.ToList()
                .Select(a => new RecordModel
                {
                    Card = a.Card.PadLeft(10, '0'),
                    EmpNo = a.EmpNo,
                    EmpName = a.EmpName
                }).ToList();
            List<RecordModel> recordModels = HrUserRespority.Cards().ToList();
            recordModels = recordModels.Concat(temporaryCardModel).ToList();

            var models = entrance37477.Records().Concat(entrance37647.Records()).ToList();
            foreach (var model in models)
            {
                model.DoorIo = DoorInResoler(model.BarCode.Substring(2, 4) + model.BarCode.Substring(17, 1));
            }
            var result = from m in models
                         join r in recordModels
                         on m.Card equals r.Card into os
                         from r2 in os.DefaultIfEmpty(
                             new RecordModel { Card = null, EmpName = null, EmpNo = null }
                             )
                         select new SwipeCard
                         {
                             Card = m.Card,
                             EmpNo = r2.EmpNo,
                             EmpName = r2.EmpName,
                             BarCode = m.BarCode,
                             DoorIo = m.DoorIo,
                             SwipdeDate = m.SwipdeDate,
                             SwipeTime = m.SwipeTime
                         };
            var swipeCards = result.DistinctBy(b => new { b.Card, b.EmpNo, b.EmpName, b.BarCode, b.DoorIo, b.SwipdeDate, b.SwipeTime })
                .OrderBy(m => m.SwipdeDate).ToList();

            List<string> ignoreEmpNos = _program.IgnoreCardRepository.Entities.Select(m => m.EmpNo).ToList();
            List<SwipeCard> ignoreCards = new List<SwipeCard>();
            foreach (var swipeCard in swipeCards)
            {
                if (!ignoreEmpNos.Contains(swipeCard.EmpNo))
                    ignoreCards.Add(swipeCard);
            }

            //保存刷卡数据到数据库
            _program.SwipeCardRepository.BulkInsertAll(ignoreCards);
            //重置时间
            DateTime dt = DateTime.Now;
            entrance37477.SetTime(dt);
            entrance37647.SetTime(dt);
            //插入权限
            long[] cards = recordModels.Select(r => r.Card.CastTo<long>()).ToArray();
            var doors = new Int64[] { 1, 2 };
            entrance37477.PrivalegeAddRange(cards, doors);
            entrance37647.PrivalegeAddRange(cards, doors);
            //清空记录
            entrance37477.RecordClear();
            entrance37647.RecordClear();
        }

        public static string DoorInResoler(string test)
        {
            if (test.Equals("0F932") || test.Equals("65920"))
                return "出";
            else
                return "进";
        }
    }
}