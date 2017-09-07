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
            Action<object> errorBrock = (browser) =>
            {
                // JavaScripteのErrorダイアログを表示させないようにする
                var axIWebBrowser2 = typeof(WebBrowser).GetProperty("AxIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
                var comObj = axIWebBrowser2.GetValue(browser, null);
            };

            // メソッドを切らず、ローカル関数として定義することでprimaryより可視性を下げれる
            Action browserInit = () =>
            {
                // ページを表示させておく
                PrimaryBrowser.Source = new Uri(PrimaryUrl.Text);
                SecondaryBrowser.Source = new Uri(SecondaryUrl.Text);
            };

            InitializeComponent();

            // JavaScripteのErrorダイアログを表示させない
            errorBrock(PrimaryBrowser);
            errorBrock(SecondaryBrowser);

            // ブラウザの初期化
            browserInit();
        }

        /// <summary>
        /// Navigateをクリックしたときの挙動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrimaryNavigateClick(object sender, RoutedEventArgs e) => PrimaryBrowser.Source = new Uri(PrimaryUrl.Text);

        /// <summary>
        /// SecondaryのNavigateをクリックしたときの挙動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SecondaryNavigateClick(object sender, RoutedEventArgs e) => SecondaryBrowser.Source = new Uri(SecondaryUrl.Text);

        /// <summary>
        /// 前ページに戻るぜよ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrimaryCanGoBackClick(object sender, RoutedEventArgs e)
        {
            // 前ページの履歴があるか
            if (PrimaryBrowser.CanGoBack == true)
                // 前ページに戻る
                PrimaryBrowser.GoBack();
        }

        /// <summary>
        /// Secondaryが前ページに戻るぜよ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SecondaryCanGoBackClick(object sender, RoutedEventArgs e)
        {
            // 前ページの履歴があるか
            if (SecondaryBrowser.CanGoBack == true)
                // 前ページに戻る
                SecondaryBrowser.GoBack();
        }
    }
}