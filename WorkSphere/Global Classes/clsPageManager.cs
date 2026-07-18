using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkSphere.Global_Classes
{
    public static class clsPageManager
    {
        private static readonly Dictionary<Type, UserControl> _pages = new Dictionary<Type, UserControl>();
        private static readonly Dictionary<Type, DateTime> _lastRefresh = new Dictionary<Type, DateTime>();
        private static readonly HashSet<Type> _changedPages = new HashSet<Type>();

        public static T GetPage<T, TForm>(TForm mainForm, Func<TForm, T> createInstance)
            where T : UserControl
            where TForm : Form
        {
            Type type = typeof(T);

            if (!_pages.ContainsKey(type) || _pages[type].IsDisposed)
            {
                _pages[type] = createInstance(mainForm);
                _lastRefresh[type] = DateTime.MinValue;
                _changedPages.Add(type);
            }

            var control = (T)_pages[type];
            _RefreshIfNeeded(control, type);
            return control;
        }
        public static async void NotifyDataChanged<T>() where T : UserControl
        {
            Type type = typeof(T);
            _changedPages.Add(type);

            if (_pages.ContainsKey(type) && _pages[type] is IRefreshable smart)
            {
                await smart.RefreshDataAsync(); 
                _lastRefresh[type] = DateTime.Now;
                _changedPages.Remove(type);
            }
        }
        private static async void _RefreshIfNeeded(UserControl control, Type type)
        {
            if (!_lastRefresh.ContainsKey(type))
                _lastRefresh[type] = DateTime.MinValue;

            bool needRefresh =
                _changedPages.Contains(type) ||
                (DateTime.Now - _lastRefresh[type]).TotalMinutes > Properties.Settings.Default.AUTOREFRESH;

            if (needRefresh && control is IRefreshable smart)
            {
                await smart.RefreshDataAsync();
                _lastRefresh[type] = DateTime.Now;
                _changedPages.Remove(type);
            }
        }
    }

    public interface IRefreshable
    {
        Task RefreshDataAsync();
    }
}