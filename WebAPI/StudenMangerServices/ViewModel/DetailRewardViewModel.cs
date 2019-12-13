using System;
using System.Collections.Generic;
using System.Text;

namespace StudenMangerServices.ViewModel
{
    public class DetailRewardViewModel
    {
        public DetailRewardViewModel()
        {
            Status = true;
        }
        public Guid? Id { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? RewardId { get; set; }
        public string Reason { get; set; }
        public string Gift { get; set; }
        public string DateReward { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public bool? Status { get; set; }

        public RewardViewModel RewardVM { get; set; }
        public StudentViewModel StudentVM { get; set; }
    }
}
