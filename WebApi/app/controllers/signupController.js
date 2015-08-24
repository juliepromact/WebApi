/// <reference path="UploadCtrl.js" />
'use strict';
app.controller('signupController', ['$scope', '$location', '$timeout','$routeParams', 'authService', function ($scope, $location, $timeout,$routeParams, authService) {

    $scope.savedSuccessfully = false;
    $scope.message = "";
    $scope.id = $routeParams.id;
    var email = $routeParams.Email;
    $scope.Email = $routeParams.Email;
    $scope.registration = {
        userName:email,
        password: "",
        description:"",
        foundedIn: "",
        //ente cheithu
        dateOfBirth: "",
        //ente cheithu
        street1:"",
        street2:"",
        city:"",
        state:"",
        country:"",
        pin:"",
        contactNumber:"",
        confirmPassword: "",
        websiteURL: "",
        twitterHandler: "",
        facebookPageURL: ""
     
    };

    $scope.signUp = function () {

        authService.saveRegistration($scope.registration).then(function (response) {

            $scope.savedSuccessfully = true;
            $scope.message = "User has been registered successfully, you will be redicted to login page in 2 seconds.";
            startTimer();

        },
         function (response) {
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = "Failed to register user due to:" + errors.join(' ');
         });
    };

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/login');
        }, 2000);
    }

}]);