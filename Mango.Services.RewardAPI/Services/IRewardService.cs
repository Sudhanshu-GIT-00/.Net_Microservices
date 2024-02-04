using Mango.Services.RewardAPI.Message;

namespace Mango.Services.EmailAPI.Services
{
    public interface IRewardService
    {
        Task UpdateRewards(RewardsMessage rewardsMessage);
    }
}
