using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data.Enum
{
    public enum CertificateVacational
    {
        [Description("Kém")]
        Bad,
        [Description("Khá")]
        Good,
        [Description("Giỏi")]
        Best
    }
}
