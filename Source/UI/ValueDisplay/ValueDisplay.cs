using BearsEngine.Worlds.Graphics.Text;
using BearsEngine.Worlds.UI.UIThemes;

namespace BearsEngine.Worlds.UI.ValueDisplay
{
    public delegate int ValueGet();

    public class ValueDisplay : Entity
    {
        protected ValueGet Value;
        private readonly string _valueName;
        private readonly HText _valueText;

        public ValueDisplay(int layer, Rect position, string graphic, UITheme theme, string valueName, ValueGet valueToTrack)
            : base(layer, position, graphic)
        {
            Value = valueToTrack;
            _valueName = valueName;
            Add(_valueText = new HText(theme, position.Zeroed, ""));

            UpdateValueText();
        }

        public void UpdateValueText()
        {
            _valueText.Text = _valueName + "\n" + Value().ToString();
        }
    }
}