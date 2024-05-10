


using Game;
using Game.Ui;

namespace Vault
{
    public class ContextController : Registere
    {
        public override void Initial()
        {
            AddObserver(EventManager.Instance);
            AddObserver(new AiController());
            AddObserver(new PlayerController());
            AddObserver(new UiController());

        }

        public override void Enable()
        {

        }

        public override void OnShow()
        {

        }
    }
}

