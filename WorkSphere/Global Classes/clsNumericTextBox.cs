using System;
using System.Linq;
using System.Windows.Forms;

namespace WorkSphere.Controls
{
    public class NumericTextBox : TextBox
    {
        public decimal? MinValue { get; set; } = null;
        public decimal? MaxValue { get; set; } = null;
        public bool AllowDecimal { get; set; } = false; // خاصية للتحكم بوجود الفاصلة العشرية

        private ErrorProvider errorProvider;

        public NumericTextBox()
        {
            this.errorProvider = new ErrorProvider();
            this.errorProvider.RightToLeft = true;
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            // السماح بالأرقام، الفاصلة العشرية (النقطة)، وأزرار التحكم
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // السماح بفاصلة عشرية واحدة إذا كانت مسموحة
                if (AllowDecimal && e.KeyChar == '.' && !this.Text.Contains('.'))
                {
                    e.Handled = false;
                    return;
                }

                e.Handled = true;
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_PASTE = 0x0302;

            if (m.Msg == WM_PASTE)
            {
                string text = Clipboard.GetText();

                // التحقق من أن النص أرقام فقط أو أرقام مع فاصلة عشرية
                if (!IsValidDecimalString(text))
                    return;
            }

            base.WndProc(ref m);
        }

        private bool IsValidDecimalString(string text)
        {
            if (string.IsNullOrEmpty(text))
                return false;

            // التحقق من أن كل حرف إما رقم أو فاصلة عشرية
            foreach (char c in text)
            {
                if (!char.IsDigit(c) && c != '.')
                    return false;
            }

            // التحقق من وجود فاصلة عشرية واحدة فقط
            int decimalPointCount = text.Count(c => c == '.');
            if (decimalPointCount > 1)
                return false;

            return true;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (string.IsNullOrEmpty(this.Text))
            {
                errorProvider.Clear();
                return;
            }

            if (decimal.TryParse(this.Text, out decimal value))
            {
                if (MinValue.HasValue && value < MinValue.Value)
                {
                    errorProvider.SetError(this, $"القيمة يجب أن تكون أكبر من أو تساوي {MinValue.Value}");
                }
                else if (MaxValue.HasValue && value > MaxValue.Value)
                {
                    errorProvider.SetError(this, $"القيمة يجب أن تكون أصغر من أو تساوي {MaxValue.Value}");
                }
                else
                {
                    errorProvider.Clear();
                }
            }
            else
            {
                errorProvider.SetError(this, "مسموح فقط بإدخال أرقام عشرية صحيحة");
            }
        }
    }
}