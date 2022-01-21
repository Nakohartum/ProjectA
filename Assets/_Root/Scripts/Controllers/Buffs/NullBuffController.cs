using _Root.Scripts.Models.Buffs;
using _Root.Scripts.Views;

namespace _Root.Scripts.Controllers.Obstacles
{
    public class NullBuffController : BuffController
    {
        public NullBuffController(BuffView buffView, IBuffModel buffModel) : base(buffView, buffModel)
        {
        }

        protected override void ApplyEffect()
        {
            
        }
    }
}