using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Data.Enum
{
    public enum LevelEnum
    {
        [Description("Khối 6")]
        Level6,
        [Description("Khối 7")]
        Level7,
        [Description("Khối 8")]
        Level8,
        [Description("Khối 9")]
        Level9,
        [Description("Tốt nghiệp")]
        Graduate,
        [Description("Bỏ học")]
        Quit,
        [Description("Chuyển trường")]
        ChangeSchool
    }
}
