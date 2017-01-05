using Assets.Framework.Utils;
using UnityEngine.UI;

namespace Assets.Framework.Components
{
    public class UnitPrefixText : Text
    {
//        private int _startFontSize;
//        private int _startFontSizeUsedForBestFit;
//        private TextGenerator _textGenerator;
//
        protected override void Awake()
        {
            base.Awake();

            resizeTextForBestFit = true;
//            _startFontSize = fontSize;
//            _textGenerator = cachedTextGenerator;
//            _startFontSizeUsedForBestFit = _textGenerator.fontSizeUsedForBestFit;
        }
//
//        private bool IsFontSizeChanged
//        {
//            get { return _textGenerator.fontSizeUsedForBestFit != _startFontSize; }
//        }
//
//        void Update()
//        {
//            if (_textGenerator.fontSizeUsedForBestFit != _startFontSizeUsedForBestFit)
//            {
//                _startFontSizeUsedForBestFit = _textGenerator.fontSizeUsedForBestFit;
//
//                if (IsFontSizeChanged)
//                {
//                    var res = 0;
//                    if (int.TryParse(base.text, out res))
//                    {
//                        base.text = SymbolUtil.FormatNumber(res);
//                    }
//                }
//            }
//        }

        public override string text
        {
            get { return base.text; }
            set
            {
                var res = 0;
                if (int.TryParse(value, out res))
                {
                    base.text = SymbolUtil.FormatNumber(res);
                }
                else
                {
                    base.text = value;
                }
            }
        }
    }
}
