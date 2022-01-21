using _Root.Scripts.Models.Buffs;
using _Root.Scripts.Views;

namespace _Root.Scripts.Controllers.Obstacles
{
    public class OxygenController : BuffController
    {
        public OxygenController(BuffView buffView, IBuffModel buffModel) : base(buffView, buffModel)
        {
        }
    }
}