using System;
using System.Collections.Generic;
using System.Linq;
using HaighFramework;
using BearsEngine.Worlds.Graphics.Text;

namespace BearsEngine.Worlds
{
    public delegate int ValueGet();

    public class ValueDisplay : Entity
    {
        protected ValueGet Value;
        private string _valueName;
        private HText _valueText;

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