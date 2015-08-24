
//original
//'use strict';
//app.controller('loginController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {

//    $scope.loginData = {
//        userName: "",
//        password: ""
//    };

//    $scope.message = "";

//    $scope.login = function () {

//        authService.login($scope.loginData).then(function (response) {

//           // $location.path('/orders');

//        },
//         function (err) {
//             $scope.message = err.error_description;
//         });
//    };

//}]);




'use strict';
app.controller('loginController', ['$scope', '$location', 'authService', 'ngAuthSettings', function ($scope, $location, authService, ngAuthSettings) {

    $scope.loginData = {
        userName: "",
        password: "",
        useRefreshTokens: false
    };

    $scope.message = "";

    $scope.login = function () {

        authService.login($scope.loginData).then(function (response) {
           
            // for the time being it wouldn't go to welcome.html
            // as welcome is not specified in app.js as it doesn't have a controller 
            // as of now.
            // so the system will by default redirect page to home page
            $location.path('/welcome');

        },
         function (err) {
             $scope.message = err.error_description;
         });
    };

    $scope.authExternalProvider = function (provider) {

             var redirectUri = location.protocol + '//' + location.host + '/authcomplete.html';
           //var redirectUri = location.protocol + '//' + location.host + '/associate.html';
      

        var externalProviderUrl = ngAuthSettings.apiServiceBaseUri + "api/Account/ExternalLogin?provider=" + provider
                                                                    + "&response_type=token&client_id=" + ngAuthSettings.clientId
                                                                    + "&redirect_uri=" + redirectUri;
        window.$windowScope = $scope;

        var oauthWindow = window.open(externalProviderUrl, "Authenticate Account", "location=0,status=0,width=1024,height=750");
    };

    $scope.authCompletedCB = function (fragment) {

        $scope.$apply(function () {

            if (fragment.haslocalaccount == 'False') {

                authService.logOut();

                authService.externalAuthData = {
                    provider: fragment.provider,
                    userName: fragment.external_user_name,
                    externalAccessToken: fragment.external_access_token
                };

                $location.path('/associate');

            }
            else {
                //Obtain access token and redirect to orders
                var externalData = { provider: fragment.provider, externalAccessToken: fragment.external_access_token };
                authService.obtainAccessToken(externalData).then(function (response) {

                  //  $location.path('/orders');

                },
             function (err) {
                 $scope.message = err.error_description;
             });
            }

        });
    }
}]);
