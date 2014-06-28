using Sample;
using System;
using System.Collections.Generic;
using System.Windows;
using Thinktecture.IdentityModel.Client;
using Thinktecture.Samples;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LoginWebView _login;
        AuthorizeResponse _response;

        public MainWindow()
        {
            InitializeComponent();

            _login = new LoginWebView();
            _login.Done += _login_Done;

            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _login.Owner = this;
        }

        void _login_Done(object sender, AuthorizeResponse e)
        {
            _response = e;
            Textbox1.Text = e.Raw;
        }

        private void LoginOnlyButton_Click(object sender, RoutedEventArgs e)
        {
            RequestToken("openid", "id_token");
        }

        private void LoginWithProfileButton_Click(object sender, RoutedEventArgs e)
        {
            RequestToken("openid profile", "id_token");
        }

        private void LoginWithProfileAndAccessTokenButton_Click(object sender, RoutedEventArgs e)
        {
            RequestToken("openid profile read write", "id_token token");
        }

        private void AccessTokenOnlyButton_Click(object sender, RoutedEventArgs e)
        {
            RequestToken("read write", "token");
        }

        private void RequestToken(string scope, string responseType)
        {
            var additional = new Dictionary<string, string>
            {
                { "nonce", "nonce" }
            };

            var client = new OAuth2Client(new Uri(Constants.AuthorizeEndpoint));
            var startUrl = client.CreateAuthorizeUrl(
                "implicitclient",
                responseType,
                scope,
                "oob://localhost/wpfclient",
                "state",
                additional);


            _login.Show();
            _login.Start(new Uri(startUrl), new Uri("oob://localhost/wpfclient"));
        }

        private void ShowIdTokenButton_Click(object sender, RoutedEventArgs e)
        {
         //   if (_response.Values.ContainsKey("id_token"))
            {
                var viewer = new IdentityTokenViewer();
                viewer.IdToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IlJDY21rTVVMdy1jVzdfNW1xTmtraXdDSm9RQSJ9.eyJpc3MiOiJodHRwOi8vaWRzcnYuYXNjZW5kLmRrIiwiYXVkIjoiNjk5NEE0QTgtMEU2NS00RkVELUE4MkItQzY4NEEwREQxNzU4IiwibmJmIjoxNDAyODU4NzIwLCJleHAiOjE0MDI4NjIzMjAsIm5vbmNlIjoiNTcwNzI1NTU3MzQ3NDAyIiwiaWF0IjoiMTQwMjg1ODY5NSIsImF0X2hhc2giOiJ4cnBJSDgzcG5xUWNXdXVmX3F4YXVRIiwic3ViIjoiZDAwMDcyODUtNWQyYS00ZDNkLWE5ZTQtOWNkODcwODFlNGY5IiwiYW1yIjoiZXh0ZXJuYWwiLCJhdXRoX3RpbWUiOiIxNDAyODQ5MzM2IiwiaWRwIjoiaHR0cHM6Ly9zdHMud2luZG93cy5uZXQvMDg0MGM3NjAtNmY3Yi00NTU2LWIzMzctOGMwOTBlMmQ0NThkLyJ9.UtCBYNF1kHW06YP3xC_psuEXbnwRPhkL5W-nVbSBZbcbHxu17mLvRUpCw53YBZt9xKrYf24ttukJocZJiJiv7sOnw-Ht_vGetTeVirPhP1dhH6tZT0ywgmCDme3P8LCKA_bpZXzPXrSLiTm9_ItpDj_6MYqauLRvvFlNFpoJtZxlwkzYcG4a1f5mLpl60I7pqf8HhBnN-EEhWRiMa80dc2YOHhUTYFujiMXeto1-SI2Odsaa2LEk8Y65KV2rx2_3lRvnErt_bcrFLoZdSfbq4W3YXR28qZ7O73LK6NKAC6Wj63bhb6jmig7jeExFVYwpE3XCwEOcZWZbNfRoljhTvQ";// _response.Values["id_token"];
                viewer.Show();
            }
        }

        private void ShowAccessTokenButton_Click(object sender, RoutedEventArgs e)
        {
         //   if (_response.Values.ContainsKey("access_token"))
            {
                var viewer = new IdentityTokenViewer();
                viewer.IdToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IlJDY21rTVVMdy1jVzdfNW1xTmtraXdDSm9RQSJ9.eyJpc3MiOiJodHRwOi8vaWRzcnYuYXNjZW5kLmRrIiwiYXVkIjoiaHR0cDovL2lkc3J2LmFzY2VuZC5kay9yZXNvdXJjZXMiLCJuYmYiOjE0MDI4NTg2OTQsImV4cCI6MTQwMjg2MjI5NCwiY2xpZW50X2lkIjoiNjk5NEE0QTgtMEU2NS00RkVELUE4MkItQzY4NEEwREQxNzU4Iiwic2NvcGUiOlsib3BlbmlkIiwiZGF0YS53cml0ZSIsImRhdGEucmVhZCIsImFsZy5leGVjdXRlIl0sInN1YiI6ImQwMDA3Mjg1LTVkMmEtNGQzZC1hOWU0LTljZDg3MDgxZTRmOSIsImFtciI6ImV4dGVybmFsIiwiYXV0aF90aW1lIjoiMTQwMjg0OTMzNiIsImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0LzA4NDBjNzYwLTZmN2ItNDU1Ni1iMzM3LThjMDkwZTJkNDU4ZC8ifQ.SC3sF-7tymQpIilhQx4QPHM_yS81nI4ogjAFsLACgnqhByOWzZrp_UrgVy17Ku6VP_v8wG87J1bO2pb1NdZrpxZ3zLhID34YyqAtg2Txi-zBDdAfP4M65CNRinzBAENuhJMWu0H6LzfQ42KK-x4Uu9nrc53vIRDB4XIPAc8iqKwReZFD0J5bcu3oiRGOgST1JZIpEqqyqLZhApfnYztOkwmVqxJbJytD8TwFDrVyzXHgCmRuhJHJf-JMnAx_onD1L0dyACyl1pZchvcy-IYcLP1W8avyWlXqji68DilT-vRz56fw5QNhVsRYGjZ-UwkDBB1DqSDro6Y-Nqe4fakrcQ";// _response.Values["access_token"];
                viewer.Show();
            }
        }
    }
}