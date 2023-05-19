using DAL;
using Model;

namespace BLL
{
    public class TBusinessAttributeBLL : BLLBase<TBusinessAttributeDAL, TBusinessAttributeModel>
    {
        public TBusinessAttributeBLL()
            : base()
        {
        }

        public TBusinessAttributeBLL(string connName)
            : base(connName)
        {
        }

        public TBusinessAttributeBLL(string connName, string areaNo)
            : base(connName, areaNo)
        {
        }

        public object GetGridDetailData(string unitSeq, string busiSeq)
        {
            return this.CreateDAL().GetGridDetailData(unitSeq, busiSeq);
        }
    }
}
