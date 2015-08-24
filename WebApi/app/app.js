var app = angular.module('AngularAuthApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    });

    $routeProvider.when("/userproductlist", {
        controller: "enduserController",
        templateUrl: "/app/views/userproductlist.html"
    });

    $routeProvider.when("/userproduct", {
        controller: "enduserproductController",
        templateUrl: "/app/views/userproduct.html"
    });

    $routeProvider.when("/userupdate", {
        controller: "enduserupdateController",
        templateUrl: "/app/views/userupdate.html"
    });

    $routeProvider.when("/userupdate/:productID", {
        controller: "enduserupdateController",
        templateUrl: "/app/views/userupdate.html"
    });

    $routeProvider.when("/usermedia", {
        controller: "endusermediaController",
        templateUrl: "/app/views/usermedia.html"
    });

    $routeProvider.when("/usermedia/:updateID", {
        controller: "mediaController",
        templateUrl: "/app/views/usermedia.html"
    });

    //$routeProvider.when("/orders", {
    //    controller: "ordersController",
    //    templateUrl: "/app/views/orders.html"
    //});

    $routeProvider.when("/refresh", {
        controller: "refreshController",
        templateUrl: "/app/views/refresh.html"
    });

    $routeProvider.when("/tokens", {
        controller: "tokensManagerController",
        templateUrl: "/app/views/tokens.html"
    });

    $routeProvider.when("/associate", {
        controller: "associateController",
        templateUrl: "/app/views/associate.html"
    });


    $routeProvider.when("/product", {
        controller: "productController",
        templateUrl: "/app/views/product.html"
    });


    $routeProvider.when("/signuprequest", {
        controller: "signuprequestController",
        templateUrl: "/app/views/signuprequest.html"
    });

    $routeProvider.when("/requestlist", {
        controller: "signuprequestController",
        templateUrl: "/app/views/requestlist.html"
    });


    $routeProvider.when("/update", {
        controller: "updateController",
        templateUrl: "/app/views/update.html"
    });
    $routeProvider.when("/update/:productID", {
        controller: "updateController",
        templateUrl: "/app/views/update.html"
    });

    $routeProvider.when("/media", {
        controller: "mediaController",
        templateUrl: "/app/views/media.html"
    });

    $routeProvider.when("/testupload", {
        controller: "UploadCtrl",
        templateUrl: "/app/views/testupload.html"
    });

    $routeProvider.when("/media/:updateID", {
        controller: "mediaController",
        templateUrl: "/app/views/media.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });
});

var serviceBase = 'http://localhost:1853/';
//var serviceBase = 'http://ngauthenticationapi.azurewebsites.net/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
    //clientId: '146338829036652'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});
app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);
