﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Chloe;
using Model;

namespace DAL
{
    public class TBusinessDAL : DALBase<TBusinessModel>
    {
        public TBusinessDAL()
            : base()
        {
        }

        public TBusinessDAL(string connName)
            : base(connName)
        {
        }

        public TBusinessDAL(string connName, string areaNo)
            : base(connName, areaNo)
        {
        }

        public TBusinessDAL(DbContext db)
            : base(db)
        {
        }

        public TBusinessDAL(DbContext db, string areaNo)
            : base(db, areaNo)
        {
        }

        public object GetGridData()
        {
            var dicType = new FDictionaryDAL(this.db, this.areaNo).GetModelQueryByName(FDictionaryString.AppointmentType);
            var unitQuery = new TUnitDAL(this.db, this.areaNo).GetQuery();
            return this.GetQuery()
                .LeftJoin(dicType, (m, t) => m.busiType.ToString() == t.Value)
                .LeftJoin(unitQuery, (m, t, u) => m.unitSeq == u.unitSeq)
                .Select((m, t, u) => new
                {
                    m.ID,
                    u.unitName,
                    m.unitSeq,
                    m.busiSeq,
                    m.busiCode,
                    m.busiName,
                    busiType = t.Name,
                    acceptBusi = m.acceptBusi ? "是" : "否",
                    getBusi = m.getBusi ? "是" : "否",
                    askBusi = m.askBusi ? "是" : "否",
                    Model = m
                })
                .OrderBy(k => k.ID)
                .ToList();
        }

        public object GetUnitList()
        {
            var busiQuery = new TBusinessDAL(this.db, this.areaNo).GetQuery();
            var unitQuery = new TUnitDAL(this.db, this.areaNo).GetQuery();
            return busiQuery
                .GroupBy(k => k.unitSeq)
                .Select(s => new { s.unitSeq })
                .InnerJoin(unitQuery, (b, u) => b.unitSeq == u.unitSeq)
                .Select((b, u) => u)
            .OrderBy(k => k.unitSeq)
            .ToList();
        }

        public object GetGridDataByUnitSeq(string unitSeq)
        {
            var busiQuery = new TBusinessDAL(this.db, this.areaNo).GetQuery();
            var dicType = new FDictionaryDAL(this.db, this.areaNo).GetModelQueryByName(FDictionaryString.AppointmentType);
            return busiQuery.LeftJoin(dicType, (m, t) => m.busiType.ToString() == t.Value)
                .Where((m, t) => m.unitSeq == unitSeq)
                .Select((m, t) => new
                {
                    m.unitSeq,
                    m.busiSeq,
                    m.busiCode,
                    m.busiName,
                    busiType = t.Name,
                    acceptBusi = m.acceptBusi ? "是" : "否",
                    getBusi = m.getBusi ? "是" : "否",
                    askBusi = m.askBusi ? "是" : "否"
                })
                .OrderBy(k => k.unitSeq)
                .ToList();
        }
    }
}
