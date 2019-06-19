using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace KyoeiSystem.Application.WCFService
{
    // アクセスインターフェース定義
    [ServiceContract]
    public interface IM90050
    {

    }

    public class M90050_TRN
    {
        [DataMember]
        public int 明細番号 { get; set; }
        public int 明細行 { get; set; }
    }

    public class M90050_UTRN
    {
        [DataMember]
        public int 明細番号 { get; set; }
        public int 明細行 { get; set; }
    }

    public class M90050_KTRN
    {
        [DataMember]
        public int 明細番号 { get; set; }
        public int 明細行 { get; set; }
    }

    public class M90050_NYUK
    {
        [DataMember]
        public int 明細番号 { get; set; }
        public int 明細行 { get; set; }
    }


}
