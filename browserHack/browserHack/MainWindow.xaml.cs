using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace browserHack
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
      
            JavaScripteErrorBrock();

            browser.Source = new Uri(url.Text);
            browserSplit.Source = new Uri(url.Text);
            
        }

        /// <summary>
        /// Navigateをクリックしたときの挙動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateClick(object sender, RoutedEventArgs e) => browser.Source = new Uri(url.Text);

        /// <summary>
        /// JavaScripteのErrorダイアログを表示させないようにする
        /// </summary>
        private void JavaScripteErrorBrock()
        {
            var axIWebBrowser2 = typeof(WebBrowser).GetProperty("AxIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            var comObj = axIWebBrowser2.GetValue(browser, null);
            comObj.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, comObj, new object[] { true });
            comObj = axIWebBrowser2.GetValue(browserSplit, null);
            comObj.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, comObj, new object[] { true });
        }

        /// <summary>
        /// 分割したほうのNavigateをクリックしたときの挙動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateSplitClick(object sender, RoutedEventArgs e) => browserSplit.Source = new Uri(urlSplit.Text);

        /// <summary>
        /// 前ページに戻るぜよ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanGoBackClick(object sender, RoutedEventArgs e)
        {
            // 前ページの履歴があるか
            if (browser.CanGoBack == true)
                // 前ページに戻る
                browser.GoBack();
        }

        /// <summary>
        /// 分割したほうが前ページに戻るぜよ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanGoBackSplitClick(object sender, RoutedEventArgs e)
        {
            // 前ページの履歴があるか
            if (browserSplit.CanGoBack == true)
                // 前ページに戻る
                browserSplit.GoBack();
        }
    }
}